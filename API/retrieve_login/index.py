#!/usr/bin/python3

'''
Retrieves the token and email from a login code if is available.

PARAMS:
    api_key: The provided API key.
    code: The login code.
    timestamp: The time at request time as an integer representing the seconds since the epoch time.
    signature: The signature calculated with the provided private key from the message = code + api_key + timestamp.

RETURNS:
    201 Created: Returns a JSON response containing the keys status (==success), token and email.
    400 Bad Request: Returns a JSON response containing the keys status (==error) and msg (a message giving context).
    401 Unauthorized: Returns a JSON response containing the keys status (==error) and msg (a message giving context).
    500 Internal Server Error: Returns a JSON response containing the keys status (==error) and msg (a message giving context).
'''

import sys
 
# Add parent's subdirectories.
sys.path.append('..')

import json, cgi, datetime
from os.path import exists
import mysql.connector
from crypto.crypto import *
from utils.utils import *
from create_login.index import *

def delete_login_code(mydb, code):

    '''
    Deletes a given code from the db.
    '''

    # Get the reference.
    mycursor = mydb.cursor()

    # Build the deletion query and run it.
    sql = "DELETE FROM login_codes WHERE (code = %s);"
    val = (code, )
    mycursor.execute(sql, val)

    # Commit for the changes to be permanent.
    mydb.commit()

def retrieve_token_email_from_code(mydb, code):

    '''
    Retrieves the token and email from a login code.
    '''

    if not check_if_code_exists(mydb, code):
        return None, None

    # get the cursor.
    mycursor = mydb.cursor()

    # Build the values.
    sql = "SELECT email, token FROM login_codes WHERE (code = %s);"
    val = (code, )
    
    # Execute the query.
    mycursor.execute(sql, val)

    # Fetch that result. It's one cause code is unique.
    email, token = mycursor.fetchall()[0]

    return email, token

if __name__ == "__main__":

    # Get the GET/POST vars.
    form = cgi.FieldStorage()

    # Get the params.
    api_key = form.getvalue('api_key')
    code = form.getvalue('code')
    timestamp = form.getvalue('timestamp')
    signature = form.getvalue('signature')

    # Check if the API key exists.
    if api_key is None or code is None or timestamp is None or signature is None:

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
    message = code + api_key + timestamp
    if not verify_signature(pub_key, message, signature):

        # Print this header to avoid 500 errors.
        print("Status: 401 Unauthorized")
        print("Content-Type: text/plain;charset=utf-8", end="\n\n")
        
        print(json.dumps({
            "status": "error",
            "msg": "The signature does not match"
        }))

        quit()

    # Create the database connection.
    mydb = mysql.connector.connect(
        host="localhost",
        user="root",
        password="mariaDB",
        database="holoway"
    )

    if not check_if_code_exists(mydb, code):

        # Print this header to avoid 500 errors.
        print("Status: 400 Bad Request")
        print("Content-Type: text/plain;charset=utf-8", end="\n\n")
        
        print(json.dumps({
            "status": "error",
            "msg": "The code does not exist."
        }))

        quit()

    # Check if the token is valid and refresh it.
    email, token = retrieve_token_email_from_code(mydb, code)

    # If these values are not empty, delete this.
    if email != "" and token != "":

        delete_login_code(mydb, code)

    # Print this header to avoid 500 errors.
    print("Status: 200 OK")
    print("Content-Type: text/plain;charset=utf-8", end="\n\n")

    print(json.dumps({
        "status": "success",
        "email": email,
        "token": token
    }))

    quit()

