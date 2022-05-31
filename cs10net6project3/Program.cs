using System.Diagnostics;
using static System.Console;
using Microsoft.Extensions.Configuration;

//// p.131 Chapter04 Writing, Debugging, and Testing Functions

//// p.132 Time table function
//static void TimeTable(byte number)
//{
//    WriteLine($"This is the {number} times table:");
//    for(int row = 1; row <= 12; row++)
//    {
//        WriteLine($"{row} x {number} = {row * number}");
//    }
//    WriteLine();
//}

//TimeTable(6);

//// p.134 Function that returns a value
//static decimal CalculateTax(decimal amount, string twoLetterRegionCode)
//{
//    decimal rate = 0.0M;

//    switch(twoLetterRegionCode){
//        case "CH": // SwitzerLand
//            rate = 0.08M;
//            break;
//        case "DK":
//        case "NO":
//            rate = 0.25M;
//            break;
//        case "FR":
//            rate = 0.2M;
//            break;
//        default:
//            rate = 0.6M;
//            break;
//    }
//     return amount * rate;
//}

//decimal taxToPay = CalculateTax(149, "FR");
//WriteLine($"You must pay {taxToPay} in tax");
//// problems with this function, if a user mistakes letters of region ? 
//// p.136 Converting cardinal to ordinal

///// <summary>
///// Pass a 32-bit integer and it will be converted into its ordinal equivalent.
///// </summary>
///// <param name="number">Number is a cardinal value e.g. 1, 2, 3, and so on. </param>
///// <returns>Number as an ordinal value e.g. 1st, 2nd, 3rd and so on.</returns>
//static string CardinaltoOrdinal(int number)
//{
//    // specific cases
//    // switch statement
//    switch (number)
//    {
//        case 11:
//        case 12:
//        case 13:
//            return $"{number}th";
//        default:
//            int lastDigit = number % 10;
//            // switch expression
//            string suffix = lastDigit switch
//            {
//                1 => "st",
//                2 => "nd",
//                3 => "rd",
//                _ => "th"
//            };
//            return $"{number}{suffix}";
//    }
//}

//static void RunCardinalToOrdinal()
//{
//    for(int i = 1; i <=40; i++)
//    {
//        Write($"{CardinaltoOrdinal(i)} ");
//    }
//    WriteLine();
//}
//RunCardinalToOrdinal();

//// p.137 Calculation factoriales
//static int Factorial(int number)
//{
//    // DO NOT FORGET to define the end of loop here
//    if(number < 1)
//    {
//        return 0;
//    }
//    else if(number == 1)
//    {
//        return 1;
//    }
//    checked
//    {
//        return number * Factorial(number - 1);
//    }


//}

//static void RunFactorial()
//{
//    for(int i =1; i <15; i++)
//    {
//        try
//        {
//            WriteLine($"{i}! = {Factorial(i):N0}");
//        }
//        catch (System.OverflowException)
//        {
//            WriteLine($"The {i} is too big for a 32-bit integer. ");
//        }

//    }
//}

//RunFactorial();

///* what's happend here ?
// when n = 5
//5 * Factorial(4)
//then
//5 * 4 * Factorial(3)
//then
//5 * 4 * Factorial(2)
//then
//5 * 4 * 3 * Factorial(1)
//then
//5 * 4 * 3 * 2 * Factorial(1)
//at last
//5 * 4 * 3 * 2 * 1 

//------Recursion model-----
//returnType func(param) {
//  if (basecase) {
//    return valueBasecase;
//  }

//  func(nextParam); 
//  return value1;
//}

//the first function calls the second one, 
//the second one calls the third one...
//the last element returns back its value to the previous element and repeat

//cf. recursion.png
//cf. https://qiita.com/drken/items/23a4f604fa3f505dd5ad
// */

//// p.141 Using lambdas in function implementations
//static int FibImperative(int term)
//{
//    if(term == 1)
//    {
//        return 0;
//    }
//    else if(term == 2)
//    {
//        return 1;
//    }
//    else
//    {
//        return FibImperative(term - 1) + FibImperative(term -2);
//    }
//}

//static void RunFibImperative()
//{
//    for(int i = 1; i <= 30; i++)
//    {
//        WriteLine("The {0} term of the Fibonacci sequence is {1:N0}.",
//            arg0: CardinaltoOrdinal(i),
//            arg1: FibImperative(term: i));
//    }
//}

////RunFibImperative();

//static int FibFunctional(int term) =>
//    term switch
//    {
//        1 => 0,
//        2 => 1,
//        _ => FibFunctional(term - 1) + FibFunctional(term - 2)
//    };

//static void RunFibFunctional()
//{
//    for(int i = 1; i < 30; i++)
//    {
//        WriteLine("The {0} term of the Fibonacci sequence is {1:N0}.",
//            arg0: CardinaltoOrdinal(i),
//            arg1: FibImperative(term: i));
//    }
//}

////RunFibFunctional();

//// P.144 Debugging
//// Toggle breakpoint : F9, start debugging : F5
//// right click on variable and select "Add watch"
//// right click on breakpoint red button and add conditions
//static double Add(double a, double b)
//{
//    return a * b;
//}

//double a = 4.5;
//double b = 2.5;
//double answer = Add(a, b);
//WriteLine($"{a} + {b} = {answer}");
//ReadLine();

// p.154 Debug and Trace
// write to a text file in the project folder
Trace.Listeners.Add(new TextWriterTraceListener(
    File.CreateText(Path.Combine(Environment.GetFolderPath(
        Environment.SpecialFolder.DesktopDirectory), "log.txt"))));

// text writer is buffered, so this option calls Flush() on all listeners after writing
Trace.AutoFlush = true;

Debug.WriteLine("Debug says, I am watching!");
Trace.WriteLine("Trace says, I am wathcing!");

// P.157 switching trace leves
ConfigurationBuilder builder = new();
builder.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsetting.json", optional: true, reloadOnChange: true);

IConfigurationRoot config = builder.Build();

TraceSwitch ts = new(
    displayName: "PacktSwitch",
    description: "This switch is set via a Json config.");

config.GetSection("PacktSwitch").Bind(ts);

Trace.WriteLine(ts.TraceError, "Trace error");
Trace.WriteLine(ts.TraceWarning, "Trace warning");
Trace.WriteLine(ts.TraceInfo, "Trace information");
Trace.WriteLine(ts.TraceVerbose, "Trace verbose");
ReadLine();