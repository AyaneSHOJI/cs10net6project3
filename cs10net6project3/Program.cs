using static System.Console;

//p.131 Chapter04 Writing, Debugging, and Testing Functions

//p.132 Time table function
static void TimeTable(byte number)
{
    WriteLine($"This is the {number} times table:");
    for(int row = 1; row <= 12; row++)
    {
        WriteLine($"{row} x {number} = {row * number}");
    }
    WriteLine();
}

TimeTable(6);

//p.134 Function that returns a value
static decimal CalculateTax(decimal amount, string twoLetterRegionCode)
{
    decimal rate = 0.0M;

    switch(twoLetterRegionCode){
        case "CH": //SwitzerLand
            rate = 0.08M;
            break;
        case "DK":
        case "NO":
            rate = 0.25M;
            break;
        case "FR":
            rate = 0.2M;
            break;
        default:
            rate = 0.6M;
            break;
    }
     return amount * rate;
}

decimal taxToPay = CalculateTax(149, "FR");
WriteLine($"You must pay {taxToPay} in tax");
//problems with this function, if a user mistakes letters of region ? 

//p.136 Converting cardinal to ordinal
static string CardinaltoOrdinal(int number)
{
    //specific cases
    //switch statement
    switch (number)
    {
        case 11:
        case 12:
        case 13:
            return $"{number}th";
        default:
            int lastDigit = number % 10;
            //switch expression
            string suffix = lastDigit switch
            {
                1 => "st",
                2 => "nd",
                3 => "rd",
                _ => "th"
            };
            return $"{number}{suffix}";
    }
}

static void RunCardinalToOrdinal()
{
    for(int i = 1; i <=40; i++)
    {
        Write($"{CardinaltoOrdinal(i)} ");
    }
    WriteLine();
}
RunCardinalToOrdinal();

//p.137 Calculation factoriales
static int Factorial(int number)
{
    if(number < 1)
    {
        return 0;
    }
    else if(number == 1)
    {
        return 1;
    }
    else
    {
        return number * Factorial(number - 1);
    }
}

static void RunFactorial()
{
    for(int i =1; i <5; i++)
    {
        WriteLine($"{i}! = {Factorial(i):N0}");
    }
}

RunFactorial();