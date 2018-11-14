using System;

namespace EffectConnectSDK.Exception
{
    public class InvalidEndpointException: System.Exception
    {
        public InvalidEndpointException(string callType): base(String.Format("Invalid endpoint for calltype `{0}`", callType))
        {
            
        }
    }
}