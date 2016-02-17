using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSocketLoopBack.Utility.Interface {

    /// <summary>
    /// Utilitário de configuração
    /// </summary>
    public interface IConfigurationUtility {
        /// <summary>
        /// Tamanho do buffer
        /// </summary>
        int BufferSize { get; }

        /// <summary>
        /// Quantidade de threads de conexões de clientes
        /// </summary>
        int Listeners { get; }

        /// <summary>
        /// Define o endereço IP
        /// </summary>
        string IpAddress { get; }
    }
}
