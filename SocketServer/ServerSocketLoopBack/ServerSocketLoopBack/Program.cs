using System;
using System.CodeDom;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Microsoft.Practices.Unity;
using ServerSocketLoopBack.Layer;
using ServerSocketLoopBack.Utility;
using ServerSocketLoopBack.Utility.Interface;

public class Program {

    /// <summary>
    /// Main method
    /// </summary>
    /// <param name="args">argumentos</param>
    /// <returns></returns>
    public static int Main(String[] args) {
        //Utilitário de configuração
        IConfigurationUtility configurationUtility = new ConfigurationUtility();

        //Define as configurações
        Program.RegisterTypes(configurationUtility);

        //Define o endereço IP
        var ipEndPoint = Program.DefineIpEndPoint(configurationUtility);

        //Inicializa o serviço
        ServerSocket.StartListening(ipEndPoint);

        return 0;
    }

    /// <summary>
    /// Define as configurações
    /// </summary>
    public static void RegisterTypes(IConfigurationUtility configurationUtility) {
        //Carrega a instância do container
        var container = DependencyInjection.GetContainerInstance();

        //Registra o tipo
        container.RegisterInstance(typeof(IConfigurationUtility), configurationUtility);
    }

    public static IPEndPoint DefineIpEndPoint(IConfigurationUtility configurationUtility) {
        //Endereço do serviço
        var ipAddress = IPAddress.Parse(configurationUtility.IpAddress);
        //Endereço local
        var ipEndPoint = new IPEndPoint(ipAddress, 1234);

        //Retorno
        return ipEndPoint;
    }
}