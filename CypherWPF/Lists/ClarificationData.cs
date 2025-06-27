using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CypherWPF
{
    //List of possible words users might say
    class ClarificationData
    {
        public static readonly List<string> clarificationTriggers = new List<string> {
            "more details", "i don't understand", "dont understand", "explain",
            "can you elaborate", "i'm confused", "confused", "clarify", "what do you mean", "more"
        };

        public static readonly string[] AddTaskTriggers = new string[]
    {
        "create a task called",
        "create task called",
        "add a task called",
        "add task called",
        "create a task named",
        "create task named",
        "add a task named",
        "add task named",
        "create a task",
        "add a task",
        "can you create a task",
        "could you create a task",
        "i want to add a task",
        "make a task called",
        "make task called"
    };

        public static readonly List<string> ExitTriggers = new List<string>
{
    "exit",
    "quit",
    "close",
    "goodbye",
    "bye",
    "see you",
    "later",
    "i'm done",
    "that's it",
    "done for now",
    "end",
    "stop",
    "terminate",
    "log off",
    "sign out",
    "peace out",
    "shut down",
    "ciao",
    "farewell",
    "leave",
    "i have to go",
    "talk later",
    "catch you later"
};
        public static readonly List<string> StartQuizTriggers = new List<string>
{
    "start quiz",
    "begin quiz",
    "quiz time",
    "let's quiz",
    "quiz",
    "quiz me"
};

        public static readonly List<string> FavoriteTopicTriggers = new List<string>
{
    "my favorite",
    "favorite",
    "i love",
    "i like",
    "love",
    "like",
    "i'm interested in",
    "i enjoy",
    "favorite topic",
    "i'm a fan of",
    "i really like",
    "my top topic",
    "i care about",
    "i prefer",
    "i'm passionate about"
};

    }
}
