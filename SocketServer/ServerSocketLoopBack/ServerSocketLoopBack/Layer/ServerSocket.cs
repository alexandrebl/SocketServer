using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using ServerSocketLoopBack.Utility;
using ServerSocketLoopBack.Utility.Interface;
using Microsoft.Practices.Unity;

namespace ServerSocketLoopBack.Layer {

    /// <summary>
    /// Servidor de teste de loopback
    /// </summary>
    public class ServerSocket {

        /// <summary>
        /// Utilitário de configuração
        /// </summary>
        private static IConfigurationUtility ConfigurationUtility { get; set; }

        /// <summary>
        /// Controle de sinal
        /// </summary>
        private static readonly ManualResetEvent AllDone = new ManualResetEvent(false);

        /// <summary>
        /// Inicia o serviço
        /// </summary>
        /// <param name="ipEndPoint">endereço do endpoint</param>
        public static void StartListening(IPEndPoint ipEndPoint) {

            //Carrega a instância do container
            var container = DependencyInjection.GetContainerInstance();

            //Carrega o utilitário de configuração
            ServerSocket.ConfigurationUtility = container.Resolve<IConfigurationUtility>();

            // Buffer de entrada
            var bytes = new byte[ServerSocket.ConfigurationUtility.BufferSize];

            //Abre a conexão de serviço
            var listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try {
                //Prepara a conexão
                listener.Bind(ipEndPoint);
                //Abre a conexão
                listener.Listen(ServerSocket.ConfigurationUtility.Listeners);

                while (true) {
                    //Envia o sinal de inicialização
                    AllDone.Reset();

                    //Aceita a conexão
                    listener.BeginAccept(new AsyncCallback(AcceptCallback), listener);

                    //Aguarda a finalização
                    AllDone.WaitOne();
                }

            } catch (Exception e) {
                //Imprime a mensagem de erro
                Console.WriteLine(e.ToString());
            }
        }

        /// <summary>
        /// Evento de inicio de recepção de dados
        /// </summary>
        /// <param name="ar">Controle de evento</param>
        private static void AcceptCallback(IAsyncResult ar) {
            //Sinaliza que a thread principal deve continuar
            AllDone.Set();

            //Obtem o estado
            var listener = (Socket)ar.AsyncState;

            //Obtem o socket
            var handler = listener.EndAccept(ar);

            //Buffer de dados
            var buffer = new byte[ServerSocket.ConfigurationUtility.BufferSize];

            //Leitura contínua
            while (true) {
                //Total de bytes recebidos
                var bytesRead = handler.Receive(buffer, 0, ServerSocket.ConfigurationUtility.BufferSize, 0);

                //Verifica se foram obtidos dados
                if (bytesRead > 0) {
                    //Reenvia os dados recebidos
                    handler.Send(buffer, 0, bytesRead, 0);
                    //Continua o loop
                    continue;
                }

                //Desabilita a conexão
                handler.Shutdown(SocketShutdown.Both);
                //Fecha a conexão
                handler.Close();

                //Para o loop
                break;
            }
        }
    }
}
