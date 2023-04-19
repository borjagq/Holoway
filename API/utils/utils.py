'''
Contains our useful funtions that used across the API.
'''

import sys
 
# Add parent's subdirectories.
sys.path.append('..')

import time, string, random, json
import mysql.connector
from os.path import abspath, dirname
from google_auth_oauthlib.flow import Flow
from google.auth.transport.requests import Request
from google.oauth2.credentials import Credentials
from googleapiclient.discovery import build
from googleapiclient.errors import HttpError

from crypto.crypto import *

# CONSTANTS
MAX_DIF = 60
SCOPES = ['https://www.googleapis.com/auth/drive', 
    'https://www.googleapis.com/auth/drive.file',
    'https://www.googleapis.com/auth/drive.appdata',
    'https://www.googleapis.com/auth/drive.apps.readonly']
REDIRECT_URL = 'https://azure.borjagq.com/login_callback/'

# CONSTANTS FOR TESTS
TEST_EMAIL_NULL = 'test@test.com'
TEST_TOKEN_NULL = "gAAAAABkPv2wa93e_omfYYz2t6h9qsnVchPD-vGs8DZTh3pYmjtf7FTC8-tpnIRCwcP7ZvTt8BXYJLKXtaucY5-AWFq1ys6HHmM6BMjvOjVBbo0s19Li_OE="
TEST_CHECK_NULL = {"client_id": '000000'}
TEST_FILES_NULL = [{"id": '000', "type": 'fake/file', "name": 'fakefile.txt'}]

TEST_EMAIL = TEST_EMAIL_NULL
TEST_TOKEN = TEST_TOKEN_NULL
TEST_FILES = TEST_FILES_NULL
TEST_INDIR = TEST_FILES_NULL
TEST_DIREC = 'asdfghasdfghs'
TEST_SHARE = 'not@gmail.com'

def check_credentials_are_valid(token):

    '''
    Check if a google token contains valid credentials.

    Params:
        token: The user token.

    Returns:
        Returns a boolean status and the refreshed token.
    '''

    # Decrypt the token and parse the json.
    key = load_symmetric_key('db')
    token = symmetric_decrypt(key, token)
    token = json.loads(token)

    # Add a check for the tests.
    if token == TEST_CHECK_NULL:
        return True, symmetric_encrypt(key, json.dumps(token)), TEST_CHECK_NULL

    # Build the credentials.
    creds = Credentials.from_authorized_user_info(token, SCOPES)

    # Check if the credetials are valid.
    if creds or creds.valid:
        
        return True, symmetric_encrypt(key, json.dumps(token)), creds
    
    # Check if it can be refreshed.
    if creds and creds.expired and creds.refresh_token:

        # Try to refresh the token, if we can't it means that it is not valid.
        try:
            creds.refresh(Request())

        except Exception as error:
            return False, None, None
        
        # If we're here, it means we got to refresh the token.
        return True, symmetric_encrypt(key, json.dumps(token)), creds
    
    # If we're here, the token is not valid.
    return False, None, None

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

def delete_login_code(mydb, code):

    '''
    Deletes a given code from the db.
    '''

    # Get the reference.
    mycursor = mydb.cursor()

    # Build the deletion query and run it.
    sql = "DELETE FROM login_codes WHERE (code = %s);"
    val = (code, )
    mycursor.execute(sql, val)

    # Commit for the changes to be permanent.
    mydb.commit()

def delete_old_codes(mydb):

    # Get the reference.
    mycursor = mydb.cursor()

    sql = "DELETE FROM login_codes WHERE (added < DATE_SUB(NOW(), INTERVAL 24 HOUR));"
    mycursor.execute(sql)

    mydb.commit()

def generate_random_code(len: 10):

    # Get the desired options.
    chars = string.ascii_lowercase + string.digits

    return ''.join(random.choice(chars) for i in range(len))

def get_user_info(token):

    '''
    Retrieves the user information.

    Params:
        token: The google API credentials object.

    Returns:
        Returns a the email.
    '''

    try:

        # Decrypt the token and parse the json.
        key = load_symmetric_key('db')
        token = symmetric_decrypt(key, token)
        token = json.loads(token)

        # Add a check for the tests.
        if token == TEST_CHECK_NULL:
            return TEST_EMAIL

        creds = Credentials.from_authorized_user_info(token, SCOPES)

        # Build the API connection.
        service = build('drive', 'v3', credentials=creds)

        about = service.about().get(fields='user(emailAddress),storageQuota(usage,usageInDrive,usageInDriveTrash)').execute()

        # Retrieve the email.
        email = about['user']['emailAddress']

        return email
    
    except Exception as e:

        return None
    
