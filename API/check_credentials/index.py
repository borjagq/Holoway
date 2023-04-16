#!/usr/bin/python3

'''
Checks if a given credentials are valid or not.

PARAMS:
    token: The stored user token.
    api_key: The provided API key.
    timestamp: The time at request time as an integer representing the seconds since the epoch time.
    signature: The signature calculated with the provided private key from the message = token + api_key + timestamp.

RETURNS:
    201 Created: Returns a JSON response containing the keys status (==success), email, and refreshed_token.
    400 Bad Request: Returns a JSON response containing the keys status (==error) and msg (a message giving context).
    401 Unauthorized: Returns a JSON response containing the keys status (==error) and msg (a message giving context).
    500 Internal Server Error: Returns a JSON response containing the keys status (==error) and msg (a message giving context).
'''

import sys
 
# Add parent's subdirectories.
sys.path.append('..')

import json, cgi, datetime
from os.path import exists
from crypto.crypto import *
from utils.utils import *
from google.auth.transport.requests import Request
from google.oauth2.credentials import Credentials
from googleapiclient.discovery import build
from googleapiclient.errors import HttpError

try:
        
    # If modifying these scopes, delete the file token.json.
    SCOPES = ['https://www.googleapis.com/auth/drive', 
        'https://www.googleapis.com/auth/drive.file',
        'https://www.googleapis.com/auth/drive.appdata',
        'https://www.googleapis.com/auth/drive.apps.readonly']

    def check_credentials_are_valid(token):

        '''
        Check if a google token contains valid credentials.

        Params:
            token: The user token.

        Returns:
            Returns a boolean status and the refreshed token.
        '''

        # Decrypt the token and parse the json.
        key = load_symmetric_key('db')
        token = symmetric_decrypt(key, token)
        token = json.loads(token)

        # Build the credentials.
        creds = Credentials.from_authorized_user_info(token, SCOPES)

        # Check if the credetials are valid.
        if creds or creds.valid:
            
            return True, symmetric_encrypt(key, json.dumps(token)), creds
        
        # Check if it can be refreshed.
        if creds and creds.expired and creds.refresh_token:

            # Try to refresh the token, if we can't it means that it is not valid.
            try:
                creds.refresh(Request())

            except Exception as error:
                return False, None, None
            
            # If we're here, it means we got to refresh the token.
            return True, symmetric_encrypt(key, json.dumps(token)), creds
        
        # If we're here, the token is not valid.
        return False, None, None

    def get_user_info(token):

        '''
        Retrieves the user information.

        Params:
            token: The google API credentials object.

        Returns:
            Returns a the email.
        '''

        # Decrypt the token and parse the json.
        key = load_symmetric_key('db')
        token = symmetric_decrypt(key, token)
        token = json.loads(token)

        try:

            creds = Credentials.from_authorized_user_info(token, SCOPES)

            # Build the API connection.
            service = build('drive', 'v3', credentials=creds)

            about = service.about().get(fields='user(emailAddress),storageQuota(usage,usageInDrive,usageInDriveTrash)').execute()

            # Retrieve the email.
            email = about['user']['emailAddress']

            return email
        
        except Exception as error:

            return None

    if __name__ == "__main__":

        # Get the GET/POST vars.
        form = cgi.FieldStorage()

        # Get the params.
        token = form.getvalue('token')
        api_key = form.getvalue('api_key')
        timestamp = form.getvalue('timestamp')
        signature = form.getvalue('signature')

        # Check if the API key exists.
        if token is None or api_key is None or timestamp is None or signature is None:

            # Print this header to avoid 500 errors.
            print("Status: 400 Bad Request")
            print("Content-Type: text/plain;charset=utf-8", end="\n\n")
            
            print(json.dumps({
                "status": "error",
                "msg": "Some parameters are missing"
            }))

            quit()


        """
        # Verify the timestamp.
        if not verify_timestamp(timestamp):

            # Print this header to avoid 500 errors.
            print("Status: 401 Unauthorized")
            print("Content-Type: text/plain;charset=utf-8", end="\n\n")
            
            print(json.dumps({
                "status": "error",
                "msg": "This request is too old or the sender's clock is not synced. {} (sender) \
                    is too far away from {} (server).".format(timestamp, datetime.datetime.now())
            }))

            quit() """

        # Check if the API key exists.
        api_key = form.getvalue('api_key')

        # Load the appropriate public key.
        pub_key = load_public_key(api_key)

        if pub_key is False:

            # Print this header to avoid 500 errors.
            print("Status: 401 Unauthorized")
            print("Content-Type: text/plain;charset=utf-8", end="\n\n")
            
            print(json.dumps({
                "status": "error",
                "msg": "The public key is missing"
            }))

            quit()

        # Verify the request.
        message = token + api_key + timestamp
        if not verify_signature(pub_key, message, signature):

            # Print this header to avoid 500 errors.
            print("Status: 401 Unauthorized")
            print("Content-Type: text/plain;charset=utf-8", end="\n\n")
            
            print(json.dumps({
                "status": "error",
                "msg": "The signature does not match"
            }))

            quit()

        # Check if the token is valid and refresh it.
        status, token, creds = check_credentials_are_valid(token)

        if status:

            # Init these.
            email = None
            email = get_user_info(token)

            if email is None:

                # Print this header to avoid 500 errors.
                print("Status: 200 OK")
                print("Content-Type: text/plain;charset=utf-8", end="\n\n")
                
                print(json.dumps({
                    "status": "error",
                    "msg": "An error ocurred when retrieving the information. Token is likely expired."
                }))

                quit()

            # Decrypt the token and parse the json.
            key = load_symmetric_key('db')
            email = symmetric_encrypt(key, email)

            # Print this header to avoid 500 errors.
            print("Status: 200 OK")
            print("Content-Type: text/plain;charset=utf-8", end="\n\n")

            print(json.dumps({
                "status": "success",
                "email": email,
                "refreshed_token": token
            }))

            quit()

        else:

            # Print this header to avoid 500 errors.
            print("Status: 200 OK")
            print("Content-Type: text/plain;charset=utf-8", end="\n\n")
            
            print(json.dumps({
                "status": "error",
                "msg": "The token is not valid or is expired."
            }))

            quit()

except Exception as e:

    print(e)
