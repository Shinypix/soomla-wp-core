using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoomlaWpCore.events
{
    public class RewardGivenEvent : SoomlaEvent
    {
        public string RewardId;

        public RewardGivenEvent(string rewardId) : this(rewardId,null)
        {
        }

        public RewardGivenEvent(string rewardId, Object sender)
            : base(sender)
        {
            RewardId = rewardId;
        }
    }
}
