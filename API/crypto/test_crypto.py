import unittest
from crypto import *

import string, random
from os.path import exists
from os import remove
from cryptography.fernet import Fernet
import Crypto

def generate_random_str(len: 10):

    # Get the desired options.
    chars = string.ascii_lowercase + string.digits

    return ''.join(random.choice(chars) for i in range(len))

class MyFirstTests(unittest.TestCase):

    def test_generate_load_symmetric_key(self):
        
        # Generate a new key in a random file.
        key_test_name = generate_random_str(10)

        # Get again a name until there is no such file.
        # This way, we avoid rewriting a key.
        while exists("keys/{}.key".format(key_test_name)):
            key_test_name = generate_random_str(10)

        # Generate the key.
        generate_symmetric_key(key_test_name)

        # Check that it exists.
        self.assertTrue(exists("keys/{}.key".format(key_test_name)))
        
        # Load that file.
        key = load_symmetric_key(key_test_name)

        # Check that it is what should be.
        self.assertTrue(isinstance(key, (Fernet,)))

        # Delete that key.
        remove("keys/{}.key".format(key_test_name))

    def test_encrypt_decrypt(self):
        
        # Generate a new key in a random file.
        key_test_name = generate_random_str(10)

        # Get again a name until there is no such file.
        # This way, we avoid rewriting a key.
        while exists("keys/{}.key".format(key_test_name)):
            key_test_name = generate_random_str(10)

        # Generate the key.
        generate_symmetric_key(key_test_name)

        # Load that file.
        key = load_symmetric_key(key_test_name)

        # Encrypt and decrypt 10 random strings.
        for i in range(10):

            # Get a random length.
            length = random.randint(0, 512)

            # Generate a random message of the given length.
            msg = generate_random_str(length)

            # Make sure encryption works.
            secret = symmetric_encrypt(key, msg)
            self.assertNotEqual(msg, secret)

            # Make sure decryption works.
            msg_2 = symmetric_decrypt(key, secret)
            self.assertEqual(msg, msg_2)

        # Delete that key.
        remove("keys/{}.key".format(key_test_name))

    def test_generate_pub_priv_keys(self):
        
        # Generate a new key in a random file.
        api_key = generate_random_str(10)

        # Get again a name until there is no such file.
        # This way, we avoid rewriting a key.
        while exists("keys/{}.pub".format(api_key)):
            api_key = generate_random_str(10)

        # Generate the keys.
        generate_priv_pub_keys(api_key)

        # Check that it exists.
        self.assertTrue(exists("keys/{}.pem".format(api_key)))
        self.assertTrue(exists("keys/{}.pub".format(api_key)))
        
        # Load the keys.
        pub_key = load_public_key(api_key)
        priv_key = load_private_key(api_key)

        # Check that they are what they should be.
        self.assertTrue(isinstance(pub_key, (Crypto.PublicKey.RSA.RsaKey,)))
        self.assertTrue(isinstance(priv_key, (Crypto.PublicKey.RSA.RsaKey,)))

        # Delete those keys.
        remove("keys/{}.pem".format(api_key))
        remove("keys/{}.pub".format(api_key))

    def test_sign_check(self):
        
        # Generate a new key in a random file.
        api_key = generate_random_str(10)

        # Get again a name until there is no such file.
        # This way, we avoid rewriting a key.
        while exists("keys/{}.pub".format(api_key)):
            api_key = generate_random_str(10)

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
            msg = generate_random_str(length)

            # Get the signature.
            signature = sign_message(priv_key, msg)

            # Make sure decryption works.
            self.assertTrue(verify_signature(pub_key, msg, signature))

        # Delete that key.
        remove("keys/{}.pem".format(api_key))
        remove("keys/{}.pub".format(api_key))
