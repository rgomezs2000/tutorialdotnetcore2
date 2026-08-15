using System;
using System.Linq;

namespace tutorialdotnetcore2
{
    public class DatosBooleanos
    {
        public static void DatoBool()
        {
            Console.Write("¿Tienes más de 18 años? (true/false)");
            bool esMayor = bool.Parse(Console.ReadLine() ?? "false");

            Console.Write("¿Tiene Cedula? (true/false)");
            bool tieneCedula = bool.Parse(Console.ReadLine() ?? "false");

            bool puedeVotar = esMayor && tieneCedula;
            Console.WriteLine($"¿Puede votar? {puedeVotar}");
        }
    }
}
