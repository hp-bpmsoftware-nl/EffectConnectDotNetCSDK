
using System;
using System.IO;
using EffectConnectSDK.Call;
using EffectConnectSDK.Enum;
using EffectConnectSDK.Interface;
using EffectConnectSDK.Model;

namespace EffectConnectSDK
{
    internal class Core
    {
        public static void Main(string[] args)
        {
            Keyset keyset = new Keyset();
            keyset.publicKey = "PutYourSuppliedPublicKey";
            keyset.secretKey = "FillInYourOwnSecretKeyAsSupplied";
            
            ApiCall call = new ApiCall(keyset);
            
            Console.WriteLine(call.call(_testOrderlist()));
        }

        private static ApiCallInterface _testOrderlist()
        {
            string path = Path.Combine(Environment.CurrentDirectory, @"example", "orderlist_read.xml");   
            Payload payload = new Payload(File.ReadAllText(path));
            OrderlistCall orderlistCall = new OrderlistCall(payload);

            return orderlistCall;
        }
        
        private static ApiCallInterface _testProducts()
        {
            string path = Path.Combine(Environment.CurrentDirectory, @"example", "products_create.xml");   
            Payload payload = new Payload(File.OpenRead(path));
            ProductsCall productsCall = new ProductsCall(Endpoint.Update, payload);
            
            // In case of a V2 payload:
            // productsCall.SetVersion(Version.V2);
            
            return productsCall;
        }
        
        private static ApiCallInterface _testOrderlineUpdate()
        {
            string path = Path.Combine(Environment.CurrentDirectory, @"example", "orderline_update.xml");   
            Payload payload = new Payload(File.ReadAllText(path));
            OrderCall orderCall = new OrderCall(Endpoint.Update, payload);
            
            return orderCall;
        }
        
        private static ApiCallInterface _testOrderUpdate()
        {
            string path = Path.Combine(Environment.CurrentDirectory, @"example", "order_update.xml");   
            Payload payload = new Payload(File.ReadAllText(path));
            OrderCall orderCall = new OrderCall(Endpoint.Update, payload);
            
            return orderCall;
        }
    }
}