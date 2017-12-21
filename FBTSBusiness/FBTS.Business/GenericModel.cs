using FBTS.Model.Common;
using System;

namespace Ezy.ERP.Model.Common
{
    public class GenericModel
    {
        public KeyValuePairItems GetData(QueryArgument queryArgument)
        {
            var controlPanelProxy = new ControlPanelProxy();
            return controlPanelProxy.GetData(queryArgument); 
        }
        public string GetNewMasterNumber(QueryArgument queryArgument)
        {
            var controlPanelProxy = new ControlPanelProxy();
            return controlPanelProxy.GetNewMasterNumber(queryArgument);
        }
        public RawDataContainer GetReportData(ObjectContainer configurations)
        {
            var controlPanelProxy = new ControlPanelProxy();
            return controlPanelProxy.GetReportData(configurations);
        }


        private ClientProfile toCommonClientProfile(ClientInfo clientInfo)
        {
            return new ClientProfile
            {
                ClientApplication = clientInfo.ClientApplication,
                ClientIpAddress = clientInfo.ClientIpAddress,
                ClientMachineName = clientInfo.ClientMachineName,
                UserId = clientInfo.UserId,
                UserName = clientInfo.UserName
            };
        }
        private string getMessage(Ezy.Library.EventLogger.SemaphoreResponseCode responseCode)
        {
            return responseCode.ToString();
        } 
    }
}