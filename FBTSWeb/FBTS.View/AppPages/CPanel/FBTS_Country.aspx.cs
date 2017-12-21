using FBTS.Library;
using FBTS.Model;
using FBTS.View.Resources.ResourceFiles;
using FBTS.View.UserControls.Common;
using FBTS.Library.Statemanagement;
using FBTS.Model.Common;
using FBTS.Model.Control;
using System;
using System.Globalization;
using System.Linq;
using System.Web.UI.WebControls;
using FBTS.Library.Common;
using FBTS.Business.Manager;
using FBTS.App.Library;
using System.Web.UI;

namespace Ezy.ERP.View.AppPages
{
    public partial class FBTS_Country : System.Web.UI.Page
    {
        public readonly ControlPanelManager _controlPanel = new ControlPanelManager();
        public readonly TransactionManager _transactionManager = new TransactionManager();
        private readonly GenericManager _genericPresenter = new GenericManager();
        private int _newPageIndex = -1;      

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ValidatePage(UserContext, QueryStringManagement.GetValue(Constants.MenuCodeQsKey, Guid.Empty.ToString()));
                BindData(BindType.List);
            }
        }
        #region Session
        public UserContext UserContext
        {
            get { return SessionManagement<UserContext>.GetValue(Constants.UserContextSessionKey); }
        }
        public string Action { get { return hidAction.Value.ToTrimString(); } set { hidAction.Value = value; } }

        #endregion

        #region Properties
        public string CountryId { get { return txtId.Text.ToTrimString(); } set { txtId.Text = value; } }
        public string CountryName { get { return txtName.Text.ToTrimString(); } set { txtName.Text = value; } }
        public string CurrencyCode { get { return txtCurrency.Text.ToTrimString(); } set { txtCurrency.Text = value; } }
        public string CurrencyName { get { return txtCName.Text.ToTrimString(); } set { txtCName.Text = value; } }
        public string CurrencySymbol { get { return txtSymbol.Text.ToTrimString(); } set { txtSymbol.Text = value; } }
        public string Denomination { get { return txtDenomination.Text.ToTrimString(); } set { txtDenomination.Text = value; } }
        #endregion

        #region visiable
        public bool IsVisibleSave
        {
            set
            {
                if (value)
                {
                    divActionAdd.Style.Add("display", "none");
                    divActionSave.Style.Add("display", "block");
                }
                else
                {
                    divActionSave.Style.Add("display", "none");
                    divActionAdd.Style.Add("display", "block");
                }
                uplActions.Update();
            }
        }
        #endregion

        protected void lnkAddNew_Click(object sender, EventArgs e)
        {
            IsVisibleSave = true;
            ClearForm();
        }

        protected void LoadDetails(object sender, EventArgs e)
        {
            divStateDetails.Visible = true;
            var lnkbtn = sender as LinkButton;
            if (lnkbtn == null) return;
            Action = lnkbtn.CommandName;
            CountryId = lnkbtn.CommandArgument;
            BindData(BindType.Form);
            IsVisibleSave = true;
            txtId.Enabled = false;
        }

        private void ClearForm()
        {
            lnkAddState.Style.Add("cursor", "Pointer");
            CountryId = string.Empty;
            CountryName = string.Empty;
            CurrencyCode = string.Empty;
            CurrencyName = string.Empty;
            CurrencySymbol = string.Empty;
            Denomination = string.Empty;
            hidAction.Value = Constants.InsertAction;
            GridViewState.DataSource = null;
            GridViewState.DataBind();
            divStateDetails.Visible = false;
            uplForm.Update();
        }

        public void BindData(BindType bindType)
        {   
            if (bindType == BindType.Form)
            {
                var queryargument = new QueryArgument(UserContext.DataBaseInfo)
                {
                    Key = CountryId,
                    filter1 = Constants.CountryType,
                    BindType = bindType,
                    QueryType = Constants.TableMCatHeader,
                    SubFilterKey = Constants.TableMCatDetls
                };
                var countries = _controlPanel.GetCountry(queryargument);

                if (countries == null) return;
                var country = countries.FirstOrDefault();
                if (country != null)
                {
                    CountryId = country.CountryId;
                    CountryName = country.CountryName.Trim();
                    CurrencyCode = country.CurrencyCode.Trim();
                    CurrencyName = country.CurrencyName.Trim();
                    CurrencySymbol = country.CurrencySymbol.Trim();
                    Denomination = country.Denomination.Trim();
                }
                lnkSubmit.Visible = hidAction.Value != Constants.ViewAction;

                var state = country.States;
                GridViewState.DataSource = state;
                if (_newPageIndex >= 0)
                    GridViewState.PageIndex = _newPageIndex;
                GridViewState.DataBind();
                uplForm.Update();
            }
            else
            {
                var queryargument = new QueryArgument(UserContext.DataBaseInfo)
                {
                    Key = CountryId,
                    filter1 = Constants.CountryType,
                    BindType = bindType,
                    QueryType = Constants.TableMCatHeader
                };
                var countries = _controlPanel.GetCountry(queryargument);

                GridViewTable.DataSource = countries;
                if (_newPageIndex >= 0)
                    GridViewTable.PageIndex = _newPageIndex;
                GridViewTable.DataBind();
                uplView.Update();
            }
        }

        protected void lnkSave_Click(object sender, EventArgs e)
        {
            if (Action == string.Empty || Action == Constants.ViewAction)
            {
                Action = Constants.InsertAction;
            }
            if(Action==Constants.InsertAction)
            {
                QueryArgument queryArgument = new QueryArgument(UserContext.DataBaseInfo)
                {
                    FilterKey = CountryId,
                    filter1 = Constants.CountryType,
                    QueryType = Constants.TableMCatHeader
                };
                if (_transactionManager.ValidateKey(queryArgument))
                {
                    CustomMessageControl.MessageBodyText = "Country Code already exist";
                    CustomMessageControl.MessageType = MessageTypes.Error;
                    CustomMessageControl.ShowMessage();
                    txtId.Focus();
                    return;
                }
            }
            var country = new Country
            {
                CountryId = CountryId,
                CountryName = CountryName,
                CurrencyCode = CurrencyCode,
                CurrencyName = CurrencyName.ToTrimString(),
                CurrencySymbol = CurrencySymbol,
                Denomination = Denomination,
                Action = Action,
                DataBaseInfo = UserContext.DataBaseInfo
            };
            var countries = new Countries { country };
             if (_controlPanel.SetCountry(countries))
            {
                CustomMessageControl.MessageBodyText = GlobalCustomResource.CountrySaved;
                CustomMessageControl.MessageType = MessageTypes.Success;                
                AuditLog.LogEvent(SysEventType.INFO, "Country Saved",
                  GlobalCustomResource.CountrySaved);
                ClearForm();
                BindData(BindType.List);
                IsVisibleSave = false;
                txtId.Enabled = true;
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "openTabFunctionCall", "openTab(1)", true);
            }
            else
            {
                CustomMessageControl.MessageBodyText = GlobalCustomResource.CountryFailed;
                CustomMessageControl.MessageType = MessageTypes.Error;
                AuditLog.LogEvent(SysEventType.INFO, "Country update failed",
                  GlobalCustomResource.CountryFailed);
            }
            CustomMessageControl.ShowMessage();
        }

        protected void GridViewTable_OnRowDataBoound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow) return;
            var gv = sender as GridView;
            var lbl = e.Row.Cells[0].FindControl("lblRowIndex") as Label;
            if (gv != null && lbl != null)
                lbl.Text = ((e.Row.RowIndex + 1) + (gv.PageIndex * gv.PageSize)).ToString(CultureInfo.InvariantCulture);
        }

        public void BindData(States state)
        {
            GridViewState.DataSource = state;
            if (_newPageIndex >= 0)
                GridViewState.PageIndex = _newPageIndex;
            GridViewState.DataBind();
            uplView.Update();
        }

        protected void GridViewState_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var gv = sender as GridView;
                var lbl = e.Row.Cells[0].FindControl("lblRowIndex") as Label;
                if (gv != null && lbl != null)
                    lbl.Text = ((e.Row.RowIndex + 1) + (gv.PageIndex * gv.PageSize)).ToString(CultureInfo.InvariantCulture);
                var state = e.Row.DataItem as State;
                if (state == null) return;
                var textBox = e.Row.Cells[0].FindControl("txtStateId") as TextBox;
                if (textBox != null)
                    textBox.Text = state.StateId;
                var box = e.Row.Cells[0].FindControl("txtStateName") as TextBox;
                if (box != null)
                    box.Text = state.StateName;
                var hiddenField = e.Row.Cells[0].FindControl("hdnAction") as HiddenField;
                if (hiddenField != null)
                    hiddenField.Value = state.Action;
                if ((e.Row.DataItem as State).Action == Constants.UpdateAction || string.IsNullOrEmpty((e.Row.DataItem as State).Action))
                {
                    (e.Row.Cells[0].FindControl("txtStateId") as TextBox).Attributes.Add("readonly", "true");
                }
            }
        }

        protected void lnkAddState_Click(object sender, EventArgs e)
        {
            var state = new States();
            foreach (GridViewRow gvRow in GridViewState.Rows)
            {
                state.Add(new State
                {
                    StateId = ((TextBox)gvRow.FindControl("txtStateId")).Text,
                    StateName = ((TextBox)gvRow.FindControl("txtStateName")).Text,
                    Action = Constants.UpdateAction
                });
            }
            state.Insert(0,new State
            {
                Action = Constants.InsertAction
            });
            BindData(state);
            lnkAddState.Enabled = false;
            lnkAddState.Style.Add("cursor", "Not-allowed");
        }

        protected void lnkSaveState_Click(object sender, EventArgs e)
        {
            var states = new States();
            lnkAddNew.Enabled = true;
            foreach (GridViewRow gvRow in GridViewState.Rows)
            {
                var action = ((HiddenField)gvRow.FindControl("hdnAction")).Value;
                var stateId = ((TextBox)gvRow.FindControl("txtStateId")).Text.ToTrimString();
                states.Add(new State
                {
                    //Slno = gvRow.FindControl("lblRowIndex").,
                    StateId =stateId,
                    StateName = ((TextBox)gvRow.FindControl("txtStateName")).Text,
                    Action = action,
                    DataBaseInfo= UserContext.DataBaseInfo
                });
                if (action == Constants.InsertAction)
                {
                    QueryArgument queryArgument = new QueryArgument(UserContext.DataBaseInfo)
                    {
                        FilterKey = CountryId,
                        filter1 = Constants.CountryType,
                        filter2 = stateId,
                        QueryType = Constants.TableMCatDetls
                    };
                    if (_transactionManager.ValidateKey(queryArgument))
                    {
                        CustomMessageControl.MessageBodyText = "State Code already exist";
                        CustomMessageControl.MessageType = MessageTypes.Error;
                        CustomMessageControl.ShowMessage();
                        ((TextBox)gvRow.FindControl("txtStateId")).Focus();
                        return;
                    }
                }
            }
            if (states.Where(x=>x.StateId.ToTrimString()==string.Empty).Any())
            {
                CustomMessageControl.MessageBodyText = "Please Enter State ID";
                CustomMessageControl.MessageType = MessageTypes.Error;
                CustomMessageControl.ShowMessage();
                return;
            }
            if (states.Where(x => x.StateName.ToTrimString() == string.Empty).Any())
            {
                CustomMessageControl.MessageBodyText = "Please Enter State Name";
                CustomMessageControl.MessageType = MessageTypes.Error;
                CustomMessageControl.ShowMessage();
                return;
            }
            if (Action == string.Empty || Action == Constants.ViewAction)
            {
                Action = Constants.InsertAction;
            }
            var country = new Country
            {
                CountryId = CountryId,
                CountryName = CountryName,
                CurrencyCode = CurrencyCode,
                CurrencyName = CurrencyName.ToTrimString(),
                CurrencySymbol = CurrencySymbol,
                Denomination = Denomination,
                Action = Action,
                DataBaseInfo = UserContext.DataBaseInfo,
                States = states
            };
            var countries = new Countries { country };

            if (_controlPanel.SetCountry(countries))
            {
                CustomMessageControl.MessageBodyText = GlobalCustomResource.StateSaved;
                CustomMessageControl.MessageType = MessageTypes.Success;
                CustomMessageControl.ShowMessage();
                AuditLog.LogEvent(SysEventType.INFO, "STATE SAVED",
                  GlobalCustomResource.StateSaved);
                BindData(BindType.Form);
                lnkAddState.Enabled = true;
                lnkAddState.Style.Add("cursor", "Pointer");
            }
            else
            {
                CustomMessageControl.MessageBodyText = GlobalCustomResource.StateFailed;
                CustomMessageControl.MessageType = MessageTypes.Error;
                CustomMessageControl.ShowMessage();
                AuditLog.LogEvent(SysEventType.INFO, "STATE UPDATE FAILED",
                  GlobalCustomResource.StateFailed);
            }
        }

        protected void lnkBack_Click(object sender, EventArgs e)
        {
            txtId.Enabled = true;
            IsVisibleSave = false;
            lnkAddState.Enabled = true;
            lnkAddState.Style.Add("cursor", "Pointer");
        }

        protected void GridViewState_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            lnkAddState.Enabled = true;
            lnkAddState.Style.Add("cursor", "Pointer");
            _newPageIndex = e.NewPageIndex;
            BindData(BindType.Form);
        }

        protected void GridViewTable_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            _newPageIndex = e.NewPageIndex;
            BindData(BindType.List);
        }
    }
}