#!/usr/bin/python3

'''
Passes the google token to the DB.

PARAMS:
    Everything passed by Google.

RETURNS:
    200 OK: Loads the page.
    400 Bad Request: The code does not exist.
    500 Internal Server Error: Shows a message.
'''

import sys

import cgi, json
 
# Add parent's subdirectories.
sys.path.append('..')

from utils.utils import *

if __name__ == "__main__":

    # Get the response codes.
    form = cgi.FieldStorage()
    error = form.getvalue('error')

    if error is not None:

        # Print this header to avoid 500 errors.
        print("Status: 500 Internal Server Error")
        print("Content-Type: text/plain;charset=utf-8", end="\n\n")

        print(json.dumps({"status": "error", "message": "Auth failed."}))
        quit()

    # Get the parameters sent by google.
    login_code = form.getvalue('state')
    google_code = form.getvalue('code')

    # Update the code.
    login_done(login_code, google_code)
