using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("What is your grade percenage?");
        string answer = Console.ReadLine();
        int percent = int.Parse(answer);

        string letter ="";

        if (percent >= 90)
        {
            letter = "A";
        }
        else if (percent >=80)
        {
            letter = "B";
        }
        else if (percent >=70)
        {
            letter= "D";
        }
        else
        {
            letter = "F";
        }

        Console.WriteLine($"Your grade is:{letter}");

        if (percent >=70)
        {
            Console.WriteLine("You passed!");
        }
        else 
        {
            Console.WriteLine("Better luck next time!");
        }

    }
}