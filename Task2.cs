// fixed variable
using System.Runtime.Intrinsics.X86;

const int secretNo = 34;
const int highThreshold = 20;
// state variables
int guess = 0;
bool isGuessed = false;
int validAttempts = 0;

do
{
    Console.Write("Enter your guess: ");
    guess = ReadGuess(ref validAttempts);

    if (guess == secretNo)
    {
        isGuessed = true;
    }
    else if ((guess - secretNo) > highThreshold)
    {
        Console.WriteLine("Guess is Too High");
    }
    else if (guess > secretNo)
    {
        Console.WriteLine("Guess is High");
    }
    else if ((secretNo - guess) > highThreshold)
    {
        Console.WriteLine("Guess is Too Low");
    }
    else if (guess < secretNo)
    {
        Console.WriteLine("Guess is Low");
    }

}
while (!isGuessed);

Console.WriteLine($"{secretNo} guessed in {validAttempts} attempts.");

int ReadGuess(ref int validAttempts)
{
    while (true)
    {
        string? input = Console.ReadLine()?.Trim();
        if (int.TryParse(input, out int result))
        {
            validAttempts += 1;
            return result;
        }
        Console.WriteLine($"{input} is not a whole number. Please try again");
    }
}


//Q: In your last task, you looked at int.Parse(). Now, explain: What is the out keyword used for in int.TryParse(input, out result)?
//(Why can't we just say int result = int.TryParse(input);?)

//int.TryParse() needs to return two things:

//1) A boolean value indicating whether the parsing was successful or not.
//2) The parsed integer value if the parsing was successful.

// In C# , a method can only return one value directly.
// To return multiple values,
// we can use the out keyword to allow the method to output additional values through parameters.



//Q) C# While loop vs Do-While loop"(When should you use one over the other ?).
//A while loop checks the condition before executing the loop body, so if the condition is false at the beginning, the loop body will not execute at all.
// A do-while loop, on the other hand, executes the loop body at least once before checking the condition. This means that even if the condition is false at the beginning, the loop body will execute at least once.

//Q) C# Boolean logic and comparison operators (>, <, ==)
// Comparison operators are used to compare two values and return a boolean result (true or false).

//Q) How to use int.TryParse in C# with an if statement
//A) You can use int.TryParse in an if statement to check if the parsing was successful. For example:
// string input = "123";
// if (int.TryParse(input, out int result))
// {
//     Console.WriteLine($"Parsing successful. The number is {result}.");
// }
// else
// {
//     Console.WriteLine("Parsing failed. Please enter a valid number.");
// }
//Q) Incrementing variables in C# (the ++ operator)
// The ++ operator is used to increment a variable by 1. It can be used in two forms: prefix (++variable) and postfix (variable++).
// The difference between the two is the order of evaluation. In prefix form, the variable is incremented before it is used in an expression,
// while in postfix form, the variable is used in the expression first and then incremented. For example:
// int x = 5;
// Console.WriteLine(x++); // Output: 5 (x is incremented after being used)