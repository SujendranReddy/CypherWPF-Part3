using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CypherWPF
{
    class TopicDefinitions
    {
        public static readonly Dictionary<string, string> topicDefinitions = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
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
    }
}
