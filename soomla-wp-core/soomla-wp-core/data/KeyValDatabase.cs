using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Diagnostics;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
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

        private const String TAG = "KeyValDatabase"; //used for Log messages

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

        public void SetKeyVal(String Key, String Value)
        {
            KeyValue kv = new KeyValue()
            {
                Key = Key,
                Value = Value
            };

            dbConn.InsertOrReplace(kv);
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
    }

    public sealed class KeyValue
    {
        [PrimaryKey]
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
