using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerSocketLoopBack.Utility.Interface;

namespace ServerSocketLoopBack.Utility {

    /// <summary>
    /// Utilitário de configuração
    /// </summary>
    public class ConfigurationUtility : IConfigurationUtility {

        /// <summary>
        /// Tamanho do buffer
        /// </summary>
        public int BufferSize {
            get { return int.Parse(ConfigurationManager.AppSettings["BufferSize"]); }
        }

        /// <summary>
        /// Quantidade de threads
        /// </summary>
        public int Listeners {
            get { return int.Parse(ConfigurationManager.AppSettings["Listeners"]); }
        }

        /// <summary>
        /// Define o endereço IP
        /// </summary>
        public string IpAddress {
            get { return ConfigurationManager.AppSettings["IpAddress"]; }
        }
    }
}
