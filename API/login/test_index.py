import unittest
import random, string
import mysql.connector
from index import *

def generate_random_code(len: 10):

    # Get the desired options.
    chars = string.ascii_lowercase + string.digits

    return ''.join(random.choice(chars) for i in range(len))

class MyFirstTests(unittest.TestCase):

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
