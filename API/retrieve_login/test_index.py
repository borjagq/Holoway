import unittest
from index import *

import string, random
import mysql.connector

# Add parent's subdirectories.
import sys
sys.path.append('..')

from crypto.crypto import *
from create_login.index import *
from login_callback.index import *

def generate_random_str(len: 10):

    # Get the desired options.
    chars = string.ascii_lowercase + string.digits

    return ''.join(random.choice(chars) for i in range(len))

class MyFirstTests(unittest.TestCase):

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
            update_login_code(mydb, new_code, 'email@email.com', 'token')

            # Retrieve the information.
            email, token = retrieve_token_email_from_code(mydb, new_code)

            # Make sure these are the same.
            self.assertEqual(email, 'email@email.com')
            self.assertEqual(token, 'token')

            # Now delete it.
            delete_login_code(mydb, new_code)

        # Now we do the same for 10 cases that won't exist.
        # Do this 10 times.
        for i in range(10):

            # Create a new code.
            # This will not exist because codes are 6-char-long.
            fake_code = generate_random_str(4)

            # Retrieve the information.
            email, token = retrieve_token_email_from_code(mydb, new_code)

            # Make sure these are the same.
            self.assertEqual(email, None)
            self.assertEqual(token, None)
