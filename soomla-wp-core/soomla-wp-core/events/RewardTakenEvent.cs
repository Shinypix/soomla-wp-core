using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoomlaWpCore.events
{
    public class RewardTakenEvent : SoomlaEvent
    {
        public string RewardId;

        public RewardTakenEvent(string rewardId) : this(rewardId,null)
        {
        }

        public RewardTakenEvent(string rewardId, Object sender) : base(sender)
        {
            RewardId = rewardId;
        }
    }
}
