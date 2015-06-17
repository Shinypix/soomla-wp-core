/// Copyright (C) 2012-2014 Soomla Inc.
///
/// Licensed under the Apache License, Version 2.0 (the "License");
/// you may not use this file except in compliance with the License.
/// You may obtain a copy of the License at
///
///      http://www.apache.org/licenses/LICENSE-2.0
///
/// Unless required by applicable law or agreed to in writing, software
/// distributed under the License is distributed on an "AS IS" BASIS,
/// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
/// See the License for the specific language governing permissions and
/// limitations under the License.
/// 
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

        public static String GetValue(String Key, bool EncryptedKey = true)
        {
            if (GetCache().Keys.Contains(Key))
            {
                return GetCache()[Key];
            }

            String clearKey = Key;
            if (EncryptedKey)
            {
                Key = AESObfuscator.ObfuscateString(Key);
            }

            string encryptedValue = GetDatabase().GetKeyVal(Key);
            if (encryptedValue == null)
            {
                return null;
            }
            string decryptedValue = AESObfuscator.UnObfuscateString(encryptedValue);

            GetCache()[clearKey] = decryptedValue;
            //SoomlaUtils.LogDebug(TAG, "Get ## Clear Key:" + Key + " Encrypted Key:" + encryptedKey + " Encrypted Value:" + encryptedValue + " Clear Value:" + decryptedValue);
            return decryptedValue;
        }

        public static String GetNonEncryptedKeyValue(String Key)
        {
            return GetValue(Key, false);
        }

        public static List<KeyValue> GetNonEncryptedQueryValues(String query)
        {
            SoomlaUtils.LogDebug(TAG, "trying to fetch values for query: " + query);
            List<KeyValue> results;

            results = GetDatabase().GetQueryVals(query);

            foreach (KeyValue kv in results)
            {
                kv.Value = AESObfuscator.UnObfuscateString(kv.Value);
            }

            SoomlaUtils.LogDebug(TAG, "fetched " + results.Count + " results");
            return results;
        }

        public static String GetOneNonEncryptedQueryValues(String query)
        {
            SoomlaUtils.LogDebug(TAG, "trying to fetch one for query: " + query);
            string result = GetDatabase().GetQueryOne(query);
            if (result == null)
            {
                return result;
            }
            return AESObfuscator.UnObfuscateString(result);
        }

        public static int GetCountNonEncryptedQueryValues(String query)
        {
            SoomlaUtils.LogDebug(TAG, "trying to fetch count for query: " + query);
            return GetDatabase().GetQueryCount(query);
        }

        /// <summary>
        /// Gets all KeyValue keys in the storage with no encryption
        /// </summary>
        /// <returns></returns>
        public static List<KeyValue> GetEncryptedKeys()
        {
            SoomlaUtils.LogDebug(TAG, "trying to fetch all keys");
            List<KeyValue> results = GetDatabase().GetAllKeys();
            foreach (KeyValue kv in results)
            {
                kv.Key = AESObfuscator.UnObfuscateString(kv.Key);
            }
            return results;
        }

        public static void SetValue(String Key, String Value, bool EncryptedKey = true)
        {
            GetCache()[Key] = Value;
            Task task = new Task(() => SetValueAsync(Key, Value, EncryptedKey));
            task.Start();
            task.Wait();
            //SoomlaUtils.LogDebug(TAG, "SetValue End");
        }

        public static void SetNonEncryptedKeyValue(String Key, String Value)
        {
            SetValue(Key, Value, false);
        }


        public static void SetValueAsync(String Key, String Value, bool EncryptedKey = true)
        {
            if (EncryptedKey)
            {
                Key = AESObfuscator.ObfuscateString(Key);
            }
            string encryptedValue = AESObfuscator.ObfuscateString(Value);
            //SoomlaUtils.LogDebug(TAG, "Set ## Clear Key:" + Key + " Encrypted Key:" + encryptedKey + " Encrypted Value:" + encryptedValue + " Clear Value:" + Value);
            GetDatabase().SetKeyVal(Key, encryptedValue);

            //string decryptedVal = AESObfuscator.UnObfuscateString(encryptedValue);
            //SoomlaUtils.LogDebug(TAG, "SetValueAsync End");
        }

        public static void DeleteKeyValue(String key, bool EncryptedKey = true)
        {
            if (GetCache().Keys.Contains(key))
            {
                GetCache().Remove(key);    
            }

            Task task = new Task(() => DeleteKeyValueAsync(key, EncryptedKey));
            task.Start();
            task.Wait();
            //SoomlaUtils.LogDebug(TAG, "DeleteKeyValue End");
        }

        private async static void DeleteKeyValueAsync(String Key, bool EncryptedKey = true)
        {
            if (EncryptedKey)
            {
                Key = AESObfuscator.ObfuscateString(Key);
            }

            int result = await GetDatabase().DeleteKeyVal(Key);
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
