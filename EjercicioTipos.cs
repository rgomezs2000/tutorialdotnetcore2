using System;
using System.Linq;

namespace tutorialdotnetcore2
{
    public class EjercicioTipos
    {
        //Método principal que orquesta el flujo
        public static void Ejecutar()
        {
            Console.WriteLine("=== FICHA DE REGISTRO DE USUARIO ===\n");

            var datos = PedirDatos();

            Console.WriteLine("\n=== FICHA DE REGISTRO DE USUARIO ===\n");

        }

        //Metodo 1: recolecta toda la entrada del usuario
        private static DatosUsuario PedirDatos()
        {

            Console.Write("Nombre completo: ");
            string nombreCompleto = Console.ReadLine()?.Trim() ?? "";

            Console.Write("Nombre completo: ");
            int edad = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Inicial del segundo nombre (o presiona Enter si no lo tiene): ");
            string entradaInicial = Console.ReadLine() ?? "";
            char? inicialSegundoNombre = entradaInicial.Length > 0 ? entradaInicial[0] : null;

            Console.Write("Salario mensual: ");
            decimal salario = decimal.Parse(Console.ReadLine() ?? "0");

            Console.Write("Estatura en metros: ");
            double estatura = double.Parse(Console.ReadLine() ?? "0");

            Console.Write("¿Tiene cédula? (true/false): ");
            bool tieneCedula = bool.Parse(Console.ReadLine() ?? "false");

            DatosUsuario datosUsuario = new DatosUsuario(nombreCompleto, edad, inicialSegundoNombre, salario, estatura, tieneCedula);

            return datosUsuario;
        }

        //Metodo 2: Toma los datos crudos y calcula todo lo derivado
        private static ResultadoProcesado ProcesadoDatos(DatosUsuario datos)
        {
            string[] partesNombre = datos.NombreCompleto.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            int cantidadPalabras = partesNombre.Length;

            int vocales = ContarVocales(datos.NombreCompleto);

            bool esMayordeEdad = datos.Edad >= 18;
            bool puedeVotar = esMayordeEdad && datos.TieneCedula;
            bool salarioValido = datos.Salario > 0;

            decimal salarioAnual = datos.Salario * 12;
            decimal bonificacion = salarioValido ? datos.Salario * 0.10m : 0m;

            string categoria = ClasificarEstatura(datos.Estatura);

            string iniciales = string.Join("", partesNombre.Select(p => char.ToUpper(p[0])));

            ResultadoProcesado resultado = new ResultadoProcesado(cantidadPalabras, vocales, iniciales, esMayordeEdad,
                puedeVotar, salarioAnual, bonificacion, categoria);

            return resultado;
        }

        //Metodo auxiliar: ContarVocales
        private static int ContarVocales(string texto)
        {
            int vocales = 0;
            string letrasVocales = "AEIOUÁÉÍÓÚÜaeiouáéíóíü";

            foreach(char c in texto)
            {
                if (letrasVocales.Contains(c))
                {
                    vocales++;
                }
            }

            return vocales;
        }

        //Metodo Auxiliar: Clasificar la estatura usando switch expression
        private static string ClasificarEstatura(double estatura)
        {
            return estatura switch
            {
                < 1.60 => "Baja",
                >= 1.60 and <= 1.80 => "Promedio",
                _ => "Alta"
            };
        }

        //Metodo 3: solo se encarga de de mostrar el resultado final
        private static void MostrarResumen(DatosUsuario datos, ResultadoProcesado resultado)
        {
            Console.WriteLine("=== RESUMEN DE LA FICHA ===");
            Console.WriteLine($"Nombre completo: {datos.NombreCompleto}");
            Console.WriteLine($"Cantidad de palabras en el nombre: {resultado.CantidadPalabras}");
            Console.WriteLine($"Vocales en el nombre: {resultado.Vocales}");
            Console.WriteLine($"Iniciales: {resultado.Iniciales}");

            if (datos.InicialSegundoNombre.HasValue)
            {
                Console.WriteLine($"Inicial del segundo nombre {char.ToUpper(datos.InicialSegundoNombre.Value)}");
            }
            else
            {
                Console.WriteLine("Sin segundo nombre registrado");
            }

            Console.WriteLine($"Edad: {datos.Edad} años - {(resultado.EsMayorDeEdad ? "Mayor de edad" : "Menor de edad")}");
            Console.WriteLine($"Estatura: {datos.Estatura:F2} m - Categoría: {resultado.Categoria}");
            Console.WriteLine($"¿Puede votar?: {resultado.PuedeVotar}");
            Console.WriteLine($"Salario Mensual: {datos.Salario:C}");
            Console.WriteLine($"Salario anual proyectado: {resultado.SalarioAnual:C}");
            Console.WriteLine($"Bonificacion (10%): {resultado.Bonificacion:C}");

        }

        //Records para transportar datos entre metodos de forma ordenada
        private record DatosUsuario(string NombreCompleto, int Edad, char? InicialSegundoNombre,
            decimal Salario, double Estatura, bool TieneCedula);

        private record ResultadoProcesado(int CantidadPalabras, int Vocales, string Iniciales, bool EsMayorDeEdad,
            bool PuedeVotar, decimal SalarioAnual, decimal Bonificacion, string Categoria);
    }
}
