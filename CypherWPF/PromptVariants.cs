using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CypherWPF
{
    class PromptVariants
    {
        public static readonly List<string> promptVariants = new List<string>
        {
            "How can I help you this time, {0}?",
            "Let's talk.",
            "Got something you'd like to ask, {0}?",
            "What would you like to explore today, {0}?",
            "Cypher's ready—what do you need, {0}?",
            "Let's get into it, {0}. What's next?",
            "Here to help, {0}. Ask away!"
        };
    }
}
