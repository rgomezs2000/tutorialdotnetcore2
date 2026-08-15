using System;
using System.Linq;

namespace tutorialdotnetcore2
{
    public class DatosNumericos
    {
        public static void DatoNumero()
        {
            Console.WriteLine($"int.MaxValue: {int.MaxValue}");
            Console.WriteLine($"long.MaxValue: {long.MaxValue}");
            Console.WriteLine($"double.Epsilon: {double.Epsilon}");

            double sumaDoble = 0.1 + 0.2;
            decimal sumaDecimal = 0.1m + 0.2m;

            Console.WriteLine($"double: {sumaDoble}");
            Console.WriteLine($"decimal: {sumaDecimal}");
        }
    }
}
