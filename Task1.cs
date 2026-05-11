string fullName ;
int  age;
int favouriteNumber;
int magicNumber;
Console.WriteLine("\n\t\t***Personal Profile Generator***");

Console.WriteLine("Enter your full name.");
fullName= Console.ReadLine() ?? "";

Console.WriteLine("Enter your age.");
int.TryParse(Console.ReadLine(), out age);

Console.WriteLine("Enter your favourite.");
int.TryParse(Console.ReadLine(), out favouriteNumber);

magicNumber = favouriteNumber * age;

Console.WriteLine($"Hello {fullName}, Age: {age}, Favourite Number, Your Magic  Number is {magicNumber}");


//Parse throws an exception if it cannot parse the value, whereas TryParse returns a bool indicating whether it succeeded.
//TryParse does not just try/catch internally - the whole point of it is that it is implemented without exceptions so that it is fast. 
//In fact the way it is most likely implemented is that internally the Parse method will call TryParse
//and then throw an exception if it returns false.