Console.WriteLine("─────────────────────────────────────────");
Console.WriteLine("       Personal Profile Generator        ");
Console.WriteLine("─────────────────────────────────────────");
Console.WriteLine();

string name = ReadText("Enter your full name");
int age = ReadInt("Enter your age");
int favouriteNumber = ReadInt("Enter your favourite number");
int magicResult = age * favouriteNumber;

Console.WriteLine();
Console.WriteLine("─────────────────────────────────────────");
Console.WriteLine($"  Hello, {name}!");
Console.WriteLine($"  You are {age} years old.");
Console.WriteLine($"  Your favourite number is {favouriteNumber}.");
Console.WriteLine($"  {age} × {favouriteNumber} = {magicResult}.");



string ReadText(string text)
{
    while (true)
    {
        Console.Write($"{text}: ");
        string? input = Console.ReadLine()?.Trim();

        if (!string.IsNullOrEmpty(input))
            return input;

        Console.WriteLine("Name cannot be empty. Please Try Agin");
    }
}


int ReadInt(string text)
{
    while (true)
    {
        Console.Write($"{text}: ");
        string? input = Console.ReadLine();

        if (int.TryParse(input, out int result))
            return result;

        Console.WriteLine($"{input} is not whole number. Please Try Agin");
    }
}


//Parse throws an exception if it cannot parse the value, whereas TryParse returns a bool indicating whether it succeeded.
//TryParse does not just try/catch internally
//- the whole point of it is that it is implemented without exceptions so that it is fast. 
//In fact the way it is most likely implemented is that internally the Parse method will call TryParse
//and then throw an exception if it returns false.


//Q) How to get input from console in C#
//A) Console.ReadLine() method is used to get input from the console in C#. It reads a line of text from the console and returns it as a string.

//Q) Difference between string and int in C#
//A) string is a sequence of characters, while an int is a whole number. A string can contain letters, numbers, and other characters,
//while an int can only contain digits. In C#,
//you can convert a string to an int using the int.Parse() method or the int.TryParse() method.
//Q) How to convert string to int in C# (int.Parse)
//A) You can convert a string to an int in C# using the int.Parse() method. For example:
// string numberString = "123";
// int number = int.Parse(numberString);
//Q)String interpolation in C# (the $ symbol)
//A)String interpolation in C# allows you to embed expressions within string literals,
//making it easier to format strings. You can use the $ symbol before the string literal to enable interpolation. For example: