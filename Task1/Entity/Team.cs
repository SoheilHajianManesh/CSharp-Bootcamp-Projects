using P1.Service;

namespace P1.Entity;

public class Team : Service.IComparable<Team>,IPrintable
{
    private string Name { get; init; }
    private int Points { get; set; }
    private int GoalsScored { get; set; }
    private int GoalsConceded { get; set; }
    private int GoalDifference { get; set; }
    public Team(string name, int points, int goalsScored, int goalsConceded)
    {
        Name = name;
        Points = points;
        GoalsScored = goalsScored;
        GoalsConceded = goalsConceded;
        GoalDifference = goalsScored - goalsConceded;
    }
    public string Print()
    {
        return $"[{Name},{Points},{GoalDifference} GD]";
    }
    public bool SmallerThan(Team toCompare)
    {
        if (Points != toCompare.Points)
            return Points < toCompare.Points;
        if (GoalDifference != toCompare.GoalDifference)
            return GoalDifference < toCompare.GoalDifference;
        if (GoalsScored != toCompare.GoalsScored)
            return GoalsScored < toCompare.GoalsScored;
        return string.Compare(Name, toCompare.Name, StringComparison.Ordinal) > 0;
    }
}