using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoomlaWpCore.util;
using System.Diagnostics;

namespace SoomlaWpCore.data
{
    public class KeyValueStorage
    {
        private static KeyValDatabase Kvd;
        private const String TAG = "SOOMLA KeyValueStorage"; //used for Log Messages

        public KeyValueStorage()
        {
            
        }
        
        public static String GetValue(String Key)
        {
            string encryptedKey = AESObfuscator.ObfuscateString(Key);
           
            string encryptedValue = GetDatabase().GetKeyVal(encryptedKey);
            if (encryptedValue == null)
            {
                return null;
            }
            string decryptedValue = AESObfuscator.UnObfuscateString(encryptedValue);
            SoomlaUtils.LogDebug(TAG, "Get ## Clear Key:" + Key + " Encrypted Key:" + encryptedKey + " Encrypted Value:" + encryptedValue + " Clear Value:" + decryptedValue);
            return decryptedValue;
        }

        public static void SetValue(String Key, String Value)
        {
            string encryptedKey = AESObfuscator.ObfuscateString(Key);
            string encryptedValue = AESObfuscator.ObfuscateString(Value);
            SoomlaUtils.LogDebug(TAG, "Set ## Clear Key:" + Key + " Encrypted Key:" + encryptedKey + " Encrypted Value:" + encryptedValue + " Clear Value:" + Value);
            GetDatabase().SetKeyVal(encryptedKey, encryptedValue);

            string decryptedVal = AESObfuscator.UnObfuscateString(encryptedValue);
            SoomlaUtils.LogDebug(TAG, "Set ### Direct uncrypt: " + decryptedVal);
        }

        public static void DeleteKeyValue(String key)
        {
            string encryptedKey = AESObfuscator.ObfuscateString(key);
            GetDatabase().DeleteKeyVal(encryptedKey);
        }

        public static KeyValDatabase GetDatabase()
        {
            if (Kvd == null)
            {
                Kvd = new KeyValDatabase();
            }
            return Kvd;
        }

    }
}
