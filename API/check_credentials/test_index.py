import unittest
from index import *

# Add parent's subdirectories.
sys.path.append('..')

from crypto.crypto import *

class MyFirstTests(unittest.TestCase):

    def test_token_verification(self):
        
        # Get a test token.
        # Insert a valid token to test.
        token = "gAAAAABkL0UD9hTmuCBvJZieutb94GFj4GD9PIHRImVewWjMMsDr-iySbTTi5IQtxf3SXtXLHNYR0dx72iYf07I_jGszBXAUdFAF8VUG4Z5pRtBMqaRWtIlGwLYisBWe8pFYHT-sPWekDArPtyZhUod5lTebB1TZck-eWpxsSq7hR59V68-94WarsTKLBeDn0CFcowEF4a5GFju-rl9_FrWZZI_cwDXRO4GJ8cnbfhqcUiw50xWZlYCyd26DJRW67AFBsc4kGlEeXuLSTAZt6RhkUtfU0VKI_iTO_zUsT0pH1TOJKqozSOuZUO1Cy77kznAh8uUdjNSn2H__DUvh69zM2Valgl567xxxZ4v5nnnaXnk_cvvCUin07nUctH8rFYZYgIOLlDbZC_vpqxUO_P8AcwfwdDc6Y-5cZ8P5sGhTrpIFNUTL97Z4liszO6tuuZvljMJRUlzf1yvm_qocRyVvvbR40bMT9PrtcFdfdY1xl09ML-dIrex80qq61Ob_YQGJ5t9fsaT09L5m2Y6ASc7SiTfsqPx0oBuJU5TwsNEz6ebSCxKhmrAq5bm3-YY97SMym2sG0W6PdbbD3VJh1QzIciEmTM8iZ3SJA4V91SeQrvNbKFnWPsZ4aSkHUzK2rrR2c-iq3WZ4pOiA51xXsnPQ-7k1ZGP5wWo8VWmbL-tuJy62S2kQWCUP3BJBE7Y0EUQ817XJFllfn7EfysR9jmPYXU39I48N17diOo-4DsMYcA-0bZCErhptz_D4JlI8vw8vlukg9nUfsBo1zFWCHaYb3yJgEdLjFvVve2NUhZJ9EP-tCeqEvkUpM-nBxYgAXlQi2tj2zWrKzfB8r1Vb8GqW9WdIzWFGub1gCRg141cONKkfhtNznCFlakRTHg2LwE54U7seLI98LD3qXt8RuRia6eq9Zhbgv_p9ZqJwk3hbCrfhIcJTRyEkEpzCR2v3ugP6ewlsYkdDwweUxUMS-pNKqplNVdUs0U56y0ofua0w45ZoV6BELBg="

        self.assertTrue(check_credentials_are_valid(token)[0])

    def test_user_information(self):

        # Get a test token.
        # Insert a valid token to test.
        token = "gAAAAABkL0UD9hTmuCBvJZieutb94GFj4GD9PIHRImVewWjMMsDr-iySbTTi5IQtxf3SXtXLHNYR0dx72iYf07I_jGszBXAUdFAF8VUG4Z5pRtBMqaRWtIlGwLYisBWe8pFYHT-sPWekDArPtyZhUod5lTebB1TZck-eWpxsSq7hR59V68-94WarsTKLBeDn0CFcowEF4a5GFju-rl9_FrWZZI_cwDXRO4GJ8cnbfhqcUiw50xWZlYCyd26DJRW67AFBsc4kGlEeXuLSTAZt6RhkUtfU0VKI_iTO_zUsT0pH1TOJKqozSOuZUO1Cy77kznAh8uUdjNSn2H__DUvh69zM2Valgl567xxxZ4v5nnnaXnk_cvvCUin07nUctH8rFYZYgIOLlDbZC_vpqxUO_P8AcwfwdDc6Y-5cZ8P5sGhTrpIFNUTL97Z4liszO6tuuZvljMJRUlzf1yvm_qocRyVvvbR40bMT9PrtcFdfdY1xl09ML-dIrex80qq61Ob_YQGJ5t9fsaT09L5m2Y6ASc7SiTfsqPx0oBuJU5TwsNEz6ebSCxKhmrAq5bm3-YY97SMym2sG0W6PdbbD3VJh1QzIciEmTM8iZ3SJA4V91SeQrvNbKFnWPsZ4aSkHUzK2rrR2c-iq3WZ4pOiA51xXsnPQ-7k1ZGP5wWo8VWmbL-tuJy62S2kQWCUP3BJBE7Y0EUQ817XJFllfn7EfysR9jmPYXU39I48N17diOo-4DsMYcA-0bZCErhptz_D4JlI8vw8vlukg9nUfsBo1zFWCHaYb3yJgEdLjFvVve2NUhZJ9EP-tCeqEvkUpM-nBxYgAXlQi2tj2zWrKzfB8r1Vb8GqW9WdIzWFGub1gCRg141cONKkfhtNznCFlakRTHg2LwE54U7seLI98LD3qXt8RuRia6eq9Zhbgv_p9ZqJwk3hbCrfhIcJTRyEkEpzCR2v3ugP6ewlsYkdDwweUxUMS-pNKqplNVdUs0U56y0ofua0w45ZoV6BELBg="

        # Get the credentials.
        status, _, creds = check_credentials_are_valid(token)

        self.assertTrue(status)
        self.assertEqual(get_user_info(creds), 'garcaqub@tcd.ie')
