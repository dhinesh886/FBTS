using System;

namespace FBTS.Library.Common.Services
{
    public static class ServiceBinding
    {

        private const string PreferredBindingKey = "PreferredBinding";
        private const string Basichttp = "BASICHTTP";
        private const string Wshttp = "WSHTTP";
        private const string Tcp = "TCP";
        private const string Ipc = "IPC";

        private static BindingTypes _defaultBinding = BindingTypes.None;
        
        /// <summary>
        ///
        /// </summary>
        public static string[,] BindingNames =
        {
            {"WSHttpBinding_IUserAccessControl","NetTcpBinding_IUserAccessControl", string.Empty,string.Empty},
            {"WSHttpBinding_IControlPanel","NetTcpBinding_IControlPanel", string.Empty,string.Empty},
            {"WSHttpBinding_ITransactionMasters","NetTcpBinding_ITransactionMasters", string.Empty,string.Empty},
            {"WSHttpBinding_ICommonTransactions","NetTcpBinding_ICommonTransactions", string.Empty,string.Empty}
        };
        
        /// <summary>
        /// Gets the binding.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="binding">The binding.</param>
        /// <returns></returns>

        public static string GetBindingName(Services service, BindingTypes binding)
        {
            switch (binding)
            {
                case BindingTypes.WsHttpBinding:
                    return BindingNames[(int)service, 0]; 
                case BindingTypes.NetTcpBinding:
                    return BindingNames[(int)service, 1];
                case BindingTypes.NetNamedPipeBinding: 
                    return BindingNames[(int)service, 2]; 
                case BindingTypes.BasicHttpBinding: 
                    return BindingNames[(int)service, 3]; 
                default: 
                    return string.Empty; 
            }
        }

        /// <summary>
        /// Gets the default binding.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <returns></returns>
        public static BindingTypes GetDefaultBinding(Services service)
        {   
            if (_defaultBinding == BindingTypes.None)
            { 
                GetBindingFromConfig(); 
            } 
            return _defaultBinding; 
             
        } 
        /// <summary> 
        /// Gets the binding from config. 
        /// </summary> 
        public static void GetBindingFromConfig()
        {
            _defaultBinding = BindingTypes.NetTcpBinding; 
            var binding = ConfigurationReader<string>.GetAppConfigurationValue(PreferredBindingKey);  
            if (string.IsNullOrEmpty(binding))
            { 
                return; 
            }  
            if (0 == String.Compare(binding, Wshttp, StringComparison.OrdinalIgnoreCase))
            { 
                _defaultBinding = BindingTypes.WsHttpBinding; 
            }
            else if (0 == String.Compare(binding, Tcp, StringComparison.OrdinalIgnoreCase))
            { 
                _defaultBinding = BindingTypes.NetTcpBinding; 
            } 
            else if (0 == String.Compare(binding, Ipc, StringComparison.OrdinalIgnoreCase))
            { 
                _defaultBinding = BindingTypes.NetNamedPipeBinding; 
            }
            else if (0 == String.Compare(binding, Wshttp, StringComparison.OrdinalIgnoreCase))
            { 
                _defaultBinding = BindingTypes.BasicHttpBinding; 
            } 
        }



        /// <summary>

        /// Gets the binding from config.

        /// </summary>

        public static BindingTypes GetBinding(string bindingName)
        {

            if (string.IsNullOrEmpty(bindingName))
            {

                return BindingTypes.WsHttpBinding;

            }



            if (0 == String.Compare(bindingName, Wshttp, StringComparison.OrdinalIgnoreCase))
            {

                return BindingTypes.WsHttpBinding;

            }

            if (0 == String.Compare(bindingName, Tcp, StringComparison.OrdinalIgnoreCase))
            {

                return BindingTypes.NetTcpBinding;

            }

            if (0 == String.Compare(bindingName, Ipc, StringComparison.OrdinalIgnoreCase))
            {

                return BindingTypes.NetNamedPipeBinding;

            }

            if (0 == String.Compare(bindingName, Basichttp, StringComparison.OrdinalIgnoreCase))
            {

                return BindingTypes.BasicHttpBinding;

            }


            return BindingTypes.WsHttpBinding;

        }

    }
}
