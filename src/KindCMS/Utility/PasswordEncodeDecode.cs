using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace KindCMS.Utility
{
    public class AuthEncodeDecode:ITextEncodeDecode
    {

        public AuthEncodeDecode()
        {
            this.KeyHash = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(key));
            this.Des = TripleDES.Create();
            Des.Key = KeyHash;
            Des.IV = new byte[Des.BlockSize / 8];
        }

        private TripleDES Des { get; set; }

        public const string key = "PasswordOfKindCMSAuthPasswordEncodeDecode";

        private byte[] KeyHash { get; set; }

        
        public string Encode(string Source)
        {
            byte[] SourceBytes = Encoding.UTF8.GetBytes(Source);
            MemoryStream MemoStream = new MemoryStream();
            CryptoStream CryptoStream = new CryptoStream(MemoStream, Des.CreateEncryptor(), CryptoStreamMode.Write);
            CryptoStream.Write(SourceBytes, 0, SourceBytes.Length);
            CryptoStream.FlushFinalBlock();
            return Convert.ToBase64String(MemoStream.ToArray());

        }

        public string Decode(string Source)
        {
            try
            {
                byte[] SourceBytes = Convert.FromBase64String(Source);
                MemoryStream MemoStream = new MemoryStream();
                CryptoStream CryptoStream = new CryptoStream(MemoStream, Des.CreateDecryptor(), CryptoStreamMode.Write);
                CryptoStream.Write(SourceBytes, 0, SourceBytes.Length);
                CryptoStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(MemoStream.ToArray());
            }
            catch
            {
                return string.Empty;
            }
        }
    }

    public interface ITextEncodeDecode
    {
        string Encode(string Source);
        string Decode(string Source);
    }
}
