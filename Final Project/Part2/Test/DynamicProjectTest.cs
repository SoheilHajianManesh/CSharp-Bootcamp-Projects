using FinalProject.Common;
using FinalProject.Common.Entities;
using FinalProject.Data;
using Part2.Operator;

namespace Part2.Test;

public class DynamicProjectTest
{
    private static IDbContext _dbContext = new AppDbContext();

    private static void assert_that(IEnumerable<object>? firstSequence, IEnumerable<object>? secondSequence,
        string testTitle)
    {
        if (firstSequence == null && secondSequence == null)
            Console.WriteLine("Test passed - " + testTitle);
        else if (firstSequence == null || secondSequence == null)
            Console.WriteLine("Test failed - " + testTitle);
        else
        {
            for (var i = 0; i < firstSequence.Count(); i++)
            {
                if (!firstSequence.ElementAt(i).ToString().Equals(secondSequence.ElementAt(i).ToString()))
                {
                    Console.WriteLine("Test failed - " + testTitle);
                    return;
                }
            }
            Console.WriteLine("Test passed - " + testTitle);
        }
    }

    public static void run_all_tests()
    {
        select_id_first_name_last_name_from_customers();
        select_tracks_from_albums();
        select_name_track_from_playlist();
    }

    private static void select_id_first_name_last_name_from_customers()
    {
        var resultTableUsingDynamicProject = (typeof(OperatorsFactory<>).MakeGenericType(typeof(Customer))
            .GetMethod("CreateAndApplyOperation")!
            .Invoke(null,
                new object[]
                {
                    "Project", _dbContext.Customers, new List<string> {"CustomerId", "FirstName", "LastName"}, null,
                    null, null
                })) as IEnumerable<Customer>;
        var resultTableUsingLinq = _dbContext.Customers.Select(c => new Customer()
        {
            CustomerId = c.CustomerId,
            FirstName = c.FirstName,
            LastName = c.LastName
        }).AsEnumerable();
        assert_that(resultTableUsingDynamicProject, resultTableUsingLinq,
            "select_id_first_name_last_name_of_customers");
    }

    private static void select_tracks_from_albums()
    {
        var resultTableUsingDynamicProject = (typeof(OperatorsFactory<>).MakeGenericType(typeof(Album))
            .GetMethod("CreateAndApplyOperation")!
            .Invoke(null,
                new object[]
                {
                    "Project", _dbContext.Albums, new List<string> {"Tracks"}, null,
                    null, null
                })) as IEnumerable<Album>;
        var resultTableUsingLinq = _dbContext.Albums.Select(a => new Album()
        {
            Tracks = a.Tracks
        }).AsEnumerable();
        assert_that(resultTableUsingDynamicProject, resultTableUsingLinq,
            "select_tracks_from_albums");
    }

    private static void select_name_track_from_playlist()
    {
        var resultTableUsingDynamicProject = (typeof(OperatorsFactory<>).MakeGenericType(typeof(Playlist))
            .GetMethod("CreateAndApplyOperation")!
            .Invoke(null,
                new object[]
                {
                    "Project", _dbContext.Playlists, new List<string> {"Name", "Tracks"}, null,
                    null, null
                })) as IEnumerable<Playlist>;
        var resultTableUsingLinq = _dbContext.Playlists.Select(p => new Playlist()
        {
            Name = p.Name,
            Tracks = p.Tracks
        }).AsEnumerable();
        assert_that(resultTableUsingDynamicProject, resultTableUsingLinq,
            "select_name_track_from_playlist");
    }
}