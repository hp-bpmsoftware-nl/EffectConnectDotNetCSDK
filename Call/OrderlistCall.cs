
using EffectConnectSDK.Enum;
using EffectConnectSDK.Interface;
using EffectConnectSDK.Model;

namespace EffectConnectSDK.Call
{
    public class OrderlistCall: ApiCall, ApiCallInterface
    {
        public OrderlistCall(Payload payload) : base(Endpoint.Read, payload)
        {
            
        }

        protected override void _PrepareCall()
        {
            SetVersion(Version.V2);
            switch (_endpoint)
            {
                case Endpoint.Read:
                    method = Method.POST;
                    break;
            }
        }

        public string GetUri()
        {
            return "/orderlist";
        }

        protected override bool _ValidateEndpoint(Endpoint endpoint)
        {
            switch (endpoint)
            {
                case Endpoint.Read:
                    return true;
                default:
                    return false;
            }
        }
    }
}