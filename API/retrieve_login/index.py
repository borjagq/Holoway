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

import cgi
from utils.api import *

if __name__ == "__main__":

    # Get the GET/POST vars.
    form = cgi.FieldStorage()

    # Get the params.
    api_key = form.getvalue('api_key')
    code = form.getvalue('code')
    timestamp = form.getvalue('timestamp')
    signature = form.getvalue('signature')

    # Run the api call to retrieve the code.
    api_retrieve_login(api_key, code, timestamp, signature)
