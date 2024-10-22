using System;
using System.Threading;

class MindfulnessActivity
{
    public void StartActivity(string activityName, string description, ref int duration)
    {
        Console.WriteLine($"Welcome to the {activityName}.");
        Console.WriteLine(description);
        Console.Write("Enter the duration of the activity (in seconds): ");
        duration = int.Parse(Console.ReadLine());
        Console.WriteLine("Prepare to begin...");
        PauseWithSpinner(3); 
    }

    public void EndActivity(string activityName, int duration)
    {
        Console.WriteLine("Well done!");
        PauseWithSpinner(3); 
        Console.WriteLine($"You have completed the {activityName} for {duration} seconds.");
        PauseWithSpinner(3);
    }

    public void PauseWithSpinner(int seconds)
    {
        for (int i = 0; i < seconds; i++)
        {
            Console.Write("|");
            Thread.Sleep(250);
            Console.Write("\b/");  
            Thread.Sleep(250);
            Console.Write("\b-");
            Thread.Sleep(250);
            Console.Write("\b\\");
            Thread.Sleep(250);
            Console.Write("\b|");
        }
        Console.Write("\b "); 
    }

    public void PauseWithCountdown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write($"{i} ");
            Thread.Sleep(1000);
        }
        Console.WriteLine();
    }
}

//--------------------------BREATHING ACTIVITY-----------------------------
class BreathingActivity : MindfulnessActivity
{
    public void DoBreathingActivity()
    {
        int duration = 0;
        StartActivity("Breathing Activity", "This activity will help you relax by guiding your breathing.", ref duration);

        for (int i = 0; i < duration; i += 6) // Cambié a 6 para sincronizar con la duración
        {
            Console.WriteLine("Breathe in...");
            PauseWithCountdown(3); 
            Console.WriteLine("Breathe out...");
            PauseWithCountdown(3); 
        }

        EndActivity("Breathing Activity", duration);
    }
}

//---------------------------REFLECTION ACTIVITY-----------------------------
class ReflectionActivity : MindfulnessActivity
{
    private string[] prompts = {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private string[] questions = {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times when you were not as successful?",
        "What is your favorite thing about this experience?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?"
    };

    public void DoReflectionActivity()
    {
        int duration = 0;
        StartActivity("Reflection Activity", "This activity will help you reflect on your strengths and resilience.", ref duration);

        Random random = new Random();
        Console.WriteLine(prompts[random.Next(prompts.Length)]);

        for (int i = 0; i < duration; i += 5)
        {
            Console.WriteLine(questions[random.Next(questions.Length)]);
            PauseWithSpinner(5); 
        }

        EndActivity("Reflection Activity", duration);
    }
}

//---------------------------LISTING ACTIVITY-----------------------------
class ListingActivity : MindfulnessActivity
{
    private string[] prompts = {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    public void DoListingActivity()
    {
        int duration = 0;
        StartActivity("Listing Activity", "This activity will help you reflect on the good things in your life.", ref duration);

        Random random = new Random();
        Console.WriteLine(prompts[random.Next(prompts.Length)]);

        PauseWithCountdown(5); 

        int itemCount = 0;
        DateTime startTime = DateTime.Now;
        while ((DateTime.Now - startTime).TotalSeconds < duration)
        {
            Console.Write("Enter an item: ");
            Console.ReadLine();
            itemCount++;
        }

        Console.WriteLine($"You listed {itemCount} items.");
        EndActivity("Listing Activity", duration);
    }
}

class Program
{
    static void Main(string[] args)
    {
        BreathingActivity breathing = new BreathingActivity();
        ReflectionActivity reflection = new ReflectionActivity();
        ListingActivity listing = new ListingActivity();

        while (true)
        {
            Console.WriteLine("Mindfulness Program");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Quit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    breathing.DoBreathingActivity();
                    break;
                case "2":
                    reflection.DoReflectionActivity();
                    break;
                case "3":
                    listing.DoListingActivity();
                    break;
                case "4":
                    Console.WriteLine("Exiting program. Goodbye!");
                    return; 
                default:
                    Console.WriteLine("Invalid option, please try again.");
                    break;
            }
        }
    }
}























