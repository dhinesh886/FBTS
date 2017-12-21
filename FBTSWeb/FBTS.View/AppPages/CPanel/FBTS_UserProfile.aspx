<%@ Page Title="" Language="C#" MasterPageFile="~/AppPages/Common/Site.Master" AutoEventWireup="true" CodeBehind="FBTS_UserProfile.aspx.cs" Inherits="FBTS.View.AppPages.CPanel.FBTS_UserProfile" %>
<%@ Import Namespace="FBTS.Model.Common" %> 
<%@ Register Src="~/UserControls/Common/CustomMessageControl.ascx" TagPrefix="ezyuc" TagName="CustomMessageControl" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        // Retains the calender after Postback
        function pageLoad() {
            $('.datepicker').datepicker({
                format: "dd/mm/yyyy"
            });
            $('.chosen').chosen();
            $('.chosen-container').css('width', '100%');
        }

        function uploadComplete() {
        }

        function uploadError() {
        }
        $(document).ready(function () {
            //tagTabLinkAttributes();
            Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        });
        function BeginRequestHandler(sender, args) {
            var elem = args.get_postBackElement();
            ShowPreLoader();
        }
        function EndRequestHandler(sender, args) {
            HidePreLoader();
        }
        function jsValidateForm() {
            if (!validateMandatoryField($('#<%=txtUserId.ClientID %>'), "User ID Cannot be Blank!!")) return false; 
            if (!validateMandatoryField($('#<%=txtPassword.ClientID %>'), "Password Cannot be Blank!!")) return false; 
            if (!validateMandatoryField($('#<%=txtUserName.ClientID %>'), "FirstName Cannot be Blank!!")) return false;             
            if (!validateMandatoryField($('#<%=txtStreet.ClientID %>'), "Address Cannot be Blank!!")) return false; 
            if (!validateMandatoryField($('#<%=txtCity.ClientID %>'), "City Cannot be Blank!!")) return false;            
            if (!validateMandatoryField($('#<%=ddlCountry.ClientID %>'), "Please Select Country!!")) return false; 
            if (!validateMandatoryField($('#<%=ddlState.ClientID %>'), "Please Select State!!")) return false; 
            if (!validateMandatoryField($('#<%=txtPostCode.ClientID %>'), "Postal Code Cannot be Blank!!")) return false;
            if (!validateMandatoryField($('#<%=txtMobile.ClientID %>'), "Mobile Number Cannot be Blank!!")) return false;
            if (!validateMandatoryField($('#<%=txtOffPhone.ClientID %>'), "Office Phone Number Cannot be Blank!!")) return false;
            if (!validateMandatoryField($('#<%=txtResPhone.ClientID %>'), "Residence Phone Number Cannot be Blank!!")) return false;
            if (!validateMandatoryField($('#<%=txtEmail.ClientID %>'), "Email Cannot be Blank!!")) return false;
            if (!validateMandatoryField($('#<%=txtDob.ClientID %>'), "DOB Cannot be Blank!!")) return false;
            if (!validateMandatoryField($('#<%=txtEmpId.ClientID %>'), "Employee ID Cannot be Blank!!")) return false;
            if (!validateMandatoryField($('#<%=ddlGender.ClientID %>'), "Please Select Gender!!")) return false;
            if (!validateMandatoryField($('#<%=ddlReporting.ClientID %>'), "Please Select Reporting To!!")) return false;
            if (!validateMandatoryField($('#<%=ddlDesignation.ClientID %>'), "Please Select Designation!!")) return false;
            if (!validateMandatoryField($('#<%=ddlBranch.ClientID %>'), "Please Select Branch!!")) return false;
            if (!validateMandatoryField($('#<%=txtActiveTill.ClientID %>'), "Active Till Date Cannot be Blank!!")) return false;
          <%--  if (!validateMandatoryField($('#<%=ddlWH.ClientID %>'), "Please Select Warehouse!!")) return false;
            if (!validateMandatoryField($('#<%=ddlDept.ClientID %>'), "Please Select Department!!")) return false;--%>
            return true;
        }
        function validEmail()
        {
            if (!validateEmail(document.getElementById('<%=txtEmail.ClientID%>'))) {
                document.getElementById('<%=txtEmail.ClientID%>').value = ""
                document.getElementById('<%=txtEmail.ClientID%>').focus();
            }
            return true;
        }
    </script> 
   <ezyuc:CustomMessageControl runat="server" ID="CustomMessageControl" />
    <section class="panel">
    <!-- BEGIN PAGE HEADER-->
    <header class="panel-heading no-b">
        <h4 runat="server" id="pageTitle">Manage Users</h4>
    </header>
    <!-- END PAGE HEADER-->
    <div class="panel-body" style="padding-top: 0"> 
    <!-- BEGIN PAGE CONTENT-->
        <div class="tabbable tabbable-custom boxless">             
            <div class="tab-content">
                <div class="tab-pane active" id="tab_1">
                    <div class="responsive" data-tablet="span12 fix-offset" data-desktop="span6">
                        <!-- BEGIN EXAMPLE TABLE PORTLET--> 
                          <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="row">                                        
                                        <div class="col-md-12 pb10">
                                             <div class=" pull-right">                                        
                                      <asp:LinkButton ID="lnkAddNew" runat="server" CssClass="btn btn-sm btn-success" OnClientClick="openTab(2);" OnClick="lnkAddNew_Click"><i class="icon-plus"></i> Add</asp:LinkButton>                                        
                                    </div> 
                                        </div>
                                    </div>                                  
                                    </ContentTemplate>
                              </asp:UpdatePanel>
                         <div class="row">
                         <div class="col-md-12">
                            <asp:UpdatePanel ID="uplView" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="no-more-tables"> 
                                        <asp:GridView ID="GridViewTable" runat="server" CssClass="table table-striped table-bordered table-hover"
                                                        FooterStyle="table table-bordered table-striped table-condensed" Width="100%" AutoGenerateColumns="false" AllowSorting="true"
                                                        EmptyDataText="No Data is Available" OnPageIndexChanging="GridViewTable_PageIndexChanging"
                                                        OnRowDataBound="GridViewTable_RowDataBound" OnSorting="GridViewTable_Sorting">
                                            <PagerStyle CssClass="dataTables_pager" HorizontalAlign="Right" VerticalAlign="Middle"></PagerStyle>
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowIndex" runat="server" />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="1%"></ItemStyle>
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="User Name" SortExpression="U_NAME">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkView" CommandArgument='<%#DataBinder.Eval(Container, "DataItem.UCode") %>' CommandName='<%#Constants.ViewAction %>'
                                                                        ToolTip="Click here to View Detail" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.Name") %>'
                                                                        OnClick="LoadDetails" OnClientClick=" openTab(2); "></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="LoginId" HeaderText="Login Id"></asp:BoundField>
                                                <asp:BoundField DataField="Email" HeaderText="E-Mail"></asp:BoundField>                                           
                                                <asp:TemplateField HeaderText="Actions">
                                                    <ItemTemplate>                                                        
                                                        <asp:LinkButton ID="lnkEdit" CommandArgument='<%#DataBinder.Eval(Container, "DataItem.UCode") %>' CommandName='<%#Constants.UpdateAction %>' CssClass="btn btn-xs btn-success"
                                                                        ToolTip="Click here to Edit" OnClientClick=" openTab(2); " runat="server" OnClick="LoadDetails"><i class="icon-edit"></i> Edit</asp:LinkButton>
                                                       <%-- <asp:LinkButton ID="lnkDelete" CommandArgument='<%#DataBinder.Eval(Container, "DataItem.UCode") %>' CommandName='<%#Constants.DeleteAction %>' CssClass="btn btn-xs btn-danger"
                                                                        ToolTip="Click here to Delete" OnClientClick=" openTab(2); " runat="server" OnClick="LoadDetails"><i class="icon-trash"></i> Delete</asp:LinkButton>--%>
                                                        <%--<asp:LinkButton ID="lnkBan" CommandArgument='<%#DataBinder.Eval(Container, "DataItem.UCode") %>' CommandName='<%#Constants.UpdateAction %>'
                                                                        ToolTip="Click here to Ban" OnClientClick=" openTab(2); " runat="server" OnClick="LoadDetails" CssClass="btn btn-xs btn-default"><i class="icon-ban-circle"></i> Ban</asp:LinkButton>--%>
                                                        <asp:LinkButton ID="lnkAccess" CommandArgument='<%#DataBinder.Eval(Container, "DataItem.UCode") +"||"+DataBinder.Eval(Container, "DataItem.Designation.Id") %>' CommandName='<%#DataBinder.Eval(Container, "DataItem.Name") %>' CssClass="btn btn-xs btn-success"
                                                                        ToolTip="Click here to Set Access Rights" OnClientClick=" openTab(3); " runat="server" OnClick="LoadAccessDetails"><i class="icon-edit"></i> Set Access Rights</asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="30%"></ItemStyle>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView> 
                                        </div>
                                </ContentTemplate>
                            </asp:UpdatePanel> 
                          </div>
                             </div>
                        <!-- END EXAMPLE TABLE PORTLET-->
                    </div>
                </div>
                <div class="tab-pane" id="tab_2"> 
                    <asp:UpdatePanel ID="uplForm" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                              <div class="panel panel-default">
                                  
                            <div class="panel-body">
                               <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>User Id(Login Id)<span class="text-danger" >*</span></label>
                                        <div>
                                            <asp:HiddenField ID="hidKey" runat="server" />
                                            <asp:HiddenField ID="hidAction" runat="server" />
                                            <asp:HiddenField ID="hidBu" runat="server" />
                                            <asp:HiddenField ID="hidStatus" runat="server" />
                                            <asp:HiddenField ID="hidCreated" runat="server" />
                                            <asp:TextBox ID="txtUserId" runat="server" class="form-control" onkeypress="return validentry('H',event);" placeholder="User Id" MaxLength="8"></asp:TextBox>
                                        </div>
                                    </div>
                                    </div>
                                    <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Password<span class="text-danger" >*</span></label>
                                        <div>
                                            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" onkeypress="return validentry('H',event);" TextMode="Password" MaxLength="8"></asp:TextBox>
                                        </div>
                                    </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Firstname<span class="text-danger" >*</span></label>
                                        <div>
                                            <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" onkeypress="return validentry('NM',event);" placeholder="User Name"></asp:TextBox>
                                        </div>
                                    </div>
                                    </div>
                                        <div class="col-md-6">
                                  <div class="form-group">
                                        <label>Last name</label>
                                        <div>
                                            <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" onkeypress="return validentry('NM',event);" placeholder="Last Name"></asp:TextBox>
                                        </div>
                                    </div>
                                            </div>
                                        </div>
                                <div class="row">
                                    <div class="col-md-6">
                                          <div class="form-group">
                                        <label>Address</label>
                                        <div>
                                                <asp:TextBox ID="txtStreet" runat="server" CssClass="form-control" onkeypress="return validentry('H',event);" placeholder="Street"  ></asp:TextBox>
                                        </div>
                                    </div>
                                    </div>
                                     <div class="col-md-6">
                                           <div class="form-group">
                                        <label>City</label>
                                        <div>
                                            <asp:TextBox ID="txtCity" runat="server" CssClass="form-control" onkeypress="return validentry('NM',event);" placeholder="City"></asp:TextBox>
                                        </div>
                                    </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                         <div class="form-group">
                                        <label>Country</label>
                                        <div>
                                                <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" AutoPostBack="true"
                                                                    OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                                </asp:DropDownList> 
                                        </div>
                                    </div>
                                    </div>
                                     <div class="col-md-6">
                                      <div class="form-group">
                                        <label>State</label>
                                        <div>
                                                <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                        </div>
                                    </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                        <label>Zip/Postal Code</label>
                                        <div>
                                                <asp:TextBox ID="txtPostCode" runat="server" CssClass="form-control" onkeypress="return validentry('N',event);" placeholder="Post Code" MaxLength="6"></asp:TextBox>
                                        </div>
                                    </div>
                                    </div>
                                     <div class="col-md-6">
                                          <div class="form-group">
                                        <label>Mobile<span class="text-danger" >*</span></label>
                                        <div>
                                                <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" onkeypress="return validentry('N',event);" placeholder="Mobile Number" MaxLength="10"></asp:TextBox>
                                        </div>
                                    </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                        <label>Office Phone</label>
                                        <div>
                                                <asp:TextBox ID="txtOffPhone" runat="server" CssClass="form-control" onkeypress="return validentry('P',event);" placeholder="Office Phone Number" MaxLength="30"></asp:TextBox>
                                        </div>
                                    </div>
                                    </div>
                                     <div class="col-md-6">
                                      <div class="form-group">
                                        <label>Residential Phone</label>
                                        <div>
                                                <asp:TextBox ID="txtResPhone" runat="server" CssClass="form-control" onkeypress="return validentry('P',event);" placeholder="Residential Phone Number" MaxLength="30"></asp:TextBox>
                                        </div>
                                    </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                        <label>Email<span class="text-danger" >*</span></label>
                                        <div>
                                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" onblur="return validEmail();" placeholder="Email Address" MaxLength="256"></asp:TextBox>   
                                        </div>
                                    </div>
                                    </div>
                                     <div class="col-md-6">
                                           <div class="form-group">
                                        <label>DOB<span class="text-danger" >*</span></label>
                                        <div>
                                                <asp:TextBox ID="txtDob" runat="server" CssClass="form-control datepicker" onkeypress="return validentry('N',event);"></asp:TextBox>
                                        </div>
                                    </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                        <label>Employee Id<span class="text-danger" >*</span></label>
                                        <div>
                                                <asp:TextBox ID="txtEmpId" runat="server" CssClass="form-control" onkeypress="return validentry('H',event);" placeholder="Employee Id"></asp:TextBox> 
                                        </div>
                                    </div>
                                    </div>
                                     <div class="col-md-6">
                                     <div class="form-group">
                                        <label>Gender</label> 
                                          <div>
                                                <asp:DropDownList ID="ddlGender" runat="server" class="form-control">
                                                     <asp:ListItem Text="" Value=""></asp:ListItem>
                                                    <asp:ListItem Text="Male" Value="M"></asp:ListItem>
                                                     <asp:ListItem Text="Female" Value="F"></asp:ListItem>
                                                </asp:DropDownList> 
                                        </div>
                                    </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                     <div class="form-group">
                                        <label>Designation</label>
                                        <div>
                                                <asp:DropDownList ID="ddlDesignation" runat="server" CssClass="form-control" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlDesignation_SelectedIndexChanged" >
                                                </asp:DropDownList> 
                                        </div>
                                    </div>
                                    </div>
                                     <div class="col-md-6">
                                      <div class="form-group">
                                        <label>Reporting To</label>
                                        <div>
                                                <asp:DropDownList ID="ddlReporting" runat="server" CssClass="form-control">
                                                </asp:DropDownList> 
                                        </div>
                                    </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                     <div class="form-group">
                                        <label>Location<span class="text-danger" >*</span></label>
                                        <div>
                                                <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control chosen">
                                                </asp:DropDownList> 
                                        </div>
                                    </div>
                                    </div>
                                     <div class="col-md-6">
                                     <div class="form-group">
                                        <label>Active Till<span class="text-danger" >*</span></label>
                                        <div>
                                                <asp:TextBox ID="txtActiveTill" runat="server" CssClass="form-control datepicker" onkeypress="return validentry('N',event);"></asp:TextBox>
                                               
                                        </div>
                                    </div> 
                                    </div>
                                </div>
                                <div class="row" style="display:none">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                        <label>Warehouse<span class="text-danger" >*</span></label>
                                        <div>
                                                <asp:DropDownList ID="ddlWH" runat="server" CssClass="form-control">
                                                </asp:DropDownList> 
                                        </div>
                                    </div>
                                    </div>
                                     <div class="col-md-6">
                                      <div class="form-group">
                                        <label>Suspend</label>
                                        <div>
                                            <asp:CheckBox ID="CheckBox1" runat="server" CssClass="form-control"></asp:CheckBox></div>
                                    </div> 
                                    </div>
                                </div>
                                <div class="row" style="display:none">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                        <label>Department<span class="text-danger" >*</span></label>
                                        <div>
                                                <asp:DropDownList ID="ddlDept" runat="server" CssClass="form-control">
                                                </asp:DropDownList> 
                                        </div>
                                    </div>
                                    </div>
                                     <div class="col-md-6">
                                      <div class="form-group">
                                        <label>Social Network 1</label>
                                        <div>
                                            <asp:TextBox ID="txtSN1" runat="server" CssClass="form-control" onkeypress="return validentry('H',event);" placeholder="LinkedIin Id"></asp:TextBox>
                                        </div>
                                    </div>
                                    </div>
                                </div>
                                <div class="row" style="display:none">
                                    <div class="col-md-6">
                                     <div class="form-group">
                                        <label>Social Network 2</label>
                                        <div>
                                            <asp:TextBox ID="txtSN2" runat="server" CssClass="form-control" onkeypress="return validentry('H',event);" placeholder="Twiter Id"></asp:TextBox>
                                        </div>
                                    </div>
                                    </div>
                                     <div class="col-md-6">
                                       <div class="form-group">
                                        <label>Social Network 3</label>
                                        <div>
                                            <asp:TextBox ID="txtSN3" runat="server" CssClass="form-control" onkeypress="return validentry('H',event);" placeholder="Facebook"></asp:TextBox>
                                        </div>
                                    </div>
                                    </div>
                                </div>                                    
                             
                                <div class="row" style="display:none">
                                    <div class="col-md-6"> 
                                    <div class="form-group">
                                        <label>Avator</label>
                                        <div>
                                                <ajaxToolkit:AsyncFileUpload OnClientUploadComplete=" uploadComplete " runat="server"
                                                                            ID="fudUserImages" Width="400px" UploaderStyle="Traditional"
                                                                            UploadingBackColor="#CCFFFF"   OnUploadedComplete="FileUploadComplete"  CssClass="uploadinput"/>
                                             <asp:HiddenField ID="hidFileName" runat="server" />                                             
                                        </div>
                                    </div>
                                    </div>
                                    <div class="col-md-6">
                                        
                                    <div class="form-group">                                             
                                                <asp:Image ID="imgUserImage" runat="server"  ImageUrl="~/Resources/UI/img/faceless.jpg" Height="100px" Width="100px"/>
                                    </div>
                                        </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">                                         
                                        <asp:LinkButton ID="lnkCancel" runat="server" CssClass="btn btn-default btn-block" OnClientClick="openTab(1)"><i class="icon-ban-circle"></i> Cancel </asp:LinkButton>
                                         </div>
                                    <div class="col-md-6"> 
                                        <asp:LinkButton ID="lnkSave" runat="server" CssClass="btn btn-primary btn-block btn-parsley" Text="Login" OnClientClick="return jsValidateForm();" OnClick="lnkSave_Click"><i class="icon-save"></i> Save </asp:LinkButton>
                                    </div>
                                </div>
                                
                                </div>
                            </div>                                                              
                        </ContentTemplate>
                    </asp:UpdatePanel> 
                </div>
                 <div class="tab-pane" id="tab_3"> 
                    <asp:UpdatePanel ID="uplAccess" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                        <div class="panel panel-default">
                            <div class="panel-body">
                                <div class="row" style="padding-bottom: 15px;">
                                     <div class="col-md-8  ">
                                        <b> User Name :</b> <b><asp:Label ID="lblAccessLevel" runat="server" Text="Label"></asp:Label></b>
                                                    <asp:HiddenField ID="hidSelectedDesign" runat="server"></asp:HiddenField>
                                         </div>     
                                <div class="col-md-4  ">                             
                            <div class="pull-right">
                                    <asp:LinkButton ID="lnkBack" runat="server" CssClass="btn btn-sm btn-default" Text="Login"  OnClientClick="openTab(1);return false;"><i class="icon-backward mr5"></i> Back </asp:LinkButton> 
                                    <asp:LinkButton ID="lnkSaveSub" runat="server" CssClass="btn btn-sm btn-success mr5" OnClientClick=" openTab(1); "  OnClick="lnkSaveAccessLevel_Click"><i class="icon-save mr5"></i>Save</asp:LinkButton>                                                                    
                             </div>
                                    </div>
                                </div>      
                                <div class="row">
                                    <header class="panel-heading"><b>Menus</b></header>
                                    <!-- BEGIN EXAMPLE TABLE PORTLET-->                                  
                                     <div class="col-md-12">                                                     
                                        <asp:GridView ID="gvMenuRights" runat="server" CssClass="table table-striped table-bordered table-hover"
                                                        AllowPaging="false" FooterStyle="table table-striped table-bordered table-hover" PageSize="25" Width="100%" AutoGenerateColumns="false" AllowSorting="true"
                                                        EmptyDataText="No Data is Available" OnRowDataBound="gvMenuRights_OnRowDataBoound">
                                            <PagerStyle CssClass="dataTables_pager" HorizontalAlign="Right" VerticalAlign="Middle"></PagerStyle>
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowIndex" runat="server" /> 
                                                        <asp:HiddenField ID="hidMenuCode" runat="server" />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="3%"></ItemStyle>
                                                </asp:TemplateField>  
                                                 <asp:BoundField DataField="MenuName" HeaderText="Menu Name" ItemStyle-Width="20%"></asp:BoundField>
                                                    <asp:TemplateField HeaderText="Level">
                                                    <ItemTemplate>
                                                      <asp:TextBox ID="txtLevel" runat="server" class="form-control" placeholder="level" ></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="15%"></ItemStyle>
                                                </asp:TemplateField>         
                                                                                
                                                <asp:TemplateField HeaderText="Actions">
                                                    <ItemTemplate>                                                      
                                                        <asp:CheckBox ID="chkSelect" runat="server" />                                                                       
                                                    </ItemTemplate>
                                                    <ItemStyle Width="5%"></ItemStyle>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>                                                          
                          </div>          
                                </div> 
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>   
            </div>
            </div>
        </div> 
    </div>         
    <!-- END PAGE CONTENT-->
    <script type="text/javascript">
        function openTab(tabId) {
            if (tabId == 1) {
                $("#tabhd_1").addClass("active");
                $("#tab_1").addClass("active");

                $("#tabhd_2").removeClass("active");
                $("#tab_2").removeClass("active");

                $("#tabhd_3").removeClass("active");
                $("#tab_3").removeClass("active");
            } else if (tabId == 2) {
                $("#tabhd_2").addClass("active");
                $("#tab_2").addClass("active");

                $("#tabhd_1").removeClass("active");
                $("#tab_1").removeClass("active");

                $("#tabhd_3").removeClass("active");
                $("#tab_3").removeClass("active");
            }
            else {
                $("#tabhd_3").addClass("active");
                $("#tab_3").addClass("active");

                $("#tabhd_1").removeClass("active");
                $("#tab_1").removeClass("active");

                $("#tabhd_2").removeClass("active");
                $("#tab_2").removeClass("active");
            }
        }
    </script>
        </section>
</asp:Content>
