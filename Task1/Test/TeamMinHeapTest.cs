using P1.Entity;
using P1.Structure;

namespace P1.Test;

public class TeamMinHeapTest
{
    private static readonly List<Team> Teams = new List<Team>
        {
            new Team("Perspolis",68,45,18),
            new Team("Esteghlal",67,40,15),
            new Team("Sepahan",57,53,26),
            new Team("Tractor",54,42,22),
            new Team("ZobAhan",42,30,30),
            new Team("Malavan",41,31,26),
            new Team("GolGohar",36,30,28),
            new Team("Aluminium",39,27,33),
            new Team("ShamsAzar",39,35,35),
            new Team("Mes",35,32,37)
        };
    private static MinHeap<Team> _heapTeams;
    private static void initialize_heap_from_teams_array(int count,int maxSize=8) 
    {
        _heapTeams = new MinHeap<Team>(maxSize);
        _heapTeams.MakeHeapFrom(Teams.GetRange(0, count).ToArray());
    }
    private static void assert_that(string expected, string actual)
    {
        if (expected.Equals(actual))
            Console.WriteLine("Test passed - " + expected);
        else
            Console.WriteLine("Test failed");
    }
    public static void make_heap_from_array()
    {
        initialize_heap_from_teams_array(10,10); 
        assert_that("[Mes,35,-5 GD] [Aluminium,39,-6 GD] [GolGohar,36,2 GD] [ShamsAzar,39,0 GD] [ZobAhan,42,0 GD] [Malavan,41,5 GD] [Sepahan,57,27 GD] [Tractor,54,20 GD] [Perspolis,68,27 GD] [Esteghlal,67,25 GD] ",
            _heapTeams.ToString());
    }
    
    public static void insert_team_with_equal_point_with_root()
    {
        initialize_heap_from_teams_array(10,11); 
        _heapTeams.Insert(new Team("RealMadrid", 35, 20,40));
        assert_that(
            "[RealMadrid,35,-20 GD] [Mes,35,-5 GD] [GolGohar,36,2 GD] [ShamsAzar,39,0 GD] [Aluminium,39,-6 GD] [Malavan,41,5 GD] [Sepahan,57,27 GD] [Tractor,54,20 GD] [Perspolis,68,27 GD] [Esteghlal,67,25 GD] [ZobAhan,42,0 GD] ",
            _heapTeams.ToString());
    }
}


