using ServerSocketBib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EjercicioSocket
{
    public class Program
    {
        static void Main(string[] args)
        {
            int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["puerto"]);
            ServerSocket servidor = new ServerSocket(puerto);

            if (servidor.Iniciar())
            {
                //ok puede iniciar
                Console.WriteLine("Servidor Iniciado");
                while (true)
                {
                    Console.WriteLine("Esperando Cliente");
                    Socket socketCliente = servidor.ObtenerCliente();
                    //construir el mecanismo para escribirle y leerle 
                    ClienteCom cliente = new ClienteCom(socketCliente);
                    //aqui esta el protocolo de comunicacion, que ambos saben

                    string respuestaCliente;
                    string respuestaServidor;
                    bool esValido = true;
                    do
                    {
                        respuestaCliente = cliente.Leer().Trim();
                        Console.WriteLine("El cliente dice: {0}", respuestaCliente);
                        respuestaServidor = Console.ReadLine();
                        cliente.Escribir(respuestaServidor);

                        if (respuestaServidor == "chao")
                        {
                            cliente.Desconectar();
                            esValido = false;
                        }
                        

                    } while (esValido);

                }
            }
            else
            {
                Console.WriteLine("Error, el puerto {0} esta en uso", puerto);
            }
        }
    }
}
