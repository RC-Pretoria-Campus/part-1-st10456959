using System;
using System.Media;
using System.Threading;
using System.IO;

namespace ChatBot2._0
{
    public class User
    {
        public string Name { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();

            DisplayAsciiArt();
            DisplayWelcomeMessage();

            string audioFilePath = "audio.wav"; // Ensure this file exists
            PlayWelcomeAudio(audioFilePath);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n💡 I'm here to help you stay safe online.");
            Console.WriteLine("📌 Feel free to ask me about cybersecurity best practices.");
            Console.ResetColor();
            Console.WriteLine("\n==================================================");

            User user = new User();
            user = GetUserDetails(user);

            StartTextChat(user);

            Console.Beep();
        }

        static void DisplayAsciiArt()
        {
            string asciiArt = @"
  ____ ____ ____ ____ ____ ____ 
 ||C |||y |||b |||e |||r |||S ||
 ||__|||__|||__|||__|||__|||__||
 |/__\|/__\|/__\|/__\|/__\|/__\|";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(asciiArt);
            Console.ResetColor();
        }

        static void DisplayWelcomeMessage()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n==================================================");
            Console.WriteLine("|          WELCOME TO YOUR CYBERSECURITY ASSISTANT!          |");
            Console.WriteLine("==================================================");
            Console.ResetColor();
        }

        static void PlayWelcomeAudio(string audioFilePath)
        {
            try
            {
                if (File.Exists(audioFilePath))
                {
                    SoundPlayer player = new SoundPlayer(audioFilePath);
                    player.PlaySync();
                    Console.WriteLine("\n🔊 Audio greeting played.");
                }
                else
                {
                    Console.WriteLine("\n⚠️ Audio file not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error playing audio: {ex.Message}");
            }
        }

        static User GetUserDetails(User user)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Please enter your name: ");
            Console.ResetColor();

            string inputName = Console.ReadLine()?.Trim();
            while (string.IsNullOrEmpty(inputName))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Name cannot be empty. Please enter your name: ");
                Console.ResetColor();
                inputName = Console.ReadLine()?.Trim();
            }

            user.Name = inputName;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\nHello, {user.Name}! Let's get started.");
            Console.ResetColor();
            return user;
        }

        static void StartTextChat(User user)
        {
            bool continueChat = true;
            bool checkHowAreYouFollowUp = false;

            while (continueChat)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n====================================");
                Console.WriteLine(" Options: ");
                Console.WriteLine("====================================");
                Console.ResetColor();

                Console.WriteLine("Type one of the following:");
                Console.WriteLine("- topics");
                Console.WriteLine("- how are you?");
                Console.WriteLine("- what is your purpose?");
                Console.WriteLine("- what can I ask you about?");
                Console.WriteLine("- exit");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("You: ");
                Console.ResetColor();

                string userInput = Console.ReadLine()?.Trim().ToLower();

                if (string.IsNullOrEmpty(userInput))
                {
                    Respond("I didn't catch that. Could you please rephrase?");
                    continue;
                }

                if (checkHowAreYouFollowUp)
                {
                    if (userInput.Contains("good") || userInput.Contains("fine") || userInput.Contains("well"))
                    {
                        Respond("That's great to hear! Do you need any assistance? (yes/no)");
                        string assist = Console.ReadLine()?.Trim().ToLower();
                        if (assist == "no")
                        {
                            Respond("Okay! Stay safe online. Goodbye!");
                            break;
                        }
                        else if (assist == "yes")
                        {
                            checkHowAreYouFollowUp = false;
                            continue;
                        }
                        else
                        {
                            Respond("I'll take that as a no. Stay safe online!");
                            break;
                        }
                    }
                    else
                    {
                        Respond("Thanks for sharing! Do you need any help today? (yes/no)");
                        checkHowAreYouFollowUp = false;
                        continue;
                    }
                }

                switch (userInput)
                {
                    case "exit":
                        Respond("Stay vigilant and protect your online presence! Goodbye!");
                        continueChat = false;
                        break;

                    case "topics":
                        ShowTopics();
                        break;

                    case "how are you?":
                        Respond("I'm good, how are you?");
                        checkHowAreYouFollowUp = true;
                        break;

                    case "what is your purpose?":
                        Respond("🤖 My purpose is to help you with cybersecurity by providing tips and best practices.");
                        break;

                    case "what can i ask you about?":
                        Respond("💡 You can ask me about phishing emails, strong passwords, and recognizing suspicious links!");
                        break;

                    default:
                        Respond("🤖 I specialize in cybersecurity! Ask me about phishing, passwords, or suspicious links.");
                        break;
                }
            }
        }

        static void ShowTopics()
        {
            string topics = "\n📚 I can help with the following topics:\n" +
                            "- 🎣 Phishing emails\n" +
                            "- 🔐 Strong password practices\n" +
                            "- 🚨 Recognizing suspicious links";
            Respond(topics);
        }

        static void Respond(string message)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"\nChatbot: {message}\n");
            Console.ResetColor();
        }
    }
}

