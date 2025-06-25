using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;

namespace CypherChatBot
{
    public class ChatBot
    {
        // Tracks current topic so user can ask for more tips on it
        private string currentTopic = null;
        private List<int> usedTipIndices = new List<int>();
        private string favoriteTopic = null;

        // Sentiment trigger words
        private readonly List<string> sentimentKeywords = new List<string>
        {
            "worried","scared","frustrated","anxious","confused","upset",
            "angry","nervous","stressed","overwhelmed","afraid","helpless",
            "violated","targeted","hacked","scammed","tricked","compromised",
            "exposed","phished","spied","monitored","leaked","breached",
            "locked out","can't access","account stolen","identity stolen","password leaked",
            "paranoid","suspicious","distrustful","uncertain","clueless","panicked"
        };

        // Clarification triggers
        private readonly List<string> clarificationTriggers = new List<string>
        {
            "more details","i don't understand","dont understand","explain",
            "can you elaborate","i’m confused","confused","clarify","what do you mean","more"
        };

        // Variation of prompts
        private readonly List<string> promptVariants = new List<string>
        {
            "How can I help you this time, {0}?",
            "Let’s talk.",
            "Got something you’d like to ask, {0}?",
            "What would you like to explore today, {0}?",
            "Cypher's ready—what do you need, {0}?",
            "Let’s get into it, {0}. What’s next?",
            "Here to help, {0}. Ask away!"
        };

        // Definitions dictionary
        private readonly Dictionary<string, string> topicDefinitions = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
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

        // Tips dictionary
        private readonly Dictionary<string, List<string>> keywordTips = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase)
        {
            { "social", new List<string>
                {
                    "Keep your social media profiles private to avoid identity theft.",
                    "Think before you post. Once it's online, it's out there forever.",
                    "Don’t overshare personal details like your location or contact info.",
                    "Regularly review your friends/followers list and remove people you don’t know.",
                    "Use strong, unique passwords for each social media account."
                }
            },
            { "phishing", new List<string>
                {
                    "Phishing is when someone pretends to be trustworthy to steal your info. Always check where emails come from.",
                    "Be cautious of emails asking for personal information—scammers disguise themselves as trusted sources.",
                    "Don’t rush to click links. Hover over them first to check where they lead.",
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
                    "Stick to trusted websites and don’t give away info to strangers online.",
                    "Always verify the legitimacy of offers and requests before taking action.",
                    "Check for secure URLs (https://) and padlock icons before entering details.",
                    "Research a company or individual before sending money or personal data."
                }
            },
            { "wi-fi", new List<string>
                {
                    "Public Wi-Fi isn’t always safe. Avoid logging into sensitive accounts.",
                    "Use a VPN to encrypt your data on public networks.",
                    "Turn off auto-connect for open networks when you’re out.",
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
                    "Don’t skip updates—they fix security bugs and keep you protected.",
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
                    "Don’t store backup codes in your inbox—keep them offline.",
                    "Consider hardware keys (e.g., YubiKey) for critical accounts."
                }
            },
            { "permission", new List<string>
                {
                    "Apps don’t need access to everything—review their permissions.",
                    "Turn off microphone or camera access if not needed.",
                    "Remove app permissions you don’t recognize or use.",
                    "Revoke location access for apps that don’t require it.",
                    "Check permissions again after each app update."
                }
            },
            { "convo", new List<string>
                {
                    "Let’s talk! Try asking about privacy, scams, or any online danger.",
                    "I’m here to chat and help you stay secure online.",
                    "Want a tip or just some cyber-chitchat? I’ve got you.",
                    "Feel free to ask about any security concern on your mind.",
                    "What’s up? Need help with a specific cybersecurity issue?"
                }
            },
            { "conversation", new List<string>
                {
                    "Let’s talk! Try asking about privacy, scams, or any online danger.",
                    "I’m here to chat and help you stay secure online.",
                    "Want a tip or just some cyber-chitchat? I’ve got you.",
                    "Feel free to ask about any security concern on your mind.",
                    "What’s up? Need help with a specific cybersecurity issue?"
                }
            },
            { "how are you", new List<string>
                {
                    "I’m doing great and ready to help you stay safe online!",
                    "Always vigilant, always cyber-secure!",
                    "Feeling firewalled and fabulous—thanks for asking!",
                    "I’m fine—just scanning for threats and ready to chat.",
                    "All systems green! How can I assist you today?"
                }
            },
            { "what can i ask you", new List<string>
                {
                    "You can ask about scams, privacy, social media, VPNs and more.",
                    "Ask me about staying safe online, phishing, passwords—anything cybersecurity.",
                    "I’ve got tips on malware, backups, updates, and way more!",
                    "Try typing 'scams', 'passwords', or any topic on the list.",
                    "Wondering what else? Type 'help' to see all commands."
                }
            },
            { "what can i ask", new List<string>
                {
                    "You can ask about scams, privacy, social media, VPNs and more.",
                    "Ask me about staying safe online, phishing, passwords—anything cybersecurity.",
                    "I’ve got tips on malware, backups, updates, and way more!",
                    "Try typing 'scams', 'passwords', or any topic on the list.",
                    "Wondering what else? Type 'help' to see all commands."
                }
            }
        };

