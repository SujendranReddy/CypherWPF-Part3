using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CypherWPF
{
    //List of possible words users might say
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
            "Phishing scams try to steal your info via fake emails or websites.",
            "Never click links in unsolicited emails without verifying them first.",
            "Look out for generic greetings and poor grammar as phishing signs.",
            "Use anti-phishing browser extensions to get extra protection.",
            "Report phishing attempts to help protect others."
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
    { "password manager", new List<string>
        {
            "Password managers generate and store strong, unique passwords for you.",
            "Using a password manager reduces the risk of reused or weak passwords.",
            "Choose a reputable password manager with strong encryption.",
            "Most password managers can autofill your login info safely.",
            "Regularly update your master password and enable 2FA on your manager."
        }
    },
    { "scam", new List<string>
        {
            "If it sounds too good to be true, it probably is.",
            "Stick to trusted websites and don't give away info to strangers online.",
            "Always verify the legitimacy of offers and requests before taking action.",
            "Check for secure URLs (https://) and padlock icons before entering details.",
            "Research a company or individual before sending money or personal data.",
            "Don’t trust callers pressuring you for urgent payments.",
            "Keep your software updated to avoid falling victim to new scams."
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
            "Use browser extensions to block trackers and ads.",
            "Check your social media privacy settings regularly.",
            "Limit the personal info you share online.",
            "Delete cookies and clear browser history to reduce tracking."
        }
    },
    { "update", new List<string>
        {
            "Don't skip updates—they fix security bugs and keep you protected.",
            "Enable auto-updates for your OS and apps to stay current.",
            "Software updates often patch vulnerabilities hackers exploit.",
            "Review change logs to understand what updates address.",
            "Schedule updates during off-hours to avoid interruptions.",
            "Ignore update prompts at your own risk!",
            "Updates often improve performance and add new features too."
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
            "Automate backup schedules to reduce manual effort.",
            "Regular backups protect you from data loss and ransomware.",
            "Keep backups offline or disconnected from your network for safety."
        }
    },
    { "two-factor authentication", new List<string>
        {
            "2FA adds an extra layer of security by requiring a second form of ID.",
            "Enable Two-Factor Authentication on all important accounts.",
            "Use authenticator apps instead of SMS for better security.",
            "Don't store backup codes in your inbox—keep them offline.",
            "Consider hardware keys like YubiKey for critical accounts."
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
            "All systems green! How can I assist you today?",
            "I'm wired to help you stay safe online! How can I assist?",
            "Doing great, thanks! Ready to fight cyber threats with you.",
            "Always alert and ready to chat about security!",
            "I'm good! Keeping the cyberworld safe one byte at a time."
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
    { "spam", new List<string>
        {
            "Spam emails often try to trick you into clicking dangerous links.",
            "Never reply to spam or click on links inside them.",
            "Use your email provider's spam filter and report suspicious messages.",
            "Be extra cautious of emails promising unrealistic offers.",
            "Spam can also come through texts and social media—stay alert."
        }
    },
    { "spam calls", new List<string>
        {
            "Don’t answer calls from unknown numbers.",
            "Register your number on the national Do Not Call list if available.",
            "Use call-blocking apps to filter suspected spam calls.",
            "Never provide personal info to unsolicited callers.",
            "If unsure, let calls go to voicemail and verify before calling back."
        }
    },
    { "identity theft", new List<string>
        {
            "Keep your personal info secure to prevent identity theft.",
            "Shred sensitive documents before disposal.",
            "Use strong, unique passwords and change them regularly.",
            "Monitor your bank and credit reports for suspicious activity.",
            "Be cautious about what you share on social media."
        }
    },
    { "cyberbullying", new List<string>
        {
            "If you or someone you know is cyberbullied, report it immediately.",
            "Block and report abusive users on social media platforms.",
            "Keep evidence like screenshots for reporting.",
            "Don’t respond to bullies—they thrive on reactions.",
            "Talk to a trusted adult or counselor if cyberbullying happens."
        }
    },
    { "dark web", new List<string>
        {
            "The dark web is a part of the internet not indexed by search engines.",
            "It’s often used for anonymous activity, both legal and illegal.",
            "Be cautious—personal info can be traded there by criminals.",
            "Avoid accessing the dark web unless you know exactly what you’re doing.",
            "Use privacy tools if you must visit, but it’s generally safer to stay away."
        }
    },
    { "password strength", new List<string>
        {
            "Use long passwords (12+ characters) mixing letters, numbers, and symbols.",
            "Avoid common words or patterns like '1234' or 'password'.",
            "Passphrases are easier to remember and often more secure.",
            "Change passwords regularly and don’t reuse them.",
            "Check your passwords against breach databases online."
        }
    },
    { "social engineering", new List<string>
        {
            "Social engineering tricks you into giving away sensitive info.",
            "Be skeptical of unsolicited calls or emails asking for details.",
            "Verify identities before sharing any private information.",
            "Don’t fall for pressure tactics or urgent requests.",
            "Awareness is your best defense against social engineering."
        }
    },
    { "browser security", new List<string>
        {
            "Keep your browser updated to patch security holes.",
            "Use extensions that block trackers and ads.",
            "Enable phishing and malware protection in browser settings.",
            "Avoid downloading unknown plugins or extensions.",
            "Clear your cache and cookies regularly."
        }
    },
    { "spam filters", new List<string>
        {
            "Spam filters help keep unwanted emails out of your inbox.",
            "Mark unwanted emails as spam to train the filter.",
            "Check your spam folder regularly to avoid missing legit messages.",
            "Adjust filter sensitivity in your email settings if needed.",
            "Use multiple layers of spam protection for best results."
        }
    },
    { "tell me a joke", new List<string>
        {
            "Why do programmers prefer dark mode? Because light attracts bugs!",
            "Why did the hacker break up with the internet? Too many connections!",
            "Why was the computer cold? It left its Windows open!",
            "How many cybersecurity experts does it take to change a lightbulb? None, they just patch the vulnerability.",
            "What do you call 8 hobbits? A hobbyte!"
        }
    },
    { "chat", new List<string>
        {
            "I’m here to chat whenever you want to talk cybersecurity or just chill.",
            "Let's talk about how to keep your digital life secure!",
            "Got questions or just want to say hi? I'm all ears.",
            "We can talk tips, tricks, or even some cyber jokes.",
            "I'm your friendly neighborhood cybersecurity bot."
        }
    },
    { "thank you", new List<string>
        {
            "You’re welcome! Stay safe out there.",
            "Anytime! Cybersecurity is a team effort.",
            "Glad I could help! Let me know if you need anything else.",
            "No problem! Keep those passwords strong.",
            "Happy to assist! Protect yourself and your data."
        }
    },
    { "ransomware", new List<string>
    {
        "Ransomware encrypts your files and demands payment for the key.",
        "Never pay ransom; instead, restore files from backups.",
        "Keep software updated to prevent ransomware infections.",
        "Use reputable antivirus with ransomware protection.",
        "Be cautious opening email attachments or links from unknown sources."
    }
},
{ "iot security", new List<string>
    {
        "Change default passwords on IoT devices immediately.",
        "Keep IoT device firmware updated regularly.",
        "Segment IoT devices on a separate network to limit access.",
        "Disable unused features on IoT devices to reduce vulnerabilities.",
        "Monitor network traffic for unusual activity from IoT devices."
    }
},
{ "physical security", new List<string>
    {
        "Lock your devices when unattended to prevent unauthorized access.",
        "Don’t leave devices unattended in public places.",
        "Use privacy screens on laptops to prevent shoulder surfing.",
        "Shred sensitive documents instead of throwing them away.",
        "Secure your home router physically to prevent tampering."
    }
},
{ "data encryption", new List<string>
    {
        "Encrypt sensitive files before sharing or storing them.",
        "Use full disk encryption on laptops and mobile devices.",
        "Use strong, proven encryption standards like AES-256.",
        "Protect encryption keys separately and securely.",
        "Verify encrypted emails with trusted certificates or keys."
    }
},
{ "safe browsing", new List<string>
    {
        "Avoid clicking pop-up ads or suspicious banners.",
        "Do not download files from untrusted websites.",
        "Use a browser with built-in security features.",
        "Disable auto-downloads unless you trust the source.",
        "Regularly clear your browsing history and cache."
    }
},
{ "account recovery", new List<string>
    {
        "Set up account recovery options like backup email and phone.",
        "Choose recovery questions with answers only you know.",
        "Regularly review and update your recovery information.",
        "Be cautious of phishing attempts targeting recovery options.",
        "Use 2FA to protect your account recovery process."
    }
},
{ "email security", new List<string>
    {
        "Verify sender addresses before opening attachments.",
        "Be wary of unexpected requests for personal info.",
        "Use email encryption for sensitive communications.",
        "Don’t use the same password for email and other accounts.",
        "Enable spam and phishing filters in your email client."
    }
},
{ "software installation", new List<string>
    {
        "Only download software from official or trusted sources.",
        "Check digital signatures or certificates before installing.",
        "Avoid installing unnecessary software to reduce attack surface.",
        "Keep installed software updated with latest patches.",
        "Uninstall software you no longer use or trust."
    }
},
{ "social engineering techniques", new List<string>
    {
        "Be aware of pretexting where attackers invent a scenario to gain info.",
        "Recognize baiting attempts that lure you into unsafe actions.",
        "Never share passwords or codes over the phone or email.",
        "Question unsolicited requests for confidential information.",
        "Stay calm under pressure to avoid being manipulated."
    }
},
{ "cloud security", new List<string>
    {
        "Use strong authentication for cloud service accounts.",
        "Regularly review cloud permissions and access logs.",
        "Encrypt sensitive data before uploading to the cloud.",
        "Back up cloud data locally when possible.",
        "Beware of phishing attempts targeting cloud services."
    }
},
{ "mobile security", new List<string>
    {
        "Install apps only from official app stores.",
        "Review app permissions before and after installation.",
        "Use screen lock features such as PIN or biometric.",
        "Keep your mobile OS updated for security patches.",
        "Avoid connecting to public USB charging stations."
    }
},
{ "password reuse risks", new List<string>
    {
        "Reusing passwords increases risk of multiple account compromise.",
        "Change reused passwords immediately after a breach.",
        "Use unique passwords for high-value accounts like email and banking.",
        "Consider using passphrases instead of simple passwords.",
        "Check if your passwords have been exposed using online tools."
    }
},
{ "security awareness training", new List<string>
    {
        "Regularly update yourself on latest cybersecurity threats.",
        "Participate in phishing simulation exercises.",
        "Share security best practices with friends and coworkers.",
        "Use official resources to verify suspicious claims.",
        "Stay skeptical and question unexpected online requests."
    }
},
{ "browser extensions risks", new List<string>
    {
        "Only install extensions from trusted developers.",
        "Review permissions requested by browser extensions carefully.",
        "Regularly audit and remove unused or suspicious extensions.",
        "Be aware that extensions can collect your browsing data.",
        "Keep your extensions updated to patch security vulnerabilities."
    }
},
{ "public charging security", new List<string>
    {
        "Avoid using public USB charging ports to prevent juice jacking.",
        "Carry your own charger and plug into electrical outlets.",
        "Use a USB data blocker if you must use public charging.",
        "Be cautious when charging at airports, cafes, or public places.",
        "Keep your device’s screen locked while charging in public."
    }
},
{ "incident response", new List<string>
    {
        "Have a plan for responding to cybersecurity incidents.",
        "Immediately disconnect infected devices from the network.",
        "Document what happened and steps taken for future reference.",
        "Report incidents to relevant authorities or IT support.",
        "Learn from incidents to improve your security posture."
    }
},
{ "safe downloads", new List<string>
    {
        "Scan downloads with antivirus before opening.",
        "Avoid downloading cracked or pirated software.",
        "Verify file hashes when provided by the source.",
        "Prefer official app stores or developer websites.",
        "Beware of disguised executable files (e.g., .exe as .pdf)."
    }
},
{ "wifi password", new List<string>
    {
        "Use a strong, unique password for your Wi-Fi network.",
        "Change default router admin passwords immediately.",
        "Disable WPS to prevent easy unauthorized access.",
        "Regularly update router firmware to fix vulnerabilities.",
        "Consider hiding your SSID to make your network less visible."
    }
},
{ "network monitoring", new List<string>
    {
        "Use tools to monitor network traffic for suspicious activity.",
        "Set up alerts for unusual login or data transfer patterns.",
        "Regularly review connected devices to your network.",
        "Update router settings and passwords periodically.",
        "Consider segmenting your network to isolate devices."
    }
},
{ "whats up", new List<string>
        {
            "Cypher: Just scanning for threats and keeping you safe"
        }
    },
    { "whats good", new List<string>
        {
            "Cypher: All systems green and ready to assist"
        }
    },
    { "who are you", new List<string>
        {
            "Cypher: I’m your friendly cybersecurity companion"
        }
    },
    { "hows it going", new List<string>
        {
            "Cypher: Running smoothly and watching for vulnerabilities"
        }
    },
    { "hey there", new List<string>
        {
            "Cypher: Hello there ready to chat security"
        }
    },
    { "hello", new List<string>
        {
            "Cypher: Hi there let’s keep you cyber safe"
        }
    },
    { "sup", new List<string>
        {
            "Cypher: Staying alert and protecting your data"
        }
    },
    { "yo", new List<string>
        {
            "Cypher: Here to help you lock down your online life"
        }
    },
    { "tell me something", new List<string>
        {
            "Cypher: Regular updates are key to staying ahead of threats"
        }
    },
    { "just chatting", new List<string>
        {
            "Cypher: Happy to talk security or anything else on your mind"
        }
            }

};
    }
}