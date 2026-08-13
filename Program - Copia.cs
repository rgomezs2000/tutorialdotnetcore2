//Program.cs
Console.Write("Escribe una palabra: ");
string? palabra = Console.ReadLine() ?? "";

int vocales = 0;

foreach (char c in palabra.ToLower()) {
    if ("aeiouáéíóúü".Contains(c)) {
        vocales++;
    }
}

Console.WriteLine($"La palabra tiene: {vocales} vocales.");
Console.WriteLine($"Primera letra en mayuscula: {char.ToUpper(palabra[0])}");

//Console.WriteLine("¿Tienes más de 18 años? (true/false)");
//bool esMayor = bool.Parse(Console.ReadLine() ?? "false");

//Console.WriteLine("¿Tienes cédula? (true/false)");
//bool tienecedula = bool.Parse(Console.ReadLine() ?? "false");

//bool puedeVotar = esMayor && tienecedula;
//Console.WriteLine($"¿Puede votar? {puedeVotar}");

//Console.WriteLine($"int.MaxValue: {int.MaxValue}");
//Console.WriteLine($"long.MaxValue: {long.MaxValue}");
//Console.WriteLine($"double.MaxValue: {double.MaxValue}");

//double sumaDouble = 0.1 + 0.2;
//decimal sumaDecimal = 0.1m + 0.2m;

//Console.WriteLine($"double: {sumaDouble}");
//Console.WriteLine($"decimal: {sumaDecimal}");

//int enteros = 42;
//double decimales = 3.14;
//decimal dinero = 1500.50m;
//char letra = 'A';
//bool activo = true;
//string mensaje = "Aprendiendo .NET Core 10.";

//Console.WriteLine($"int: {enteros}");
//Console.WriteLine($"double: {decimales}");
//Console.WriteLine($"decimal: {dinero}");
//Console.WriteLine($"char: {letra}");
//Console.WriteLine($"bool: {activo}");
//Console.WriteLine($"string: {mensaje}");

//Console.WriteLine("---Primer progama en .NET Core 10---");

//Console.Write("¿Como te llamas?");
//string? nombre = Console.ReadLine();

//Console.Write("¿Cuanto años tienes?");
//int edad = int.Parse(Console.ReadLine() ?? "0");

//Console.WriteLine($"\nHola, {nombre}, Tienes {edad} años.");

//if(edad >= 18)
//{
//    Console.WriteLine("Eres mayor de edad.");
//}
//else
//{
//    Console.WriteLine("Eres menor de edad.");
//}

//var builder = WebApplication.CreateBuilder(args);
//var app = builder.Build();

//app.MapGet("/", () => "Hello World!");

//app.Run();
