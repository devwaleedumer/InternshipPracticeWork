int secretNo = 34;
int validAttempts = 0;
int guess = 0;
bool isGuessed = false;
int highThreshold = 20;

do
{
    Console.WriteLine("Enter your guess:");

    if (int.TryParse(Console.ReadLine(), out guess))
    {
        validAttempts++;

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
    else
    {
        Console.WriteLine("Please Enter Valid number");
    }

}
while (!isGuessed);

Console.WriteLine($"{secretNo} guessed in {validAttempts} attempts.");


//Q: In your last task, you looked at int.Parse(). Now, explain: What is the out keyword used for in int.TryParse(input, out result)?
//(Why can't we just say int result = int.TryParse(input);?)

//int.TryParse() needs to return two things:

//1) A boolean value indicating whether the parsing was successful or not.
//2) The parsed integer value if the parsing was successful.

// In C# , a method can only return one value directly.
// To return multiple values,
// we can use the out keyword to allow the method to output additional values through parameters.
