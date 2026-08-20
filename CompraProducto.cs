using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace tutorialdotnetcore2
{
    public class CompraProducto
    {
        //Metodo que permite ejecutar y orquestar la facturacion
        public static void Ejecutar()
        {
            Console.WriteLine("=== FACTURACION ===\n");
            Console.WriteLine("=== POR FAVOR LLENE TUS DATOS ===\n");

            DatosClientes datosClientes = PedirDatosCliente();

            Console.WriteLine("=== INICIAR PROCESO DE FACTURACION ===\n");

            DatosFactura factura = TotalizarFactura(datosClientes);

            Console.WriteLine("=== TU FACTURA ===\n");

            MostrarFactura(datosClientes, factura);
        }

        //Método que permite recolectar datos del cliente a facturar
        private static DatosClientes PedirDatosCliente()
        {
            Console.Write("Nombre del cliente: ");
            string nombre = Console.ReadLine()?.Trim() ?? "";

            Console.Write("Identificacion del cliente: ");
            string dni = Console.ReadLine() ?? "0";

            Console.Write("Direccion del cliente: ");
            string direccion = Console.ReadLine()?.Trim() ?? "";

            Console.Write("Teléfono del cliente: ");
            string telefono = Console.ReadLine() ?? "0";

            Console.Write("¿Es cliente VIP?: (si/no)");
            bool vip = ConvertirSiNo(Console.ReadLine() ?? "no");
            
            DatosClientes datosClientes = new DatosClientes(nombre, dni, direccion, telefono, vip);

            return datosClientes;
        }

        //Metodo para recolectar los datos de los productos
        private static DatosProductos PedirProductosXFactura()
        {
            int codigoProducto = Random.Shared.Next(1, 999999);

            Console.Write("Nombre del producto: ");
            string nombre = Console.ReadLine()?.Trim() ?? "";

            Console.Write("Categoria del producto: ");
            string strCategoria = Console.ReadLine()?.Trim() ?? "";

            char categoria;

            switch (strCategoria)
            {
                case "A":
                    categoria = 'A';
                    break;
                case "R":
                    categoria = 'R';
                    break;
                case "T":
                    categoria = 'T';
                    break;
                case "Otros":
                    categoria = 'O';
                    break;
                default:
                    categoria = 'N';
                    break;
            }

            Console.Write("Precio del producto: ");
            decimal precio = decimal.Parse(Console.ReadLine() ?? "0");
            precio = precio > 0 ? precio : 0m;

            Console.Write("Cantidad del producto: ");
            int cantidad = int.Parse(Console.ReadLine() ?? "0");
            cantidad = cantidad > 0 ? cantidad : 0;

            // Validación explícita
            bool compraValida = (precio > 0) && (cantidad > 0);

            if (!compraValida)
            {
                Console.WriteLine("⚠️ Advertencia: Precio y cantidad deben ser mayores a 0");
            }

            Console.Write("Porcentaje: ");
            decimal porcentaje = decimal.Parse(Console.ReadLine() ?? "0");
            porcentaje = porcentaje > 0 ? porcentaje : 0m;

            decimal totalProducto = TotalProducto(precio, cantidad);
            totalProducto = totalProducto > 0 ? totalProducto : 0m;

            decimal descuento = ObtenerDescuentoPrecio(porcentaje, precio);

            DatosProductos datosProductos = new DatosProductos(codigoProducto, nombre, precio, cantidad, totalProducto, porcentaje, descuento, categoria);

            return datosProductos;
        }

        //Metodo para armar y totalizar la factura:

        private static DatosFactura TotalizarFactura(DatosClientes datosClientes)
        {
            int codigoFactura = Random.Shared.Next(1, 999999);
            DateTime fechaFactura = DateTime.Now;

            List<DatosProductos> productos = new List<DatosProductos>();
            bool agregarProducto = false;

            do
            {
                DatosProductos datosProductos = PedirProductosXFactura();

                productos.Add(datosProductos);

                Console.Write("¿Desea agregar otro producto? (si/no): ");
                string strAgregarProd = Console.ReadLine() ?? "no";
                agregarProducto = ConvertirSiNo(strAgregarProd);

            } while (agregarProducto == true);

            decimal subtotalFactura = 0m;
            decimal descuentoFactura = 0m;

            foreach(var producto in productos)
            {
                subtotalFactura += producto.TotalProducto;
            }

            foreach (var producto in productos)
            {
                descuentoFactura += producto.DescuentoProducto;
            }

            decimal subtotalDescuentoFactura = ObtenerDescuentoSubtotal(subtotalFactura, descuentoFactura);

            decimal VIPDescuento= 0m;

            if (datosClientes.VIP)
            {
                VIPDescuento = ObtenerDescuentoVIP(subtotalDescuentoFactura);
            }

            decimal subtotalDescuentoFacturaFinal = ObtenerSubtotalDescuentoFinal(subtotalDescuentoFactura, VIPDescuento);

            decimal iva = ObtenerIVA(subtotalDescuentoFacturaFinal);

            decimal total = ObtenerTotal(subtotalDescuentoFacturaFinal, iva);

            DatosFactura datosFactura = new DatosFactura(codigoFactura, fechaFactura, productos,
                subtotalFactura,descuentoFactura, subtotalDescuentoFactura,
                VIPDescuento, subtotalDescuentoFacturaFinal, iva,
                total);

            return datosFactura;

        }

        //Metodo que permite mostrar la factura
        private static void MostrarFactura(DatosClientes datosClientes, DatosFactura datosFactura)
        {
            string linea = new string('=', 80);
            string lineaProductos = new string('-', 80);

            Console.WriteLine(linea);
            Console.WriteLine(new string(' ', 30) + "*** FACTURA ***");
            Console.WriteLine(linea);
            Console.WriteLine();

            // Encabezado de la factura
            Console.WriteLine($"Factura #: {datosFactura.CodigoFactura}");
            Console.WriteLine($"Fecha: {datosFactura.FechaFactura:dd/MM/yyyy HH:mm:ss}");
            Console.WriteLine();

            // Datos del cliente
            Console.WriteLine(lineaProductos);
            Console.WriteLine("DATOS DEL CLIENTE");
            Console.WriteLine(lineaProductos);
            Console.WriteLine($"Nombre: {datosClientes.NombreCliente}");
            Console.WriteLine($"DNI: {datosClientes.DNICliente}");
            Console.WriteLine($"Dirección: {datosClientes.DireccionCliente}");
            Console.WriteLine($"Teléfono: {datosClientes.TelefonoCliente}");
            Console.WriteLine($"Estado: {(datosClientes.VIP ? "CLIENTE VIP ⭐" : "Cliente Regular")}");
            Console.WriteLine();

            // Detalles de productos
            Console.WriteLine(lineaProductos);
            Console.WriteLine(String.Format("{0,-15} {1,-25} {2,-12} {3,-12} {4,-10} {5,-13}",
                "CÓDIGO", "PRODUCTO", "CATEGORÍA", "PRECIO", "CANTIDAD", "SUBTOTAL"));
            Console.WriteLine(lineaProductos);

            foreach (var producto in datosFactura.Productos)
            {
                string nombreCategoria = ObtenerNombreCategoria(producto.CategoriaProducto);

                Console.WriteLine(String.Format("{0,-15} {1,-25} {2,-12} {3,-12:C} {4,-10} {5,-13:C}",
                    producto.CodigoProducto,
                    producto.NombreProducto,
                    nombreCategoria,
                    producto.PrecioProducto,
                    producto.CantidadProducto,
                    producto.TotalProducto));
            }

            Console.WriteLine(lineaProductos);
            Console.WriteLine();

            // Resumen de totales
            Console.WriteLine("RESUMEN DE TOTALES");
            Console.WriteLine(lineaProductos);
            Console.WriteLine($"Subtotal (sin descuentos)............... {datosFactura.SubtotalFactura,18:C}");
            Console.WriteLine($"Descuento de productos................. -${datosFactura.DescuentoFactura,14:F2}");
            Console.WriteLine($"Subtotal con descuento................. ${datosFactura.SubtotalDescuentoFactura,15:F2}");

            if (datosClientes.VIP)
            {
                Console.WriteLine($"Descuento VIP (5%)...................... -${datosFactura.VIPDescuentoFactura,14:F2}");
            }

            Console.WriteLine($"Subtotal final.......................... ${datosFactura.SubtotalDescuentoFacturaFinal,15:F2}");
            Console.WriteLine($"IVA (19%).............................. +${datosFactura.IVAFactura,14:F2}");
            Console.WriteLine(linea);
            Console.WriteLine($"TOTAL A PAGAR........................... ${datosFactura.TotalFactura,15:F2}");
            Console.WriteLine(linea);
            Console.WriteLine();
            Console.WriteLine(new string(' ', 25) + "¡Gracias por su compra!");
            Console.WriteLine();
        }

        //Metodo auxiliar calculo de cantidad del producto por precio
        private static decimal TotalProducto(decimal precio, int cantidad)
        {
            decimal total = precio * cantidad;
            
            return total;
        }

        //Metodo auxiliar para obtener el valor del descuento de un precio
        private static decimal ObtenerDescuentoPrecio(decimal porcentaje, decimal precio)
        {
            decimal descuento = (precio * porcentaje) / 100;

            return descuento;
        }

        //Metodo auxiliar que permite calcular el descuento aplicado al subtotal de la factura
        private static decimal ObtenerDescuentoSubtotal(decimal subtotal, decimal descuento)
        {
            decimal subtotalDescuentoFactura = subtotal - descuento;
            
            return subtotalDescuentoFactura;
        }

        //Metodo auxiliar para obtener el valor del subtotal para los clientes VIP
        private static decimal ObtenerDescuentoVIP(decimal subtotal)
        {
            decimal descuentoVIP = (subtotal * 5) / 100;

            return descuentoVIP;
        }

        //Metodo para calcular el subtotal general (con el descuento)
        private static decimal ObtenerSubtotalDescuentoFinal(decimal subtotal, decimal descuentoVIP)
        {
            decimal SubtotalDescuentoFacturaFinal = subtotal - descuentoVIP;

            return SubtotalDescuentoFacturaFinal;
        }

        //Metodo auxiliar para obtener el valor del IVA en base al subtotal de la factura
        private static decimal ObtenerIVA(decimal subtotal)
        {
            decimal iva = (subtotal * 19) / 100;

            return iva;
        }

        //Metodo auxiliar para obtener el total de la factura
        private static decimal ObtenerTotal(decimal subtotal, decimal iva)
        {
            decimal total = subtotal + iva;

            return total;
        }
        
        //Metodo auxiliar para establecer respuesta si y no
        private static bool ConvertirSiNo(string respuesta)
        {
            return respuesta.ToLower() == "si" || respuesta.ToLower() == "sí";
        }

        //Metodo auxiliar para mostrar categoria:
        private static string ObtenerNombreCategoria(char? categoria)
        {
            return categoria switch
            {
                'A' => "Alimentos",
                'R' => "Ropa",
                'T' => "Tecnología",
                'O' => "Otros",
                _ => "No categorizado"
            };
        }

        //Record para los datos del Cliente
        private record DatosClientes(string NombreCliente,
            string DNICliente,
            string DireccionCliente,
            string TelefonoCliente,
            bool VIP);

        // Record para los datos de los productos individuales
        private record DatosProductos(
            int CodigoProducto,
            string NombreProducto,
            decimal PrecioProducto,
            int CantidadProducto,
            decimal TotalProducto,
            decimal PorcentajeDescuento,
            decimal DescuentoProducto,
            char? CategoriaProducto);

        // Record para la factura completa
        private record DatosFactura(
            int CodigoFactura,
            DateTime FechaFactura,
            List<DatosProductos> Productos,
            decimal SubtotalFactura,
            decimal DescuentoFactura,
            decimal SubtotalDescuentoFactura,
            decimal VIPDescuentoFactura,
            decimal SubtotalDescuentoFacturaFinal,
            decimal IVAFactura,
            decimal TotalFactura);
    }
}
