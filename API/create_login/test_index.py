import unittest
import random
import mysql.connector
from index import *

class MyFirstTests(unittest.TestCase):

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
