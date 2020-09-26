using CommentoIntegrationTest.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Security.Cryptography;
using System.Text;

namespace CommentoIntegrationTest.Middleware
{
    public class CreateUrlForSSO: ICreateUrlForSSO
    {
        private readonly IConfiguration _configuration;

        public CreateUrlForSSO(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string DoOperations(ApplicationUser user, string token, string hmac)
        {
            //TODO: Add checks for right value
            int secretDecoded = SecretKey();
            int tokenDecoded = Decode(token);
            int hmacDecoded = Decode(hmac);

            var expectedHmac = Int32.Parse(HashHMAC(BitConverter.GetBytes(tokenDecoded), BitConverter.GetBytes(secretDecoded)).ToString());

            if (hmacDecoded != expectedHmac)
            {
                //TODO: change return string
                return "";
            }

            JsonModel jsonModel = new JsonModel
            {
                Token = token,
                Email = user.Email,
                Name = user.UserName
            };

            string jsonPayload = JsonConvert.SerializeObject(jsonModel);
            var xx = Encoding.ASCII.GetBytes(jsonPayload);
            var newHmac = Encode(HashHMAC(xx, BitConverter.GetBytes(secretDecoded)).ToString());
            var payloadHex = Encode(jsonPayload);


            string url = CreateUrl(newHmac, payloadHex);

            return url;
        }

        private static string CreateUrl(string newHmac, string payloadHex)
        {
            return "https://commento.io/api/oauth/sso/callback?payload=" + payloadHex + "&hmac=" + newHmac;
        }

        private int SecretKey()
        {
            string secret = _configuration.GetValue<string>("SecretKeySSO:secret");

            return  Decode(secret);
        }

        private int Decode(string toDecode)
        => Int32.Parse(toDecode, System.Globalization.NumberStyles.HexNumber);

        private string Encode(string toEncode)
        {
            byte[] byteArr = Encoding.Default.GetBytes(toEncode);
            return BitConverter.ToString(byteArr).Replace("-", "");
        }

        private byte[] HashHMAC(byte[] decodedToken, byte[] secretKey)
        {
            var hash = new HMACSHA256(secretKey);
            return hash.ComputeHash(decodedToken);
        }
    }
}
