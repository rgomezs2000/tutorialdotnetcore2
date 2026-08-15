using System;
using System.Linq;

namespace tutorialdotnetcore2
{
    public class TipoDatos
    {
        public static void TipoDeDatos()
        {
            int enteros = 42;
            double decimales = 3.14;
            decimal dinero = 1500.50m;
            char letra = 'A';
            bool activo = true;
            string mensaje = "Aprendiendo .NET Core 10";

            Console.WriteLine($"int: {enteros}, double: {decimales}, decimal: {dinero}");
            Console.WriteLine($"char: {letra}, bool: {activo}");
            Console.WriteLine($"string: {mensaje}");
        }
    }
}
