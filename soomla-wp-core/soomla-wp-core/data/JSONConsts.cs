﻿/// Copyright (C) 2012-2014 Soomla Inc.
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

namespace SoomlaWpCore.data
{
    public class JSONConsts
    {
        /** Global **/
        public const String SOOM_ENTITY_NAME       = "name";
        public const String SOOM_ENTITY_DESCRIPTION = "description";
        public const String SOOM_ENTITY_ID         = "itemId";
        public const String SOOM_ASSOCITEMID       = "associatedItemId";
        public const String SOOM_DESIRED_BALANCE   = "desiredBalance";
        public const String SOOM_NAME              = "name";
        public const String SOOM_CLASSNAME         = "className";


        /** Reward **/

        public const String SOOM_REWARDS           = "rewards";
        public const String SOOM_REWARD_REWARDID   = "rewardId";
        public const String SOOM_REWARD_AMOUNT     = "amount";
        public const String SOOM_REWARD_ICONURL    = "iconUrl";
        public const String SOOM_REWARD_REPEAT     = "repeatable";
    }
}
