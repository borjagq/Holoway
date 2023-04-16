'''
Contains our useful funtions that used across the API.
'''

import time

MAX_DIF = 60

def verify_timestamp(timestamp, max_dif=MAX_DIF):

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
