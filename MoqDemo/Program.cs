using MoqDemo;

Console.WriteLine("Moq Demo!");

MyLogger myLogger = new MyLogger();
Calculator calculator = new Calculator(myLogger);
int a = 10, b = 2, c;

c = calculator.Add(a, b);
Console.WriteLine($"{a} + {b} = {c}");

c = calculator.Subtract(a, b);
Console.WriteLine($"{a} - {b} = {c}");

c = await calculator.Multiply(a, b);
Console.WriteLine($"{a} * {b} = {c}");

c = calculator.Devide(a, b);
Console.WriteLine($"{a} / {b} = {c}");
