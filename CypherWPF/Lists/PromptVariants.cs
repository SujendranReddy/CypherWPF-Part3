using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CypherWPF
{
    //List of possible prompts, randomly used. 
    class PromptVariants
    {
        public static readonly List<string> promptVariants = new List<string>
{
    "What’s next, {0}?",
    "How can I assist you now, {0}?",
    "What else can I do for you?",
    "Anything else on your mind?",
    "Ready for the next step?",
    "Is there something else you’d like?",
    "What would you like to do next?",
    "How can I help you further?",
    "What’s your next request?",
    "What’s your next question?"
};
    }
}
