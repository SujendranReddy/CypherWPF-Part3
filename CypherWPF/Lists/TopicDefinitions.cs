using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CypherWPF
{
    //List of definitions
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
    { "app permissions", "App Permissions control what data and features an app can access on your device, like location or camera." },
    { "encryption", "Encryption is the process of converting data into a code to prevent unauthorized access." },
    { "firewall", "A Firewall is a security system that monitors and controls incoming and outgoing network traffic." },
    { "identity theft", "Identity Theft occurs when someone steals your personal information to impersonate you for fraud or other crimes." },
    { "spam", "Spam refers to unsolicited and often deceptive messages sent in bulk, usually via email or text." },
    { "social engineering", "Social Engineering is the use of manipulation to trick people into giving up confidential information." },
    { "browser security", "Browser Security involves using secure settings and extensions to protect against malicious websites and trackers." },
    { "cyberbullying", "Cyberbullying is bullying or harassment that happens through digital devices like phones, computers, or tablets." },
    { "dark web", "The Dark Web is a part of the internet that's not indexed by search engines and is often used for anonymous activity." },
    { "password manager", "A Password Manager is a tool that securely stores and generates strong passwords for all your accounts." },
    { "ransomware", "Ransomware is a type of malware that locks your data and demands payment to restore access." },
    { "security patches", "Security Patches are software updates that fix vulnerabilities exploited by hackers." },
    { "secure websites", "Secure Websites use HTTPS to encrypt communication between your browser and the site’s server." },
    { "online tracking", "Online Tracking is the practice of collecting data about your behavior across websites and apps." },
    { "cookies", "Cookies are small files stored by websites on your device to remember your preferences or track activity." },
    { "zero-day", "A Zero-Day is a security flaw that hackers exploit before the software developer has released a fix." },
    { "clickjacking", "Clickjacking is a trick where users are fooled into clicking something different from what they think, often for malicious purposes." },
    { "data breach", "A Data Breach is an incident where sensitive, protected, or confidential data is accessed or disclosed without authorization." },
    { "antivirus", "Antivirus software detects and removes malicious programs to protect your devices." },
    { "multi-factor authentication", "Multi-Factor Authentication is a security process requiring two or more forms of verification to access an account." },
    { "two-step verification", "Two-Step Verification is a type of authentication requiring a password and a separate method like a phone code." }
};
    }
}