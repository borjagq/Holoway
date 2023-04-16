import unittest
from index import *

# Add parent's subdirectories.
sys.path.append('..')

from crypto.crypto import *

class MyFirstTests(unittest.TestCase):

    def test_list_files_in_drive(self):
        
        # Get a test token.
        # Insert a valid token to test.
        token = "gAAAAABkL0UD9hTmuCBvJZieutb94GFj4GD9PIHRImVewWjMMsDr-iySbTTi5IQtxf3SXtXLHNYR0dx72iYf07I_jGszBXAUdFAF8VUG4Z5pRtBMqaRWtIlGwLYisBWe8pFYHT-sPWekDArPtyZhUod5lTebB1TZck-eWpxsSq7hR59V68-94WarsTKLBeDn0CFcowEF4a5GFju-rl9_FrWZZI_cwDXRO4GJ8cnbfhqcUiw50xWZlYCyd26DJRW67AFBsc4kGlEeXuLSTAZt6RhkUtfU0VKI_iTO_zUsT0pH1TOJKqozSOuZUO1Cy77kznAh8uUdjNSn2H__DUvh69zM2Valgl567xxxZ4v5nnnaXnk_cvvCUin07nUctH8rFYZYgIOLlDbZC_vpqxUO_P8AcwfwdDc6Y-5cZ8P5sGhTrpIFNUTL97Z4liszO6tuuZvljMJRUlzf1yvm_qocRyVvvbR40bMT9PrtcFdfdY1xl09ML-dIrex80qq61Ob_YQGJ5t9fsaT09L5m2Y6ASc7SiTfsqPx0oBuJU5TwsNEz6ebSCxKhmrAq5bm3-YY97SMym2sG0W6PdbbD3VJh1QzIciEmTM8iZ3SJA4V91SeQrvNbKFnWPsZ4aSkHUzK2rrR2c-iq3WZ4pOiA51xXsnPQ-7k1ZGP5wWo8VWmbL-tuJy62S2kQWCUP3BJBE7Y0EUQ817XJFllfn7EfysR9jmPYXU39I48N17diOo-4DsMYcA-0bZCErhptz_D4JlI8vw8vlukg9nUfsBo1zFWCHaYb3yJgEdLjFvVve2NUhZJ9EP-tCeqEvkUpM-nBxYgAXlQi2tj2zWrKzfB8r1Vb8GqW9WdIzWFGub1gCRg141cONKkfhtNznCFlakRTHg2LwE54U7seLI98LD3qXt8RuRia6eq9Zhbgv_p9ZqJwk3hbCrfhIcJTRyEkEpzCR2v3ugP6ewlsYkdDwweUxUMS-pNKqplNVdUs0U56y0ofua0w45ZoV6BELBg="

        # Init a list of expected files.
        expected_files = [{'id': '1GjZyXcnGiEHDH5YqzchDhkXWPNAHky-VBLVMbj18Ps0', 'type': 'application/vnd.google-apps.form', 'name': 'Untitled form'}, {'id': '14C9PIwRd6U3cUVXtOStLRUgQ7UF8igED', 'type': 'application/vnd.google-apps.folder', 'name': 'CHECKY'}, {'id': '1sImaqT6WyUp90r4MiXUHxtOopZGDC___', 'type': 'application/vnd.google-apps.folder', 'name': 'Adaptive Applications'}, {'id': '1NdZ4a-XHMhd0UkjiR3rjSQIzI1W-09zy', 'type': 'application/vnd.google-apps.folder', 'name': 'Augmented Reality'}, {'id': '16mRrIpkTMp1hcAH9TMxm4RQA_nMPeJF1', 'type': 'application/vnd.google-apps.folder', 'name': 'Internet of Things'}, {'id': '1nm_WhKDTdKfUYZN5VapZT3gARX4YaGOG', 'type': 'application/vnd.google-apps.shortcut', 'name': 'DUB-GLA'}, {'id': '1-DQDtDwMLZLCWifOBp8mhAvtKA-QvvOM', 'type': 'application/vnd.google-apps.folder', 'name': 'Mathematics of light and sound'}, {'id': '1OTcyj33zdpWYHqSL6fGql-o7foGazIRz', 'type': 'application/vnd.google-apps.folder', 'name': 'Computer Vision'}, {'id': '1IVhZcvmYv0ZwGiDbC6Qs-i009J7HmJ9-', 'type': 'application/vnd.google-apps.folder', 'name': 'Research and Innovation'}, {'id': '1WIenbicSO3ZF3BUfoyfQtSB284-HR9Bi', 'type': 'application/vnd.google-apps.folder', 'name': 'Software Engineering'}, {'id': '1JdA9ByayvyQ5XX_iRdrRaimoVbN_NE0S', 'type': 'application/vnd.google-apps.folder', 'name': 'Computer Graphics'}, {'id': '1FFSpslP-QqD001whZNU6R90me2iO3sG3', 'type': 'application/vnd.google-apps.folder', 'name': 'Resources'}, {'id': '1AN9OtbyUkXYFx1ENdGKJtXwwCMP_6xM5', 'type': 'application/vnd.google-apps.folder', 'name': 'Colab Notebooks'}, {'id': '1LRmg1cVQRxgFeOXWm3Ptz8Y_m98VQmUT', 'type': 'application/zip', 'name': 'Passport.zip'}]

        status, files, msg = list_files_in_drive(token, None)

        self.assertTrue(status)
        self.assertEqual(files, expected_files)

    def test_list_files_in_drive_folder(self):
        
        # Get a test token.
        # Insert a valid token to test.
        token = "gAAAAABkL0UD9hTmuCBvJZieutb94GFj4GD9PIHRImVewWjMMsDr-iySbTTi5IQtxf3SXtXLHNYR0dx72iYf07I_jGszBXAUdFAF8VUG4Z5pRtBMqaRWtIlGwLYisBWe8pFYHT-sPWekDArPtyZhUod5lTebB1TZck-eWpxsSq7hR59V68-94WarsTKLBeDn0CFcowEF4a5GFju-rl9_FrWZZI_cwDXRO4GJ8cnbfhqcUiw50xWZlYCyd26DJRW67AFBsc4kGlEeXuLSTAZt6RhkUtfU0VKI_iTO_zUsT0pH1TOJKqozSOuZUO1Cy77kznAh8uUdjNSn2H__DUvh69zM2Valgl567xxxZ4v5nnnaXnk_cvvCUin07nUctH8rFYZYgIOLlDbZC_vpqxUO_P8AcwfwdDc6Y-5cZ8P5sGhTrpIFNUTL97Z4liszO6tuuZvljMJRUlzf1yvm_qocRyVvvbR40bMT9PrtcFdfdY1xl09ML-dIrex80qq61Ob_YQGJ5t9fsaT09L5m2Y6ASc7SiTfsqPx0oBuJU5TwsNEz6ebSCxKhmrAq5bm3-YY97SMym2sG0W6PdbbD3VJh1QzIciEmTM8iZ3SJA4V91SeQrvNbKFnWPsZ4aSkHUzK2rrR2c-iq3WZ4pOiA51xXsnPQ-7k1ZGP5wWo8VWmbL-tuJy62S2kQWCUP3BJBE7Y0EUQ817XJFllfn7EfysR9jmPYXU39I48N17diOo-4DsMYcA-0bZCErhptz_D4JlI8vw8vlukg9nUfsBo1zFWCHaYb3yJgEdLjFvVve2NUhZJ9EP-tCeqEvkUpM-nBxYgAXlQi2tj2zWrKzfB8r1Vb8GqW9WdIzWFGub1gCRg141cONKkfhtNznCFlakRTHg2LwE54U7seLI98LD3qXt8RuRia6eq9Zhbgv_p9ZqJwk3hbCrfhIcJTRyEkEpzCR2v3ugP6ewlsYkdDwweUxUMS-pNKqplNVdUs0U56y0ofua0w45ZoV6BELBg="

        # Init a list of expected files.
        expected_files = [{'id': '1gYhBEk5Cb-F89cN63xOhkiimmUbe4nHk', 'type': 'application/vnd.google-apps.folder', 'name': 'Deliverables semester 2'}, {'id': '1-eT4b1dF1lntsbswusTa_1YeliwnNlzE9lfmGqkLg7I', 'type': 'application/vnd.google-apps.spreadsheet', 'name': 'VR Room - Components'}]

        status, files, msg = list_files_in_drive(token, '1WIenbicSO3ZF3BUfoyfQtSB284-HR9Bi')

        self.assertTrue(status)
        self.assertEqual(files, expected_files)
