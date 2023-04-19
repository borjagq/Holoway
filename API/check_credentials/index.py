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

import cgi

from utils.api import *

if __name__ == "__main__":

    # Get the GET/POST vars.
    form = cgi.FieldStorage()

    # Get the params.
    token = form.getvalue('token')
    api_key = form.getvalue('api_key')
    timestamp = form.getvalue('timestamp')
    signature = form.getvalue('signature')

    api_check_credentials(token, api_key, timestamp, signature)
