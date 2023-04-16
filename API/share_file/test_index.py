import unittest
from index import *

# Add parent's subdirectories.
sys.path.append('..')

from crypto.crypto import *

class MyFirstTests(unittest.TestCase):

    def test_share_file(self):
        
        # Get a test token.
        # Insert a valid token to test.
        token = "gAAAAABkL1s7evp8Uwgnny46loPTNcDRko3PaoRLG1F8auqLUb4NykWBwM-3fwtfhyhJRGhmTe13D9DJj53E9I6QLsETmWxqZHxfsAtMI04DSZZ3v46EBWYof67OqJ9HulV8rvEl2_LhgBYnmCcBeVOcZdO-icpFvIffFY4Zt_MyQHsP3CWXS16MQs0yEvQ3jQTl6tF-LOS1RApVRIFWXkQf60vZ4mzNIdi7J8yRm-u6vN4RUinNtfU3Gul2NGK8JL8WyVLZ5IKv032CkKbCQXsOMyP40BIJvrwtvpKLNKqRh3fVPeD5dS6KxQcRMdcInPwy_0XI1Em9uJvpZDMt3znxBfj3N9LPHS4rj8WLry415mdmVSpe11TMBV1lPgmJRATJbghWN7fjpkE2SS5CpWP-fBY0moCkFimuiZCpEcKXK3N39mfHatg_ef5w6pGqfzHl3NLrzTdIZGbtCvma0fn113iBKeAM2UmSUL5ByBbUtPKKL8LIcwkuSYL1QMtSRw_C-YY7IUmpTmnpY07RyOCmVHNTr5xq1-Tv2J0N9ohSrrFd39L9xTiNuIgzOAiePjazrSDBQs71L3LXD7chUiRWf7npaCVbuOq_7dGA-_1xIIPbmUgNwIjuGdSNF9jKEp5vgj41xpZRY2V3E_zHdvHumxi-Cu4_2hjrppJfx941ntB_9PTMdElDljJ_uRmaGK3D3DmzkmYYmOPzZh1XOeQFaVz6AKrWN6T5OUE9i2Q4vETwoBRa8AMFHt-axhbNUjN03yHMWrEo4AdkROV6Qhx6uiPscvgNHoDXZuZ4zzGL0N7wy1QKtgLg0QUnenI9SVD9gSZozrNUeDX38oHCIZh1HMiycOO-kiUR9eZQUs1xO3oVXLnm2gIEVV01LIqf5C1IowfIQPHWawrDLAiyxYOi7KUcbXSvBdHh6qeYkpMcUugp4KxE4VTcisg5YbH-zNqB0zk8kydmZ4AJ7PiZPln4HEVWfiis4M_4bqbX3igcEte3v-avp2E="

        # Init a list of expected files.
        expected_files = [{'id': '1GjZyXcnGiEHDH5YqzchDhkXWPNAHky-VBLVMbj18Ps0', 'type': 'application/vnd.google-apps.form', 'name': 'Untitled form'}, {'id': '14C9PIwRd6U3cUVXtOStLRUgQ7UF8igED', 'type': 'application/vnd.google-apps.folder', 'name': 'CHECKY'}, {'id': '1sImaqT6WyUp90r4MiXUHxtOopZGDC___', 'type': 'application/vnd.google-apps.folder', 'name': 'Adaptive Applications'}, {'id': '1NdZ4a-XHMhd0UkjiR3rjSQIzI1W-09zy', 'type': 'application/vnd.google-apps.folder', 'name': 'Augmented Reality'}, {'id': '16mRrIpkTMp1hcAH9TMxm4RQA_nMPeJF1', 'type': 'application/vnd.google-apps.folder', 'name': 'Internet of Things'}, {'id': '1nm_WhKDTdKfUYZN5VapZT3gARX4YaGOG', 'type': 'application/vnd.google-apps.shortcut', 'name': 'DUB-GLA'}, {'id': '1-DQDtDwMLZLCWifOBp8mhAvtKA-QvvOM', 'type': 'application/vnd.google-apps.folder', 'name': 'Mathematics of light and sound'}, {'id': '1OTcyj33zdpWYHqSL6fGql-o7foGazIRz', 'type': 'application/vnd.google-apps.folder', 'name': 'Computer Vision'}, {'id': '1IVhZcvmYv0ZwGiDbC6Qs-i009J7HmJ9-', 'type': 'application/vnd.google-apps.folder', 'name': 'Research and Innovation'}, {'id': '1WIenbicSO3ZF3BUfoyfQtSB284-HR9Bi', 'type': 'application/vnd.google-apps.folder', 'name': 'Software Engineering'}, {'id': '1JdA9ByayvyQ5XX_iRdrRaimoVbN_NE0S', 'type': 'application/vnd.google-apps.folder', 'name': 'Computer Graphics'}, {'id': '1FFSpslP-QqD001whZNU6R90me2iO3sG3', 'type': 'application/vnd.google-apps.folder', 'name': 'Resources'}, {'id': '1AN9OtbyUkXYFx1ENdGKJtXwwCMP_6xM5', 'type': 'application/vnd.google-apps.folder', 'name': 'Colab Notebooks'}, {'id': '1LRmg1cVQRxgFeOXWm3Ptz8Y_m98VQmUT', 'type': 'application/zip', 'name': 'Passport.zip'}]

        # The users with whom the files will be shared.
        emails = ['borjagqcole@gmail.com']

        status, msg = share_file(token, '1LRmg1cVQRxgFeOXWm3Ptz8Y_m98VQmUT', emails)

        print(msg)

        self.assertTrue(status)
