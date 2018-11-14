using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using EffectConnectSDK.Enum;
using EffectConnectSDK.Exception;
using EffectConnectSDK.Interface;
using EffectConnectSDK.Model;
using Version = EffectConnectSDK.Enum.Version;

namespace EffectConnectSDK
{
    public class ApiCall
    {
        private const string _endpoint = "https://submit.effectconnect.com";
        private Keyset _keyset;
        
        public ApiCall(Keyset keyset)
        {
            _keyset = keyset;
        }

        public string call(ApiCallInterface apiCall)
        {
            HttpClient client = new HttpClient();
            DateTime callTime = DateTime.Now;
            string version;
            switch (apiCall.GetVersion())
            {
                case Version.V1:
                    version = "1.0";
                    break;
                case Version.V2:
                    version = "2.0";
                    break;
                default:
                    throw new InvalidVersionException();
            }
            client.DefaultRequestHeaders.Add("KEY", _keyset.publicKey); 
            client.DefaultRequestHeaders.Add("VERSION", version); 
            client.DefaultRequestHeaders.Add("URI", apiCall.GetUri()); 
            client.DefaultRequestHeaders.Add("RESPONSETYPE", "XML"); 
            client.DefaultRequestHeaders.Add("RESPONSELANGUAGE", "en"); 
            client.DefaultRequestHeaders.Add("TIME", String.Format("{0}T{1}", callTime.ToString("yyyy-MM-dd"), callTime.ToString("HH:mm:ssK")));
            client.DefaultRequestHeaders.Add("SIGNATURE", _signEffectConnectApiCall(
                apiCall.GetPayload().GetSize(),
                apiCall.GetMethod(),
                apiCall.GetUri(),
                apiCall.GetVersion(),
                callTime,
                _keyset.secretKey
            ));
            HttpResponseMessage response;
            switch (apiCall.GetMethod())
            {
                case Method.POST:
                    if (apiCall.GetPayload().GetType() == PayloadType.File)
                    {
                        response = client.PostAsync(_endpoint+apiCall.GetUri(), _createMultipartRequest(apiCall.GetPayload())).Result;
                    }
                    else
                    {
                        response = client.PostAsync(_endpoint+apiCall.GetUri(), _createRequest(apiCall.GetPayload())).Result;
                    }
                    break;
                case Method.PUT:
                    if (apiCall.GetPayload().GetType() == PayloadType.File)
                    {
                        response = client.PutAsync(_endpoint+apiCall.GetUri(), _createMultipartRequest(apiCall.GetPayload())).Result;
                    }
                    else
                    {
                        response = client.PutAsync(_endpoint+apiCall.GetUri(), _createRequest(apiCall.GetPayload())).Result;
                    }
                    break;
                default:
                    return "";
            }
            return response.Content.ReadAsStringAsync().Result;
        }

        private StringContent _createRequest(Payload payload)
        {
            return new StringContent(payload.GetStrContent());
        }
        
        private MultipartFormDataContent _createMultipartRequest(Payload payload)
        {
            MultipartFormDataContent request = new MultipartFormDataContent();
            request.Headers.ContentType = new MediaTypeHeaderValue("multipart/form-data");
            request.Add(new StreamContent(payload.GetFileContent()));

            return request;
        }
        
        private string _signEffectConnectApiCall(long size, Method method, string uri, Version version, DateTime date, string secret)
        {
            StringBuilder nonce = new StringBuilder(size.ToString());
            switch (method)
            {
                case Method.GET:
                    nonce.Append("GET");
                    break;
                case Method.PUT:
                    nonce.Append("PUT");
                    break;
                case Method.POST:
                    nonce.Append("POST");
                    break;
            }
            nonce.Append(uri);
            switch (version)
            {
                case Version.V1:
                    nonce.Append("1.0");
                    break;
                case Version.V2:
                    nonce.Append("2.0");
                    break;
            }
            nonce.Append(String.Format("{0}T{1}", date.ToString("yyyy-MM-dd"), date.ToString("HH:mm:ssK")));
            HMACSHA512 hmac = new HMACSHA512(Encoding.UTF8.GetBytes(secret));
            byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(nonce.ToString()));
    
            return Convert.ToBase64String(hash);
        }
    }
}