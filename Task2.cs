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