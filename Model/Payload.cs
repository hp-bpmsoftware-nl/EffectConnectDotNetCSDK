using System.IO;
using EffectConnectSDK.Enum;

namespace EffectConnectSDK.Model
{
    public class Payload
    {
        private long _size;
        private PayloadType _type;
        private string _strContent;
        private FileStream _fileContent;

        public Payload(string content)
        {
            _strContent = content;
            _type = PayloadType.String;
            _size = content.Length;
        }

        public Payload(FileStream file)
        {
            _fileContent = file;
            _type = PayloadType.File;
            _size = file.Length;
        }

        public long GetSize()
        {
            return _size;
        }

        public PayloadType GetType()
        {
            return _type;
        }

        public string GetStrContent()
        {
            return _strContent;
        }

        public FileStream GetFileContent()
        {
            return _fileContent;
        }
    }
}