using System;

namespace LINQ;
internal static class InitializeData
{
    public static List<Person> s_people;
    public static List<Pet> s_pets;
    public static List<Student> s_students;
    public static List<Employee> s_employees;
    static InitializeData()
    {
        List<Person> people = new();
        Person magnus = new(FirstName: "Magnus", LastName: "Hedlund");
        Person terry = new("Terry", "Adams");
        Person charlotte = new("Charlotte", "Weiss");
        Person arlene = new("Arlene", "Huff");
        Person rui = new("Rui", "Raposo");
        s_people= new List<Person>()
        {
            magnus, terry, charlotte, arlene, rui,
        };

        s_pets = new()
        {
            new (Name: "Barley", Owner: terry),
            new ("Boots", terry),
            new ("Whiskers", charlotte),
            new ("Blue Moon", rui),
            new ("Daisy", magnus),
        };
        s_students = new()
        {
            new(
            FirstName: "Terry", LastName: "Adams", ID: 120,
            Year: GradeLevel.SecondYear,
            ExamScores: new() { 99, 82, 81, 79 }
            ),
            new(
            "Fadi", "Fakhouri", 116,
            GradeLevel.ThirdYear,
            new() { 99, 86, 90, 94 }
            ),
            new(
            "Hanying", "Feng", 117,
            GradeLevel.FirstYear,
            new() { 93, 92, 80, 87 }
            ),
            new(
            "Cesar", "Garcia", 114,
            GradeLevel.FourthYear,
            new() { 97, 89, 85, 82 }
            ),
            new(
            "Debra", "Garcia", 115,
            GradeLevel.ThirdYear,
            new() { 35, 72, 91, 70 }
            ),
            new(
            "Hugo", "Garcia", 118,
            GradeLevel.SecondYear,
            new() { 92, 90, 83, 78 }
            ),
            new(
            "Sven", "Mortensen", 113,
            GradeLevel.FirstYear,
            new() { 88, 94, 65, 91 }
            ),
            new(
            "Claire", "O'Donnell", 112,
            GradeLevel.FourthYear,
            new() { 75, 84, 91, 39 }
            ),
            new(
            "Svetlana", "Omelchenko", 111,
            GradeLevel.SecondYear,
            new() { 97, 92, 81, 60 }
            ),
            new(
            "Lance", "Tucker", 119,
            GradeLevel.ThirdYear,
            new() { 68, 79, 88, 92 }
            ),
            new(
            "Michael", "Tucker", 122,
            GradeLevel.FirstYear,
            new() { 94, 92, 91, 91 }
            ),
            new(
            "Eugene", "Zabokritski", 121,
            GradeLevel.FourthYear,
            new() { 96, 85, 91, 60 }
            ),
            new(FirstName: "Vernette", LastName: "Price", StudentID: 9562),
            new("Terry", "Earls", 9870),
            new("Terry", "Adams", 9913)
        };

        s_employees= new List<Employee>()
        {
            new(FirstName: "Terry", LastName: "Adams", EmployeeID: 522459),
            new("Charlotte", "Weiss", 204467),
            new("Magnus", "Hedland", 866200),
            new("Vernette", "Price", 437139)
        };
    }
}

