namespace Nop.Plugin.Api.Helpers
{
    using Microsoft.IdentityModel.Tokens;
    using Newtonsoft.Json;
    using System.IO;
    using System.Security.Cryptography;
    using Nop.Core;
    using System.Collections.Generic;

    public static class CryptoHelper
    {
        // Need to ensure that the key would be the same through the application lifetime.
        private static RsaSecurityKey _key;        
        private const string TokenSigningKeyFileName = "api-token-signing-key.json";
        //WilliamHo 20181008 AWS Public Key \/
        private static AWSKeys _keyParams;
        private const string AwsCognitoPublicKeyFileName = "aws-public-key.json";
        //WilliamHo 20181008 AWS Public Key /\

        public static RsaSecurityKey CreateRsaSecurityKey()
        {
            if (_key == null)
            {
                string pathToKey = CommonHelper.MapPath($"~/App_Data/{TokenSigningKeyFileName}");

                if (!File.Exists(pathToKey))
                {
                    // generate random parameters
                    var randomParameters = GetRandomParameters();

                    var rsaParams = new RSAParametersWithPrivate();
                    rsaParams.SetParameters(randomParameters);
                    string serializedParameters = JsonConvert.SerializeObject(rsaParams);

                    // create file and save the key
                    File.WriteAllText(pathToKey, serializedParameters);
                }

                // load the key
                if (!File.Exists(pathToKey))
                    throw new FileNotFoundException("Check configuration - cannot find auth key file: " + pathToKey);

                var keyParams = JsonConvert.DeserializeObject<RSAParametersWithPrivate>(File.ReadAllText(pathToKey));

                // create signing key by the key above
                _key = new RsaSecurityKey(keyParams.ToRSAParameters());
            }

            return _key;
        }

        public static RSAParameters GetRandomParameters()
        {       
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                try
                {
                    return rsa.ExportParameters(true);
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
        }

        // https://github.com/mrsheepuk/ASPNETSelfCreatedTokenAuthExample/blob/master/src/TokenAuthExampleWebApplication/RSAKeyUtils.cs
        private class RSAParametersWithPrivate
        {
            public byte[] D { get; set; }
            public byte[] DP { get; set; }
            public byte[] DQ { get; set; }
            public byte[] Exponent { get; set; }
            public byte[] InverseQ { get; set; }
            public byte[] Modulus { get; set; }
            public byte[] P { get; set; }
            public byte[] Q { get; set; }

            public void SetParameters(RSAParameters p)
            {
                D = p.D;
                DP = p.DP;
                DQ = p.DQ;
                Exponent = p.Exponent;
                InverseQ = p.InverseQ;
                Modulus = p.Modulus;
                P = p.P;
                Q = p.Q;
            }
            public RSAParameters ToRSAParameters()
            {
                return new RSAParameters()
                {
                    D = this.D,
                    DP = this.DP,
                    DQ = this.DQ,
                    Exponent = this.Exponent,
                    InverseQ = this.InverseQ,
                    Modulus = this.Modulus,
                    P = this.P,
                    Q = this.Q

                };
            }
        }

        //WilliamHo 20181008 AWS Public Key \/
        public class AWSKeys
        {
            public string client_id { get; set; }
            public string finsys_id { get; set; }
            public string issuer { get; set; }
            public IList<AWSPublicKey> keys { get; set; }
        }

        public class AWSPublicKey
        {
            /// <summary>
            /// KeyId
            /// </summary>
            public string kid { get; set; }
            /// <summary>
            /// Algorithm
            /// </summary>
            public string alg { get; set; }
            /// <summary>
            /// KeyType
            /// </summary>
            public string kty { get; set; }
            /// <summary>
            /// Exponent
            /// </summary>
            public string e { get; set; }
            /// <summary>
            /// Modulus
            /// </summary>
            public string n { get; set; }
            /// <summary>
            /// Use
            /// </summary>
            public string use { get; set; }
        }

        public static AWSKeys CreateAwsPublicKeys()
        {
            if (_keyParams == null)
            {
                string pathToKey = CommonHelper.MapPath($"~/App_Data/{AwsCognitoPublicKeyFileName}");
                if (!File.Exists(pathToKey))
                {
                    var awsParams = new AWSPublicKey();
                    string serializedParameters = JsonConvert.SerializeObject(awsParams);

                    // create file and save the key
                    File.WriteAllText(pathToKey, serializedParameters);
                }

                // load the key
                if (!File.Exists(pathToKey))
                    throw new FileNotFoundException("Check configuration - cannot find cognito public key file: " + pathToKey);

                //extract json file into AWSKeys model
                _keyParams = JsonConvert.DeserializeObject<AWSKeys>(File.ReadAllText(pathToKey));
            }
            return _keyParams;
        }
        //WilliamHo 20181008 AWS Public Key /\
    }
}