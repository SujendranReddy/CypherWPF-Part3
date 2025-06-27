using CypherWPF.Methods;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using static CypherWPF.MainWindow;


namespace CypherWPF
{
    public partial class MainWindow : Window
    {
        private static string currentTopic = null;
        private List<TaskItem> taskList = new List<TaskItem>();
        private static List<int> usedTipIndices = new List<int>();
        private static string favoriteTopic = null;
        private string userName = "";
        private bool waitingForName = false;
        private bool waitingForFavoriteResponse = false;
        private int? _pendingReminderIndex = null;
        private ReminderState _reminderState = ReminderState.None;
        private int? _quizCurrent = null;
        private int _quizScore = 0;
        private bool _quizActive = false;

        private enum ReminderState
        {
            None,                 

            AwaitingYesNo,          
            AwaitingTimeSpec        
        }


        public MainWindow()
        {
            InitializeComponent();
            InitializeApp();
        }

        private async void InitializeApp()
        {
            // Display banner with typewriter effect
            await DisplayBannerWithEffect();

            // Play intro audio
            PlayIntroductionAudio("Cypher Chatbot.wav");

            // Ask for name
            await AskNameAndGreet();
        }

        private async Task DisplayBannerWithEffect()
        {
            // Banner
            string banner = @"
_________                  .__                     
\_   ___ \  ___.__.______  |  |__    ____  _______ 
/    \  \/ <   |  |\____ \ |  |  \ _/ __ \ \_  __ \
\     \____ \___  ||  |_> >|   Y  \  ___/  |  | \/
 \______  / / ____||   __/ |___|  /\___  > |__|   
        \/  \/     |__|         \/     \/        ";

            await TypeWriterEffect(BannerText, banner, 20);
        }

        private void PlayIntroductionAudio(string filePath)
        {
            // Play audio and catch error
            try
            {
                string fullPath = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                if (File.Exists(fullPath))
                {
                    var player = new SoundPlayer(fullPath);
                    player.Play(); 
                }
                else
                {
                    AddChatMessage("Cypher: Audio file not found: " + filePath, Brushes.Red);
                }
            }
            catch (Exception ex)
            {
                AddChatMessage("Cypher: Error playing audio: " + ex.Message, Brushes.Red);
            }
        }

        private async Task AskNameAndGreet()
        {
            //What the user is greeted with
            await AddChatMessageWithEffect("Cypher: What's your name?", Brushes.Green);
            waitingForName = true;
            InputTextBox.IsEnabled = true;
            SendButton.IsEnabled = true;
            InputTextBox.Focus();
        }

        private async Task CompleteGreeting()
        {
            //Method after user enters their name
            DrawDivider("WELCOME");
            await AddChatMessageWithEffect($"\nHello, {userName}! I'm Cypher, your online safety buddy. Let's keep the web safe together!\n", Brushes.Magenta);

            await ShowMainTopics();
            await AddChatMessageWithEffect("\nType 'help' for help, or 'exit' to leave the chat anytime.", Brushes.Gray);

            // After greeting, if favoriteTopic exists offer tip
            if (favoriteTopic != null)
            {
                await FavoriteTopicTip();
            }

            // Show first prompt
            await AddChatMessageWithEffect($"\nCypher: {GetRandomPrompt(userName)}", Brushes.White);
        }

        //Method that shows main topics
        private async Task ShowMainTopics()
        {
            string topics = @"
Topics you can ask me about:
  - Social Media Safety  
  - Phishing Awareness  
  - Strong Passwords  
  - Avoiding Scams  
  - Wi-Fi Security  
  - Device Protection  
  - Staying Safe Online  
  - Privacy Settings  
  - Software Updates  
  - Malware Protection  

Other commands you can try:
  - add task <description>     (Add a new task)
  - view tasks                (View your tasks)
  - view acitivity log        (View your acitivities)
  - complete task <number>    (Mark a task complete)
  - delete task <number>      (Delete a task)
  - start quiz                (Start the cybersecurity quiz)";

            await AddChatMessageWithEffect(topics, Brushes.Yellow);
        }

        // Method to seperate responses
        private void DrawDivider(string text)
        {
            var dividerText = $"\n{new string('-', 40)}\n--- {text} ---\n{new string('-', 40)}";
            AddChatMessage(dividerText, Brushes.DarkBlue);
        }

