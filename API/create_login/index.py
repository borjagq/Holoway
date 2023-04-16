#!/usr/bin/python3

'''
Generates a login code that can be accessed through the login portal.

PARAMS:
    api_key: The provided API key.
    timestamp: The time at request time as an integer representing the seconds since the epoch time.
    signature: The signature calculated with the provided private key from the message = api_key + timestamp.

RETURNS:
    201 Created: Returns a JSON response containing the keys status (==success) and code (the generated code).
    400 Bad Request: Returns a JSON response containing the keys status (==error) and msg (a message giving context).
    401 Unauthorized: Returns a JSON response containing the keys status (==error) and msg (a message giving context).
    500 Internal Server Error: Returns a JSON response containing the keys status (==error) and msg (a message giving context).
'''

import sys
 
# Add parent's subdirectories.
sys.path.append('..')

import random
import string
import mysql.connector
import json, cgi, datetime
from os.path import exists
from crypto.crypto import *
from utils.utils import *

def generate_random_code(len: 10):

    # Get the desired options.
    chars = string.ascii_lowercase + string.digits

    return ''.join(random.choice(chars) for i in range(len))

def check_if_code_exists(mydb, check_code):

    # Get the reference.
    mycursor = mydb.cursor()

    # Get the reference.
    sql = "SELECT l.code FROM login_codes l WHERE (l.code = %s)"
    adr = (check_code, )

    # Run the query.
    mycursor.execute(sql, adr)

    # Get the result.
    query_res = mycursor.fetchone()

    if query_res and query_res[0] == check_code:
        return True
    
    return False

def create_new_login_code(mydb):

    # Get the reference.
    mycursor = mydb.cursor()

    # Generate the code.
    code = generate_random_code(6)

    # Make sure it does not exist.
    while check_if_code_exists(mydb, code):
        code = generate_random_code(6)

    sql = "INSERT INTO login_codes (code) VALUES (%s)"
    val = (code, )
    mycursor.execute(sql, val)

    mydb.commit()

    if mycursor.rowcount == 0:
        return None

    return code

def delete_old_codes(mydb):

    # Get the reference.
    mycursor = mydb.cursor()

    sql = "DELETE FROM login_codes WHERE (added < DATE_SUB(NOW(), INTERVAL 24 HOUR));"
    mycursor.execute(sql)

    mydb.commit()

if __name__ == "__main__":

    # Get the GET/POST vars.
    form = cgi.FieldStorage()

    # Get the params.
    api_key = form.getvalue('api_key')
    timestamp = form.getvalue('timestamp')
    signature = form.getvalue('signature')

    # Check if the API key exists.
    if api_key is None or timestamp is None or signature is None:

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
            "msg": "This request is too old or the sender's clock is not synced. " +
                "{} (sender) is too far away from {} (server).".format(timestamp, datetime.datetime.now())
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
    message = api_key + timestamp
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

        quit()

    else:

        # Print this header to avoid 500 errors.
        print("Status: 500 Internal Server Error")
        print("Content-Type: text/plain;charset=utf-8", end="\n\n")
        
        print(json.dumps({
            "status": "error",
            "msg": "The code could not be created"
        }))

        quit()
