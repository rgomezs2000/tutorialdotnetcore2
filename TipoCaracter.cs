using System;
using System.Linq;

namespace tutorialdotnetcore2
{
    public class TipoCaracter
    {
        public static void TipoChar()
        {
            Console.Write("Escriba una palabra:");
            string palabra = Console.ReadLine() ?? "";

            int vocales = 0;

            foreach(char c in palabra)
            {
                if("AEIOUÁÉÍÓÚÜaeiouáéíóúü".Contains(c))
                {
                    vocales++;
                }
            }

            Console.WriteLine($"La palabra tiene {vocales} vocales");
            Console.WriteLine($"La primera letra en mayuscula: {char.ToUpper(palabra[0])}");
        }
    }
}
