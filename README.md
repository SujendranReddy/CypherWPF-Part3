
---------------------------------------------------------------------------------
Cypher - Cybersecurity Chatbot
---------------------------------------------------------------------------------
Description

Cypher is a simple WPF application built in C#, an interactive chatbot that educates users on cybersecurity. Some of its features include: text-based interaction, voice greeting, and personalized responses. 

YOUTUBE LINK: https://youtu.be/otpbwrzPhdc?si=m9S2vpItxuCLmxvW 
---------------------------------------------------------------------------------
Main Goals:
---------------------------------------------------------------------------------
-Provide cybersecurity awareness in a friendly, conversational format.

-Encourage good digital habits for non-technical users.

-Make learning about safety online interactive and accessible.

---------------------------------------------------------------------------------
Challenges Faced:
---------------------------------------------------------------------------------
Designing a natural response system for a console app.

Simulating natural responses without AI or external NLP tools.

---------------------------------------------------------------------------------
How to Install and Run the Project
---------------------------------------------------------------------------------
Prerequisites

-Windows OS

-.NET SDK installed

-Optional Visual Studio

Steps

1.Clone the repository or download the source code.

2.Ensure the audio file (Cypher Chatbot.wav) is in the same directory/floder as the .exe.

3. Build and run. 

---------------------------------------------------------------------------------
How to Use the Project
---------------------------------------------------------------------------------
Once launched, Cypher will:

1.Greet you with a banner.

2.Play the intro audio.

3.Ask for your name.

4.Greet you, provide a brief description and present the main topics.

5.Prompt how can I assist you. 

---------------------------------------------------------------------------------
New Features
---------------------------------------------------------------------------------

## Core Features  
- **Task Management**  
  - **Add**, **complete**, **view**, and **delete** security-related tasks (“add task update passwords”, “complete task 3”, “view tasks”).  
  - Optional reminders after each task addition.  
  - Tasks persist only for the session but are tracked in the activity log.

- **Quiz System**  
  - A gamified multiple-choice quiz that tests your cybersecurity knowledge (phishing, passwords, networks, etc.).  
  - Questions are drawn from a built-in question bank and scored in real time.  
  - Exit or replay the quiz anytime with simple commands (`start quiz`, `exit quiz`).

- **Activity Log**  
  - Automatically records key actions: task additions, completions, deletions, and quiz attempts.  
  - Summarize your session at any point with `show activity log` or natural variants (“can you show me my activity log?”).  
  - Gives you a quick overview of what you’ve done today—perfect for reviewing your progress.

### Task Commands  
- `add task <description>` – Create a new task.  
- `complete task <number>` – Mark task # as done.  
- `delete task <number>` – Remove a task.  
- `view tasks` / `show tasks` / `list tasks` – List all current tasks.  

> After adding, Cypher will ask “Would you like a reminder for this task? (yes/no)”.

### Quiz Commands  
- `start quiz` – Begin a multiple-choice cybersecurity quiz.  
- `exit quiz` / `quit quiz` – Stop the quiz and return to normal chat.  
- Answer each question by typing the letter (`A`, `B`, `C`, or `D`).  

### Activity Log Commands  
- `show activity log` – Display everything you’ve done (tasks & quizzes).  
- Variants like “can you show me my activity log?” also work.

### Help & Exit  
- `help` – Display all available commands and natural-language tips.  
- `exit` – Close the chatbot safely.

## Example Session  
```text
Cypher: Hello! What’s your name?  
User: Alex  
Cypher: Welcome, Alex! How can I assist you today?  

User: add task change default passwords  
Cypher: Task added – “change default passwords”. Would you like a reminder for this task? (yes/no)  
User: yes  
Cypher: Reminder set for task #1.  

User: start quiz  
Cypher: Question 1 – What is phishing?  
  A) Secure login method  
  B) Fraudulent email attack  
  C) Network protocol  
  D) Hardware device  
User: B  
Cypher: Correct! 🎉  

User: show activity log  
Cypher:  
  • Task added: “change default passwords”  
  • Reminder set for task #1  
  • Quiz started (1 question answered) ## Core Features  
- **Task Management**  
  - **Add**, **complete**, **view**, and **delete** security-related tasks (“add task update passwords”, “complete task 3”, “view tasks”).  
  - Optional reminders after each task addition.  
  - Tasks persist only for the session but are tracked in the activity log.

- **Quiz System**  
  - A gamified multiple-choice quiz that tests your cybersecurity knowledge (phishing, passwords, networks, etc.).  
  - Questions are drawn from a built-in question bank and scored in real time.  
  - Exit or replay the quiz anytime with simple commands (`start quiz`, `exit quiz`).

- **Activity Log**  
  - Automatically records key actions: task additions, completions, deletions, and quiz attempts.  
  - Summarize your session at any point with `show activity log` or natural variants (“can you show me my activity log?”).  
  - Gives you a quick overview of what you’ve done today—perfect for reviewing your progress.

### Task Commands  
- `add task <description>` – Create a new task.  
- `complete task <number>` – Mark task # as done.  
- `delete task <number>` – Remove a task.  
- `view tasks` / `show tasks` / `list tasks` – List all current tasks.  

> After adding, Cypher will ask “Would you like a reminder for this task? (yes/no)”.

### Quiz Commands  
- `start quiz` – Begin a multiple-choice cybersecurity quiz.  
- `exit quiz` / `quit quiz` – Stop the quiz and return to normal chat.  
- Answer each question by typing the letter (`A`, `B`, `C`, or `D`).  

### Activity Log Commands  
- `show activity log` – Display everything you’ve done (tasks & quizzes).  
- Variants like “can you show me my activity log?” also work.

### Help & Exit  
- `help` – Display all available commands and natural-language tips.  
- `exit` – Close the chatbot safely.

## Example Session  
```text
Cypher: Hello! What’s your name?  
User: Alex  
Cypher: Welcome, Alex! How can I assist you today?  

User: add task change default passwords  
Cypher: Task added – “change default passwords”. Would you like a reminder for this task? (yes/no)  
User: yes  
Cypher: Reminder set for task #1.  

User: start quiz  
Cypher: Question 1 – What is phishing?  
  A) Secure login method  
  B) Fraudulent email attack  
  C) Network protocol  
  D) Hardware device  
User: B  
Cypher: Correct! 🎉  

User: show activity log  
Cypher:  
  • Task added: “change default passwords”  
  • Reminder set for task #1  
  • Quiz started (1 question answered) 
