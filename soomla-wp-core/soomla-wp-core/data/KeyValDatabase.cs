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
        private SQLiteConnection dbConn;

        public KeyValDatabase()
        {
            /// Create the database connection.
            dbConn = new SQLiteConnection(DB_PATH);

            if (SoomlaConfig.DB_DELETE)
            {
                dbConn.DropTable<KeyValue>();
            }

            /// Create the table Task, if it doesn't exist.
            dbConn.CreateTable<KeyValue>();
            /// Retrieve the task list from the database.
            List<KeyValue> retrievedKeyValue = dbConn.Table<KeyValue>().ToList<KeyValue>();
            /// Clear the list box that will show all the tasks.

            SoomlaUtils.LogDebug(TAG,"DB content");
            foreach (KeyValue t in retrievedKeyValue)
            {
                SoomlaUtils.LogDebug(TAG,t.Key + " | " + t.Value);
            }
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
            KeyValue findKv = dbConn.Find<KeyValue>(Key);
            if (findKv != null)
            {
                return findKv.Value;
            }
            else
            {
                return null;
            }
        }

        public async void DeleteKeyVal(String Key)
        {
            await Delete(Key);
        }

        private Task<int> Delete(String Key)
        {
            Task<int> ret = System.Threading.Tasks.Task.Factory.StartNew(() => dbConn.Delete<KeyValue>(Key));
            //SoomlaUtils.LogDebug(TAG, "After Deleted");
            return ret;
        }
    }

    public sealed class KeyValue
    {
        [PrimaryKey]
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
