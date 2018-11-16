
using EffectConnectSDK.Enum;
using EffectConnectSDK.Interface;
using EffectConnectSDK.Model;

namespace EffectConnectSDK.Call
{
    public class OrderCall: ApiCall, ApiCallInterface
    {
        public OrderCall(Endpoint endpoint, Payload payload) : base(endpoint, payload)
        {
            
        }

        protected override void _PrepareCall()
        {
            SetVersion(Version.V2);
            switch (_endpoint)
            {
                case Endpoint.Update:
                    method = Method.PUT;
                    break;
            }
        }

        public string GetUri()
        {
            return "/orders";
        }

        protected override bool _ValidateEndpoint(Endpoint endpoint)
        {
            switch (endpoint)
            {
                case Endpoint.Update:
                    return true;
                default:
                    return false;
            }
        }
    }
}