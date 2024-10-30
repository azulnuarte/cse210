using System;
using System.Collections.Generic;
using System.IO;

namespace EternalQuest
{
    // Base class Goal
    abstract class Goal
    {
        protected string _name; // Private member
        protected int _points; // Private member
        protected bool _isComplete; // Private member
        
        public string Name => _name; // Property to access name
        public int Points => _points; // Property to access points
        public bool IsComplete => _isComplete; // Property to access completion status
        
        public Goal(string name, int points)
        {
            _name = name;
            _points = points;
            _isComplete = false;
        }

        // Abstract method to record the progress of the goal
        public abstract void RecordProgress();

        // Virtual method to save the state of the goal
        public virtual string GetStringRepresentation()
        {
            return $"{GetType().Name}:{_name},{_points},{_isComplete}";
        }

        // Method to create a goal from a string
        public static Goal CreateFromString(string line)
        {
            var parts = line.Split(':');
            var details = parts[1].Split(',');
            var name = details[0];
            var points = int.Parse(details[1]);

            return parts[0] switch
            {
                "SimpleGoal" => new SimpleGoal(name, points),
                "EternalGoal" => new EternalGoal(name, points),
                "ChecklistGoal" => new ChecklistGoal(name, points, int.Parse(details[2])),
                _ => throw new Exception("Unknown goal type")
            };
        }
    }

    // Simple goal
    class SimpleGoal : Goal
    {
        public SimpleGoal(string name, int points) : base(name, points) {}

        public override void RecordProgress()
        {
            _isComplete = true;
            Console.WriteLine($"Goal completed! {_name} has granted you {_points} points.");
        }
    }

    // Eternal goal
    class EternalGoal : Goal
    {
        public EternalGoal(string name, int points) : base(name, points) {}

        public override void RecordProgress()
        {
            Console.WriteLine($"Progress recorded! You earned {_points} points for {_name}.");
        }
    }

    // Checklist goal
class ChecklistGoal : Goal
{
    private int _requiredCompletions; // Private member
    private int _currentCompletions; // Private member
    private const int _bonusPoints = 500; // Constant for bonus points

    public ChecklistGoal(string name, int points, int requiredCompletions) 
        : base(name, points)
    {
        _requiredCompletions = requiredCompletions;
        _currentCompletions = 0;
    }

    public int CurrentCompletions => _currentCompletions; // Property for current completions
    public int RequiredCompletions => _requiredCompletions; // Property for required completions

    public override void RecordProgress()
    {
        _currentCompletions++;
        if (_currentCompletions >= _requiredCompletions)
        {
            _isComplete = true;
            Console.WriteLine($"You completed {_name}! You earned {_points + _bonusPoints} points.");
        }
        else
        {
            Console.WriteLine($"Progress recorded for {_name}! Completed {_currentCompletions}/{_requiredCompletions}. You earned {_points} points.");
        }
    }

    public override string GetStringRepresentation()
    {
        return base.GetStringRepresentation() + $",{_requiredCompletions},{_currentCompletions}";
    }
}

    // Main class to manage goals
    class GoalManager
    {
        private List<Goal> _goals = new List<Goal>(); // Private member
        private int _score = 0; // Private member
        private int _level = 1; // Private member

        public void AddGoal(Goal goal)
        {
            _goals.Add(goal);
        }

        public void RecordGoal(string goalName)
        {
            var goal = _goals.Find(g => g.Name == goalName);
            if (goal != null)
            {
                goal.RecordProgress();
                _score += goal.Points;
                CheckLevelUp();
            }
            else
            {
                Console.WriteLine("Goal not found.");
            }
        }

        private void CheckLevelUp()
        {
            if (_score >= _level * 1000)
            {
                _level++;
                Console.WriteLine($"Level up! You are now level {_level}.");
            }
        }

        public void ShowGoals()
        {
            foreach (var goal in _goals)
            {
                string status = goal.IsComplete ? "[X]" : "[ ]";
                if (goal is ChecklistGoal checklistGoal)
                {
                    status += $" Completed {checklistGoal.CurrentCompletions}/{checklistGoal.RequiredCompletions}";
                }
                Console.WriteLine($"{status} {goal.Name} - {goal.Points} points");
            }
            Console.WriteLine($"Total score: {_score} - Level: {_level}");
        }

        public void SaveGoals(string filename)
        {
            using (StreamWriter outputFile = new StreamWriter(filename))
            {
                outputFile.WriteLine(_score);
                outputFile.WriteLine(_level);
                foreach (var goal in _goals)
                {
                    outputFile.WriteLine(goal.GetStringRepresentation());
                }
            }
        }

        public void LoadGoals(string filename)
        {
            if (File.Exists(filename))
            {
                string[] lines = File.ReadAllLines(filename);
                _score = int.Parse(lines[0]);
                _level = int.Parse(lines[1]);
                _goals.Clear();

                for (int i = 2; i < lines.Length; i++)
                {
                    _goals.Add(Goal.CreateFromString(lines[i]));
                }
            }
        }
    }

    // Program class to run the system
    class Program
    {
        static void Main()
        {
            GoalManager manager = new GoalManager();

            // Menu
            while (true)
            {
                Console.WriteLine("\n1. View goals");
                Console.WriteLine("2. Create goal");
                Console.WriteLine("3. Record goal");
                Console.WriteLine("4. Save");
                Console.WriteLine("5. Load");
                Console.WriteLine("6. Exit");
                Console.Write("Select an option: ");
                if (!int.TryParse(Console.ReadLine(), out int option))
                {
                    Console.WriteLine("Invalid option. Please enter a number.");
                    continue;
                }

                switch (option)
                {
                    case 1:
                        manager.ShowGoals();
                        break;
                    case 2:
                        Console.Write("Goal name: ");
                        string name = Console.ReadLine();
                        Console.Write("Points: ");
                        if (!int.TryParse(Console.ReadLine(), out int points))
                        {
                            Console.WriteLine("Invalid points. Please enter a number.");
                            continue;
                        }
                        Console.Write("Type (1-Simple, 2-Eternal, 3-Checklist): ");
                        if (!int.TryParse(Console.ReadLine(), out int type))
                        {
                            Console.WriteLine("Invalid type. Please enter a number.");
                            continue;
                        }

                        switch (type)
                        {
                            case 1:
                                manager.AddGoal(new SimpleGoal(name, points));
                                break;
                            case 2:
                                manager.AddGoal(new EternalGoal(name, points));
                                break;
                            case 3:
                                Console.Write("Number of completions required: ");
                                if (!int.TryParse(Console.ReadLine(), out int completions))
                                {
                                    Console.WriteLine("Invalid completions. Please enter a number.");
                                    continue;
                                }
                                manager.AddGoal(new ChecklistGoal(name, points, completions));
                                break;
                            default:
                                Console.WriteLine("Invalid goal type.");
                                break;
                        }
                        break;
                    case 3:
                        Console.Write("Name of the goal to record: ");
                        string goalName = Console.ReadLine();
                        manager.RecordGoal(goalName);
                        break;
                    case 4:
                        manager.SaveGoals("goals.txt");
                        break;
                    case 5:
                        manager.LoadGoals("goals.txt");
                        break;
                    case 6:
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please enter a number between 1 and 6.");
                        break;
                }
            }
        }
    }
}
