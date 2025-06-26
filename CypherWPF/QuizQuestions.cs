using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CypherWPF
{
    class QuizQuestions
    {
        public static readonly List<(string Question, string[] Options, int Correct)> quizQuestions =
            new List<(string Question, string[] Options, int Correct)>    
            {
        (
            "What does 'phishing' mean?",
            new[] { "A type of malware", "Tricking someone to reveal personal info",
                    "A firewall setting", "An encryption method" },
            1
        ),
        (
            "Which is the strongest password?",
            new[] { "password123", "Qwerty", "7&F!gT$9vL@", "abc123" },
            2
        ),
        (
            "Public Wi-Fi is safest when you …",
            new[] { "Use a VPN", "Disable Bluetooth", "Lower the screen brightness", "Turn on airplane mode" },
            0
        ),
        (
            "Two-factor authentication requires …",
            new[] { "Two passwords", "Password + second proof",
                    "Twice the bandwidth", "A paid subscription" },
            1
        ),
        (
            "A VPN primarily …",
            new[] { "Speeds up downloads", "Encrypts your internet traffic",
                    "Blocks pop-ups", "Fixes Wi-Fi dead spots" },
            1
        ),
        (
            "A good way to spot a fake email is …",
            new[] { "Look for typos and odd grammar", "Check if it’s colourful",
                    "See if it’s marked 'urgent'", "Count the attachments" },
            0
        ),
        (
            "Backing up data protects against …",
            new[] { "Hardware failure", "Ransomware", "Accidental deletion", "All of the above" },
            3
        ),
        (
            "Which file extension is most likely a Windows executable?",
            new[] { ".jpg", ".exe", ".txt", ".pdf" },
            1
        ),
        (
            "A strong passphrase is …",
            new[] { "Short and complex", "Long and memorable", "Name + birthdate", "Same as Wi-Fi password" },
            1
        ),
        (
            "Software updates are important because they …",
            new[] { "Add new colours", "Fix security vulnerabilities",
                    "Increase battery life", "Erase cookies" },
            1
        )
    };
    }
}
