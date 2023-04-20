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

import cgi

from crypto.crypto import *
from utils.utils import *
from utils.api import *

if __name__ == "__main__":

    # Get the GET/POST vars.
    form = cgi.FieldStorage()

    # Get the params.
    api_key = form.getvalue('api_key')
    timestamp = form.getvalue('timestamp')
    signature = form.getvalue('signature')

    # Call the api function.
    api_create_login(api_key, timestamp, signature)
