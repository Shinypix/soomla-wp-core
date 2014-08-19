using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using SoomlaWpCore.data;

namespace SoomlaWpCore
{
    public abstract class SoomlaEntity
    {
        public SoomlaEntity(String Name, String Description, String ID)
        {
            mName = Name;
            mDescription = Description;
            mID = ID.Trim();
        }

        public SoomlaEntity(JObject jsonObject) {
            mName = jsonObject.Value<string>(JSONConsts.SOOM_ENTITY_NAME);
            mDescription = jsonObject.Value<string>(JSONConsts.SOOM_ENTITY_DESCRIPTION);
            mID = jsonObject.Value<string>(JSONConsts.SOOM_ENTITY_ID);
        }

        public JObject toJSONObject()
        {
            if (mID == null)
            {
                SoomlaUtils.LogError(TAG, "This is BAD! We don't have ID in the this SoomlaEntity. Stopping here.");
                return null;
            }
            JObject jsonObject = new JObject();
            jsonObject[JSONConsts.SOOM_ENTITY_NAME] = mName;
            jsonObject[JSONConsts.SOOM_ENTITY_DESCRIPTION] = mDescription;
            jsonObject[JSONConsts.SOOM_ENTITY_ID] = mID;
           
            return jsonObject;
        }

        public bool Equal(Object o)
        {
            if(this == o) return true;
            if (!(o is SoomlaEntity)) return false;

            SoomlaEntity that = (SoomlaEntity)o;
            return (mID.Equals(that.mID));
        }

        public int hashCode()
        {
            return mID != null ? mID.GetHashCode() : 0;
        }

        private const String TAG = "SOOMLA SoomlaEntity"; //used for Log messages
        protected String mName;
        protected String mDescription;
        protected String mID;
    }
}
