using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace NopCommerce.Api.AdapterLibrary
{
    public class ApiAuthorizer
    {
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly string _serverUrl;

		// Authorization Code Grant Flow with PKCE
		private static string _verifier;

		public ApiAuthorizer(string clientId, string clientSecret, string serverUrl)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
            _serverUrl = serverUrl;
		}

		public string GetAuthorizationUrl(string redirectUrl, string[] scope, string state = null)
        {
			//https://YOUR_AUTH0_DOMAIN/authorize?
			//scope = appointments % 20contacts
			//	& audience = appointments:api
			//	   & response_type = code
			//	   & client_id = YOUR_CLIENT_ID
			//	   & code_challenge = E9Melhoa2OwvFrEMTJguCHaoeK1t8URWbuGJSstw-cM
			//	   & code_challenge_method = S256
			//	   & redirect_uri = com.myclientapp://myclientapp.com/callback

			var stringBuilder = new StringBuilder();

            stringBuilder.AppendFormat("{0}/oauth/authorize", _serverUrl);
            stringBuilder.AppendFormat("?client_id={0}", HttpUtility.UrlEncode(_clientId));
            stringBuilder.AppendFormat("&redirect_uri={0}", HttpUtility.UrlEncode(redirectUrl));

			// Execute an Authorization Code Grant Flow with PKCE
			var buffer = new byte[32];

			var rng = new RNGCryptoServiceProvider();
			rng.GetBytes(buffer);

			var verifier = Convert.ToBase64String(buffer)
								  .Replace('+', '-')
								  .Replace('/', '_')
								  .Replace("=", "");

			_verifier = verifier;

			var sha = new SHA256Managed();
			sha.ComputeHash(Encoding.UTF8.GetBytes(_verifier));

			string challenge = Convert.ToBase64String(sha.Hash)
							   .Replace('+', '-')
							   .Replace('/', '_')
							   .Replace("=", "");

			//stringBuilder.AppendFormat("&scope={0}", "openid");
			//stringBuilder.AppendFormat("&audience={0}", "appointments:api");
			stringBuilder.AppendFormat("&code_challenge={0}", challenge);
			stringBuilder.AppendFormat("&code_challenge_method={0}", "S256");
			// Execute an Authorization Code Grant Flow with PKCE

			stringBuilder.Append("&response_type=code");

            if (!string.IsNullOrEmpty(state))
            {
                stringBuilder.AppendFormat("&state={0}", state);
            }

            if (scope != null && scope.Length > 0)
            {
                string scopeJoined = string.Join(",", scope);
                stringBuilder.AppendFormat("&scope={0}", HttpUtility.UrlEncode(scopeJoined));
            }

            return stringBuilder.ToString();
        }

		public string AuthorizeClient(string code, string grantType, string redirectUrl)
        {
            string requestUriString = string.Format("{0}/api/token", _serverUrl);

			//string queryParameters = string.Format("client_id={0}&client_secret={1}&code={2}&grant_type={3}&redirect_uri={4}", _clientId, _clientSecret, code, grantType, redirectUrl);

			// {
			//   "grant_type":"authorization_code",
			//   "client_id": "Ui9T4mAy8TR6DBor5RnxiseaLdXHGIVh",
			//   "code_verifier": "YOUR_GENERATED_CODE_VERIFIER",
			//   "code": "YOUR_AUTHORIZATION_CODE",
			//   "redirect_uri": "com.myclientapp://myclientapp.com/callback", 
			// }

			// Exchange the Authorization Code for an Access Token
			string queryParameters = string.Format(
				"client_id={0}" +
				"&grant_type={1}" +
				"&code_verifier={2}" +
				"&code={3}" +
				"&redirect_uri={4}", 
				_clientId,
				grantType,
				_verifier,
				code,
				redirectUrl
			);

			var httpWebRequest = (HttpWebRequest)WebRequest.Create(requestUriString);
            httpWebRequest.Method = "POST";
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";

            using (new MemoryStream())
            {
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(queryParameters);
                    streamWriter.Close();
                }
            }

            var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            string json = string.Empty;

            using (Stream responseStream = httpWebResponse.GetResponseStream())
            {
                if (responseStream != null)
                {
                    var streamReader = new StreamReader(responseStream);
                    json = streamReader.ReadToEnd();
                    streamReader.Close();
                }
            }

            return json;
        }

        public string RefreshToken(string refreshToken, string grantType)
        {
            string requestUriString = string.Format("{0}/api/token", _serverUrl);

            string queryParameters = string.Format("client_id={0}&client_secret={1}&grant_type={2}&refresh_token={3}", _clientId, _clientSecret, grantType, refreshToken);

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(requestUriString);
            httpWebRequest.Method = "POST";
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";

            using (new MemoryStream())
            {
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(queryParameters);
                    streamWriter.Close();
                }
            }

            var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            string json = string.Empty;

            using (Stream responseStream = httpWebResponse.GetResponseStream())
            {
                if (responseStream != null)
                {
                    var streamReader = new StreamReader(responseStream);
                    json = streamReader.ReadToEnd();
                    streamReader.Close();
                }
            }

            return json;
        }
    }
}