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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using Sqlite;
using SQLite;
using Windows.Storage;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq.Expressions;

namespace SoomlaWpCore.data
{
    public class KeyValDatabase
    {
        /// <summary>
        /// The database path.
        /// </summary>
        public static string DB_PATH = Path.Combine(Path.Combine(ApplicationData.Current.LocalFolder.Path, "data.db"));

        private const String TAG = "SOOMLA KeyValDatabase"; //used for Log messages

        /// <summary>
        /// The sqlite connection.
        /// </summary>
        private static SQLiteConnection dbConn;

        public KeyValDatabase()
        {
            /// Create the database connection.
            if (dbConn == null)
            {
                dbConn = new SQLiteConnection(DB_PATH);
                dbConn.BusyTimeout = new TimeSpan(0, 0, 1);
            }
            
            
            if (SoomlaConfig.DB_DELETE)
            {
                dbConn.DropTable<KeyValue>();
            }
            
            /// Create the table Task, if it doesn't exist.
            try
            {
                dbConn.CreateTable<KeyValue>();
            }
            catch (Exception e)
            {
                SoomlaUtils.LogDebug(TAG, e.Message);
            }
            
            /*
            //For testing
            List<KeyValue> retrievedKeyValue = dbConn.Table<KeyValue>().ToList<KeyValue>();
            SoomlaUtils.LogDebug(TAG,"DB content");
            foreach (KeyValue t in retrievedKeyValue)
            {
                SoomlaUtils.LogDebug(TAG,t.Key + " | " + t.Value);
            }
             */
        }

        public void PurgeDatabse()
        {
            dbConn.Dispose();
            IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication();
            storage.DeleteFile(DB_PATH);
        }

        public void PurgeDatabaseEntries()
        {
            dbConn.DeleteAll<KeyValue>();
        }

        public async void SetKeyVal(String Key, String Value)
        {
            KeyValue kv = new KeyValue()
            {
                Key = Key,
                Value = Value
            };
            await InsertOrReplace(kv);
        }

        private Task<int> InsertOrReplace(KeyValue kv)
        {
            return System.Threading.Tasks.Task.Factory.StartNew(() => dbConn.InsertOrReplace(kv)); ;
        }

        public String GetKeyVal(String Key)
        {
            KeyValue findKv = null;
            try
            {
                findKv = dbConn.Find<KeyValue>(Key);
            }
            catch (Exception e)
            {
                SoomlaUtils.LogDebug(TAG, e.Message);
            }
            if (findKv != null)
            {
                return findKv.Value;
            }
            else
            {
                return null;
            }
        }

        public async Task<int> DeleteKeyVal(String Key)
        {
            return await Delete(Key);
        }

        private Task<int> Delete(String Key)
        {
            Task<int> ret = null;
            try
            {
                ret = System.Threading.Tasks.Task.Factory.StartNew(() => dbConn.Delete<KeyValue>(Key));
            }
            catch (Exception e)
            {
                SoomlaUtils.LogDebug(TAG, e.Message);
            }
            
            //SoomlaUtils.LogDebug(TAG, "After Deleted");
            return ret;
        }

        public List<KeyValue> GetQueryVals(String query) {
            query = query.Replace('*', '%');
            return dbConn.Query<KeyValue>("select * from KeyValue where Key LIKE ?",query);
        }

        public string GetQueryOne(String query)
        {
            List<KeyValue> kvlist = GetQueryVals(query);
            if (kvlist.Count > 0)
            {
                return kvlist[0].Value;
            }
            else
            {
                return null;
            }
        }

        public int GetQueryCount(String query)
        {
            query = query.Replace('*', '%');
            int count = dbConn.ExecuteScalar<int>("select COUNT(*) from KeyValue where Key LIKE ?", query);
            return count;
        }

        /// <summary>
        /// Get all Keys from KeyValue without the Values
        /// </summary>
        /// <returns></returns>
        public List<KeyValue> GetAllKeys()
        {
            return dbConn.Query<KeyValue>("select Key from KeyValue");
        }
    }

    public sealed class KeyValue
    {
        [PrimaryKey]
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
