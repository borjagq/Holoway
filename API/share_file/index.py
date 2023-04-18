#!/usr/bin/python3

'''
Share the file with the given emails.

PARAMS:
    file_id: The id of the file that will be shared with other users.
    emails: A list containing the list of emails of users that will receive the file on google drive.
    token: The stored user token.
    api_key: The provided API key.
    timestamp: The time at request time as an integer representing the seconds since the epoch time.
    signature: The signature calculated with the provided private key from the message = file_id + "".join(emails) + token + api_key + timestamp


RETURNS:
    201 Created: Returns a JSON response containing the keys status (==success)
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
    file_id = form.getvalue('file_id')
    emails = form.getlist('emails')
    token = form.getvalue('token')
    api_key = form.getvalue('api_key')
    timestamp = form.getvalue('timestamp')
    signature = form.getvalue('signature')

    api_share_file(file_id, emails, token, api_key, timestamp, signature)
