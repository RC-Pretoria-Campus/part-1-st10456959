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
        static bool awaitingHowAreYouReply = false;
        static bool awaitingAssistanceReply = false;

        static void Main(string[] args)
        {
            Console.Clear();
            DisplayAsciiArt();
            DisplayWelcomeMessage();

            string audioFilePath = "audio.wav";
            PlayWelcomeAudio(audioFilePath);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n💡 I'm here to help you stay safe online.");
            Console.WriteLine("📌 Feel free to ask me about cybersecurity best practices.");
            Console.ResetColor();
            Console.WriteLine("\n==================================================");

            User user = new User();
            user = GetUserDetails(user);
            StartTextChat(user);
        }

        static void DisplayAsciiArt()
        {
            string asciiArt = @"
  ____ ____ ____ ____ ____ ____ 
 ||C |||y |||b |||e |||r |||S ||
 ||__|||__|||__|||__|||__|||__||
 |/__\|/__\|/__\|/__\|/__\|/__\|
";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(asciiArt);
            Console.ResetColor();
        }

        static void DisplayWelcomeMessage()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n==================================================");
            Console.WriteLine("|      WELCOME TO YOUR CYBERSECURITY ASSISTANT!     |");
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
                    Console.WriteLine("\nAudio loaded and playing...");
                }
                else
                {
                    Console.WriteLine("\nOops! Audio file not found.");
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
            while (true)
            {
                if (!awaitingHowAreYouReply && !awaitingAssistanceReply)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\n====================================");
                    Console.WriteLine(" Options: ");
                    Console.WriteLine("====================================");
                    Console.ResetColor();
                    Console.WriteLine("Type 'topics' to view topics, 'exit' to quit, 'how are you?', 'what is your purpose?', or 'what can I ask you about?'");
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("You: ");
                Console.ResetColor();

                string userInput = Console.ReadLine()?.Trim().ToLower();
                if (string.IsNullOrEmpty(userInput))
                {
                    Respond("I didn't catch that. Could you please rephrase?");
                    continue;
                }

                if (awaitingHowAreYouReply)
                {
                    if (userInput.Contains("good") || userInput.Contains("fine") || userInput.Contains("okay"))
                    {
                        Respond("Glad to hear that! Do you need any assistance? (yes/no)");
                        awaitingHowAreYouReply = false;
                        awaitingAssistanceReply = true;
                        continue;
                    }
                    else
                    {
                        Respond("Thanks for sharing! Do you need any assistance? (yes/no)");
                        awaitingHowAreYouReply = false;
                        awaitingAssistanceReply = true;
                        continue;
                    }
                }
                else if (awaitingAssistanceReply)
                {
                    if (userInput == "yes")
                    {
                        awaitingAssistanceReply = false;
                        continue; // Show options again
                    }
                    else if (userInput == "no")
                    {
                        Respond("Alright, have a safe and secure day!");
                        break;
                    }
                    else
                    {
                        Respond("Please reply with 'yes' or 'no'.");
                        continue;
                    }
                }

                switch (userInput)
                {
                    case "exit":
                        Respond("Stay vigilant and protect your online presence! Have a great day!");
                        return;
                    case "topics":
                        ShowTopics();
                        break;
                    case "how are you?":
                        Respond("I'm good, how are you?");
                        awaitingHowAreYouReply = true;
                        break;
                    case "what is your purpose?":
                        Respond("🤖 My purpose is to help you with cybersecurity by providing tips and best practices.");
                        break;
                    case "what can i ask you about?":
                        Respond("💡 You can ask me about phishing emails, strong passwords, and recognizing suspicious links!");
                        break;
                    default:
                        Respond(GenerateResponse(userInput));
                        break;
                }
            }
        }

        static string GenerateResponse(string userInput)
        {
            if (userInput.Contains("phishing") || userInput.Contains("email"))
            {
                return "🚨 *Phishing scams can be tricky to spot!*\n" +
                       "- ⚠️ Beware of emails that create urgency.\n" +
                       "- 🔗 Never click on suspicious links.\n" +
                       "- 📧 Verify the sender's email carefully.";
            }
            else
            {
                return "🤖 I specialize in cybersecurity! Ask about:\n" +
                       "- 🎣 Phishing emails\n" +
                       "- 🔑 Strong password practices\n" +
                       "- 🚨 Suspicious links";
            }
        }

        static void ShowTopics()
        {
            string topics = "\n📚 I can help with the following topics:\n" +
                            "- 🎣 Phishing emails\n" +
                            "- 🔑 Strong password practices\n" +
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

