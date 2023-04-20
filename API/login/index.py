#!/usr/bin/python3

'''
Launches google login.

PARAMS:
    code: A valid login code.

RETURNS:
    200 OK: Loads the page.
    400 Bad Request: The code does not exist.
    500 Internal Server Error: Shows a message.
'''

import sys

import cgi
 
# Add parent's subdirectories.
sys.path.append('..')

# The other imports.
from utils.utils import *

if __name__ == "__main__":

    # Get the GET/POST vars.
    form = cgi.FieldStorage()

    # Get the params.
    code = form.getvalue('code')

    start_login_pipeline(code)
