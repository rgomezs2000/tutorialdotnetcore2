using Microsoft.AspNetCore.Components.Web;
using System;
using System.Linq;

namespace tutorialdotnetcore2
{
    public class ValidadorCompra
    {
        //Estructura para transportar datos
        private record DatosCompra(
            decimal Monto,
            bool EsVIP,
            bool TieneCupon,
            bool EsPrimeraCompra);

        private record ResultadoCompra(
            decimal DescuentoDuplicado,
            string RazonDescuento,
            decimal MontoConDescuento,
            decimal IVA,
            decimal total);

        //Metodo 1: Pedir datos
        private static DatosCompra PedirDatos()
        {
            Console.Write("Monto de la compra:");
            decimal monto = decimal.Parse(Console.ReadLine() ?? "0");

            Console.Write("¿Es cliente VIP? (si/no):");
            bool esVIP = Console.ReadLine()?.ToLower() == "si";

            Console.Write("¿Tiene cupon de descuento? (si/no):");
            bool tieneCupon = Console.ReadLine()?.ToLower() == "si";

            Console.Write("¿Primera compra? (si/no):");
            bool esPrimeraCompra = Console.ReadLine()?.ToLower() == "si";

            DatosCompra datosCompra = new DatosCompra(monto, esVIP, tieneCupon, esPrimeraCompra);

            return datosCompra;
        }

        //Metodo 2: Validar monto
        private static bool ValidarMonto(decimal monto)
        {
            if(monto <= 0)
            {
                Console.WriteLine("❌ Error: El monto debe ser mayor a 0");
                return false;
            }

            return true;
        }

        //Metodo 3: Mostrar validacion
        private static void MostrarValidacion(decimal monto)
        {
            if(monto > 1000000)
            {
                Console.WriteLine("⚠️ Compra muy grande. Se requiere verificación adicional.");
            }
            else
            {
                Console.WriteLine("✅ Monto validado correctamente");
            }
        }

        //Metodo 4: Calcular el descuento
    }
}
