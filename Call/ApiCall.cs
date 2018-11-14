using EffectConnectSDK.Enum;
using EffectConnectSDK.Exception;
using EffectConnectSDK.Model;

namespace EffectConnectSDK.Call
{
    public abstract class ApiCall
    {
        protected Method method = Method.GET;
        protected Endpoint _endpoint;
        private Payload _payload;
        private Version _version = Version.V1;
        
        public ApiCall(Endpoint endpoint, Payload payload)
        {
            if (_ValidateEndpoint(endpoint))
            {
                _endpoint = endpoint;
                _payload = payload;
                _PrepareCall();
            }
            else
            {
                throw new InvalidEndpointException(this.GetType().ToString());
            }
        }

        public Payload GetPayload()
        {
            return _payload;
        }
        
        public Method GetMethod()
        {
            return method;
        }
        
        public void SetVersion(Version version)
        {
            _version = version;
        }

        public Version GetVersion()
        {
            return _version;
        }

        protected abstract void _PrepareCall();
        
        protected abstract bool _ValidateEndpoint(Endpoint endpoint);
    }
}