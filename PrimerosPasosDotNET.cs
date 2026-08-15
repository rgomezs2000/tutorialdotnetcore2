using System;
using System.Linq;

namespace tutorialdotnetcore2
{
    public class PrimerosPasosDotNET
    {
        public static void PrimerosPasos()
        {
            Console.WriteLine("=== Mi primera app en .NET 10 ===");

            Console.Write("¿Cómo te llamas? ");
            string? nombre = Console.ReadLine();

            Console.Write("¿Cuántos años tienes? ");
            int edad = int.Parse(Console.ReadLine() ?? "0");

            Console.WriteLine($"\nHola, {nombre}. Tienes {edad} años.");

            if (edad >= 18)
            {
                Console.WriteLine("Eres mayor de edad.");
            }
            else
            {
                Console.WriteLine("Eres menor de edad.");
            }
        }
    }
}