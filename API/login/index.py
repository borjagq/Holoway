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
 
# Add parent's subdirectories.
sys.path.append('..')

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

if __name__ == "__main__":

    # Get the GET/POST vars.
    form = cgi.FieldStorage()

    # Get the params.
    code = form.getvalue('code')

    # Create the database connection.
    mydb = mysql.connector.connect(
        host="localhost",
        user="root",
        password="mariaDB",
        database="holoway"
    )

    # Retrieve the code tuple.
    if not code or not check_if_code_exists(mydb, code):

        # Go back to the main page if it does not exist.
        print("Location: /", end="\n\n")

        quit()

    # Get the current file's path.
    path_creds = abspath(dirname(abspath(__file__)))
    path_creds = abspath(path_creds + "/../crypto/credentials/credentials.json")

    try:

        # Use the client_secret.json file to identify the application requesting
        # authorization. The client ID (from that file) and access scopes are required.
        flow = Flow.from_client_secrets_file(path_creds, scopes=SCOPES, state=code)

        # Set the redirect page that will load when auth is done.
        flow.redirect_uri = 'https://azure.borjagq.com/login_callback/'

        # Generate URL for request to Google's OAuth 2.0 server.
        # Enable offline access so that you can refresh an access token without
        # re-prompting the user for permission. Recommended for web server apps.
        # Enable incremental authorization. Recommended as a best practice.
        authorization_url, state = flow.authorization_url(access_type='offline', include_granted_scopes='true')

        # Redirect the user to the page.
        print("Status: 303 See other")
        print("Location: {}".format(authorization_url), end="\n\n")

    except Exception as e:

        # Print this header to avoid 500 errors.
        print("Status: 500 Internal Server Error")
        print("Content-Type: text/plain;charset=utf-8", end="\n\n")

        print(e)
