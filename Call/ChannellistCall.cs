
using EffectConnectSDK.Enum;
using EffectConnectSDK.Interface;
using EffectConnectSDK.Model;

namespace EffectConnectSDK.Call
{
    public class ChannellistCall: ApiCall, ApiCallInterface
    {
        public ChannellistCall(Payload payload) : base(Endpoint.Read, payload)
        {
        }

        public string GetUri()
        {
            return "/channellist";
        }

        protected override void _PrepareCall()
        {
            SetVersion(Version.V2);
            switch (_endpoint)
            {
                case Endpoint.Read:
                    method = Method.GET;
                    break;
            }
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