        /// <summary>
        /// Attempts to play the introduction audio file synchronously.
        /// Returns an error message if missing or if playback fails; otherwise returns empty string.
        /// </summary>
        public string TryPlayIntroductionAudio(string fileName)
        {
            try
            {
                string fullPath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
                if (File.Exists(fullPath))
                {
                    new SoundPlayer(fullPath).PlaySync();
                    return string.Empty;
                }
                else
                {
                    return $"Audio file not found: {fileName}";
                }
            }
            catch (Exception ex)
            {
                return $"Error playing audio: {ex.Message}";
            }
        }

        /// <summary>
        /// Returns a string of dashes and a label, exactly as a console "divider" would.
        /// </summary>
        public string GetDivider(string label)
        {
            var sb = new StringBuilder();
            sb.AppendLine();
            sb.AppendLine(new string('-', 40));
            sb.AppendLine($"--- {label} ---");
            sb.AppendLine(new string('-', 40));
            sb.AppendLine();
            return sb.ToString().TrimEnd();
        }

        /// <summary>
        /// Returns the initial name prompt (no "Cypher:" prefix here; WPF will add it).
        /// </summary>
        public string GetNamePrompt()
        {
            return "What’s your name?";
        }

        /// <summary>
        /// Returns greeting + topics + help text after user enters their name.
        /// </summary>
        public string GetWelcomeAndTopics(string userName)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Hello, {userName}! I’m Cypher, your online safety buddy. Let’s keep the web safe together!");
            sb.AppendLine();
            sb.AppendLine(GetTopicsList());
            sb.AppendLine();
            sb.AppendLine("Type 'help' for help, or 'exit' to leave the chat anytime.");
            return sb.ToString().TrimEnd();
        }

        /// <summary>
        /// Builds and returns the multi‐line list of topics.
        /// </summary>
        public string GetTopicsList()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Topics you can ask me about:");
            sb.AppendLine("  [1] Social Media Safety");
            sb.AppendLine("  [2] Phishing Awareness");
            sb.AppendLine("  [3] Strong Passwords");
            sb.AppendLine("  [4] Avoiding Scams");
            sb.AppendLine("  [5] Wi-Fi Security");
            sb.AppendLine("  [6] Device Protection");
            sb.AppendLine("  [7] Staying Safe Online");
            sb.AppendLine("  [8] Privacy Settings");
            sb.AppendLine("  [9] Software Updates");
            sb.AppendLine("  [10] Malware Protection");
            sb.AppendLine("  [11] Using a VPN");
            sb.AppendLine("  [12] Backing Up Your Data");
            sb.AppendLine("  [13] Two-Factor Authentication");
            sb.AppendLine("  [14] App Permissions");
            return sb.ToString().TrimEnd();
        }

        /// <summary>
        /// Returns a random follow‐up prompt (e.g. "How can I help you this time, {userName}?").
        /// </summary>
        public string GetRandomPrompt(string userName)
        {
            var rand = new Random();
            return string.Format(promptVariants[rand.Next(promptVariants.Count)], userName);
        }

        /// <summary>
        /// Main query handler (returns a multi‐line response). Does not prefix "Cypher:".
        /// </summary>
        public string HandleUserQuery(string rawInput, string userName)
        {
            string input = rawInput.ToLower().Trim();
            var sb = new StringBuilder();

            // If the user types "exit" or "help", we let WPF handle that outside.
            if (input == "exit" || input == "help")
                return string.Empty;

            // Sentiment detection
            string detectedEmotion = DetectSentiment(input);
            if (detectedEmotion != null)
            {
                sb.AppendLine($"I understand you’re feeling {detectedEmotion}. Cybersecurity can feel overwhelming—here’s a tip:");
                sb.AppendLine(GetRandomTip(detectedEmotion));
                return sb.ToString().TrimEnd();
            }

            // Definition requests
            if (TryGetDefinition(input, out string definition))
            {
                sb.AppendLine(definition);
                return sb.ToString().TrimEnd();
            }

            // Clarification (“more details”)
            if (clarificationTriggers.Any(phrase => input.Contains(phrase)))
            {
                if (currentTopic != null && keywordTips.ContainsKey(currentTopic))
                {
                    sb.AppendLine(GetNextTipForCurrentTopic());
                }
                else
                {
                    sb.AppendLine("Could you clarify which topic you’d like more details on?");
                }
                return sb.ToString().TrimEnd();
            }

            // Favorite‐topic detection
            if (TryDetectFavoriteTopic(input))
            {
                sb.AppendLine($"Got it! I’ll remember you’re interested in {favoriteTopic}.");
                sb.AppendLine($"Would you like another {favoriteTopic} tip? (yes/no)");
                return sb.ToString().TrimEnd();
            }

            // “Bored” / small‐talk triggers: offer favorite topic if set
            if (IsBored(input) && favoriteTopic != null)
            {
                sb.AppendLine($"Would you like another {favoriteTopic} tip? (yes/no)");
                return sb.ToString().TrimEnd();
            }

            // New topic selected
            string matchedTopic = GetMatchedTopic(input);
            if (matchedTopic != null)
            {
                currentTopic = matchedTopic;
                usedTipIndices.Clear();
                sb.AppendLine(GetRandomTip(matchedTopic));
                return sb.ToString().TrimEnd();
            }

            // Fallback
            sb.AppendLine("Sorry, I don’t have info on that right now. Try asking about scams, privacy, or social media safety.");
            return sb.ToString().TrimEnd();
        }

        /// <summary>
        /// Handles “yes/no” after favorite‐topic prompt.
        /// </summary>
        public string HandleFavoriteTipContinuation(string rawInput)
        {
            string input = rawInput.ToLower().Trim();
            if (input == "yes" || input == "y")
            {
                return GetRandomTip(favoriteTopic);
            }
            else
            {
                return "No worries! Let me know if you want tips on anything else.";
            }
        }

        //─── Helper Methods ────────────────────────────────────────────────────

        private string DetectSentiment(string input)
        {
            foreach (var word in sentimentKeywords)
                if (input.Contains(word))
                    return word;
            return null;
        }

        private bool TryGetDefinition(string input, out string definition)
        {
            string[] definitionTriggers = { "what is", "define", "meaning of", "explain", "whats" };
            foreach (var trigger in definitionTriggers)
            {
                if (input.Contains(trigger))
                {
                    foreach (var topic in topicDefinitions.Keys)
                    {
                        if (input.Contains(topic.ToLower()))
                        {
                            definition = topicDefinitions[topic];
                            return true;
                        }
                    }
                }
            }
            definition = null;
            return false;
        }

        private bool TryDetectFavoriteTopic(string input)
        {
            string[] triggers = { "favorite", "favourite", "interested in", "like", "love" };
            foreach (var t in triggers)
            {
                if (input.Contains(t))
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
            string[] bored = { "hello", "hi", "hey", "greetings", "whats up", "ok", "cool", "bored", "boring", "idk", "i dont know" };
            return bored.Any(g => input.Contains(g));
        }

        private string GetMatchedTopic(string input)
        {
            foreach (var topic in keywordTips.Keys)
                if (input.Contains(topic))
                    return topic;
            return null;
        }

        private string GetNextTipForCurrentTopic()
        {
            var tips = keywordTips[currentTopic];
            var available = Enumerable.Range(0, tips.Count).Where(i => !usedTipIndices.Contains(i)).ToList();
            if (!available.Any())
            {
                usedTipIndices.Clear();
                available = Enumerable.Range(0, tips.Count).ToList();
            }
            int idx = new Random().Next(available.Count);
            usedTipIndices.Add(available[idx]);
            return tips[available[idx]];
        }

        private string GetRandomTip(string topic)
        {
            if (!keywordTips.ContainsKey(topic))
                return $"Sorry, I don’t have tips for {topic} right now.";
            var tips = keywordTips[topic];
            var availableIndices = Enumerable.Range(0, tips.Count).Except(usedTipIndices).ToList();
            if (!availableIndices.Any())
            {
                usedTipIndices.Clear();
                availableIndices = Enumerable.Range(0, tips.Count).ToList();
            }
            int index = new Random().Next(availableIndices.Count);
            usedTipIndices.Add(index);
            return tips[index];
        }
    }
}