        private async Task AddChatMessageWithEffect(string message, Brush color, int delay = 10)
        {
            var textBlock = new TextBlock
            {
                Foreground = color,
                FontFamily = new FontFamily("Consolas"),
                FontSize = 12,
                Margin = new Thickness(0, 2, 0, 2),
                TextWrapping = TextWrapping.Wrap
            };

            ChatPanel.Children.Add(textBlock);
            await TypeWriterEffect(textBlock, message, delay);
            ChatScrollViewer.ScrollToEnd();
        }

        private void AddChatMessage(string message, Brush color)
        {
            var textBlock = new TextBlock
            {
                Text = message,
                Foreground = color,
                FontFamily = new FontFamily("Consolas"),
                FontSize = 12,
                Margin = new Thickness(0, 2, 0, 2),
                TextWrapping = TextWrapping.Wrap
            };

            ChatPanel.Children.Add(textBlock);
            ChatScrollViewer.ScrollToEnd();
        }

        //Effect to delay text
        private async Task TypeWriterEffect(TextBlock textBlock, string text, int delay)
        {
            textBlock.Text = "";
            foreach (char c in text)
            {
                textBlock.Text += c;
                await Task.Delay(delay);
            }
        }

        //Method to retrieve a random prompt
        private string GetRandomPrompt(string userName)
        {
            var rand = new Random();
            return string.Format(PromptVariants.promptVariants[rand.Next(PromptVariants.promptVariants.Count)], userName);
        }

        private async void SendButton_Click(object sender, RoutedEventArgs e)
        {
            await ProcessInput();
        }

