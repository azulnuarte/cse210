using System;
using System.Collections.Generic;

// Base class
public class Activity
{
    private DateTime _date;
    private int _durationInMinutes; // Encapsulation: Private member variables

    public Activity(DateTime date, int durationInMinutes)
    {
        _date = date;
        _durationInMinutes = durationInMinutes;
    }

    public DateTime Date => _date;
    public int DurationInMinutes => _durationInMinutes;

    public virtual double GetDistance() => 0;
    public virtual double GetSpeed() => 0;
    public virtual double GetPace() => 0;

    public virtual string GetSummary()
    {
        return $"{_date:dd MMM yyyy} ({_durationInMinutes} min)";
    }
}

// Derived class for Running
public class Running : Activity
{
    private double _distanceInMiles;

    public Running(DateTime date, int durationInMinutes, double distanceInMiles)
        : base(date, durationInMinutes)
    {
        _distanceInMiles = distanceInMiles;
    }

    public override double GetDistance() => _distanceInMiles;
    public override double GetSpeed() => (_distanceInMiles / DurationInMinutes) * 60;
    public override double GetPace() => DurationInMinutes / _distanceInMiles;

    public override string GetSummary()
    {
        return $"{base.GetSummary()} Running - Distance: {GetDistance()} miles, Speed: {GetSpeed():F1} mph, Pace: {GetPace():F1} min per mile";
    }
}

// Derived class for Cycling
public class Cycling : Activity
{
    private double _speedInMph;

    public Cycling(DateTime date, int durationInMinutes, double speedInMph)
        : base(date, durationInMinutes)
    {
        _speedInMph = speedInMph;
    }

    public override double GetDistance() => (_speedInMph * DurationInMinutes) / 60;
    public override double GetSpeed() => _speedInMph;
    public override double GetPace() => 60 / _speedInMph;

    public override string GetSummary()
    {
        return $"{base.GetSummary()} Cycling - Speed: {GetSpeed()} mph, Distance: {GetDistance():F1} miles, Pace: {GetPace():F1} min per mile";
    }
}

// Derived class for Swimming
public class Swimming : Activity
{
    private int _laps;

    public Swimming(DateTime date, int durationInMinutes, int laps)
        : base(date, durationInMinutes)
    {
        _laps = laps;
    }

    public override double GetDistance() => _laps * 50 / 1000.0 * 0.62; // Distance in miles
    public override double GetSpeed() => (GetDistance() / DurationInMinutes) * 60;
    public override double GetPace() => DurationInMinutes / GetDistance();

    public override string GetSummary()
    {
        return $"{base.GetSummary()} Swimming - Distance: {GetDistance():F2} miles, Speed: {GetSpeed():F1} mph, Pace: {GetPace():F1} min per mile";
    }
}

// Main program to test the implementation
public class Program
{
    public static void Main()
    {
        // Create instances of each activity type
        List<Activity> activities = new List<Activity>
        {
            new Running(new DateTime(2022, 11, 3), 30, 3.0),
            new Cycling(new DateTime(2022, 11, 4), 60, 15.0),
            new Swimming(new DateTime(2022, 11, 5), 45, 20)
        };

        // Display summaries for each activity
        foreach (Activity activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}
