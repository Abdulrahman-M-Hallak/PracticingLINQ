using System.Xml.Linq;
using static LINQ.InitializeData;

namespace LINQ;

internal class Program
{
    static void Main(string[] args)
    {
        //Queries.QueryExamScore(1, 70);
        //Queries.QueryYear(GradeLevel.FirstYear, out IEnumerable<Student> yearQuery);
        //foreach (var item in yearQuery)
        //{
        //    Console.WriteLine(item.FirstName);
        //}

        //Console.WriteLine("**************************************");
        //Queries.QueryWithLet();

        //Console.WriteLine("**************************************");
        //Queries.GroupByLastNameQuery();

        //Console.WriteLine("**************************************");
        //Queries.GroupByNumericValueQuery();

        //Console.WriteLine("**************************************");
        //Queries.NestedGrouping();

        //Console.WriteLine("**************************************");
        //Queries.HighestScoreQuery();
        //Console.WriteLine("****************************************");
        //Queries.OrderJoinQueryResult();
        Console.WriteLine("Group join");
        Queries.GroupJoin();
        Console.WriteLine("Join");
        Queries.GroupJoin4();
    }
}
