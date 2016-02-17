using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace ServerSocketLoopBack.Utility {

    /// <summary>
    /// Controle de injeção de dependência
    /// </summary>
    public static class DependencyInjection {

        /// <summary>
        /// Instância
        /// </summary>
        private static IUnityContainer _instance;

        /// <summary>
        /// Objeto de sincronismo
        /// </summary>
        private static readonly object SyncObj = new object();

        /// <summary>
        /// Método para obter a instância
        /// </summary>
        /// <returns></returns>
        public static IUnityContainer GetContainerInstance() {
            
            //Verifica se o objeto já existe
            if (_instance != null) return _instance;

            //Efetua o lock do objeto
            lock (SyncObj) {
                //Verifica se o objeto já existe
                if (_instance == null){
                    //Inicializa a variáel
                    _instance = new UnityContainer();
                }
            }

            //Retorno
            return _instance;
        }
    }

}
