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

        public string GetId()
        {
            return mID;
        }

        public string GetName()
        {
            return mName;
        }

        public string GetDescription()
        {
            return mDescription;
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
