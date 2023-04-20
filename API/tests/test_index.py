import unittest

import sys, random, time, io, re, json
from os import remove
from os.path import exists
import mysql.connector
from cryptography.fernet import Fernet
import Crypto

# Add parent's subdirectories.
sys.path.append('..')

from crypto.crypto import *
from utils.utils import *
from utils.api import *

class ApiTests(unittest.TestCase):

    def test_api_check_credentials(self):

        # Generate a new key in a random file.
        api_key = generate_random_code(10)

        # Get the params.
        timestamp = str(int(time.time()))
        token = TEST_TOKEN

        # Get again a name until there is no such file.
        # This way, we avoid rewriting a key.
        while exists("../crypto/keys/{}.key".format(api_key)):
            api_key = generate_random_code(10)

        # Generate the keys.
        generate_priv_pub_keys(api_key)
        
        # Load the keys.
        priv_key = load_private_key(api_key)

        # Generate a random message of the given length.
        msg = token + api_key + timestamp

        # Make sure encryption works.
        signature = sign_message(priv_key, msg)

        # Capture the stdoutput.
        printed = io.StringIO()
        sys.stdout = printed

        # Call the api function.
        api_check_credentials(token, api_key, timestamp, signature)

        # Reset the std out.
        sys.stdout = sys.__stdout__

        # Delete that key.
        remove("../crypto/keys/{}.pem".format(api_key))
        remove("../crypto/keys/{}.pub".format(api_key))

        self.assertRegex(printed.getvalue(), r'''^Status: 200 OK
Content-Type: text/plain;charset=utf-8

\{"status": "success", "email": ".+", "refreshed_token": ".+"\}$''')

    def test_api_create_login(self):

        # Generate a new key in a random file.
        api_key = generate_random_code(10)

        # Get the params.
        timestamp = str(int(time.time()))

        # Get again a name until there is no such file.
        # This way, we avoid rewriting a key.
        while exists("../crypto/keys/{}.key".format(api_key)):
            api_key = generate_random_code(10)

        # Generate the keys.
        generate_priv_pub_keys(api_key)
        
        # Load the keys.
        pub_key = load_public_key(api_key)
        priv_key = load_private_key(api_key)

        # Generate a random message of the given length.
        msg = api_key + timestamp

        # Make sure encryption works.
        signature = sign_message(priv_key, msg)

        # Capture the stdoutput.
        printed = io.StringIO()
        sys.stdout = printed

        # Call the api function.
        api_create_login(api_key, timestamp, signature)

        # Reset the std out.
        sys.stdout = sys.__stdout__

        # Delete that key.
        remove("../crypto/keys/{}.pem".format(api_key))
        remove("../crypto/keys/{}.pub".format(api_key))

        # Get the http response.
        response = printed.getvalue()

        self.assertRegex(response, r'''^Status: 201 Created
Content-Type: text/plain;charset=utf-8

\{"status": "success", "code": "[a-zA-Z0-9]+"\}$''')
                         
        # Retrieve the code using regex.
        code = re.search(r'\{"status": "success", "code": "([a-zA-Z0-9]+)"\}', response)
        code = code.group(1)
                         
        # Create the database connection.
        mydb = mysql.connector.connect(
            host="localhost",
            user="root",
            password="mariaDB",
            database="holoway"
        )
                         
        # Now delete it.
        delete_login_code(mydb, code)

    def test_api_list_files(self):

        # Generate a new key in a random file.
        api_key = generate_random_code(10)

        # Get the params.
        timestamp = str(int(time.time()))
        token = TEST_TOKEN
        dir_id = ""

        # Get again a name until there is no such file.
        # This way, we avoid rewriting a key.
        while exists("../crypto/keys/{}.key".format(api_key)):
            api_key = generate_random_code(10)

        # Generate the keys.
        generate_priv_pub_keys(api_key)
        
        # Load the keys.
        priv_key = load_private_key(api_key)

        # Generate a random message of the given length.
        msg = dir_id + token + api_key + timestamp

        # Make sure encryption works.
        signature = sign_message(priv_key, msg)

        # Capture the stdoutput.
        printed = io.StringIO()
        sys.stdout = printed

        # Call the api function.
        api_list_files(token, api_key, timestamp, signature, dir_id)

        # Reset the std out.
        sys.stdout = sys.__stdout__

        # Delete that key.
        remove("../crypto/keys/{}.pem".format(api_key))
        remove("../crypto/keys/{}.pub".format(api_key))

        # Get the http response.
        response = printed.getvalue()

        self.assertRegex(response, r'''^Status: 200 OK
Content-Type: text/plain;charset=utf-8

(\{.+\})$''')
                         
        # Retrieve the code using regex.
        files = re.search(r'''^Status: 200 OK
Content-Type: text/plain;charset=utf-8

(\{.+\})$''', response)
        files = files.group(1)

        self.assertEqual(json.loads(files)['files'], TEST_FILES)
                         
    def test_api_retrieve_login(self):

        # Create the database connection.
        mydb = mysql.connector.connect(
            host="localhost",
            user="root",
            password="mariaDB",
            database="holoway"
        )

        # Create a new code.
        new_code = create_new_login_code(mydb)
    
        # Update those values.
        update_login_code(mydb, new_code, 'email@email.com', 'token', 'name')

        # Generate a new key in a random file.
        api_key = generate_random_code(10)

        # Get the params.
        timestamp = str(int(time.time()))

        # Get again a name until there is no such file.
        # This way, we avoid rewriting a key.
        while exists("../crypto/keys/{}.key".format(api_key)):
            api_key = generate_random_code(10)

        # Generate the keys.
        generate_priv_pub_keys(api_key)
        
        # Load the keys.
        priv_key = load_private_key(api_key)

        # Generate a random message of the given length.
        msg = new_code + api_key + timestamp

        # Make sure encryption works.
        signature = sign_message(priv_key, msg)

        # Capture the stdoutput.
        printed = io.StringIO()
        sys.stdout = printed

        # Retrieve the information.
        api_retrieve_login(api_key, new_code, timestamp, signature)

        # Reset the std out.
        sys.stdout = sys.__stdout__

        # Delete that key.
        remove("../crypto/keys/{}.pem".format(api_key))
        remove("../crypto/keys/{}.pub".format(api_key))

        self.assertRegex(printed.getvalue(), r'''^Status: 200 OK
Content-Type: text/plain;charset=utf-8

{"status": "success", "email": "email@email.com", "name": "name", "token": "token"}$''')
                         
    def test_share_files(self):

        # Generate a new key in a random file.
        api_key = generate_random_code(10)

        # Get the params.
        timestamp = str(int(time.time()))
        token = TEST_TOKEN
        file_id = TEST_DIREC
        emails = TEST_SHARE

        # Get again a name until there is no such file.
        # This way, we avoid rewriting a key.
        while exists("../crypto/keys/{}.key".format(api_key)):
            api_key = generate_random_code(10)

        # Generate the keys.
        generate_priv_pub_keys(api_key)
        
        # Load the keys.
        priv_key = load_private_key(api_key)

        # Generate a random message of the given length.
        msg = file_id + "".join(emails) + token + api_key + timestamp

        # Make sure encryption works.
        signature = sign_message(priv_key, msg)

        # Capture the stdoutput.
        printed = io.StringIO()
        sys.stdout = printed

        # Call the api function.
        api_share_file(file_id, emails, token, api_key, timestamp, signature)

        # Reset the std out.
        sys.stdout = sys.__stdout__

        # Delete that key.
        remove("../crypto/keys/{}.pem".format(api_key))
        remove("../crypto/keys/{}.pub".format(api_key))

        # Get the http response.
        response = printed.getvalue()

        self.assertRegex(response, r'''^Status: 200 OK
Content-Type: text/plain;charset=utf-8

{"status": "success"}$''')
                         
    def test_token_verification(self):
        
        # Get a test token.
        # Insert a valid token to test.
        token = TEST_TOKEN

        self.assertTrue(check_credentials_are_valid(token)[0])

    def test_code_create(self):

        # Create the database connection.
        mydb = mysql.connector.connect(
            host="localhost",
            user="root",
            password="mariaDB",
            database="holoway"
        )

        # Get the reference.
        mycursor = mydb.cursor()

        # Get the reference.
        sql = "SELECT code FROM login_codes;"

        # Run the query.
        mycursor.execute(sql)

        # Get the result.
        all_codes_res = mycursor.fetchall()

        # Get all the current codes into a variable.
        all_codes = []
        for curr_code in all_codes_res:
            all_codes.append(curr_code[0])
        
        # Create the new code.
        new_code = create_new_login_code(mydb)

        # Make sure that it was not in the DB already.
        self.assertFalse(new_code in all_codes)

        # Make sure it does now.
        self.assertTrue(check_if_code_exists(mydb, new_code))

        # Now delete it.
        delete_login_code(mydb, new_code)

    def test_code_exists(self):

        # Create the database connection.
        mydb = mysql.connector.connect(
            host="localhost",
            user="root",
            password="mariaDB",
            database="holoway"
        )

        # Get all the codes that exist.
        mycursor = mydb.cursor() 
        sql = "SELECT code FROM login_codes;"
        mycursor.execute(sql)
        all_codes_res = mycursor.fetchall()
        codes_yes = []
        for curr_code in all_codes_res:
            codes_yes.append(curr_code[0])

        # Get a set of random codes.
        codes_no = [generate_random_code(6) for x in range(10)]

        # Remove those that do exist.
        codes_no = set(codes_no).difference(set(codes_yes))

        # Check that those that have to exist, do.
        for code in codes_yes:
            self.assertTrue(check_if_code_exists(mydb, code))

        # Check that those that do not exist, don't.
        for code in codes_no:
            self.assertFalse(check_if_code_exists(mydb, code))

    def test_delete_login_code(self):

        # Create the database connection.
        mydb = mysql.connector.connect(
            host="localhost",
            user="root",
            password="mariaDB",
            database="holoway"
        )

        # Do this 10 times.
        for i in range(10):

            # Create a new code.
            new_code = create_new_login_code(mydb)

            # Make sure it exists now.
            self.assertTrue(check_if_code_exists(mydb, new_code))
        
            # Now delete it.
            delete_login_code(mydb, new_code)

            # Make sure it no longer exists.
            self.assertFalse(check_if_code_exists(mydb, new_code))

    def test_delete_old_codes(self):

        # Create the database connection.
        mydb = mysql.connector.connect(
            host="localhost",
            user="root",
            password="mariaDB",
            database="holoway"
        )

        # Get the reference.
        mycursor = mydb.cursor()

        # Add one old code to make sure there is something to erase.
        # We know that this code is just for this test.
        sql = "INSERT INTO login_codes (code, added) VALUES ('erase_test', DATE_SUB(NOW(), INTERVAL 48 HOUR));"
        mycursor.execute(sql)
        mydb.commit()

        # Check that we added it.
        self.assertEqual(mycursor.rowcount, 1)

        # Get all the old codes that exist.
        mycursor = mydb.cursor()
        sql = "SELECT code FROM login_codes WHERE (added < DATE_SUB(NOW(), INTERVAL 24 HOUR));"
        mycursor.execute(sql)
        old_codes = len(mycursor.fetchall())

        # Make sure there's at least one, cause we added one.
        self.assertGreaterEqual(old_codes, 1)

        # Delete all the old ones.
        delete_old_codes(mydb)

        # Now count them again.
        # Get all the old codes that exist.
        mycursor = mydb.cursor()
        sql = "SELECT code FROM login_codes WHERE (added < DATE_SUB(NOW(), INTERVAL 24 HOUR));"
        mycursor.execute(sql)
        old_codes = len(mycursor.fetchall())

        # Make sure there's none now.
        self.assertEqual(old_codes, 0)

    def test_encrypt_decrypt(self):
        
        # Generate a new key in a random file.
        key_test_name = generate_random_code(10)

        # Get again a name until there is no such file.
        # This way, we avoid rewriting a key.
        while exists("../crypto/keys/{}.key".format(key_test_name)):
            key_test_name = generate_random_code(10)

        # Generate the key.
        generate_symmetric_key(key_test_name)

        # Load that file.
        key = load_symmetric_key(key_test_name)

        # Encrypt and decrypt 10 random strings.
        for i in range(10):

            # Get a random length.
            length = random.randint(0, 512)

            # Generate a random message of the given length.
            msg = generate_random_code(length)

            # Make sure encryption works.
            secret = symmetric_encrypt(key, msg)
            self.assertNotEqual(msg, secret)

            # Make sure decryption works.
            msg_2 = symmetric_decrypt(key, secret)
            self.assertEqual(msg, msg_2)

        # Delete that key.
        remove("../crypto/keys/{}.key".format(key_test_name))

    def test_generate_load_symmetric_key(self):
        
        # Generate a new key in a random file.
        key_test_name = generate_random_code(10)

        # Get again a name until there is no such file.
        # This way, we avoid rewriting a key.
        while exists("../crypto/keys/{}.key".format(key_test_name)):
            key_test_name = generate_random_code(10)

        # Generate the key.
        generate_symmetric_key(key_test_name)

        # Check that it exists.
        self.assertTrue(exists("../crypto/keys/{}.key".format(key_test_name)))
        
        # Load that file.
        key = load_symmetric_key(key_test_name)

        # Check that it is what should be.
        self.assertTrue(isinstance(key, (Fernet,)))

        # Delete that key.
        remove("../crypto/keys/{}.key".format(key_test_name))

    def test_generate_pub_priv_keys(self):
        
        # Generate a new key in a random file.
        api_key = generate_random_code(10)

        # Get again a name until there is no such file.
        # This way, we avoid rewriting a key.
        while exists("../crypto/keys/{}.pub".format(api_key)):
            api_key = generate_random_code(10)

        # Generate the keys.
        generate_priv_pub_keys(api_key)

        # Check that it exists.
        self.assertTrue(exists("../crypto/keys/{}.pem".format(api_key)))
        self.assertTrue(exists("../crypto/keys/{}.pub".format(api_key)))
        
        # Load the keys.
        pub_key = load_public_key(api_key)
        priv_key = load_private_key(api_key)

        # Check that they are what they should be.
        self.assertTrue(isinstance(pub_key, (Crypto.PublicKey.RSA.RsaKey,)))
        self.assertTrue(isinstance(priv_key, (Crypto.PublicKey.RSA.RsaKey,)))

        # Delete those keys.
        remove("../crypto/keys/{}.pem".format(api_key))
        remove("../crypto/keys/{}.pub".format(api_key))

    def test_list_files_in_drive(self):
        
        # Get a test token.
        # Insert a valid token to test.
        token = TEST_TOKEN

        # Init a list of expected files.
        expected_files = TEST_FILES

        status, files, msg = list_files_in_drive(token, None)

        self.assertTrue(status)
        self.assertEqual(files, expected_files)

    def test_list_files_in_drive_folder(self):
        
        # Get a test token.
        # Insert a valid token to test.
        token = TEST_TOKEN

        # Init a list of expected files.
        expected_files = TEST_INDIR

        status, files, msg = list_files_in_drive(token, TEST_DIREC)

        self.assertTrue(status)
        self.assertEqual(files, expected_files)

    def test_retrieve_token_email_from_code(self):

        # Create the database connection.
        mydb = mysql.connector.connect(
            host="localhost",
            user="root",
            password="mariaDB",
            database="holoway"
        )

        # Do this 10 times.
        for i in range(10):

            # Create a new code.
            new_code = create_new_login_code(mydb)
        
            # Update those values.
            update_login_code(mydb, new_code, 'email@email.com', 'token', 'name')

            # Retrieve the information.
            email, token, name = retrieve_token_email_from_code(mydb, new_code)

            # Make sure these are the same.
            self.assertEqual(email, 'email@email.com')
            self.assertEqual(token, 'token')
            self.assertEqual(name, 'name')

            # Now delete it.
            delete_login_code(mydb, new_code)

        # Now we do the same for 10 cases that won't exist.
        # Do this 10 times.
        for i in range(10):

            # Create a new code.
            # This will not exist because codes are 6-char-long.
            fake_code = generate_random_code(4)

            # Retrieve the information.
            email, token, name = retrieve_token_email_from_code(mydb, fake_code)

            # Make sure these are the same.
            self.assertEqual(email, None)
            self.assertEqual(token, None)
            self.assertEqual(name, None)

    def test_sign_check(self):
        
        # Generate a new key in a random file.
        api_key = generate_random_code(10)

        # Get again a name until there is no such file.
        # This way, we avoid rewriting a key.
        while exists("../crypto/keys/{}.pub".format(api_key)):
            api_key = generate_random_code(10)

        # Generate the keys.
        generate_priv_pub_keys(api_key)

        # Load the keys.
        pub_key = load_public_key(api_key)
        priv_key = load_private_key(api_key)

        # Encrypt and decrypt 10 random strings.
        for i in range(10):

            # Get a random length.
            length = random.randint(0, 512)

            # Generate a random message of the given length.
            msg = generate_random_code(length)

            # Get the signature.
            signature = sign_message(priv_key, msg)

            # Make sure decryption works.
            self.assertTrue(verify_signature(pub_key, msg, signature))

        # Delete that key.
        remove("../crypto/keys/{}.pem".format(api_key))
        remove("../crypto/keys/{}.pub".format(api_key))

    def test_start_login_pipeline(self):

        # Create the database connection.
        mydb = mysql.connector.connect(
            host="localhost",
            user="root",
            password="mariaDB",
            database="holoway"
        )

        # Create the new code.
        code = create_new_login_code(mydb)

        # Capture the stdoutput.
        printed = io.StringIO()
        sys.stdout = printed

        # Call the api function.
        start_login_pipeline(code)

        # Reset the std out.
        sys.stdout = sys.__stdout__

        self.assertRegex(printed.getvalue(), """^Status: 303 See other
Location: https://accounts.google.com/o/oauth2/auth?.+redirect_uri=https%3A%2F%2Fazure.borjagq.com%2Flogin_callback%2F.+state={}.+

$""".format(code))
        
        # Now delete it.
        delete_login_code(mydb, code)
    
        # Get a random code that does not exist.
        code = generate_random_code(6)

        # Make sure it does not exist.
        while check_if_code_exists(mydb, code):
            code = generate_random_code(6)

        # Capture the stdoutput.
        printed = io.StringIO()
        sys.stdout = printed

        # Call the api function.
        start_login_pipeline(code)

        # Reset the std out.
        sys.stdout = sys.__stdout__

        self.assertRegex(printed.getvalue(), """^Location: /

$""".format(code))
        
    def test_update_login_code(self):

        # Create the database connection.
        mydb = mysql.connector.connect(
            host="localhost",
            user="root",
            password="mariaDB",
            database="holoway"
        )

        # Do the same for 10 random values.
        for i in range(10):

            code = create_new_login_code(mydb)

            # Get all the old values.
            mycursor = mydb.cursor()
            sql = "SELECT email, token FROM login_codes WHERE (code = %s);"
            mycursor.execute(sql, (code, ))
            email_org, token_org = mycursor.fetchall()[0]

            update_login_code(mydb, code, 'new email', 'new token', 'name')

            # Get all the new values.
            mycursor = mydb.cursor()
            sql = "SELECT email, token FROM login_codes WHERE (code = %s);"
            mycursor.execute(sql, (code, ))
            email_new, token_new = mycursor.fetchall()[0]

            # Check that we added it.
            self.assertEqual(email_new, "new email")
            self.assertEqual(token_new, "new token")
            self.assertNotEqual(email_org, email_new)
            self.assertNotEqual(token_org, token_new)

            delete_login_code(mydb, code)

    def test_user_information(self):

        # Get a test token.
        # Insert a valid token to test.
        token = TEST_TOKEN

        # Get the credentials.
        status, token, creds = check_credentials_are_valid(token)

        self.assertTrue(status)
        self.assertEqual(get_user_info(token), (TEST_EMAIL, TEST_NAMES))

    def test_share_file(self):
        
        # Get a test token.
        # Insert a valid token to test.
        token = TEST_TOKEN

        # Init a list of expected files.
        expected_files = TEST_FILES

        # The users with whom the files will be shared.
        emails = [TEST_SHARE]

        status, msg = share_file(token, TEST_DIREC, emails)

        self.assertTrue(status)

    def test_verify_timestamp(self):

        # Do this test 10 times.
        for i in range(10):
        
            # Get random differences from 0 to the double of the
            # maximum allowed.
            diff = random.randrange(-MAX_DIF * 2, MAX_DIF * 2)

            # Get the date.
            comp = int(time.time() + diff)
            comp_str = str(comp)

            # Check if this is within the acceptable range or not.
            if diff < MAX_DIF and diff > -MAX_DIF:

                self.assertTrue(verify_timestamp(comp_str))

            else:

                self.assertFalse(verify_timestamp(comp_str))

    def test_10_random_codes(self):

        # Create the database connection.
        mydb = mysql.connector.connect(
            host="localhost",
            user="root",
            password="mariaDB",
            database="holoway"
        )
        
        # Do this 10 times for random lengths from 0 to 100.
        for i in range(10):

            rand_len = random.randint(0, 100)
        
            self.assertEqual(len(generate_random_code(rand_len)), rand_len)
