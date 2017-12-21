using FBTS.Library.Common;
using FBTS.Model.Common;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using FBTS.Data.Manager;
using System;
using FBTS.Library.EventLogger;
using System.Runtime.InteropServices;

namespace FBTS.Business.Manager
{
    public class GenericManager
    {
        private readonly ControlPanelManager _controlPanel = new ControlPanelManager();
        private readonly ReportManager _reportManager = new ReportManager();
        public KeyValuePairItems GetData(QueryArgument queryArgument)
        {
            KeyValuePairItems keyValuePairItems;
            try
            {
                ControlPanelReadHelper.QueryArgument = queryArgument;
                keyValuePairItems = ControlPanelReadHelper.GetData();
            }
            catch (Exception ex)
            {
                EventLogger.LogEvent(SysEventType.ERROR.ToString(), "Error", "GetData failed with exception", ex);
                throw;
            }
            return keyValuePairItems;
        }
        public string GetNewMasterNumber(QueryArgument queryArgument)
        {
            try
            {
                ControlPanelReadHelper.QueryArgument = queryArgument;
                return ControlPanelReadHelper.GetNewMasterNumber();
            }
            catch (Exception ex)
            {
                EventLogger.LogEvent(SysEventType.ERROR.ToString(), "Error", "GetNewMasterNumber failed with exception", ex);
                throw;
            }
        }

        public KeyValuePairItems LoadDropDown(DropDownList argsDdl, KeyValuePairItems argsFilters, StringList argsSort,
            DataBaseInfo dataBaseInfo, KeyValuePairItems datasource = null)
        {
            KeyValuePairItems dt;
            if (datasource != null)
            {
                dt = datasource;
            }
            else
            {
                var queryArgument = new QueryArgument(dataBaseInfo)
                {
                    Filters = argsFilters,
                    Sort = argsSort
                };
                dt = GetData(queryArgument);
            }
            if (argsDdl == null)
                return dt;
            WebControls.ClearDdl(argsDdl, string.Empty);
            foreach (var dr in dt)
            {
                argsDdl.Items.Add(new ListItem(dr.Value, dr.Key));
            }
            return dt;
        }
        public bool LoadDropDownIfMorereturnFalse(DropDownList argsDdl, KeyValuePairItems argsFilters, StringList argsSort,
           DataBaseInfo dataBaseInfo)
        {
            KeyValuePairItems dt;
            var more = false;

            var queryArgument = new QueryArgument(dataBaseInfo)
            {
                Filters = argsFilters,
                Sort = argsSort
            };
            dt = GetData(queryArgument);
            WebControls.ClearDdl(argsDdl, string.Empty);
            if (dt.Count > 10000 )
            {
                more = true;
            }
            else
            {
                foreach (var dr in dt)
                {
                    argsDdl.Items.Add(new ListItem(dr.Value, dr.Key));
                }
            }
            return more;
        }

        public void LoadList(CheckBoxList checkboxList, KeyValuePairItems argsFilters, StringList argsSort,
                                DataBaseInfo dataBaseInfo, KeyValuePairItems datasource = null)
        {
            KeyValuePairItems dt; 
            if (datasource != null)
            {
                dt = datasource;
            }
            else
            {
                var queryArgument = new QueryArgument(dataBaseInfo)
                {
                    Filters = argsFilters,
                    Sort = argsSort
                };
                dt = GetData(queryArgument);
            }
            foreach (var dr in dt)
            {
                checkboxList.Items.Add(new ListItem(dr.Value, dr.Key));
            } 
        }

        public List<ListItem> LoadList(KeyValuePairItems argsFilters, StringList argsSort,
           DataBaseInfo dataBaseInfo, KeyValuePairItems datasource = null)
        {
            KeyValuePairItems dt;
            List<ListItem> listItems = new List<ListItem>();
            if (datasource != null)// && argsType == DDLTypes.None)
            {
                dt = datasource;
            }
            else
            {
                var queryArgument = new QueryArgument(dataBaseInfo)
                {
                    Filters = argsFilters,
                    Sort = argsSort
                };
                dt = GetData(queryArgument);
            }            
            foreach (var dr in dt)
            {
                listItems.Add(new ListItem(dr.Value, dr.Key));
            }
            return listItems;
        }
      
        public string GetNewMasterNumber(string key, string argsType, DataBaseInfo dataBaseInfo)
        {
            var queryArgument = new QueryArgument(dataBaseInfo)
            {
                Key = key,
                FilterKey = argsType
            };
            var number = GetNewMasterNumber(queryArgument);
            return number;           
        }

        public RawDataContainer GetReportData(KeyValuePairItems configurations, DataBaseInfo dataBaseInfo)
        {
            var objectContainer = new ObjectContainer(configurations, dataBaseInfo);
            return _reportManager.GetReportData(objectContainer);
        }

        public KeyValuePairItem Delete(string key1, string key2, string key3, string Action,
                                   string queryType, string type, DataBaseInfo dataBaseInfo)
        {
            KeyValuePairItem status;
            try
            {
                var queryArgument = new QueryArgument(dataBaseInfo)
                {
                    filter1 = key1,
                    filter2 = key2,
                    filter3 = key3,
                    Action = Action,
                    QueryType = queryType,
                    filterType = type
                };
                ControlPanelReadHelper.QueryArgument = queryArgument;
                status= ControlPanelReadHelper.ConfirmAndDeleteBan();
            }
            catch(Exception ex)
            {
                status = new KeyValuePairItem("2", "Could not complete your request!");
                EventLogger.LogEvent(SysEventType.ERROR.ToString(), "Error", "Delete failed with exception", ex);
                throw;
            }
            return status;
        }
    }
}