        private async void InputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                await ProcessInput();
            }
        }

        //This method process input and was added to adjust the code to WPF
        private async Task ProcessInput()
        {
            string input = InputTextBox.Text.Trim();
            if (string.IsNullOrEmpty(input)) return;

            AddChatMessage($"You: {input}", Brushes.Yellow);
            InputTextBox.Text = "";

            if (waitingForName)
            {
                userName = input;
                waitingForName = false;
                await CompleteGreeting();
                return;
            }

            if (waitingForFavoriteResponse)
            {
                waitingForFavoriteResponse = false;
                if (input.Equals("yes", StringComparison.OrdinalIgnoreCase) ||
                    input.Equals("y", StringComparison.OrdinalIgnoreCase))
                {
                    await RandomTip(favoriteTopic);
                }
                else
                {
                    await AddChatMessageWithEffect("Cypher: No worries! Let me know if you want tips on anything else.", Brushes.Gray);
                }
                if (!_quizActive)
                    await AddChatMessageWithEffect($"\nCypher: {GetRandomPrompt(userName)}", Brushes.White);
                return;
            }

            if (ClarificationData.ExitTriggers.Any(trigger => input.Equals(trigger, StringComparison.OrdinalIgnoreCase)))
            {
                DrawDivider("GOODBYE");
                await AddChatMessageWithEffect("\nCypher: Stay secure out there!", Brushes.Magenta);
                await Task.Delay(2000);
                Application.Current.Shutdown();
                return;
            }

            if (input.Equals("help", StringComparison.OrdinalIgnoreCase))
            {
                await HandleHelpMenu();
                if (!_quizActive)
                    await AddChatMessageWithEffect($"\nCypher: {GetRandomPrompt(userName)}", Brushes.White);
                return;
            }

            DrawDivider("RESPONSE");

            if (input.Equals("show activity log", StringComparison.OrdinalIgnoreCase))
            {
                var logEntries = ActivityLog.GetSummary();
                await AddChatMessageWithEffect("Cypher: Activity log:\n" + string.Join("\n", logEntries), Brushes.LightBlue);
                return;
            }

            await HandleUserQuery(input.ToLower());

            if (!_quizActive)
                await AddChatMessageWithEffect($"\nCypher: {GetRandomPrompt(userName)}", Brushes.White);
        }

        // Help menu
        private async Task HandleHelpMenu()
        {
            DrawDivider("HELP MENU");
            await ShowMainTopics();

            string otherCommands = @"Other Commands:
  help   - Show this menu again
  exit   - Leave Cypher's safe zone";

            await AddChatMessageWithEffect(otherCommands, Brushes.Cyan);
            await AddChatMessageWithEffect("\nTip: Type a number or keyword like 'phishing' to learn more.", Brushes.Gray);
        }

        //Handles user input by having entry points for other methods
        private async Task HandleUserQuery(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return;

            // Handle reminder yes/no first
            if (_reminderState == ReminderState.AwaitingYesNo &&
                (input.Equals("yes", StringComparison.OrdinalIgnoreCase) ||
                 input.Equals("no", StringComparison.OrdinalIgnoreCase)))
            {
                await HandleReminderYesNo(input);
                return;
            }

            // Handle reminder time specification next
            if (_reminderState == ReminderState.AwaitingTimeSpec)
            {
                await HandleReminderTimeSpec(input);
                return;
            }

            // Quiz stop commands
            if (_quizActive)
            {
                string[] quitWords = { "exit quiz", "quit quiz", "stop quiz", "cancel quiz",
                               "leave quiz", "end quiz", "abort quiz", "give up quiz" };

                if (quitWords.Any(w => input.IndexOf(w, StringComparison.OrdinalIgnoreCase) >= 0))
                {
                    int total = QuizQuestions.quizQuestions.Count;
                    await AddChatMessageWithEffect(
                        $"Cypher: Quiz cancelled. You answered {_quizCurrent ?? 0} of {total} questions. " +
                        $"Your score so far: {_quizScore}/{_quizCurrent ?? 0}.",
                        Brushes.Gold);

                    _quizActive = false;
                    _quizCurrent = null;
                    _quizScore = 0;
                    return;
                }
            }

            // Activity log commands
            if (Regex.IsMatch(input,
                @"\b(?:can you\s+)?show(?: me)?(?: the)?(?: my)? activity log\b",
                RegexOptions.IgnoreCase))
            {
                await AddChatMessageWithEffect(ActivityLog.GetSummary(), Brushes.LightBlue);
                return;
            }

            // Start quiz
            if (ClarificationData.StartQuizTriggers.Any(t => input.Equals(t, StringComparison.OrdinalIgnoreCase)))
            {
                _quizActive = true;
                _quizCurrent = 0;
                _quizScore = 0;
                ActivityLog.RecordQuizStart();

                string quizRules =
                    " **Quiz Time – Rules of the Game:**\n" +
                    "- Answer using **numbers only** (e.g., 1, 2, 3).\n" +
                    "- Type **exit quiz** at any time to stop.\n" +
                    "- One shot per question – no changing answers.\n" +
                    "- Your score will appear at the end.\n" +
                    "Good luck!";

                await AddChatMessageWithEffect(quizRules, Brushes.LightYellow);
                await SendQuizQuestion();
                return;
            }

            // Handle quiz answers
            if (await HandleQuizAnswer(input))
                return;

            // Task commands
            if (await HandleTaskAssistantCommands(input))
                return;

            // Sentiment
            string emotion = DetectSentiment(input);
            if (emotion != null)
            {
                await RespondSentiment(emotion, input);
                return;
            }

            // Definitions
            if (await TryGetDefinition(input))
                return;

            // Clarifications
            if (ClarificationData.clarificationTriggers.Any(p =>
                    input.IndexOf(p, StringComparison.OrdinalIgnoreCase) >= 0))
            {
                await HandleClarificationFlow(input);
                return;
            }

            // Favorite topic / boredom / new topic
            if (TryDetectFavoriteTopic(input))
            {
                await AddChatMessageWithEffect($"Cypher: Got it! I'll remember you're interested in {favoriteTopic}.", Brushes.Magenta);
                await FavoriteTopicTip();
                return;
            }

            if (IsBored(input) && favoriteTopic != null)
            {
                await FavoriteTopicTip();
                return;
            }

            string matchedTopic = GetMatchedTopic(input);
            if (matchedTopic != null)
            {
                currentTopic = matchedTopic;
                usedTipIndices.Clear();
                await RandomTip(matchedTopic);
                return;
            }

            // Fallback
            await AddChatMessageWithEffect(
                "Cypher: Sorry, I don't have info on that right now. Try asking about scams, privacy, or social media safety.",
                Brushes.Gray);
        }

        //Method to detect sentiment
        private string DetectSentiment(string input)
        {
            foreach (var word in SentimentData.sentimentKeywords)
            {
                if (input.Contains(word))
                    return word;
            }
            return null;
        }

        //Method to respond when sentiment is detected 
        private async Task RespondSentiment(string emotion, string input)
        {
            await AddChatMessageWithEffect($"Cypher: I understand you're feeling {emotion}. Cybersecurity can feel overwhelming, here's a tip…", Brushes.Magenta);

            string topicForTip = GetMatchedTopic(input);

            if (topicForTip != null)
            {
                await RandomTip(topicForTip);
            }
            else if (favoriteTopic != null)
            {
                await RandomTip(favoriteTopic);
            }
            else
            {
                await RandomTip("convo");
            }
        }

        private bool TryDetectFavoriteTopic(string input)
        {
            var triggerWords = ClarificationData.FavoriteTopicTriggers; 

            foreach (string trigger in triggerWords)
            {
                if (input.IndexOf(trigger, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    foreach (var topic in KeywordTips.keywordTips.Keys)
                    {
                        if (input.IndexOf(topic, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            favoriteTopic = topic;
                            return true;
                        }
                    }
                }
            }
            return false;
        }


        private bool IsBored(string input)
        {
            string[] bored = new[] { "tips", "tip", "greetings", "whats up", "ok", "cool", "bored", "boring", "idk", "i dont know" };
            return bored.Any(g => input.Contains(g));
        }

        private string GetMatchedTopic(string input)
        {
            foreach (var topic in KeywordTips.keywordTips.Keys)
            {
                if (input.Contains(topic))
                    return topic;
            }
            return null;
        }

        private async Task RandomTip(string topic)
        {
            if (!KeywordTips.keywordTips.ContainsKey(topic))
            {
                await AddChatMessageWithEffect($"Cypher: Sorry, I don't have tips for {topic} right now.", Brushes.Red);
                return;
            }

            var tips = KeywordTips.keywordTips[topic];
            var availableIndices = Enumerable.Range(0, tips.Count).Except(usedTipIndices).ToList();

            if (availableIndices.Count == 0)
            {
                usedTipIndices.Clear();
                availableIndices = Enumerable.Range(0, tips.Count).ToList();
            }

            var random = new Random();
            int index = availableIndices[random.Next(availableIndices.Count)];
            usedTipIndices.Add(index);

            await AddChatMessageWithEffect($"Cypher: {tips[index]}", Brushes.Green);
        }

        private async Task FavoriteTopicTip()
        {
            await AddChatMessageWithEffect($"Cypher: Would you like another {favoriteTopic} tip? (yes/no)", Brushes.Cyan);
            waitingForFavoriteResponse = true;
        }

        private async Task<bool> TryGetDefinition(string input)
        {
            string[] definitionTriggers = { "what is", "define", "meaning of", "explain", "whats" };

            foreach (var trigger in definitionTriggers)
            {
                if (input.ToLower().Contains(trigger))
                {
                    foreach (var topic in TopicDefinitions.topicDefinitions.Keys)
                    {
                        if (input.ToLower().Contains(topic.ToLower()))
                        {
                            await AddChatMessageWithEffect($"Cypher: {TopicDefinitions.topicDefinitions[topic]}", Brushes.Green);
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private async void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            await HandleHelpMenu();
        }

        private async void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            DrawDivider("GOODBYE");
            await AddChatMessageWithEffect("\nCypher: Stay secure out there!", Brushes.Magenta);
            await Task.Delay(2000);
            Application.Current.Shutdown();
        }
        public class TaskItem
        {
            public string Description { get; set; }
            public bool IsDone { get; set; }

            public TaskItem(string description)
            {
                Description = description;
                IsDone = false;
            }
        }
        // Regex for units of time
        private static readonly Regex RemindCmdRegex =
            new Regex(@"task\s+(\d+)\s+in\s+(\d+)\s+(seconds?|minutes?|hours?|days?)",
                      RegexOptions.IgnoreCase | RegexOptions.Compiled);

        //This method hadnles task related features, add, delete, complete, and reminder.
        private async Task<bool> HandleTaskAssistantCommands(string input)
        {
            if (_reminderState == ReminderState.AwaitingYesNo &&
        (input.Equals("yes", StringComparison.OrdinalIgnoreCase) ||
         input.Equals("no", StringComparison.OrdinalIgnoreCase)))
            {
                return await HandleReminderYesNo(input);
            }

            if (_reminderState == ReminderState.AwaitingTimeSpec)
            {
                return await HandleReminderTimeSpec(input);
            }
            string nlTask = ExtractNaturalLanguageTask(input);
            if (nlTask != null) input = "add task " + nlTask;

            if (input.StartsWith("add task ", StringComparison.OrdinalIgnoreCase))
            {
                string task = input.Substring("add task ".Length).Trim();
                if (string.IsNullOrWhiteSpace(task))
                {
                    await AddChatMessageWithEffect("Cypher: Please provide a valid task description.", Brushes.Red);
                    return true;
                }

                taskList.Add(new TaskItem(task));
                ActivityLog.RecordTaskAdded();
                int idx = taskList.Count;

                await AddChatMessageWithEffect($"Cypher: Task added – \"{task}\" (#{idx})", Brushes.LightGreen);

                _pendingReminderIndex = idx;
                _reminderState = ReminderState.AwaitingYesNo;
                await AddChatMessageWithEffect("Cypher: Would you like a reminder for this task? (yes/no)",
                                               Brushes.LightBlue);
                return true;
            }
            //Remind task
            if (input.IndexOf("remind", StringComparison.OrdinalIgnoreCase) >= 0 ||
                input.IndexOf("reminder", StringComparison.OrdinalIgnoreCase) >= 0 ||
                input.IndexOf("alert", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                Match m = RemindCmdRegex.Match(input);
                if (m.Success)
                {
                    int taskIdx = int.Parse(m.Groups[1].Value);
                    int qty = int.Parse(m.Groups[2].Value);
                    string unit = m.Groups[3].Value.ToLower();

                    if (taskIdx < 1 || taskIdx > taskList.Count)
                    {
                        await AddChatMessageWithEffect("Cypher: That task number doesn't exist.", Brushes.Red);
                        return true;
                    }

                    TimeSpan delay = unit.StartsWith("second") ? TimeSpan.FromSeconds(qty) :
                                     unit.StartsWith("minute") ? TimeSpan.FromMinutes(qty) :
                                     unit.StartsWith("hour") ? TimeSpan.FromHours(qty) :
                                                                 TimeSpan.FromDays(qty);

                    ScheduleReminder(taskIdx, taskList[taskIdx - 1].Description, delay);
                    await AddChatMessageWithEffect(
                        $"Cypher: Reminder set for task {taskIdx} in {qty} {unit}.", Brushes.LightBlue);
                    return true;
                }
                else if (_reminderState == ReminderState.None)   
                {
                    await AddChatMessageWithEffect(
                        "Cypher: Try `remind task <number> in <number> <seconds|minutes|hours|days>`.",
                        Brushes.Red);
                    return true;
                }
            }
            //COmplete task
            var match = Regex.Match(input,
 @"\b(?:can you\s+)?(?:(?:complete)|(?:finished?)|(?:done\s+with))\s+task\s+(\d+)\b",
 RegexOptions.IgnoreCase);
            if (match.Success)
            {
                if (!int.TryParse(match.Groups[1].Value, out int idx) || idx < 1 || idx > taskList.Count)
                {
                    await AddChatMessageWithEffect("Cypher: Invalid task number.", Brushes.Red);
                    return true;
                }

                if (!taskList[idx - 1].IsDone)
                {
                    taskList[idx - 1].IsDone = true;
                    ActivityLog.RecordTaskCompleted();
                    await AddChatMessageWithEffect($"Cypher: Task {idx} marked as complete!", Brushes.LightGreen);
                }
                else
                {
                    await AddChatMessageWithEffect($"Cypher: Task {idx} is already marked complete.", Brushes.Yellow);
                }
                return true;
            }

            //Delete tasks
            var deleteMatch = Regex.Match(input,
    @"\b(?:delete|remove)\s+task\s+(\d+)\b",
    RegexOptions.IgnoreCase);
            if (deleteMatch.Success)
            {
                if (!int.TryParse(deleteMatch.Groups[1].Value, out int idx) || idx < 1 || idx > taskList.Count)
                {
                    await AddChatMessageWithEffect("Cypher: Invalid task number.", Brushes.Red);
                    return true;
                }

                string desc = taskList[idx - 1].Description;
                taskList.RemoveAt(idx - 1);
                ActivityLog.RecordTaskDeleted();
                await AddChatMessageWithEffect($"Cypher: Deleted task \"{desc}\"", Brushes.Orange);
                return true;
            }

            var viewMatch = Regex.IsMatch(input,
                @"\b(?:view|show|list)\s+tasks\b",
                RegexOptions.IgnoreCase);
            if (viewMatch)
            {
                if (taskList.Count == 0)
                {
                    await AddChatMessageWithEffect(
                        "Cypher: No tasks yet. Use 'add task <description>' to get started!",
                        Brushes.Gray);
                }
                else
                {
                    var sb = new StringBuilder("Here are your tasks:\n");
                    for (int i = 0; i < taskList.Count; i++)
                        sb.AppendLine($"{i + 1}. [{(taskList[i].IsDone ? '✓' : ' ')}] {taskList[i].Description}");
                    await AddChatMessageWithEffect(sb.ToString(), Brushes.Cyan);
                }
                return true;
            }

            return false;  
        }
        //This method strips the natural language for add task
        private string ExtractNaturalLanguageTask(string input)
        {
            foreach (var trigger in ClarificationData.AddTaskTriggers)
            {
                int idx = input.IndexOf(trigger, StringComparison.OrdinalIgnoreCase);
                if (idx >= 0)
                {
                    string after = input.Substring(idx + trigger.Length).Trim();

                    string[] connectors = { "called", "named", "as", ":" };
                    foreach (var c in connectors)
                        if (after.StartsWith(c + " ", StringComparison.OrdinalIgnoreCase))
                            after = after.Substring(c.Length).Trim();

                    return string.IsNullOrWhiteSpace(after) ? null : after;
                }
            }
            return null;
        }
        // When a user enter more/ details/ etc.. this method activates 
        private async Task HandleClarificationFlow(string input)
        {
            if (currentTopic != null && KeywordTips.keywordTips.ContainsKey(currentTopic))
            {
                var tips = KeywordTips.keywordTips[currentTopic];
                var available = Enumerable.Range(0, tips.Count)
                                          .Where(i => !usedTipIndices.Contains(i))
                                          .ToList();

                if (available.Count == 0)
                {
                    await AddChatMessageWithEffect(
                        "Cypher: I've shared all I have on that topic. Want to ask about something else?",
                        Brushes.Cyan);
                    currentTopic = null;
                    usedTipIndices.Clear();
                }
                else
                {
                    int r = new Random().Next(available.Count);
                    usedTipIndices.Add(available[r]);
                    await AddChatMessageWithEffect(
                        $"Cypher: Sure, here's another tip on {currentTopic}: {tips[available[r]]}",
                        Brushes.Cyan);
                }
            }
            else
            {
                await AddChatMessageWithEffect(
                    "Cypher: Could you clarify which topic you'd like more details on?",
                    Brushes.Cyan);
            }
        }
        // This method directs to related method when the user wants to set a reminder
        private async Task<bool> HandleReminderYesNo(string input)
        {
            if (!_pendingReminderIndex.HasValue)             
            {
                ResetReminderDialog();
                return true;
            }
            int idx = _pendingReminderIndex.Value;           

            if (input.Equals("yes", StringComparison.OrdinalIgnoreCase))
            {
                _reminderState = ReminderState.AwaitingTimeSpec;
                await AddChatMessageWithEffect(
                    "Cypher: When should I remind you? Use '<number> <seconds|minutes|hours|days>'.",
                    Brushes.LightBlue);
                return true;
            }

            if (input.Equals("no", StringComparison.OrdinalIgnoreCase))
            {
                await AddChatMessageWithEffect("Cypher: Okay, no reminder set.", Brushes.Gray);
                ResetReminderDialog();
                return true;
            }

            await AddChatMessageWithEffect("Cypher: Please answer 'yes' or 'no'.", Brushes.Red);
            return true;
        }

        // Handles the input of the reminder time specified
        private async Task<bool> HandleReminderTimeSpec(string input)
        {
            if (!_pendingReminderIndex.HasValue)            
            {
                ResetReminderDialog();
                return true;
            }
            int idx = _pendingReminderIndex.Value;

            TimeSpan? delay = ParseTimeSpan(input);
            if (delay is null)
            {
                await AddChatMessageWithEffect(
                    "Cypher: Sorry, I didn't understand. Use '<number> <seconds|minutes|hours|days>'.",
                    Brushes.Red);
                return true;
            }

            string taskDescription = taskList[idx - 1].Description;
            await AddChatMessageWithEffect(
                $"Cypher: Reminder set for task {idx} \"{taskDescription}\" in {input}.",
                Brushes.LightCyan);

            ScheduleReminder(idx, taskDescription, delay.Value);

            ResetReminderDialog();
            return true;
        }
        // This method schedules reminders, to go off
        private void ScheduleReminder(int idx, string description, TimeSpan delay)
        {
            _ = Task.Run(async () =>
            {
                try
                {
                    await Task.Delay(delay);
                    await Application.Current.Dispatcher.InvokeAsync(async () =>
                    {
                        await AddChatMessageWithEffect(
                            $"Cypher: Reminder – Task {idx}: \"{description}\"",
                            Brushes.Yellow);
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Reminder error: {ex}");
                }
            });
        }

        // Switch for different units of time
        private static TimeSpan? ParseTimeSpan(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return null;

            var parts = input.Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 2) return null;

            if (!int.TryParse(parts[0], out int value) || value <= 0) return null;

            string unit = parts[1].ToLowerInvariant();

            switch (unit)
            {
                case "second":
                case "seconds":
                    return TimeSpan.FromSeconds(value);

                case "minute":
                case "minutes":
                    return TimeSpan.FromMinutes(value);

                case "hour":
                case "hours":
                    return TimeSpan.FromHours(value);

                case "day":
                case "days":
                    return TimeSpan.FromDays(value);

                default:
                    return null; 
            }
        }

        private void ResetReminderDialog()
        {
            _reminderState = ReminderState.None;
            _pendingReminderIndex = null;
        }

        // Start Quiz records the activity
        private async Task StartQuiz()
        {
            _quizScore = 0;
            _quizCurrent = 0;
            _quizActive = true;

            await SendQuizQuestion();
        }
        // Send Quiz QUestions sends the next question
        private async Task SendQuizQuestion()
        {
            if (_quizCurrent == null || _quizCurrent >= QuizQuestions.quizQuestions.Count)
            {
                await EndQuiz();
                return;
            }

            var (question, options, _) = QuizQuestions.quizQuestions[_quizCurrent.Value];

            var sb = new StringBuilder();
            sb.AppendLine($"Quiz Question #{_quizCurrent + 1}: {question}");
            for (int i = 0; i < options.Length; i++)
                sb.AppendLine($"{i + 1}. {options[i]}");
            sb.AppendLine("Reply with the number of your answer.");

            await AddChatMessageWithEffect(sb.ToString(), Brushes.LightSkyBlue);
        }

        //Handle Quiz Answer method checks for errors, and marks the answers, recording the score.
        private async Task<bool> HandleQuizAnswer(string input)
        {
            if (!_quizActive) return false;

            if (_quizCurrent == null || _quizCurrent >= QuizQuestions.quizQuestions.Count)
            {
                await AddChatMessageWithEffect("Quiz error. Please restart the quiz.", Brushes.Red);
                _quizActive = false;
                _quizCurrent = null;
                return true;
            }

            if (!int.TryParse(input.Trim(), out int answer))
            {
                await AddChatMessageWithEffect("Please reply with a number.", Brushes.Red);
                return true;
            }

            var (question, options, correct) = QuizQuestions.quizQuestions[_quizCurrent.Value];
            string explanation = QuizQuestions.quizExplanations[_quizCurrent.Value];

            if (answer < 1 || answer > options.Length)
            {
                await AddChatMessageWithEffect("That number isn’t one of the options. Try again.", Brushes.Red);
                return true;
            }

            if (answer - 1 == correct)
            {
                _quizScore++;
                await AddChatMessageWithEffect("Correct! 🎉", Brushes.LightGreen);
            }
            else
            {
                await AddChatMessageWithEffect($"Incorrect. The correct answer was: {options[correct]}", Brushes.Red);
            }

            // 💬 Show explanation no matter what
            await AddChatMessageWithEffect($"Explanation: {explanation}", Brushes.LightSkyBlue);

            _quizCurrent++;

            if (_quizCurrent >= QuizQuestions.quizQuestions.Count)
            {
                await EndQuiz();
            }
            else
            {
                await SendQuizQuestion();
            }

            return true;
        }

        // End quiz method provides score and records the quiz attempt. 
        private async Task EndQuiz()
        {
            _quizActive = false;
            _quizCurrent = null;


            ActivityLog.RecordQuizEnd();

            await AddChatMessageWithEffect(
                $"Quiz finished! Your score: {_quizScore} / {QuizQuestions.quizQuestions.Count}",
                Brushes.Gold);
        }

    }
}
