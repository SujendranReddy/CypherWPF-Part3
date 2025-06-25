using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
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
        private enum ReminderState
        {
            None,                 

            AwaitingYesNo,          
            AwaitingTimeSpec        
        }

        private static readonly List<string> sentimentKeywords = new List<string>
        {
            "worried", "scared", "frustrated", "anxious", "confused", "upset",
            "angry", "nervous", "stressed", "overwhelmed", "afraid", "helpless",
            "violated", "targeted", "hacked", "scammed", "tricked", "compromised",
            "exposed", "phished", "spied", "monitored", "leaked", "breached",
            "locked out", "can't access", "account stolen", "identity stolen", "password leaked",
            "paranoid", "suspicious", "distrustful", "uncertain", "clueless", "panicked"
        };

        private static readonly List<string> clarificationTriggers = new List<string> {
            "more details", "i don't understand", "dont understand", "explain",
            "can you elaborate", "i'm confused", "confused", "clarify", "what do you mean", "more"
        };

        private static readonly List<string> promptVariants = new List<string>
        {
            "How can I help you this time, {0}?",
            "Let's talk.",
            "Got something you'd like to ask, {0}?",
            "What would you like to explore today, {0}?",
            "Cypher's ready—what do you need, {0}?",
            "Let's get into it, {0}. What's next?",
            "Here to help, {0}. Ask away!"
        };

        private static readonly Dictionary<string, string> topicDefinitions = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "social media safety", "Social Media Safety involves protecting your personal information and managing privacy settings to avoid online threats." },
            { "phishing", "Phishing is a cyber attack where attackers try to trick you into giving personal information via fake emails or websites." },
            { "passwords", "Strong Passwords are long, unique, and use a mix of letters, numbers, and symbols to protect your accounts." },
            { "scam", "Avoiding Scams means staying cautious of online offers or messages that seem too good to be true or request personal data." },
            { "wi-fi security", "Wi-Fi Security ensures your wireless network is protected using strong passwords and encryption like WPA3." },
            { "device protection", "Device Protection refers to using antivirus software, keeping systems updated, and avoiding unsafe downloads." },
            { "staying safe online", "Staying Safe Online means practicing good habits like not oversharing, using secure websites, and avoiding suspicious links." },
            { "privacy", "Privacy Settings help control what information you share online and who can access it." },
            { "software updates", "Software Updates fix security bugs and improve the performance and safety of your apps and operating system." },
            { "malware", "Malware Protection defends against harmful software that can damage or control your device without permission." },
            { "vpn", "A VPN (Virtual Private Network) encrypts your internet connection, making your browsing more private and secure." },
            { "backing up your data", "Backing Up Your Data means saving copies of your files in case your device is lost, stolen, or crashes." },
            { "two-factor authentication", "Two-Factor Authentication adds an extra layer of security by requiring a second code in addition to your password." },
            { "app permissions", "App Permissions control what data and features an app can access on your device, like location or camera." }
        };

        private static Dictionary<string, List<string>> keywordTips = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase)
        {
            { "social", new List<string>
                {
                    "Keep your social media profiles private to avoid identity theft.",
                    "Think before you post. Once it's online, it's out there forever.",
                    "Don't overshare personal details like your location or contact info.",
                    "Regularly review your friends/followers list and remove people you don't know.",
                    "Use strong, unique passwords for each social media account."
                }
            },
            { "phishing", new List<string>
                {
                    "Phishing is when someone pretends to be trustworthy to steal your info. Always check where emails come from.",
                    "Be cautious of emails asking for personal information—scammers disguise themselves as trusted sources.",
                    "Don't rush to click links. Hover over them first to check where they lead.",
                    "Look for poor spelling or generic greetings—signs of phishing.",
                    "Enable two-factor authentication to protect accounts even if credentials are compromised."
                }
            },
            { "password", new List<string>
                {
                    "Use strong passwords with a mix of letters, numbers, and symbols.",
                    "Avoid reusing the same password on multiple accounts.",
                    "Consider using a password manager to generate and store your passwords.",
                    "Change default passwords on new devices immediately.",
                    "Use passphrases—long, memorable sentences—as passwords."
                }
            },
            { "scam", new List<string>
                {
                    "If it sounds too good to be true, it probably is.",
                    "Stick to trusted websites and don't give away info to strangers online.",
                    "Always verify the legitimacy of offers and requests before taking action.",
                    "Check for secure URLs (https://) and padlock icons before entering details.",
                    "Research a company or individual before sending money or personal data."
                }
            },
            { "wi-fi", new List<string>
                {
                    "Public Wi-Fi isn't always safe. Avoid logging into sensitive accounts.",
                    "Use a VPN to encrypt your data on public networks.",
                    "Turn off auto-connect for open networks when you're out.",
                    "Set your home network to WPA3 if available.",
                    "Rename default SSIDs to deter attackers from guessing your router model."
                }
            },
            { "device", new List<string>
                {
                    "Keep your phone and computer updated regularly.",
                    "Avoid installing random apps from untrusted sources.",
                    "Use screen locks and antivirus software for extra protection.",
                    "Back up your device settings and data before major OS updates.",
                    "Disable unused hardware features like Bluetooth when not in use."
                }
            },
            { "safety", new List<string>
                {
                    "Trust your instincts—if something feels off, it probably is.",
                    "Slow down and double-check before clicking or sharing online.",
                    "Cybersecurity is about awareness—stay alert!",
                    "Keep up with security news to know about emerging threats.",
                    "Educate friends and family—security is stronger when everyone protects themselves."
                }
            },
            { "privacy", new List<string>
                {
                    "Check app and browser settings to control what you share.",
                    "Only give apps the permissions they truly need.",
                    "Use private browsing when researching sensitive topics.",
                    "Review privacy policies before signing up for new services.",
                    "Use browser extensions to block trackers and ads."
                }
            },
            { "update", new List<string>
                {
                    "Don't skip updates—they fix security bugs and keep you protected.",
                    "Enable auto-updates for your OS and apps to stay current.",
                    "Software updates often patch vulnerabilities hackers exploit.",
                    "Review change logs to understand what updates address.",
                    "Schedule updates during off-hours to avoid interruptions."
                }
            },
            { "malware", new List<string>
                {
                    "Avoid downloading pirated software—it often carries malware.",
                    "Install antivirus software and keep it updated.",
                    "Think twice before opening unknown email attachments.",
                    "Use sandbox environments to test unknown files safely.",
                    "Regularly scan external drives for hidden malware."
                }
            },
            { "vpn", new List<string>
                {
                    "A VPN encrypts your connection and hides your IP address.",
                    "Use a VPN on public Wi-Fi to protect your data.",
                    "VPNs help prevent tracking and improve online privacy.",
                    "Choose a VPN provider with a strict no-logs policy.",
                    "Disconnect from the VPN when not needed to avoid unnecessary latency."
                }
            },
            { "backup", new List<string>
                {
                    "Back up your files regularly to avoid data loss.",
                    "Use both cloud and local backups for safety.",
                    "Test your backups occasionally to ensure they work.",
                    "Keep at least one offline backup to protect against ransomware.",
                    "Automate backup schedules to reduce manual effort."
                }
            },
            { "2fa", new List<string>
                {
                    "Enable Two-Factor Authentication on all important accounts.",
                    "2FA adds an extra layer of security even if your password is leaked.",
                    "Use authenticator apps for more secure 2FA than SMS.",
                    "Don't store backup codes in your inbox—keep them offline.",
                    "Consider hardware keys (e.g., YubiKey) for critical accounts."
                }
            },
            { "permission", new List<string>
                {
                    "Apps don't need access to everything—review their permissions.",
                    "Turn off microphone or camera access if not needed.",
                    "Remove app permissions you don't recognize or use.",
                    "Revoke location access for apps that don't require it.",
                    "Check permissions again after each app update."
                }
            },
            { "convo", new List<string>
                {
                    "Let's talk! Try asking about privacy, scams, or any online danger.",
                    "I'm here to chat and help you stay secure online.",
                    "Want a tip or just some cyber-chitchat? I've got you.",
                    "Feel free to ask about any security concern on your mind.",
                    "What's up? Need help with a specific cybersecurity issue?"
                }
            },
            { "conversation", new List<string>
                {
                    "Let's talk! Try asking about privacy, scams, or any online danger.",
                    "I'm here to chat and help you stay secure online.",
                    "Want a tip or just some cyber-chitchat? I've got you.",
                    "Feel free to ask about any security concern on your mind.",
                    "What's up? Need help with a specific cybersecurity issue?"
                }
            },
            { "how are you", new List<string>
                {
                    "I'm doing great and ready to help you stay safe online!",
                    "Always vigilant, always cyber-secure!",
                    "Feeling firewalled and fabulous—thanks for asking!",
                    "I'm fine—just scanning for threats and ready to chat.",
                    "All systems green! How can I assist you today?"
                }
            },
            { "what can i ask you", new List<string>
                {
                    "You can ask about scams, privacy, social media, VPNs and more.",
                    "Ask me about staying safe online, phishing, passwords—anything cybersecurity.",
                    "I've got tips on malware, backups, updates, and way more!",
                    "Try typing 'scams', 'passwords', or any topic on the list.",
                    "Wondering what else? Type 'help' to see all commands."
                }
            },
            { "what can i ask", new List<string>
                {
                    "You can ask about scams, privacy, social media, VPNs and more.",
                    "Ask me about staying safe online, phishing, passwords—anything cybersecurity.",
                    "I've got tips on malware, backups, updates, and way more!",
                    "Try typing 'scams', 'passwords', or any topic on the list.",
                    "Wondering what else? Type 'help' to see all commands."
                }
            },
        };

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
            try
            {
                string fullPath = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                if (File.Exists(fullPath))
                {
                    var player = new SoundPlayer(fullPath);
                    player.Play(); // Non-blocking play
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
            await AddChatMessageWithEffect("Cypher: What's your name?", Brushes.Green);
            waitingForName = true;
            InputTextBox.IsEnabled = true;
            SendButton.IsEnabled = true;
            InputTextBox.Focus();
        }

        private async Task CompleteGreeting()
        {
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

        private async Task ShowMainTopics()
        {
            string topics = @"
Topics you can ask me about:
  [1] Social Media Safety  
  [2] Phishing Awareness  
  [3] Strong Passwords  
  [4] Avoiding Scams  
  [5] Wi-Fi Security  
  [6] Device Protection  
  [7] Staying Safe Online  
  [8] Privacy Settings  
  [9] Software Updates  
  [10] Malware Protection  
  [11] Using a VPN  
  [12] Backing Up Your Data  
  [13] Two-Factor Authentication  
  [14] App Permissions";

            await AddChatMessageWithEffect(topics, Brushes.Yellow);
        }

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

        private async Task TypeWriterEffect(TextBlock textBlock, string text, int delay)
        {
            textBlock.Text = "";
            foreach (char c in text)
            {
                textBlock.Text += c;
                await Task.Delay(delay);
            }
        }

        private string GetRandomPrompt(string userName)
        {
            var rand = new Random();
            return string.Format(promptVariants[rand.Next(promptVariants.Count)], userName);
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

        private async Task ProcessInput()
        {
            string input = InputTextBox.Text.Trim();
            if (string.IsNullOrEmpty(input)) return;

            // Display user input
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
                if (input.ToLower() == "yes" || input.ToLower() == "y")
                {
                    await RandomTip(favoriteTopic);
                }
                else
                {
                    await AddChatMessageWithEffect("Cypher: No worries! Let me know if you want tips on anything else.", Brushes.Gray);
                }
                await AddChatMessageWithEffect($"\nCypher: {GetRandomPrompt(userName)}", Brushes.White);
                return;
            }

            if (input.ToLower() == "exit")
            {
                DrawDivider("GOODBYE");
                await AddChatMessageWithEffect("\nCypher: Stay secure out there!", Brushes.Magenta);
                await Task.Delay(2000);
                Application.Current.Shutdown();
                return;
            }

            if (input.ToLower() == "help")
            {
                await HandleHelpMenu();
                await AddChatMessageWithEffect($"\nCypher: {GetRandomPrompt(userName)}", Brushes.White);
                return;
            }

            DrawDivider("RESPONSE");
            await HandleUserQuery(input.ToLower());
            await AddChatMessageWithEffect($"\nCypher: {GetRandomPrompt(userName)}", Brushes.White);
        }

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

        private async Task HandleUserQuery(string input)
        {
            bool handled = await HandleTaskAssistantCommands(input);
            if (handled)
                return;

            // Sentiment detection
            string detectedEmotion = DetectSentiment(input);
            if (detectedEmotion != null)
            {
                await RespondSentiment(detectedEmotion, input);
                return;
            }

            // Definition requests
            if (await TryGetDefinition(input))
                return;

            // More details 
            if (clarificationTriggers.Any(phrase => input.Contains(phrase)))
            {
                if (currentTopic != null && keywordTips.ContainsKey(currentTopic))
                {
                    var tips = keywordTips[currentTopic];
                    var available = Enumerable.Range(0, tips.Count)
                                              .Where(i => !usedTipIndices.Contains(i))
                                              .ToList();
                    if (available.Count == 0)
                    {
                        await AddChatMessageWithEffect("Cypher: I've shared all I have on that topic. Want to ask about something else?", Brushes.Cyan);
                        currentTopic = null;
                        usedTipIndices.Clear();
                    }
                    else
                    {
                        int idx = new Random().Next(available.Count);
                        usedTipIndices.Add(available[idx]);
                        await AddChatMessageWithEffect($"Cypher: Sure, here's another tip on {currentTopic}: {tips[available[idx]]}", Brushes.Cyan);
                    }
                }
                else
                {
                    await AddChatMessageWithEffect("Cypher: Could you clarify which topic you'd like more details on?", Brushes.Cyan);
                }
                return;
            }

            // Favorite topic
            if (TryDetectFavoriteTopic(input))
            {
                await AddChatMessageWithEffect($"Cypher: Got it! I'll remember you're interested in {favoriteTopic}.", Brushes.Magenta);
                await FavoriteTopicTip();
                return;
            }

            // IsBored method
            if (IsBored(input) && favoriteTopic != null)
            {
                await FavoriteTopicTip();
                return;
            }

            // New topic selected
            string matchedTopic = GetMatchedTopic(input);
            if (matchedTopic != null)
            {
                currentTopic = matchedTopic;
                usedTipIndices.Clear();
                await RandomTip(matchedTopic);
                return;
            }

            // Default response
            await AddChatMessageWithEffect("Cypher: Sorry, I don't have info on that right now. Try asking about scams, privacy, or social media safety.", Brushes.Gray);
        }

        private string DetectSentiment(string input)
        {
            foreach (var word in sentimentKeywords)
            {
                if (input.Contains(word))
                    return word;
            }
            return null;
        }

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
            string[] triggerWords = new[] { "favorite", "favourite", "interested in", "like", "love" };
            foreach (string trigger in triggerWords)
            {
                if (input.Contains(trigger))
                {
                    foreach (var topic in keywordTips.Keys)
                    {
                        if (input.Contains(topic))
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
            string[] bored = new[] { "hello", "hi", "hey", "greetings", "whats up", "ok", "cool", "bored", "boring", "idk", "i dont know" };
            return bored.Any(g => input.Contains(g));
        }

        private string GetMatchedTopic(string input)
        {
            foreach (var topic in keywordTips.Keys)
            {
                if (input.Contains(topic))
                    return topic;
            }
            return null;
        }

        private async Task RandomTip(string topic)
        {
            if (!keywordTips.ContainsKey(topic))
            {
                await AddChatMessageWithEffect($"Cypher: Sorry, I don't have tips for {topic} right now.", Brushes.Red);
                return;
            }

            var tips = keywordTips[topic];
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
                    foreach (var topic in topicDefinitions.Keys)
                    {
                        if (input.ToLower().Contains(topic.ToLower()))
                        {
                            await AddChatMessageWithEffect($"Cypher: {topicDefinitions[topic]}", Brushes.Green);
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

        private async Task<bool> HandleTaskAssistantCommands(string input)
        {
            if (_reminderState == ReminderState.AwaitingYesNo)
                return await HandleReminderYesNo(input);

            if (_reminderState == ReminderState.AwaitingTimeSpec)
                return await HandleReminderTimeSpec(input);

            if (input.StartsWith("add task ", StringComparison.OrdinalIgnoreCase))
            {
                string task = input.Substring("add task ".Length).Trim();   
                if (!string.IsNullOrWhiteSpace(task))
                {
                    taskList.Add(new TaskItem(task));
                    int idx = taskList.Count;                              

                    await AddChatMessageWithEffect(
                        $"Cypher: Task added – \"{task}\" (#{idx})",
                        Brushes.LightGreen);

                    
                    _pendingReminderIndex = idx;
                    _reminderState = ReminderState.AwaitingYesNo;

                    await AddChatMessageWithEffect(
                        "Cypher: Would you like a reminder for this task? (yes/no)",
                        Brushes.LightBlue);
                }
                else
                {
                    await AddChatMessageWithEffect(
                        "Cypher: Please provide a valid task description.",
                        Brushes.Red);
                }
                return true;
            }

           
            if (input.Equals("view tasks", StringComparison.OrdinalIgnoreCase))
            {
                if (taskList.Count == 0)
                {
                    await AddChatMessageWithEffect(
                        "Cypher: No tasks yet. Use 'add task <description>' to get started!",
                        Brushes.Gray);
                }
                else
                {
                    var sb = new StringBuilder("Here are your cybersecurity tasks:\n");
                    for (int i = 0; i < taskList.Count; i++)
                        sb.AppendLine($"{i + 1}. [{(taskList[i].IsDone ? '✓' : ' ')}] {taskList[i].Description}");
                    await AddChatMessageWithEffect(sb.ToString(), Brushes.Cyan);
                }
                return true;
            }

            if (input.StartsWith("complete task ", StringComparison.OrdinalIgnoreCase))
            {
                string numStr = input.Substring("complete task ".Length).Trim();
                if (int.TryParse(numStr, out int idx) && idx >= 1 && idx <= taskList.Count)
                {
                    taskList[idx - 1].IsDone = true;
                    await AddChatMessageWithEffect(
                        $"Cypher: Task {idx} marked as complete!",
                        Brushes.LightGreen);
                }
                else
                {
                    await AddChatMessageWithEffect("Cypher: Invalid task number.", Brushes.Red);
                }
                return true;
            }

            if (input.StartsWith("delete task ", StringComparison.OrdinalIgnoreCase))
            {
                string numStr = input.Substring("delete task ".Length).Trim();
                if (int.TryParse(numStr, out int idx) && idx >= 1 && idx <= taskList.Count)
                {
                    string desc = taskList[idx - 1].Description;
                    taskList.RemoveAt(idx - 1);
                    await AddChatMessageWithEffect(
                        $"Cypher: Deleted task \"{desc}\"",
                        Brushes.Orange);
                }
                else
                {
                    await AddChatMessageWithEffect("Cypher: Invalid task number.", Brushes.Red);
                }
                return true;
            }

            return false;
        }

        
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

        
        private async Task<bool> HandleReminderTimeSpec(string input)
        {
            if (!_pendingReminderIndex.HasValue)             // <<— FIX ②
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
    }
}
