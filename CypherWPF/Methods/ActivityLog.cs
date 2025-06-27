using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CypherWPF.Methods
{
    // Record acitvity
    public static class ActivityLog
    {
        public static int QuizAttempts { get; private set; }
        public static int QuizCompletions { get; private set; }
        public static int TasksAdded { get; private set; }
        public static int TasksCompleted { get; private set; }
        public static int TasksDeleted { get; private set; }

        public static void RecordQuizStart() => QuizAttempts++;
        public static void RecordQuizEnd() => QuizCompletions++;

        public static void RecordTaskAdded() => TasksAdded++;
        public static void RecordTaskCompleted() => TasksCompleted++;
        public static void RecordTaskDeleted() => TasksDeleted++;

        public static string GetSummary()
        {
            return "Here's what you've been up to:\n" +
                   $"- You started {QuizAttempts} quiz{(QuizAttempts == 1 ? "" : "zes")}.\n" +
                   $"- Added {TasksAdded} task{(TasksAdded == 1 ? "" : "s")}.\n" +
                   $"- Completed {TasksCompleted}.\n" +
                   $"- Deleted {TasksDeleted}.\n" +
                   "Nice progress!";
        }
    }
}