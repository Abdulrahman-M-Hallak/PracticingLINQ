using LINQ.DTO;
using System.Xml.Linq;
using static LINQ.DTO.InitializeData;

namespace LINQ;
internal static class Queries
{
    public static void QueryExamScore(int exam, int score)
    {
        var highScoreQuery =
            from student in s_students
            where student.ExamScores[exam] >= score
            orderby student.ExamScores[exam]
            select new
            {
                name = student.FirstName,
                score = student.ExamScores[exam]
            };
        foreach (var item in highScoreQuery)
        {
            Console.WriteLine(item);
        }
    }

    public static void QueryYear(GradeLevel level, out IEnumerable<Student> yearQuery)
    {
        yearQuery =
            from student in s_students
            where student.Year == level
            select student;
    }

    public static void QueryWithLet()
    {
        var query =
            from student in s_students
            let lastname = student.LastName
            where lastname[0] == 'G'
            select student;
        foreach (var item in query)
        {
            Console.WriteLine(item.FirstName);
        }
    }

    public static void GroupByLastNameQuery()
    {
        var query =
            from student in s_students
            group student by student.LastName into newGroup
            orderby newGroup.Key
            select newGroup;

        foreach (var item in query)
        {
            Console.WriteLine($"Key: {item.Key}");
            foreach (var item1 in item)
            {
                Console.WriteLine($"Name: {item1.FirstName}");
            }
        }
    }

    public static void GroupByNumericValueQuery()
    {
        var query =
            from student in s_students
            let percentile = GetPercentile(student)
            group new
            {
                name = student.FirstName + " " + student.LastName,
            } by percentile into newGroup
            orderby newGroup.Key
            select newGroup;

        foreach (var item in query)
        {
            Console.WriteLine($"Key: {item.Key * 10}");
            foreach (var item1 in item)
            {
                Console.WriteLine(item1);
            }
        }
    }

    public static void GroupByCompoundKey()
    {
        var groupByCompoundKey =
            from student in s_students
            group student by new
            {
                FirstLetter = student.LastName[0],
                IsScoreOver85 = student.ExamScores[0] > 85
            } into studentGroup
            orderby studentGroup.Key.FirstLetter
            select studentGroup;

        foreach (var scoreGroup in groupByCompoundKey)
        {
            string s = scoreGroup.Key.IsScoreOver85 ? "more than 85" : "less than 85";
            Console.WriteLine($"Name starts with {scoreGroup.Key.FirstLetter} who scored {s}");
            foreach (var item in scoreGroup)
            {
                Console.WriteLine($"\t{item.FirstName} {item.LastName}");
            }
        }
    }
    private static double GetPercentile(Student s)
    {
        double avg = s.ExamScores.Average();
        return avg > 0 ? (int)avg / 10 : 0;
    }

    public static void NestedGrouping()
    {
        var nestedGroupingQuery =
            from student in s_students
            group student by student.Year into newGroup1
            from newGroup2 in (
            from student in newGroup1
            group student by student.LastName)
            group newGroup2 by newGroup1.Key;

        foreach (var item in nestedGroupingQuery)
        {
            Console.WriteLine($"DataClass.Student Level = {item.Key}");
            foreach (var item1 in item)
            {
                Console.WriteLine($"\tNames that begin with: {item1.Key}");
                foreach (var item2 in item1)
                {
                    Console.WriteLine($"\t\t{item2.LastName} {item2.FirstName}");
                }
            }
        }
    }

    public static void HighestScoreQuery()
    {
        var queryHighestScore =
            from student in s_students
            group student by student.Year into studentGroup
            select new
            {
                level = studentGroup.Key,
                highestScore = (
                    from student2 in studentGroup
                    select student2.ExamScores.Average()).Max()
            };
        int count = queryHighestScore.Count();
        Console.WriteLine($"Number of groups = {count}");

        foreach (var item in queryHighestScore)
        {
            Console.WriteLine($"  {item.level} Highest Score={item.highestScore}");
        }
    }

    public static void DynamicallyDecideOnThePredicateQuery(bool oddYear)
    {
        IEnumerable<Student> studentQuery;
        if (oddYear)
        {
            studentQuery =
                from student in s_students
                where student.Year == GradeLevel.FirstYear || student.Year == GradeLevel.ThirdYear
                select student;
        }
        else
        {
            studentQuery =
                from student in s_students
                where student.Year == GradeLevel.SecondYear || student.Year == GradeLevel.FourthYear
                select student;
        }

        string descr = oddYear ? "odd" : "even";
        Console.WriteLine($"The following students are at an {descr} year level:");
        foreach (Student name in studentQuery)
        {
            Console.WriteLine($"{name.LastName}: {name.ID}");
        }
    }

    public static void JoinQuery()
    {
        var queryPets =
            from person in s_people
            join pet in s_pets on person equals pet.Owner
            select new
            {
                PersonName = person.FirstName,
                PetName = pet.Name
            };
        foreach (var item in queryPets)
        {
            Console.WriteLine($"{item.PersonName} owns {item.PetName}");
        }
    }

