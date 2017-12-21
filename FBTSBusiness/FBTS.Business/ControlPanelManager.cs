using FBTS.Data.Manager;
using FBTS.Model.Common;
using System;
using FBTS.Library.EventLogger;
using FBTS.Model.Control;
using FBTS.Library.Statemanagement;
using FBTS.Model.Transaction;
using FBTS.Model.Transaction.Accounts;

namespace FBTS.Business.Manager
{
    public class ControlPanelManager
    {
        public UserContext UserContext
        {
            get { return SessionManagement<UserContext>.GetValue(Constants.UserContextSessionKey); }
        }
        public Designations GetDesignations(QueryArgument queryArgument)
        {
            Designations designations;
            try
            {
                ControlPanelReadHelper.QueryArgument = queryArgument;
                designations = ControlPanelReadHelper.GetDesignations();
            }
            catch (Exception ex)
            {
                EventLogger.LogEvent(SysEventType.ERROR.ToString(), "Error", "GetDesignation failed with exception", ex);
                throw;
            }
            return designations;
        }
        public bool SetDesignation(Designations designations)
        {
            try
            {
                bool status = ControlPanelWriteHelper.SetDesignations(designations);
                return status;
            }
            catch (Exception ex)
            {
                EventLogger.LogEvent(SysEventType.ERROR.ToString(), "Error", "SetDesignation failed with exception", ex);
                throw;
            }
        }
        public Menus GetMenuAccessRights(QueryArgument queryArgument)
        {
            Menus menus;
            try
            {
                ControlPanelReadHelper.QueryArgument = queryArgument;
                menus = ControlPanelReadHelper.GetMenuAccessRights();
            }
            catch (Exception ex)
            {
                EventLogger.LogEvent(SysEventType.ERROR.ToString(), "Error", "GetMenuAccessRights failed with exception", ex);
                throw;
            }
            return menus;
        }
        public bool SetMenuAccessRights(MenuAccessRights menuAccessRights)
        {
            bool result;
            try
            {
                result = ControlPanelWriteHelper.SetDesignationAccessRights(menuAccessRights);
            }
            catch (Exception ex)
            {
                EventLogger.LogEvent(SysEventType.ERROR.ToString(), "Error", "DesignationAccessRights failed with exception", ex);
                throw;
            }
            return result;
        }
        public UserProfiles GetUserProfiles(QueryArgument queryArgument)
        {
            UserProfiles userProfiles;
            try
            {
                ControlPanelReadHelper.QueryArgument = queryArgument;
                userProfiles = ControlPanelReadHelper.GetUserDetails();
            }
            catch (Exception ex)
            {
                EventLogger.LogEvent(SysEventType.ERROR.ToString(), "Error", "GetUserProfiles failed with exception", ex);
                throw;
            }
            return userProfiles;
        }
        public bool ManageUsers(UserProfiles userProfiles)
        {
            try
            {
                bool status = ControlPanelWriteHelper.ManageUsers(userProfiles);
                return status;
            }
            catch (Exception ex)
            {
                EventLogger.LogEvent(SysEventType.ERROR.ToString(), "Error", "ManageUsers failed with exception", ex);
                throw;
            }

        }
        public Categories GetCategories(QueryArgument queryArgument)
        {
            Categories categories;
            try
            {
                ControlPanelReadHelper.QueryArgument = queryArgument;
                categories = ControlPanelReadHelper.GetCategoryDetails();
            }
            catch (Exception ex)
            {
                EventLogger.LogEvent(SysEventType.ERROR.ToString(), "Error", "GetCategory failed with exception", ex);
                throw;
            }
            return categories;
        }
        public bool SetCategory(Categories categories)
        {
            try
            {
                bool status = ControlPanelWriteHelper.SetCategory(categories);
                return status;
            }
            catch (Exception ex)
            {
                EventLogger.LogEvent(SysEventType.ERROR.ToString(), "Error", "SetCategory failed with exception", ex);
                throw;
            }
        }
        public MaterialHierarchies GetMaterialHierarchies(QueryArgument queryArgument)
        {
            MaterialHierarchies materialHierarchies;
            try
            {
                ControlPanelReadHelper.QueryArgument = queryArgument;
                materialHierarchies = ControlPanelReadHelper.GetMaterialHierarchyDetails();
            }
            catch (Exception ex)
            {
                EventLogger.LogEvent(SysEventType.ERROR.ToString(), "Error", "GetMaterialHierarchies failed with exception", ex);
                throw;
            }
            return materialHierarchies;
        }

