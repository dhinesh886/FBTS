using System.Web.UI.WebControls;
using FBTS.Model.Common;
using FBTS.Library.Common;
using FBTS.Model.Control;
using Ezy.ERP.Model.Common;

namespace Ezy.ERP.Presenter.Common
{
    public class GenericPresenter
    {  
        private readonly GenericModel _genericModel = new GenericModel();
            
        public void LoadDropDown(DropDownList argsDdl, KeyValuePairItems argsFilters, StringList argsSort,
            DDLTypes argsType, DataBaseInfo dataBaseInfo, KeyValuePairItems datasource = null)
        {
            KeyValuePairItems dt;
            if (datasource != null)// && argsType == DDLTypes.None)
            {
                dt = datasource;
            }
            else
            {
                var queryArgument = new QueryArgument(dataBaseInfo)
                {
                    DdlType = argsType,
                    Filters = argsFilters,
                    Sort = argsSort
                };
               dt = _genericModel.GetData(queryArgument);
            } 
            WebControls.ClearDdl(argsDdl, string.Empty); 
            foreach (var dr in dt)
            {
                argsDdl.Items.Add(new ListItem(dr.Value,dr.Key));
            } 
        } 

        public string GetNewMasterNumber(string argsCode, string argsType, DataBaseInfo dataBaseInfo)
        {
            var queryArgument = new QueryArgument(dataBaseInfo)
            {
                FilterKey = argsCode,
                SubFilterKey =  argsType 
            };
            var number = _genericModel.GetNewMasterNumber(queryArgument);
            return number;
        }
        public RawDataContainer GetReportData(KeyValuePairItems configurations, DataBaseInfo dataBaseInfo)
        {
            var objectContainer = new ObjectContainer(configurations, dataBaseInfo);
            return _genericModel.GetReportData(objectContainer);
        }
    }
}