    public static void GroupJoin()
    {
        var query1 =
            from person in s_people
            join pet in s_pets on person equals pet.Owner into gj
            select new
            {
                OwnerName = person.FirstName,
                Pets = gj
            };
        foreach (var item in query1)
        {
            Console.WriteLine(item.OwnerName);
            foreach (var item1 in item.Pets)
            {
                Console.WriteLine(item1);
            }
        }
    }

    public static void GroupJoin2()
    {
        var query =
            from category in s_categories
            join product in s_products on category.ID equals product.CategoryID into prodGroup
            select prodGroup;

        int totalItems = 0;

        Console.WriteLine("Simple GroupJoin:");

        // A nested foreach statement is required to access group items.
        foreach (var prodGrouping in query)
        {
            Console.WriteLine("Group:");
            foreach (var item in prodGrouping)
            {
                totalItems++;
                Console.WriteLine("   {0,-10}{1}", item.Name, item.CategoryID);
            }
        }
        Console.WriteLine("Unshaped GroupJoin: {0} items in {1} unnamed groups", totalItems, query.Count());
        Console.WriteLine(System.Environment.NewLine);
    }

    public static void GroupJoin3()
    {
        var query =
            from category in s_categories
            join product in s_products on category.ID equals product.CategoryID into prodGroup
            from prod in prodGroup
            orderby prod.CategoryID
            select new
            {
                Category = prod.CategoryID,
                ProductName = prod.Name
            };
        int totalItems = 0;

        Console.WriteLine("GroupJoin3:");
        foreach (var item in query)
        {
            totalItems++;
            Console.WriteLine($"   {item.ProductName}:{item.Category}" );
        }

        Console.WriteLine($"GroupJoin3: {totalItems} items in 1 group") ;
        Console.WriteLine(System.Environment.NewLine);
    }

    public static void GroupJoin4()
    {
        var query =
            from product in s_products
            join category in s_categories on product.CategoryID equals category.ID into prodGroup
            select new
            {
                Category = prodGroup,
                ProductName = product.Name
            };
        foreach (var item in query)
        {
            Console.WriteLine(item.ProductName);
            foreach (var item1 in item.Category)
            {
                Console.WriteLine(item1);
            }
        }
    }

    public static void XMLQuery()
    {
        XElement ownersAndPets = new("PetOwners",
            from person in s_people
            join pet in s_pets on person equals pet.Owner into gj
            select new XElement("Person",
                new XAttribute("FirstName", person.FirstName),
                new XAttribute("LastName", person.LastName),
                from subpet in gj
                select new XElement("Pet", subpet.Name)
            )
          );

        Console.WriteLine(ownersAndPets);
    }

    public static void LeftOuterJoin()
    {
        var leftOuterQuery =
            from person in s_people
            join pet in s_pets on person equals pet.Owner into gj
            from subset in gj.DefaultIfEmpty()
            select new
            {
                person.FirstName,
                PetName = subset?.Name ?? string.Empty,
            };

        foreach (var v in leftOuterQuery)
        {
            Console.WriteLine($"{v.FirstName + ":",-15}{v.PetName}");
        }
    }

    public static void InnerJoin()
    {
        var queryStudents =
            from student in s_students
            join employee in s_employees on new
            {
                student.FirstName,
                student.LastName,
            } equals new
            {
                employee.FirstName,
                employee.LastName,
            }
            select employee.FirstName + " " + employee.LastName;
        foreach (var item in queryStudents)
        {
            Console.WriteLine(item);
        }
    }

    public static void OrderJoinQueryResult()
    {
        var query =
            from category in s_categories
            join product in s_products on category.ID equals product.CategoryID into prodGroup
            orderby category.Name
            select new
            {
                Category = category.Name,
                Products =
                from prod in prodGroup
                orderby prod.Name
                select prod
            };

        foreach (var productGroup in query)
        {
            Console.WriteLine(productGroup.Category);
            foreach (var prodItem in productGroup.Products)
            {
                Console.WriteLine($"  {prodItem.Name,-10} {prodItem.CategoryID}");
            }
        }
    }

    public static void CrossJoinQuery()
    {
        var query =
            from category in s_categories
            from product in s_products
            select new
            {
                category.ID,
                product.Name
            };
        Console.WriteLine("Cross Join Query:");
        foreach (var v in query)
        {
            Console.WriteLine($"{v.ID,-5}{v.Name}");
        }
    }

    public static void NonEquiJoin()
    {
        // This query produces a sequence of all the products whose category ID is listed in the category list on the left side.
        var query =
            from product in s_products
            let catID =
            from category in s_categories
            select category.ID
            where catID.Contains(product.CategoryID)
            select new
            {
                Product = product.Name,
                product.CategoryID
            };
        Console.WriteLine("Non-equijoin query:");
        foreach (var v in query)
        {
            Console.WriteLine($"{v.CategoryID,-5}{v.Product}");
        }
    }
}
