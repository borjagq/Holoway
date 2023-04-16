#!/usr/bin/python3

'''
Lists the files in a directory or in the root directory.

PARAMS:
    dir_id: The directory id that wants to be listed. Don't pass or pass empty if you want the root files.
    token: The stored user token.
    api_key: The provided API key.
    timestamp: The time at request time as an integer representing the seconds since the epoch time.
    signature: The signature calculated with the provided private key from the message = dir_id + token + api_key + timestamp.

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
from check_credentials.index import *
from google.auth.transport.requests import Request
from google.oauth2.credentials import Credentials
from googleapiclient.discovery import build
from googleapiclient.errors import HttpError

# If modifying these scopes, delete the file token.json.
SCOPES = ['https://www.googleapis.com/auth/drive', 
    'https://www.googleapis.com/auth/drive.file',
    'https://www.googleapis.com/auth/drive.appdata',
    'https://www.googleapis.com/auth/drive.apps.readonly']

def list_files_in_drive(token, dir_id):

    # Return: success, files, msg

    # get the credentials.
    status, _, creds = check_credentials_are_valid(token)

    if not status:
        return False, [], "Credentials are not valid"
    
    try:

        # Build the API connection.
        service = build('drive', 'v3', credentials=creds)

        # If the directory id is empty or none, replace it with root.
        if not dir_id:
            dir_id = 'root'

        # Call the Drive v3 API.
        results = service.files().list(includeItemsFromAllDrives=False, fields="files(id, name, mimeType)", q="'{}' in parents and trashed = false".format(dir_id)).execute()
        items = results.get('files', [])

        # Init the files.
        files = []

        # Iter through the results.
        for item in items:

            files.append({
                "id": item['id'],
                "type": item['mimeType'],
                "name": item['name']
            })

    except Exception as e:

        return False, [], str(e)

    return True, files, ""

if __name__ == "__main__":
        
    try:

        # Get the GET/POST vars.
        form = cgi.FieldStorage()

        # Get the params.
        dir_id = form.getvalue('dir_id') if form.getvalue('dir_id') else None
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

            quit()

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
        message = ("" if dir_id is None else dir_id) + token + api_key + timestamp
        
        if not verify_signature(pub_key, message, signature):

            # Print this header to avoid 500 errors.
            print("Status: 401 Unauthorized")
            print("Content-Type: text/plain;charset=utf-8", end="\n\n")
            
            print(json.dumps({
                "status": "error",
                "msg": "The signature does not match: " + message
            }))

            quit()

        # Check if the token is valid and refresh it.
        status, token, creds = check_credentials_are_valid(token)
        if not status:

            # Print this header to avoid 500 errors.
            print("Status: 400 Bad Request")
            print("Content-Type: text/plain;charset=utf-8", end="\n\n")
            
            print(json.dumps({
                "status": "error",
                "msg": "The token is not valid."
            }))

            quit()

        # retrieve the actual files from Google Drive API.
        success, files, msg = list_files_in_drive(token, dir_id)

        if not success:

            # Print this header to avoid 500 errors.
            print("Status: 500 Internal Server Error")
            print("Content-Type: text/plain;charset=utf-8", end="\n\n")
            
            print(json.dumps({
                "status": "error",
                "msg": msg
            }))

            quit()

        # Print this header to avoid 500 errors.
        print("Status: 200 OK")
        print("Content-Type: text/plain;charset=utf-8", end="\n\n")

        print(json.dumps({
            "status": "success",
            "files": files,
        }))

        quit()

    except Exception as e:

        print("Content-Type: text/plain;charset=utf-8", end="\n\n")
        print(e)
