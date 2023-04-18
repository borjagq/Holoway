'''
Contains the API calls.
'''

import sys
 
# Add parent's subdirectories.
sys.path.append('..')

import mysql.connector
import json, datetime

from crypto.crypto import *
from utils.utils import *

def api_check_credentials(token, api_key, timestamp, signature):

    # Check if the API key exists.
    if token is None or api_key is None or timestamp is None or signature is None:

        # Print this header to avoid 500 errors.
        print("Status: 400 Bad Request")
        print("Content-Type: text/plain;charset=utf-8", end="\n\n")
        
        print(json.dumps({
            "status": "error",
            "msg": "Some parameters are missing"
        }))

        return

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

        return

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

        return

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

        return

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

            return

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

        return

    else:

        # Print this header to avoid 500 errors.
        print("Status: 200 OK")
        print("Content-Type: text/plain;charset=utf-8", end="\n\n")
        
        print(json.dumps({
            "status": "error",
            "msg": "The token is not valid or is expired."
        }))

        return

def api_create_login(api_key, timestamp, signature):

    # Check if the API key exists.
    if api_key is None or timestamp is None or signature is None:

        # Print this header to avoid 500 errors.
        print("Status: 400 Bad Request")
        print("Content-Type: text/plain;charset=utf-8", end="\n\n")
        
        print(json.dumps({
            "status": "error",
            "msg": "Some parameters are missing"
        }))

        return

    # Verify the timestamp.
    if not verify_timestamp(timestamp):

        # Print this header to avoid 500 errors.
        print("Status: 401 Unauthorized")
        print("Content-Type: text/plain;charset=utf-8", end="\n\n")
        
        print(json.dumps({
            "status": "error",
            "msg": "This request is too old or the sender's clock is not synced. " +
                "{} (sender) is too far away from {} (server).".format(timestamp, datetime.datetime.now())
        }))

        return

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

        return

    # Verify the request.
    message = api_key + timestamp
    if not verify_signature(pub_key, message, signature):

        # Print this header to avoid 500 errors.
        print("Status: 401 Unauthorized")
        print("Content-Type: text/plain;charset=utf-8", end="\n\n")
        
        print(json.dumps({
            "status": "error",
            "msg": "The signature does not match"
        }))

        return

    # Create the database connection.
    mydb = mysql.connector.connect(
        host="localhost",
        user="root",
        password="mariaDB",
        database="holoway"
    )

    # Make sure to delete old codes.
    delete_old_codes(mydb)

    # Create the new code.
    code = create_new_login_code(mydb)

    if check_if_code_exists(mydb, code):

        # Print this header to avoid 500 errors.
        print("Status: 201 Created")
        print("Content-Type: text/plain;charset=utf-8", end="\n\n")

        print(json.dumps({
            "status": "success",
            "code": code
        }))

        return

    else:

        # Print this header to avoid 500 errors.
        print("Status: 500 Internal Server Error")
        print("Content-Type: text/plain;charset=utf-8", end="\n\n")
        
        print(json.dumps({
            "status": "error",
            "msg": "The code could not be created"
        }))

        return
    
def api_list_files(token, api_key, timestamp, signature, dir_id):

    try:

        # Check if the API key exists.
        if token is None or api_key is None or timestamp is None or signature is None:

            # Print this header to avoid 500 errors.
            print("Status: 400 Bad Request")
            print("Content-Type: text/plain;charset=utf-8", end="\n\n")
            
            print(json.dumps({
                "status": "error",
                "msg": "Some parameters are missing"
            }))

            return

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

            return

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

            return

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

            return

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

            return

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

            return

        # Print this header to avoid 500 errors.
        print("Status: 200 OK")
        print("Content-Type: text/plain;charset=utf-8", end="\n\n")

        print(json.dumps({
            "status": "success",
            "files": files,
        }))

        return

    except Exception as e:

        print("Content-Type: text/plain;charset=utf-8", end="\n\n")
        print(e)
    
def api_retrieve_login(api_key, code, timestamp, signature):

    # Check if the API key exists.
    if api_key is None or code is None or timestamp is None or signature is None:

        # Print this header to avoid 500 errors.
        print("Status: 400 Bad Request")
        print("Content-Type: text/plain;charset=utf-8", end="\n\n")
        
        print(json.dumps({
            "status": "error",
            "msg": "Some parameters are missing"
        }))

        return

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

        return

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

        return

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

        return

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

        return

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

    return

def api_share_file(file_id, emails, token, api_key, timestamp, signature):

        # Check if the API key exists.
        if file_id is None or not emails or token is None or api_key is None or timestamp is None or signature is None:

            # Print this header to avoid 500 errors.
            print("Status: 400 Bad Request")
            print("Content-Type: text/plain;charset=utf-8", end="\n\n")
            
            print(json.dumps({
                "status": "error",
                "msg": "Some parameters are missing"
            }))

            return

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

            return

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

            return

        # Verify the request.
        message = file_id + "".join(emails) + token + api_key + timestamp

        if not verify_signature(pub_key, message, signature):

            # Print this header to avoid 500 errors.
            print("Status: 401 Unauthorized")
            print("Content-Type: text/plain;charset=utf-8", end="\n\n")
            
            print(json.dumps({
                "status": "error",
                "msg": "The signature does not match: " + message
            }))

            return

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

            return

        # retrieve the actual files from Google Drive API.
        success, msg = share_file(token, file_id, emails)

        if not success:

            # Print this header to avoid 500 errors.
            print("Status: 500 Internal Server Error")
            print("Content-Type: text/plain;charset=utf-8", end="\n\n")
            
            print(json.dumps({
                "status": "error",
                "msg": msg
            }))

            return

        # Print this header to avoid 500 errors.
        print("Status: 200 OK")
        print("Content-Type: text/plain;charset=utf-8", end="\n\n")

        print(json.dumps({
            "status": "success",
        }))

        return