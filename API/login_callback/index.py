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
 
# Add parent's subdirectories.
sys.path.append('..')

from crypto.crypto import *
from check_credentials.index import *

# The other imports.
import cgi, json, mysql.connector
from os.path import abspath, dirname
from google.oauth2.credentials import Credentials
from google_auth_oauthlib.flow import Flow

# Get the scopes.
SCOPES = ['https://www.googleapis.com/auth/drive', 
    'https://www.googleapis.com/auth/drive.file',
    'https://www.googleapis.com/auth/drive.appdata',
    'https://www.googleapis.com/auth/drive.apps.readonly']

def update_login_code(mydb, code, email, token):

    # Get the reference.
    mycursor = mydb.cursor()

    # Get the reference.
    sql = "UPDATE login_codes SET email = %s, token = %s WHERE code = %s"
    val = (email, token, code)

    # Run the query.
    mycursor.execute(sql, val)

    mydb.commit()

    return mycursor.rowcount > 0

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
    scopes = (form.getvalue('scope') if form.getvalue('scope') is not None else "").split(' ')

    # Get the current file's path.
    path_creds = abspath(dirname(abspath(__file__)))
    path_creds = abspath(path_creds + "/../crypto/credentials/credentials.json")

    try:

        # Build the google drive and fetch its tokens.
        flow = Flow.from_client_secrets_file(path_creds, scopes=scopes, state=login_code)

        # Set the redirect page that will load when auth is done.
        flow.redirect_uri = 'https://azure.borjagq.com/login_callback/'

        # Get the response from Google.
        flow.fetch_token(code=google_code)

        # Grab the token.
        token = json.loads(flow.credentials.to_json())

        # Specify the necessary attributes.
        token['refresh_token'] = None

        # Build the credentials.
        creds = Credentials.from_authorized_user_info(token, SCOPES)

        # Transform it back to a string.
        token = json.dumps(token)

        # Encrypt the token.
        key = load_symmetric_key('db')
        token = symmetric_encrypt(key, token)

        # Get the email.
        email = get_user_info(token)

        # Encrypt the email.
        email = symmetric_encrypt(key, email)

        # Create the database connection.
        mydb = mysql.connector.connect(
            host="localhost",
            user="root",
            password="mariaDB",
            database="holoway"
        )

        # Update this login code.
        update_login_code(mydb, login_code, email, token)

        print("Status: 303 See Other")
        print("Location: https://azure.borjagq.com/success/", end="\n\n")

    except Exception as e:

        # Print this header to avoid 500 errors.
        print("Status: 500 Internal Server Error")
        print("Content-Type: text/plain;charset=utf-8", end="\n\n")

        print(json.dumps({"status": "error", "message": str(e)}))
        quit()
