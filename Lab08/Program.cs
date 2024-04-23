class Player
{
    public string Name { get; set; }
    public int Points { get; set; }
}
class Program
{
    private static List<string> names = new()
    {
        "Adam",
        "Karol",
        "Ewa",
        "Robert",
        "Alicja"
    };
    public static void Main(string[] args)
    {
        FilterNames(names, Is3Letters).Print();
        FilterNames(names, IsStartWithA).Print();
        /* Find3LetterNames(names).Print();
        FindALetterNames(names).Print();
        Console.ReadKey(); */
        FilterNames(names, delegate(string name) { return name.Length % 2 == 0; });
        FilterNamesPredicate(names, name => name.Length % 2 != 0);
        Calculate(3, 5, delegate(double a, double b) { return a * b; });
        Calculate(15, 5, (x, y) => x / y);
        ActionPredicateDemo();
    }

    public static void ActionPredicateDemo()
    {
        Action<string> print = toPrint => Console.WriteLine(toPrint);
        print("Mikołaj");
        Action<int, string> repeat = (n, str) =>
        {
            for (int i = 0; i < n; i++)
            {
                print(str);
            }
        };
        repeat(5, "ABC");
    }

    public static void FuncDelegateDemo()
    {
        Func<double, double, double> binaryOp = (a, b) => a + b;
        Console.WriteLine(binaryOp(4, 5));
        binaryOp = (a, b) => a * b;
        Console.WriteLine(binaryOp(4, 5));
        binaryOp = (a, b) => a / b;
        Console.WriteLine(binaryOp(4, 5));
        Func<double, double> power = (a) => Math.Pow(a, 2);
        Console.WriteLine(power(4));
        Func<string, double> bouncer = a => Double.Parse(a);
        Console.WriteLine(bouncer("123,3"));
        bouncer = a => a.Length;
        Func<string, Player> mapper = str =>
        {
            var tokens = str.Split(";");
            return new Player()
            {
                Name = tokens[0],
                Points = int.Parse(tokens[1])
            };
        };
    }

    public static void Calculate(double x, double y, BinaryOperator op)
    {
        Console.WriteLine(op(x, y));
    }

    public static List<string> Find3LetterNames(List<string> items)
    {
        List<string> result = new();
        foreach (var item in items)
        {
            if (Is3Letters(item))
            {
                result.Add(item);
            }
        }
        return result;
    }
    public static List<string> FindALetterNames(List<string> items)
    {
        List<string> result = new();
        foreach (var item in items)
        {
            if (IsStartWithA(item))
            {
                result.Add(item);
            }
        }
        return result;
    }

    public static bool IsStartWithA(string item)
    {
        return item.StartsWith("A");
    }
    public static bool Is3Letters(string item)
    {
        return item.Length == 3;
    }

    public static List<string> FilterNames(List<string> items, StringFilter filter)
    {
        List<string> result = new();
        foreach (var item in items)
        {
            if (filter(item))
            {
                result.Add(item);
            }
        }
        return result;
    }

    public static List<string> FilterNamesPredicate(List<string> items, Predicate<string> filter)
    {
        List<string> result = new();
        foreach (var item in items)
        {
            if (Is3Letters(item))
            {
                result.Add(item);
            }
        }

        return result;
    }

    public static double Addition(double a, double b)
    {
        return a + b;
    }

}

delegate bool StringFilter(string item); //to nie definicja funkcji, tylko okreslenie delegata. rodzina funkcji przyjmuje to samo i oddaje to samo

delegate double BinaryOperator(double a, double b); // Zdefiniuj metode w klasie program, ktora jest zgodna z delegatem binary operator i zwraca sume parametrow


static class CollectionExtensions
{
    public static void Print<T>(this IEnumerable<T> items)
    {
        Console.WriteLine(string.Join(", ", items));
    }
}