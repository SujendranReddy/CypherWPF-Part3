using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CypherWPF
{
    class SentimentData
    {
        public static readonly List<string> sentimentKeywords = new List<string>
        {
            "worried", "scared", "frustrated", "anxious", "confused", "upset",
            "angry", "nervous", "stressed", "overwhelmed", "afraid", "helpless",
            "violated", "targeted", "hacked", "scammed", "tricked", "compromised",
            "exposed", "phished", "spied", "monitored", "leaked", "breached",
            "locked out", "can't access", "account stolen", "identity stolen", "password leaked",
            "paranoid", "suspicious", "distrustful", "uncertain", "clueless", "panicked"
        };

    }
}
