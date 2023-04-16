import unittest
from utils import *

import datetime, random

class MyFirstTests(unittest.TestCase):

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
