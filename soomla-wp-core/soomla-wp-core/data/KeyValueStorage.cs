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
        private static Dictionary<string,string> cache;

        public KeyValueStorage()
        {
        }

        public static Dictionary<string, string> GetCache()
        {
            if (cache == null)
            {
                cache = new Dictionary<string, string>();
            }
            return cache;
        }
        
        public static String GetValue(String Key)
        {
            if (GetCache().Keys.Contains(Key))
            {
                return GetCache()[Key];
            }
            
            string encryptedKey = AESObfuscator.ObfuscateString(Key);
           
            string encryptedValue = GetDatabase().GetKeyVal(encryptedKey);
            if (encryptedValue == null)
            {
                return null;
            }
            string decryptedValue = AESObfuscator.UnObfuscateString(encryptedValue);

            GetCache()[Key] = decryptedValue;
            //SoomlaUtils.LogDebug(TAG, "Get ## Clear Key:" + Key + " Encrypted Key:" + encryptedKey + " Encrypted Value:" + encryptedValue + " Clear Value:" + decryptedValue);
            return decryptedValue;
        }

        public static void SetValue(String Key, String Value)
        {
            GetCache()[Key] = Value;
            /*
            string encryptedKey = AESObfuscator.ObfuscateString(Key);
            string encryptedValue = AESObfuscator.ObfuscateString(Value);
            //SoomlaUtils.LogDebug(TAG, "Set ## Clear Key:" + Key + " Encrypted Key:" + encryptedKey + " Encrypted Value:" + encryptedValue + " Clear Value:" + Value);
            GetDatabase().SetKeyVal(encryptedKey, encryptedValue);

            string decryptedVal = AESObfuscator.UnObfuscateString(encryptedValue);*/
            //SoomlaUtils.LogDebug(TAG, "Set ### Direct uncrypt: " + decryptedVal);
            Task task = new Task(() => SetValueAsync(Key,Value));
            task.Start();
            //SoomlaUtils.LogDebug(TAG, "SetValue End");
        }

        private static void SetValueAsync(String Key, String Value)
        {
            string encryptedKey = AESObfuscator.ObfuscateString(Key);
            string encryptedValue = AESObfuscator.ObfuscateString(Value);
            //SoomlaUtils.LogDebug(TAG, "Set ## Clear Key:" + Key + " Encrypted Key:" + encryptedKey + " Encrypted Value:" + encryptedValue + " Clear Value:" + Value);
            GetDatabase().SetKeyVal(encryptedKey, encryptedValue);

            string decryptedVal = AESObfuscator.UnObfuscateString(encryptedValue);
            //SoomlaUtils.LogDebug(TAG, "SetValueAsync End");
        }

        public static void DeleteKeyValue(String key)
        {
            if (GetCache().Keys.Contains(key))
            {
                GetCache().Remove(key);    
            }

            Task task = new Task(() => DeleteKeyValueAsync(key));
            task.Start();
            //SoomlaUtils.LogDebug(TAG, "DeleteKeyValue End");
        }

        private static void DeleteKeyValueAsync(String Key)
        {
            string encryptedKey = AESObfuscator.ObfuscateString(Key);
            GetDatabase().DeleteKeyVal(encryptedKey);
            //SoomlaUtils.LogDebug(TAG, "DeleteKeyValueAsync End");
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
