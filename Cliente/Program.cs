using Cliente.Comunicacion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Comunicaciones
{
    internal class Program
    {
        static void Main(string[] args)
        {

            int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["puerto"]);
            string servidor = ConfigurationManager.AppSettings["servidor"];

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Conectado a Servidor {0} en puerto {1}", servidor, puerto);
            ClienteSocket clienteSocket = new ClienteSocket(servidor, puerto);
            bool esValido = true;

            if (clienteSocket.conectar())
            {

                string respuestaServidor;
                string respuestaCliente;

                Console.WriteLine("Conectado");

                do
                {

                    respuestaCliente = Console.ReadLine();
                    clienteSocket.Escribir(respuestaCliente);
                    respuestaServidor = clienteSocket.Leer().Trim();
                    Console.WriteLine("El servidor dice: {0}", respuestaServidor);
                    respuestaCliente = Console.ReadLine();
                    clienteSocket.Escribir(respuestaCliente);
                    if (respuestaCliente == "chao")
                    {
                        Console.WriteLine("Desconectado");
                        clienteSocket.Desconectar();
                        esValido = false;
                    }


                } while (esValido);
            }
            else
            {
                Console.WriteLine("Error de comunicacion");
            }

        }
    }
}