def list_files_in_drive(token, dir_id):

    # Return: success, files, msg

    # get the credentials.
    status, _, creds = check_credentials_are_valid(token)

    # Add a check for the tests.
    if creds == TEST_CHECK_NULL:
        return True, TEST_FILES_NULL, ""

    if not status:
        return False, [], "Credentials are not valid"
    
    try:

        # Build the API connection.
        service = build('drive', 'v3', credentials=creds)

        # If the directory id is empty or none, replace it with root.
        if not dir_id:
            dir_id = 'root'

        # Call the Drive v3 API.
        results = service.files().list(includeItemsFromAllDrives=False, fields="files(id, name, mimeType)", q="'{}' in parents and trashed = false".format(dir_id)).execute()
        items = results.get('files', [])

        # Init the files.
        files = []

        # Iter through the results.
        for item in items:

            files.append({
                "id": item['id'],
                "type": item['mimeType'],
                "name": item['name']
            })

    except Exception as e:

        return False, [], str(e)

    return True, files, ""
    
def login_done(login_code, google_code):

    try:

        # Get the current file's path.
        path_creds = abspath(dirname(abspath(__file__)))
        path_creds = abspath(path_creds + "/../crypto/credentials/credentials.json")

        # Build the google drive and fetch its tokens.
        flow = Flow.from_client_secrets_file(path_creds, scopes=SCOPES, state=login_code)

        # Set the redirect page that will load when auth is done.
        flow.redirect_uri = REDIRECT_URL

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
        return
    
def start_login_pipeline(code):

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

        return

    # Get the current file's path.
    path_creds = abspath(dirname(abspath(__file__)))
    path_creds = abspath(path_creds + "/../crypto/credentials/credentials.json")

    try:

        # Use the client_secret.json file to identify the application requesting
        # authorization. The client ID (from that file) and access scopes are required.
        flow = Flow.from_client_secrets_file(path_creds, scopes=SCOPES, state=code)

        # Set the redirect page that will load when auth is done.
        flow.redirect_uri = REDIRECT_URL

        # Generate URL for request to Google's OAuth 2.0 server.
        # Enable offline access so that you can refresh an access token without
        # re-prompting the user for permission. Recommended for web server apps.
        # Enable incremental authorization. Recommended as a best practice.
        authorization_url, state = flow.authorization_url(access_type='offline', include_granted_scopes='true')

        # Redirect the user to the page.
        print("Status: 303 See other")
        print("Location: {}".format(authorization_url), end="\n\n")

        return

    except Exception as e:

        # Print this header to avoid 500 errors.
        print("Status: 500 Internal Server Error")
        print("Content-Type: text/plain;charset=utf-8", end="\n\n")

        print(e)

def retrieve_token_email_from_code(mydb, code):

    '''
    Retrieves the token and email from a login code.
    '''

    if not check_if_code_exists(mydb, code):
        return None, None

    # get the cursor.
    mycursor = mydb.cursor()

    # Build the values.
    sql = "SELECT email, token FROM login_codes WHERE (code = %s);"
    val = (code, )
    
    # Execute the query.
    mycursor.execute(sql, val)

    # Fetch that result. It's one cause code is unique.
    email, token = mycursor.fetchall()[0]

    return email, token

def share_file(token, file_id, emails):

    # Return: success, msg

    # get the credentials.
    status, _, creds = check_credentials_are_valid(token)

    # Add a check for the tests.
    if creds == TEST_CHECK_NULL:
        return True, ""

    if not status:
        return False, [], "Credentials are not valid"
    
    try:

        # Build the API connection.
        service = build('drive', 'v3', credentials=creds)

        # Do this for every email individually.
        for email in emails:

            # Create permissions that allow this new user to access the file as editor.
            service.permissions().create(fileId=file_id, sendNotificationEmail=False, body={'type': 'user', "role": "writer", 'emailAddress': email}).execute()

        return True, ""

    except Exception as e:

        return False, str(e)

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

def verify_timestamp(timestamp, max_dif=MAX_DIF):

    '''
    Verifies if a timestamp is valid relatively to the current one.
    '''

    # Check that the timestamp is a number.
    if not timestamp.isnumeric():
        return False

    # Parse the date.
    target = int(timestamp)

    # Get the current time.
    now = int(time.time())

    # Get the time difference.
    diff = (now - target)
    if diff < max_dif and diff > -max_dif:
        return True
    
    return False
