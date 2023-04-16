'''
Contains our cryptographic module for performing all the signing,
checking, encrypting and decrypting operations.
'''

from cryptography.fernet import Fernet
from Crypto.Hash import SHA256
from Crypto.PublicKey import RSA
from Crypto.Signature import PKCS1_v1_5
import base64
from os.path import dirname, abspath, exists

def generate_symmetric_key(name):

    '''
    Generates an 128-bit AES in CBC mode.
    
    There is a module that does this called Fernet.
    '''

    # Generate the new key.
    key = Fernet.generate_key()

    # Get the current file's path.
    file_path = abspath(dirname(abspath(__file__)))

    # Store it in the file specified by the name.
    with open(file_path + "/keys/{}.key".format(name), "wb") as file:
        file.write(key)

def load_symmetric_key(name):
    
    '''
    Loads an 128-bit AES in CBC mode.
    '''

    # Get the current file's path.
    file_path = abspath(dirname(abspath(__file__)))

    # Check if it exists.
    if not exists(file_path):
        return False

    # Load the contents of the file.
    with open(file_path + "/keys/{}.key".format(name), "rb") as file:
        key = file.read()

    # Load it as a fernet instance.
    return Fernet(key)

def symmetric_encrypt(key, msg):

    '''
    Given a string (msg) and Fernet key (key), it encrypts the information
    and returns it.

    This function uses the 128-bit AES in CBC mode, using PKCS#7 padding.

    Fernet also uses base_64 to make the messages safe for HTTP.
    '''

    # Transform it to bytes.
    msg = bytes(msg, 'utf-8')

    # Encrypt
    return key.encrypt(msg).decode("utf-8")

def symmetric_decrypt(key, crypt):

    '''
    Given an encrypted string (crypt) and Fernet key (key), it decrypts
    the information and returns it.

    This function uses the 128-bit AES in CBC mode, using PKCS#7 padding.
    '''

    # Transform it to bytes.
    crypt = bytes(crypt, 'utf-8')

    # Decrypt
    return key.decrypt(crypt).decode("utf-8")

def generate_priv_pub_keys(api_key):

    '''
    Generates a pair of SHA256 private and public keys.

    For this, we will use the rsa package that already implements the
    Rivest-Shamir-Adleman scheme.
    '''

    # Generate a 4096 bit key. This is way beyond what is required,
    # we'll test how it performs.
    key = RSA.generate(4096)

    # Get the current file's path.
    file_path = abspath(dirname(abspath(__file__)))

    # Write the private key into the keys folder.
    # It needs to be removed from there and given to the other side.
    with open(file_path + "/keys/{}.pem".format(api_key), 'wb') as file:
        file.write(key.export_key('PEM'))

    # Write the public key into the keys folder.
    # This one stays here to verify the identity of the other side.
    with open(file_path + "/keys/{}.pub".format(api_key), 'wb') as file:
        file.write(key.public_key().export_key('PEM'))

def load_public_key(api_key):
    
    '''
    Loads the Rivest-Shamir-Adleman public key in keys/api_key.pub.
    '''

    # Get the current file's path.
    file_path = abspath(dirname(abspath(__file__)))

    # Check if it exists.
    if not exists(file_path):
        return False

    # Load the contents of the file.
    with open(file_path + "/keys/{}.pub".format(api_key), "rb") as file:
        key = RSA.import_key(file.read())

    return key

def load_private_key(api_key):
    
    '''
    Loads the Rivest-Shamir-Adleman private key in keys/api_key.pem.
    '''

    # Get the current file's path.
    file_path = abspath(dirname(abspath(__file__)))

    # Check if it exists.
    if not exists(file_path):
        return False

    # Load the contents of the file.
    with open(file_path + "/keys/{}.pem".format(api_key), "rb") as file:
        key = RSA.import_key(file.read())

    return key

def sign_message(priv_key, message):

    '''
    Given a string (message) and a private key (priv_key), obtains a signature
    for the message to guarantee the identity of the sender.

    This function uses the PKCS#1 signing algorithm.
    '''

    # Transform the message to bytes.
    message = bytes(message, 'utf-8')

    # Obtains the hash for this message using the SHA256 algorithm (sota).
    hash = SHA256.new(message)

    # Now we build the actual signer from the private key that will get the
    # signature.
    signer = PKCS1_v1_5.new(priv_key)

    # Now we return the signature.
    signature = signer.sign(hash)

    # Encode it in base 64 to make it HTTP safe.
    return base64.b64encode(signature)

def verify_signature(pub_key, message, signature):

    '''
    Given a string (message), its signature and a public key (pub_key), verifies
    if the signature corresponds to the pairing private key for this message,
    guaranteeing the identity of the sender.

    This function uses the PKCS#1 signing algorithm.
    '''

    # Transform the message to bytes.
    message = bytes(message, 'utf-8')

    # First of all, let's decode the base64 encoding.
    signature = base64.b64decode(signature)

    # Obtains the hash for this message using the SHA256 algorithm (sota).
    hash = SHA256.new(message)

    # Now we build the verifier from the public key.
    verifier = PKCS1_v1_5.new(pub_key)
    
    # Verify the signature.
    return verifier.verify(hash, signature)
