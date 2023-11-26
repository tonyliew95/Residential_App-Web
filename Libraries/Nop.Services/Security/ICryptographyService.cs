namespace Nop.Services.Security
{
    /// <summary>
    /// Cryptography service
    /// </summary>
    public interface ICryptographyService
    {
        /// <summary>
        /// Encrypt text with TripleDES/ECB/PKCS7Padding algorithm. 
        /// </summary>
        /// <param name="plainText">Text to encrypt</param>
        /// <param name="data">Encrypted text</param>
        /// <returns>Encrypted text</returns>
        string TripleDesEncryptor(string plainText);

        /// <summary>
        /// Decrypt text with TripleDES/ECB/PKCS7Padding algorithm. 
        /// </summary>
        /// <param name="cipherText">Text to decrypt</param>
        /// <param name="data">Decrypted text</param>
        /// <returns>Decrypted text</returns>
        string TripleDesDecryptor(string cipherText);

        /// <summary>
        /// Signature verification with public key using RSA SHA1 algorithm
        /// </summary>
        /// <param name="OriginalData">Original data</param>
        /// <param name="SignatureData">Data has been signed</param>
        /// <returns>true if the signature is valid; otherwise, false.</returns>
        bool VerifyRSADigitalSignatureSHA1(string OriginalData, string SignatureData);

        /// <summary>
        /// Hash and sign the data using RSA SHA1 algorithm 
        /// </summary>
        /// <param name="DataToSign">Original data</param>
        /// <returns>Signed data in Base64 string format</returns>
        string RSADigitalSignatureSHA1(string DataToSign);

        /// <summary>
        /// Create signatures with Hash-based Message Authentication Code (HMAC) by using the SHA256 hash function.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        string HMACSHA256Signature(string message);

        /// <summary>
        /// Verify signatures with Hash-based Message Authentication Code (HMAC) by using the SHA256 hash function.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="signatures"></param>
        /// <returns></returns>
        bool HMACSHA256Verify(string message, string signatures);
    }
}