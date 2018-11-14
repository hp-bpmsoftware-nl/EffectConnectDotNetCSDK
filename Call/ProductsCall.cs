
using EffectConnectSDK.Enum;
using EffectConnectSDK.Exception;
using EffectConnectSDK.Interface;
using EffectConnectSDK.Model;

namespace EffectConnectSDK.Call
{
    public class ProductsCall: ApiCall, ApiCallInterface
    {
        public ProductsCall(Endpoint endpoint, Payload payload) : base(endpoint, payload)
        {
        }

        protected override void _PrepareCall()
        {
            switch (_endpoint)
            {
                case Endpoint.Create:
                    method = Method.POST;
                    if (GetPayload().GetType() != PayloadType.File)
                    {
                        throw new InvalidPayloadException("string", "file");
                    }
                    break;
                case Endpoint.Update:
                    method = Method.PUT;
                    if (GetPayload().GetType() != PayloadType.File)
                    {
                       throw new InvalidPayloadException("string", "file");
                    }
                    break;
            }
        }

        public string GetUri()
        {
            return "/products";
        }

        protected override bool _ValidateEndpoint(Endpoint endpoint)
        {
            switch (endpoint)
            {
                case Endpoint.Create:
                case Endpoint.Update:
                    return true;
                default:
                    return false;
            }
        }
    }
}