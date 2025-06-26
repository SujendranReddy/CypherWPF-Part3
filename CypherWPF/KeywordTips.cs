using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CypherWPF
{
    class KeywordTips
    {
        public static Dictionary<string, List<string>> keywordTips = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase)
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
    }
}
