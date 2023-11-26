using Nop.Core;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Nop.Services.Security
{
    public class CryptographyService : ICryptographyService
    {
        #region Fields
        private const string SHA1 = "SHA1";
        private const string SHA256 = "SHA256";
        private const string SHA512 = "SHA512";
        private const string ENCRYPT = "encrypt";
        private const string DECRYPT = "decrypt";
        private const string PATH = "~/App_Data/ApiConfiguration/Crypto/";
        private const string SHARED_KEY_FILE = "SharedKey.dat";
        private const string SERVER_KEY_FILE = "ServerKey.dat";
        private const string PUBLIC_KEY_XML  = "MobilePublicKey.xml";
        private const string PRIVATE_KEY_XML = "MobilePrivateKey.xml";
        private string SecretKey;
        private string ServerKey;
        private string CertPublicKey;
        private string CertPrivateKey;

        #endregion

        #region Ctor
        public CryptographyService()
        {
        }
        #endregion

        #region Utilities

        /// <summary>
        /// Convert Base64 string into bytes and save into file.
        /// </summary>
        /// <param name="fileName">Filename</param>
        /// <param name="secret">Secret key to save into file</param>
        protected void CreateBufferFile(string fileName, string secret)
        {
            using (FileStream stream = new FileStream(fileName, FileMode.Create)) //FileMode.Create will overwrite the file if it exist!
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    byte[] buf = Convert.FromBase64String(secret);
                    writer.Write(buf);
                    writer.Close();
                }
                stream.Close();
            }
        }

        /// <summary>
        /// Read bytes from file and convert it into Base64 string.
        /// </summary>
        /// <param name="fileName">Filename</param>
        /// <returns>Secret key</returns>
        protected string ReadBufferFile(string fileName)
        {
            byte[] buffer;
            if (File.Exists(fileName))
            {
                using (BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read)))
                {
                    buffer = reader.ReadBytes(100); //try to read bytes up to 100 size of byte array
                    reader.Close();
                }
                if (buffer.Length > 0)
                    return Convert.ToBase64String(buffer);
            }
            return string.Empty;
        }

        protected string ReadXMLFile(string fileName)
        {
            if (File.Exists(fileName))
                return File.ReadAllText(fileName);

            return string.Empty;
        }

        private string ReplaceSpaceWithPlusSign(string str)
        {
            if ((str != null) && (str.IndexOf(' ') >= 0))
                str = str.Replace(' ', '+');

            return str;
        }

        private string ConvertByteToBase64String(byte[] input)
        {
            return Convert.ToBase64String(input);
        }

        private byte[] ConvertBase64StringToByte(string input)
        {
            return Convert.FromBase64String(input);
        }

        private string ConvertByteToHex(byte[] input)
        {
            return BitConverter.ToString(input).Replace("-", "").ToLower();
        }
        #endregion

        #region Methods

        /// <summary>
        /// Encrypt text with TripleDES/ECB/PKCS7Padding algorithm.
        /// </summary>
        /// <param name="plainText">Text to encrypt</param>
        /// <param name="data">Encrypted text</param>
        /// <returns>Encrypted text</returns>
        public virtual string TripleDesEncryptor(string plainText)
        {
            if (string.IsNullOrEmpty(SecretKey))
                SecretKey = ReadBufferFile(CommonHelper.MapPath($"{PATH}{SHARED_KEY_FILE}"));

            return TripleDESCrypto(plainText, Encoding.UTF8.GetBytes(SecretKey), ENCRYPT);
        }

        /// <summary>
        /// Decrypt text with TripleDES/ECB/PKCS7Padding algorithm.
        /// </summary>
        /// <param name="cipherText">Text to decrypt</param>
        /// <param name="data">Decrypted text</param>
        /// <returns>Decrypted text</returns>
        public virtual string TripleDesDecryptor(string cipherText)
        {
            if (string.IsNullOrEmpty(SecretKey))
                SecretKey = ReadBufferFile(CommonHelper.MapPath($"{PATH}{SHARED_KEY_FILE}"));

            return TripleDESCrypto(cipherText, Encoding.UTF8.GetBytes(SecretKey), DECRYPT);
        }

        /// <summary>
        /// Encrypt/Decrypt text with TripleDES/ECB/PKCS7Padding encryption engine. Encrypted/Decrypted text in "data" parameter.
        /// </summary>
        /// <param name="inputText">Text to encrypt/decrypt</param>
        /// <param name="secret">Encryption private key</param>
        /// <param name="data">Encrypted/Decrypted text</param>
        /// <param name="type">Type to justify the function for encryption or decryption</param>
        /// <returns>Encrypted/Decrypted text</returns>
        public virtual string TripleDESCrypto(string inputText, byte[] secret, string type)
        {
            ICryptoTransform cryptoEngine = null;
            byte[] ciphertext = null;
            byte[] results;
            string data = null;

            // 3DES key size 192 bits/24 bytes, allocate 24 bytes to keyArray.
            byte[] keyArray = new byte[24];
            Array.Copy(secret, keyArray, 24);
            
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider
            {
                //set the secret key for the tripleDES algorithm
                Key = keyArray,
                //mode of operation. there are other 4 modes. We choose ECB(Electronic code Book)
                Mode = CipherMode.ECB,
                //padding mode(if any extra byte added)
                Padding = PaddingMode.PKCS7
            };
            try
            {
                switch (type)
                {
                    case DECRYPT:
                        cryptoEngine = tdes.CreateDecryptor();
                        ciphertext = ConvertBase64StringToByte(inputText);
                        results = cryptoEngine.TransformFinalBlock(ciphertext, 0, ciphertext.Length);
                        data = Encoding.UTF8.GetString(results);
                        break;
                    case ENCRYPT:
                    default:
                        cryptoEngine = tdes.CreateEncryptor();
                        ciphertext = Encoding.UTF8.GetBytes(inputText);
                        results = cryptoEngine.TransformFinalBlock(ciphertext, 0, ciphertext.Length);
                        data = ConvertByteToBase64String(results);
                        break;
                }

                return data;
            }
            catch 
            {
                return data;
            }
            finally
            {
                tdes.Clear();
            }
        }

        /// <summary>
        /// Signature verification with public key using RSA SHA1 algorithm
        /// </summary>
        /// <param name="OriginalData">Original data</param>
        /// <param name="SignatureData">Data has been signed</param>
        /// <returns>true if the signature is valid; otherwise, false.</returns>
        public virtual bool VerifyRSADigitalSignatureSHA1(string OriginalData, string SignatureData)
        {
            RSACryptoServiceProvider RSAVerifier = new RSACryptoServiceProvider(2048);
            try
            {
                if(string.IsNullOrEmpty(CertPublicKey))
                    CertPublicKey = ReadXMLFile(CommonHelper.MapPath($"{PATH}{PUBLIC_KEY_XML}"));

                RSAVerifier.FromXmlString(CertPublicKey);
                byte[] signedData = ConvertBase64StringToByte(ReplaceSpaceWithPlusSign(SignatureData));
                
                return RSAVerifier.VerifyData(Encoding.UTF8.GetBytes(OriginalData), SHA1, signedData);
            }
            catch
            {
                return false;
            }
            finally
            {
                RSAVerifier.Clear();
            }
        }

        /// <summary>
        /// Hash and sign the data using RSA SHA1 algorithm 
        /// </summary>
        /// <param name="DataToSign">Original data</param>
        /// <returns>Signed data in Base64 string format</returns>
        public virtual string RSADigitalSignatureSHA1(string DataToSign)
        {
            byte[] dataToEncrypt = Encoding.UTF8.GetBytes(DataToSign);
            RSACryptoServiceProvider RSACrypto = new RSACryptoServiceProvider(2048);

            try
            {
                if (string.IsNullOrEmpty(CertPrivateKey))
                    CertPrivateKey = ReadXMLFile(CommonHelper.MapPath($"{PATH}{PRIVATE_KEY_XML}"));

                RSACrypto.FromXmlString(CertPrivateKey);
                return ConvertByteToBase64String(RSACrypto.SignData(dataToEncrypt, SHA1));
            }
            catch
            {
                return string.Empty;
            }
            finally
            {
                RSACrypto.Clear();
            }
        }

        /// <summary>
        /// Create signatures with Hash-based Message Authentication Code (HMAC) by using the SHA256 hash function.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public virtual string HMACSHA256Signature(string message)
        {
            return ConvertByteToHex(HMACSignatures(SHA256, message));
        }

        /// <summary>
        /// Verify signatures with Hash-based Message Authentication Code (HMAC) by using the SHA256 hash function.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="signatures"></param>
        /// <returns></returns>
        public virtual bool HMACSHA256Verify(string message, string signatures)
        {
            return HMACVerify(SHA256, message, signatures);
        }

        /// <summary>
        /// Create signatures with HMAC algorithm
        /// </summary>
        /// <param name="algorithm"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public virtual byte[] HMACSignatures(string algorithm, string message)
        {
            dynamic Hmac;
            if (string.IsNullOrEmpty(ServerKey))
                ServerKey = ReadBufferFile(CommonHelper.MapPath($"{PATH}{SERVER_KEY_FILE}"));

            byte[] key = ConvertBase64StringToByte(ServerKey); // secret key MUST in base64 string(Hashed string)

            switch (algorithm)
            {
                case SHA1:
                    Hmac = new HMACSHA1(key);
                    break;
                case SHA256:
                    Hmac = new HMACSHA256(key);
                    break;
                case SHA512:
                    Hmac = new HMACSHA512(key);
                    break;
                default:
                    throw new Exception("Invalid hashing algorithm");
            }

            byte[] result = Hmac.ComputeHash(Encoding.UTF8.GetBytes(message));
            Hmac.Clear();
            Hmac = null;

            return result;
        }

        /// <summary>
        /// Verify signature with HMAC algorithm
        /// </summary>
        /// <param name="algorithm"></param>
        /// <param name="message"></param>
        /// <param name="signatures"></param>
        /// <returns></returns>
        public virtual bool HMACVerify(string algorithm, string message, string signatures)
        {
            if (string.IsNullOrEmpty(ServerKey))
                ServerKey = ReadBufferFile(CommonHelper.MapPath($"{PATH}{SERVER_KEY_FILE}"));

            if (signatures == ConvertByteToHex(HMACSignatures(algorithm, message)))
                return true;

            return false;
        }

        #endregion

    }
}
