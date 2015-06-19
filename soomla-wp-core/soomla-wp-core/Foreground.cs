/// Copyright (C) 2012-2015 Soomla Inc.
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
using System.Text;
using System.Threading.Tasks;
using Microsoft.Phone.Shell;
using SoomlaWpCore.util;
using SoomlaWpCore.events;

namespace SoomlaWpCore
{
    public class Foreground
    {
        public const String TAG = "SOOMLA Foreground";
        private static Foreground mInstance;
        private static bool isForeground;
        public static Foreground Instance
        {
            get
            {
                if (mInstance == null)
                {
                    mInstance = new Foreground();
                    mInstance.Init();
                }
                return mInstance;
            }
            set
            {
            }
        }

        private void Init()
        {
            
            PhoneApplicationService.Current.Activated += OnActivated;
            PhoneApplicationService.Current.Deactivated += OnDeactivated;
            PhoneApplicationService.Current.Closing += OnClosing;
            PhoneApplicationService.Current.Launching += OnLaunching;
            isForeground = true;
        }

        private void OnLaunching(object sender, LaunchingEventArgs e)
        {
            isForeground = true;
            BusProvider.Instance.Post(new AppToForegroundEvent());
            SoomlaUtils.LogDebug(TAG, "became foreground");
        }

        private void OnClosing(object sender, ClosingEventArgs e)
        {
            isForeground = false;
            BusProvider.Instance.Post(new AppToBackgroundEvent());
            SoomlaUtils.LogDebug(TAG, "became close");
        }

        private void OnDeactivated(object sender, DeactivatedEventArgs e)
        {
            isForeground = false;
            BusProvider.Instance.Post(new AppToBackgroundEvent());
            SoomlaUtils.LogDebug(TAG, "became background");
        }

        private void OnActivated(object sender, ActivatedEventArgs e)
        {
            isForeground = true;
            BusProvider.Instance.Post(new AppToForegroundEvent());
            SoomlaUtils.LogDebug(TAG, "became foreground");
        }

        
        public bool IsForeground()
        {
            return isForeground;
        }

        public bool IsBackground()
        {
            return !isForeground;
        }
    }
}
