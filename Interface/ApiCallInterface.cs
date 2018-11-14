
using EffectConnectSDK.Enum;
using EffectConnectSDK.Model;

namespace EffectConnectSDK.Interface
{
    public interface ApiCallInterface
    {
        string GetUri();
        
        Method GetMethod();

        void SetVersion(Version version);

        Version GetVersion();

        Payload GetPayload();
    }
}