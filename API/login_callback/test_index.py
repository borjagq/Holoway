import unittest
import random, string
import mysql.connector
from index import *

import sys
 
# Add parent's subdirectories.
sys.path.append('..')

from create_login.index import *

class MyFirstTests(unittest.TestCase):

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

            update_login_code(mydb, code, 'new email', 'new token')

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
