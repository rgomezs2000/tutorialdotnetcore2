using System;
using System.Linq;

namespace tutorialdotnetcore2
{
    public class CadenaCaracteres
    {
        public static void LeerSring()
        {
            Console.Write("Escribe tu nombre:");
            string nombreCompleto = Console.ReadLine() ?? "";

            string[] partes = nombreCompleto.Trim().Split(" ");

            Console.WriteLine($"Cantidad de palabras: {partes.Length}");
            Console.WriteLine($"Nombre en mayúscula: {nombreCompleto.ToUpper()}");
            Console.WriteLine($"¿Contiene 'Roger'?: {nombreCompleto.Contains("Roger")}");

            if (partes.Length > 0)
            {
                Console.WriteLine($"Iniciales: {string.Join("", partes.Select(p => p[0]))}");
            }
        }
    }
}
