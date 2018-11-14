using System;

namespace EffectConnectSDK.Exception
{
    public class InvalidPayloadException: System.Exception
    {
        public InvalidPayloadException(string given, string expected) : base(String.Format("Invalid payload. Expected was `{0}`. Received `{1}`", expected, given))
        {
        }
    }
}