        public bool SetMaterialHierarchies(MaterialHierarchies materialHierarchies)
        {
            bool result;
            try
            {
                result = ControlPanelWriteHelper.SetMaterialHierarchies(materialHierarchies);
            }
            catch (Exception ex)
            {
                EventLogger.LogEvent(SysEventType.ERROR.ToString(), "Error", "SetMaterialHierarchies failed with exception", ex);
                throw;
            }
            return result;
        }
       
        public WFComponents GetTeams(QueryArgument queryArgument)
        {
            WFComponents wfcComponents;
            try
            {
                ControlPanelReadHelper.QueryArgument = queryArgument;
                wfcComponents = ControlPanelReadHelper.GetTeamMaster();
            }
            catch (Exception ex)
            {
                EventLogger.LogEvent(SysEventType.ERROR.ToString(), "Error", "Get Team failed with exception", ex);
                throw;
            }
            return wfcComponents;
        }
        public bool SetTeams(WFComponents wFComponents)
        {
            try
            {
                bool status = ControlPanelWriteHelper.SetTeamMaster(wFComponents);
                return status;
            }
            catch (Exception ex)
            {
                EventLogger.LogEvent(SysEventType.ERROR.ToString(), "Error", "Set Teams failed with exception", ex);
                throw;
            }
        }
        public DataViewSetupInfo GetStages(QueryArgument queryArgument)
        {
            DataViewSetupInfo stagesMasters;
            try
            {
                ControlPanelReadHelper.QueryArgument = queryArgument;
                stagesMasters = ControlPanelReadHelper.GetStages();
            }
            catch (Exception ex)
            {
                EventLogger.LogEvent(SysEventType.ERROR.ToString(), "Error", "Get Stages failed with exception", ex);
                throw;
            }
            return stagesMasters;
        }
        public bool SetStages(DataViewSetupInfo stages)
        {
            try
            {
                bool status = ControlPanelWriteHelper.SetStages(stages);
                return status;
            }
            catch (Exception ex)
            {
                EventLogger.LogEvent(SysEventType.ERROR.ToString(), "Error", "set Stages failed with exception", ex);
                throw;
            }
        }
        public Locations GetLocation(QueryArgument queryArgument)
        {
            Locations locations;
            try
            {
                ControlPanelReadHelper.QueryArgument = queryArgument;
                locations = ControlPanelReadHelper.GetLocationDetails();
            }
            catch (Exception ex)
            {
                EventLogger.LogEvent(SysEventType.ERROR.ToString(), "Error", "GetLocation failed with exception", ex);
                throw;
            }
            return locations;
        }
        public bool SetLocation(Locations locations)
        {
            bool result;
            try
            {
                result = ControlPanelWriteHelper.SetLocations(locations);
            }
            catch (Exception ex)
            {
                EventLogger.LogEvent(SysEventType.ERROR.ToString(), "Error", "SetResource failed with exception", ex);
                throw;
            }
            return result;
        }
        public Units GetUOM(QueryArgument queryArgument)
        {
            Units units;
            try
            {
                ControlPanelReadHelper.QueryArgument = queryArgument;
                units = ControlPanelReadHelper.GetUOMDetails();
            }
            catch (Exception ex)
            {
                EventLogger.LogEvent(SysEventType.ERROR.ToString(), "Error", "GetUOM failed with exception", ex);
                throw;
            }
            return units;
        }
        public bool SetUom(Units units)
        {
            bool result;
            try
            {
                result = ControlPanelWriteHelper.SetUomDetails(units);
            }
            catch (Exception ex)
            {
                EventLogger.LogEvent(SysEventType.ERROR.ToString(), "Error", "SetUOM failed with exception", ex);
                throw;
            }
            return result;
        }
        public bool SetMaterials(Materials materials)
        {
            bool result;
            try
            {
                result = ControlPanelWriteHelper.SetMaterials(materials);
            }
            catch (Exception ex)
            {
                EventLogger.LogEvent(SysEventType.ERROR.ToString(), "Error", "SetMaterials failed with exception", ex);
                throw;
            }
            return result;
        }
        public Materials GetMaterials(QueryArgument queryArgument)
        {
            Materials materials;
            try
            {
                ControlPanelReadHelper.QueryArgument = queryArgument;
                materials = ControlPanelReadHelper.GetMaterials();
            }
            catch (Exception ex)
            {
                EventLogger.LogEvent(SysEventType.ERROR.ToString(), "Error", "GetMaterials failed with exception", ex);
                throw;
            }
            return materials;
        }
        public Accounts GetAccounts(QueryArgument queryArgument)
        {
            Accounts accounts;
            try
            {
                ControlPanelReadHelper.QueryArgument = queryArgument;
                accounts = ControlPanelReadHelper.GetAccounts();
            }
            catch (Exception ex)
            {
                EventLogger.LogEvent(SysEventType.ERROR.ToString(), "Error", "GetAccounts failed with exception", ex);
                throw;
            }
            return accounts;
        }
        public bool SetAccounts(Accounts accounts)
        {
            bool result;
            try
            {
                result = ControlPanelWriteHelper.SetAccounts(accounts);
            }
            catch (Exception ex)
            {
                EventLogger.LogEvent(SysEventType.ERROR.ToString(), "Error", "SetAccounts failed with exception", ex);
                throw;
            }
            return result;
        }
        public DataViewSetupInfo GetStagesByUser(Guid userId, DataBaseInfo databaseInfo)
        {
            var dataManager = new Data.Manager.ControlPanelReadHelper();
            return dataManager.GetStagesByUser(userId, databaseInfo);
        }
        public WFComponentSubs GetReferences(QueryArgument queryArgument)
        {
            WFComponentSubs refrences;
            try
            {
                ControlPanelReadHelper.QueryArgument = queryArgument;
                refrences = ControlPanelReadHelper.GetReferences();
            }
            catch (Exception ex)
            {
                EventLogger.LogEvent(SysEventType.ERROR.ToString(), "Error", "GetAccounts failed with exception", ex);
                throw;
            }
            return refrences;
        }
        public CompanyProfiles GetCompanyProfiles(QueryArgument queryArgument)
        {
            CompanyProfiles companyProfiles;
            try
            {
                ControlPanelReadHelper.QueryArgument = queryArgument;
                companyProfiles = ControlPanelReadHelper.GetCompanyProfiles();
            }
            catch (Exception ex)
            {
                EventLogger.LogEvent(SysEventType.ERROR.ToString(), "Error", "GetCompanyProfiles failed with exception", ex);
                throw;
            }
            return companyProfiles;
        }
        public Countries GetCountry(QueryArgument queryArgument)
        {
            Countries countries;
            try
            {
                ControlPanelReadHelper.QueryArgument = queryArgument;
                countries = ControlPanelReadHelper.GetCountry();
            }
            catch (Exception ex)
            {
                EventLogger.LogEvent(SysEventType.ERROR.ToString(), "Error", "GetCountry failed with exception", ex);
                throw;
            }
            return countries;
        }

        public bool SetCountry(Countries countries)
        {
            bool result = false;
            try
            {
                result = ControlPanelWriteHelper.SetCountry(countries);
            }
            catch (Exception ex)
            {
                EventLogger.LogEvent(SysEventType.ERROR.ToString(), "Error", "SetCountry failed with exception", ex);
                throw;
            }
            return result;
        }

        public void Cancle(QueryArgument queryArgument)
        {            
            try
            {
                ControlPanelReadHelper.QueryArgument = queryArgument;
                ControlPanelReadHelper.Cancel();
            }
            catch (Exception ex)
            {
                EventLogger.LogEvent(SysEventType.ERROR.ToString(), "Error", "Cancle failed with exception", ex);
                throw;
            }
          
        }
    }
}
