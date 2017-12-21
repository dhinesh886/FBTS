<%@ Page Title="" Language="C#" MasterPageFile="~/AppPages/Common/Site.Master" AutoEventWireup="true" CodeBehind="FBTS_ManageMaterialHierarchy.aspx.cs" Inherits="FBTS.View.AppPages.CPanel.FBTS_ManageMaterialHierarchy" %>

<%@ Register Src="~/UserControls/Common/CustomMessageControl.ascx" TagPrefix="ezyuc" TagName="CustomMessageControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        function pageLoad() {
            $('.datepicker').datepicker({
                format: "dd/mm/yyyy"
            });
            $('.chosen').chosen();
            $('.chosen-container').css('width', '100%');
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
            openWizard($("#<%=hidActiveStep.ClientID%>").val());
        }

        function jsValidateClassForm() {
            if (!validateMandatoryField($('#<%=txtClassId.ClientID %>'), "Class ID Cannot be Blank!!")) return false;
            var classId = $('#<%=txtClassId.ClientID %>').val();
            if (classId.length < 2) {
                showCustomMessage("Input is Short", "Class Id to this field is too short....\n\n Should 2 characters are expected.", "Error");
                $('#<%=txtClassId.ClientID %>').focus();
                return false;
            }
            if (!validateMandatoryField($('#<%=txtClassDescription.ClientID %>'), "Description Cannot be Blank!!")) return false;
            return true;
        }

        function jsValidateTypeForm() {
            if (!validateMandatoryField($('#<%=ddlClassType.ClientID %>'), "Please Select Class")) return false;
            if (!validateMandatoryField($('#<%=txtTypeId.ClientID %>'), "Type ID Cannot be Blank!!")) return false;
            var TypeId = $('#<%=txtTypeId.ClientID %>').val();
            if (TypeId.length < 2) {
                showCustomMessage("Input is Short", "Type Id to this field is too short....\n\n Should 2 characters are expected.", "Error");
                $('#<%=txtTypeId.ClientID %>').focus();
                return false;
            }
            if (!validateMandatoryField($('#<%=txtTypeDescription.ClientID %>'), "Description Cannot be Blank!!")) return false;
            return true;
        }

        function jsValidateGroupForm() {
            if (!validateMandatoryField($('#<%=ddlClassGroups.ClientID %>'), "Please Select Class")) return false;
            if (!validateMandatoryField($('#<%=ddlTypeGroups.ClientID %>'), "Please Select Type")) return false;
            if (!validateMandatoryField($('#<%=txtGroupId.ClientID %>'), "Group ID Cannot be Blank!!")) return false;
            var TypeId = $('#<%=txtGroupId.ClientID %>').val();
            if (TypeId.length < 2) {
                showCustomMessage("Input is Short", "Type Id to this field is too short....\n\n Should 2 characters are expected.", "Error");
                $('#<%=txtGroupId.ClientID %>').focus();
                return false;
            }
            if (!validateMandatoryField($('#<%=txtGroupDescription.ClientID %>'), "Description Cannot be Blank!!")) return false;
            <%--if (!validateMandatoryField($('#<%=txtMargin.ClientID %>'), "Margin Cannot be Blank!!")) return false;--%>
            return true;
        }
    </script> 
    <ezyuc:CustomMessageControl runat="server" id="CustomMessageControl" />
    <section class="panel">
        <!-- BEGIN PAGE HEADER-->
        <header class="panel-heading no-b pb0">
            <h4 runat="server" id="pageTitle">Material Hierarchy</h4>
        </header>    
        <!-- END PAGE HEADER-->
        <div class="panel-body "> 
        <!-- BEGIN PAGE CONTENT--> 
            <asp:UpdatePanel ID="uplView" runat="server" UpdateMode="Conditional">
                <ContentTemplate> 
                    <div class="panel panel-default">
                        <div class="panel-body no-p">
                            <div id="wizard" class="wizard">
                                <ul class="steps">
                                    <li data-target="#step1" class="active" id="step1hdr" onclick="javascript:openWizard(1); return false;">
                                        <span class="badge bg-info">1</span>Setup Class 
                                    </li>
                                    <li data-target="#step2" id="step2hdr" onclick="javascript:openWizard(2); return false;">
                                        <span class="badge bg-info">2</span>Setup Types
                                    </li>
                                    <li data-target="#step3" id="step3hdr" onclick="javascript:openWizard(3); return false;">
                                        <span class="badge bg-info">3</span>Setup Group
                                    </li>                                         
                                </ul>
                                <div class="actions btn-group">
                                    <button class="btn btn-default btn-sm btn-prev">
                                        <i class="fa fa-angle-left"></i>
                                    </button>
                                    <button class="btn btn-default btn-sm btn-next">
                                        <i class="fa fa-angle-right"></i>
                                    </button>
                                </div>
                            </div>
                            <div class="step-content">
                                <div class="step-pane active" id="step1">                                    
                                    <div class="row">
                                        <div class="col-md-6"> 
                                             <div class="no-more-tables">
                                            <asp:GridView ID="gvClass" runat="server" CssClass="table table-striped table-bordered table-hover"
                                                AllowPaging="true" FooterStyle="table table-striped table-bordered table-hover" PageSize="10" Width="100%" AutoGenerateColumns="false" AllowSorting="true"
                                                EmptyDataText="No Data is Available" OnRowDataBound="Grid_OnRowDataBound" OnPageIndexChanging="gvClass_PageIndexChanging">
                                                <PagerStyle CssClass="dataTables_pager" HorizontalAlign="Right" VerticalAlign="Middle"></PagerStyle>
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl#">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hdnAction" runat="server"></asp:HiddenField>
                                                            <asp:HiddenField ID="hidCreatedDate" runat="server"></asp:HiddenField>
                                                            <asp:Label ID="lblRowIndex" runat="server" />
                                                        </ItemTemplate>
                                                        <ItemStyle Width="5%"></ItemStyle>
                                                    </asp:TemplateField>  
                                                    <asp:BoundField DataField="Id" HeaderText="Code" ItemStyle-Width="5%"></asp:BoundField>
                                                    <asp:BoundField DataField="Description" HeaderText="Description" ItemStyle-Width="45%"></asp:BoundField>                                                                                                                                       
                                                    <asp:TemplateField HeaderText="Actions">
                                                    <ItemTemplate> 
                                                        <asp:LinkButton ID="lnkEdit" CommandArgument='<%#DataBinder.Eval(Container, "DataItem.Id") %>' CommandName='<%#DataBinder.Eval(Container, "DataItem.Description") %>' CssClass="btn btn-xs btn-success"
                                                                ToolTip="Click here to Edit" runat="server" OnClick="LoadClassDetails"><i class="icon-edit"></i> Edit</asp:LinkButton> 
                                                        <asp:LinkButton ID="lnkManageTypes" CommandArgument='<%#DataBinder.Eval(Container, "DataItem.Id") %>' CommandName='<%#DataBinder.Eval(Container, "DataItem.Description") %>' CssClass="btn btn-xs btn-success"
                                                            ToolTip="Click here to Edit" OnClientClick=" openWizard(2); " runat="server" OnClick="LoadClassTypeDetails"><i class="icon-edit"></i> Manage Types</asp:LinkButton> 
                                                            </ItemTemplate> 
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>     
                                                 </div>                  
                                        </div>
                                        <div class="col-md-6">
                                            <div class="panel panel-default">
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-md-12  ">                             
                                                            <div class="pull-right">
                                                                <asp:LinkButton ID="lnkSaveClass" runat="server" CssClass="btn btn-sm btn-success mr5" OnClientClick="return jsValidateClassForm();" OnClick="lnkSaveClass_Click"><i class="icon-save"></i> Save</asp:LinkButton>                                       
                                                                <asp:LinkButton ID="lnkAddNewClass" runat="server" CssClass="btn btn-sm btn-success mr5"  OnClick="lnkAddNewClass_Click"><i class="icon-plus"></i> Add</asp:LinkButton>                                                                                                      
                                                            </div>                                                                             
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <div class="form-group">
                                                                <label>Class Id</label>
                                                                <div>
                                                                    <asp:HiddenField ID="hidClassId" runat="server" /> 
                                                                    <asp:HiddenField ID="hidClassCreated" runat="server" /> 
                                                                    <asp:HiddenField ID="hidClassAction" runat="server" />  
                                                                    <asp:TextBox ID="txtClassId" runat="server" CssClass="form-control uperCase" placeholder="Class Id" MaxLength="2"></asp:TextBox> 
                                                                </div>
                                                            </div> 
                                                            <div class="form-group">
                                                                <label>Description</label>
                                                                <div>
                                                                    <asp:TextBox ID="txtClassDescription" runat="server" CssClass="form-control" placeholder="Description" MaxLength="45"></asp:TextBox>
                                                                </div>
                                                            </div> 
                                                            <div class="pull-right"> 
                                                                <asp:LinkButton ID="lnkNext" runat="server" CssClass="btn btn-sm btn-success" OnClientClick="return jsValidateClassForm();" OnClick="lnkSaveClass_Click">
                                                                    <i class="icon-save mr5"></i> Save & Continue </asp:LinkButton> 
                                                                <asp:LinkButton ID="lnkSkip" runat="server" CssClass="btn btn-sm btn-success" OnClientClick="openWizard(2); return false;">
                                                                    <i class="icon-forward mr5"></i> Continue </asp:LinkButton> 
                                                            </div>
                                                        </div>   
                                                    </div> 
                                                </div>
                                            </div>
                                        </div> 
                                    </div> 
                                  <%--  <div class="row">
                                        <div class="col-md-12 " > 
                                           
                                        </div>
                                    </div>--%>
                                </div>
                                <div class="step-pane" id="step2">
                                    <div class="row">
                                        <div class="col-md-6"> 
                                            <asp:GridView ID="gvTypes" runat="server" CssClass="table table-striped table-bordered table-hover"
                                                AllowPaging="true" FooterStyle="table table-striped table-bordered table-hover" PageSize="10" Width="100%" AutoGenerateColumns="false" AllowSorting="true"
                                                EmptyDataText="No Data is Available" OnRowDataBound="Grid_OnRowDataBound" OnPageIndexChanging="gvTypes_PageIndexChanging">
                                                <PagerStyle CssClass="dataTables_pager" HorizontalAlign="Right" VerticalAlign="Middle"></PagerStyle>
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl#">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRowIndex" runat="server" />
                                                            <asp:HiddenField ID="hdnAction" runat="server"></asp:HiddenField>
                                                            <asp:HiddenField ID="hidCreatedDate" runat="server"></asp:HiddenField>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="5%"></ItemStyle>
                                                    </asp:TemplateField>  
                                                    <asp:BoundField DataField="Id" HeaderText="Code" ItemStyle-Width="5%"></asp:BoundField>
                                                    <asp:BoundField DataField="Description" HeaderText="Description" ItemStyle-Width="45%"></asp:BoundField>                                                                                                                                    
                                                    <asp:TemplateField HeaderText="Actions">
                                                    <ItemTemplate>         
                                                        <asp:LinkButton ID="lnkEditType" CommandArgument='<%#DataBinder.Eval(Container, "DataItem.Id") %>' CommandName='<%#DataBinder.Eval(Container, "DataItem.Description") %>' CssClass="btn btn-xs btn-success"
                                                            ToolTip="Click here to Edit" OnClientClick=" openWizard(2); " runat="server" OnClick="LoadTypeDetails"><i class="icon-edit"></i> Edit </asp:LinkButton> 
                                                        <asp:LinkButton ID="lnkEdit" CommandArgument='<%#DataBinder.Eval(Container, "DataItem.Id") %>' CommandName='<%#DataBinder.Eval(Container, "DataItem.Description") %>' CssClass="btn btn-xs btn-success"
                                                                                ToolTip="Click here to Edit" OnClientClick=" openWizard(2); " runat="server" OnClick="LoadTypeGroupDetails"><i class="icon-edit"></i> Manage Groups </asp:LinkButton> 
                                                            </ItemTemplate> 
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>                       
                                        </div>
                                        <div class="col-md-6">
                                            <div class="panel panel-default">
                                                <div class="panel-body">   
                                                    <div class="row">
                                                        <div class="col-md-12  ">                             
                                                            <div class="pull-right">
                                                                <asp:LinkButton ID="lnkSaveType" runat="server" CssClass="btn btn-sm btn-success mr5" OnClientClick="return jsValidateTypeForm();" OnClick="lnkSaveType_Click"><i class="icon-save"></i> Save</asp:LinkButton>                                       
                                                                <asp:LinkButton ID="lnkAddNewType" runat="server" CssClass="btn btn-sm btn-success mr5"  OnClick="lnkAddNewType_Click"><i class="icon-plus"></i> Add</asp:LinkButton>                                       
                                                            </div>                 
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <div class="form-group">
                                                                <label>Class</label>
                                                                <div> 
                                                                    <asp:DropDownList ID="ddlClassType" runat="server" CssClass="form-control chosen" AutoPostBack="true" OnSelectedIndexChanged="ddlClassType_SelectedIndexChanged">
                                                                        </asp:DropDownList> 
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label>Type Id</label>
                                                                <div>
                                                                    <asp:HiddenField ID="hidTypeAction" runat="server" /> 
                                                                    <asp:HiddenField ID="hidTypeId" runat="server" />  
                                                                    <asp:HiddenField ID="hidTypeCreated" runat="server" />  
                                                                    <asp:TextBox ID="txtTypeId" runat="server" CssClass="form-control uperCase" placeholder="Type Id" MaxLength="2"></asp:TextBox> 
                                                                </div>
                                                            </div> 
                                                            <div class="form-group">
                                                                <label>Description</label>
                                                                <div>
                                                                    <asp:TextBox ID="txtTypeDescription" runat="server" CssClass="form-control" placeholder="Description" MaxLength="45"></asp:TextBox>
                                                                </div>
                                                            </div>   
                                                            <div class="pull-right">
                                                                <button type="button" class="btn btn-sm btn-default mr5" onclick="openWizard(1); return false; ">
                                                                    <i class="icon-backward mr5"></i> Back to Class Setup</button>  
                                                                <asp:LinkButton ID="lnkSaveTypeBtm" runat="server" CssClass="btn btn-sm btn-success" OnClick="lnkSaveType_Click"
                                                                    OnClientClick="return jsValidateTypeForm();"><i class="icon-save mr5"></i> Save & Continue </asp:LinkButton> 
                                                                <asp:LinkButton ID="lnkAddNewTypeBtm" runat="server" CssClass="btn btn-sm btn-success" OnClientClick="openWizard(3); return false;">
                                                                    <i class="icon-forward mr5"></i> Continue </asp:LinkButton> 
                                                            </div>                                                       
                                                        </div>   
                                                    </div> 
                                                </div>
                                            </div>
                                        </div> 
                                    </div> 
                                  <%--  <div class="row">
                                        <div class="col-md-12 " > 
                                           
                                        </div>
                                    </div> --%> 
                                </div>
                                <div class="step-pane" id="step3"> 
                                    <div class="row">
                                        <div class="col-md-6"> 
                                            <asp:GridView ID="gvGroup" runat="server" CssClass="table table-striped table-bordered table-hover"
                                                AllowPaging="true" FooterStyle="table table-striped table-bordered table-hover" PageSize="10" Width="100%" AutoGenerateColumns="false" AllowSorting="true"
                                                EmptyDataText="No Data is Available" OnRowDataBound="Grid_OnRowDataBound" OnPageIndexChanging="gvGroup_PageIndexChanging">
                                                <PagerStyle CssClass="dataTables_pager" HorizontalAlign="Right" VerticalAlign="Middle"></PagerStyle>
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl#">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRowIndex" runat="server" />  
                                                            <asp:HiddenField ID="hdnAction" runat="server"></asp:HiddenField>
                                                            <asp:HiddenField ID="hidCreatedDate" runat="server"></asp:HiddenField>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="5%"></ItemStyle>
                                                    </asp:TemplateField>  
                                                    <asp:BoundField DataField="Id" HeaderText="Code" ItemStyle-Width="5%"></asp:BoundField>
                                                    <asp:BoundField DataField="Description" HeaderText="Description" ItemStyle-Width="45%"></asp:BoundField>                                                                                                                                            
                                                    <asp:TemplateField HeaderText="Actions">
                                                        <ItemTemplate> 
                                                            <asp:LinkButton ID="lnkEdit" CommandArgument='<%#DataBinder.Eval(Container, "DataItem.Id") %>' CommandName='<%#DataBinder.Eval(Container, "DataItem.Description") %>' CssClass="btn btn-xs btn-success"
                                                                ToolTip="Click here to Edit"  runat="server" OnClick="LoadGroupDetails"><i class="icon-edit"></i> Edit </asp:LinkButton> 
                                                        </ItemTemplate> 
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>                       
                                        </div>
                                        <div class="col-md-6">
                                            <div class="panel panel-default">
                                                <div class="panel-body">   
                                                    <div class="row">
                                                        <div class="col-md-12  ">                             
                                                            <div class="pull-right">
                                                                <asp:LinkButton ID="lnkSaveGroup" runat="server" CssClass="btn btn-sm btn-success mr5" OnClientClick="return jsValidateGroupForm();" OnClick="lnkSaveGroup_Click"><i class="icon-save"></i> Save</asp:LinkButton>                                       
                                                                <asp:LinkButton ID="lnkAddNewGroup" runat="server" CssClass="btn btn-sm btn-success mr5" OnClick="lnkAddNewGroup_Click"><i class="icon-plus"></i> Add</asp:LinkButton>                                       
                                                            </div>                 
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <div class="form-group">
                                                                <label>Class</label>
                                                                <div>
                                                                    <asp:DropDownList ID="ddlClassGroups" runat="server" CssClass="form-control chosen" AutoPostBack="true" OnSelectedIndexChanged="ddlClassGroups_SelectedIndexChanged">
                                                                    </asp:DropDownList> 
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label>Type</label>
                                                                <div>
                                                                    <asp:DropDownList ID="ddlTypeGroups" runat="server" CssClass="form-control chosen" AutoPostBack="true" OnSelectedIndexChanged="ddlTypeGroups_SelectedIndexChanged">
                                                                    </asp:DropDownList> 
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label>Group Id</label>
                                                                <div>
                                                                    <asp:HiddenField ID="hidGroupId" runat="server" /> 
                                                                    <asp:HiddenField ID="hidGroupAction" runat="server" /> 
                                                                    <asp:HiddenField ID="hidGroupCreated" runat="server" />  
                                                                    <asp:TextBox ID="txtGroupId" runat="server" CssClass="form-control uperCase" placeholder="Group Id" MaxLength="4"></asp:TextBox> 
                                                                </div>
                                                            </div> 
                                                            <div class="form-group">
                                                                <label>Description</label>
                                                                <div>
                                                                    <asp:TextBox ID="txtGroupDescription" runat="server" CssClass="form-control" placeholder="Description" MaxLength="45"></asp:TextBox>
                                                                </div>
                                                            </div>   
                                                            <%--<div class="form-group">
                                                                <label>Margin</label>
                                                                <div>
                                                                    <asp:TextBox ID="txtMargin" runat="server" CssClass="form-control text-right" placeholder="Margin" onkeypress="return validentry('D',event);" MaxLength="5"></asp:TextBox>
                                                                </div>
                                                            </div>--%>  
                                                            <div class="pull-right">
                                                                <button type="button" class="btn btn-sm btn-default mr5" onclick="openWizard(1); return false; ">
                                                                    <i class="icon-backward mr5"></i> Back to Class Setup</button> 
                                                                <button type="button" class="btn btn-sm btn-default mr5" onclick="openWizard(2); return false; ">
                                                                    <i class="icon-backward mr5"></i> Back to Type Setup</button>                                                 
                                                            </div>                                                        
                                                        </div>   
                                                    </div> 
                                                </div>
                                            </div>
                                        </div> 
                                    </div> 
                                   <%-- <div class="row">
                                        <div class="col-md-12 " > 
                                            
                                        </div>
                                    </div> --%> 
                                </div> 
                            </div>
                        </div>
                    </div>
                    <asp:HiddenField ID="hidActiveStep" runat="server" Value="1"></asp:HiddenField>
                </ContentTemplate>
            </asp:UpdatePanel>  
        </div> 
        <!-- END PAGE CONTENT-->
        <script type="text/javascript">
            function openWizard(stepId) {
                if (stepId == 1) {
                    $("#step1hdr").removeClass("complete");
                    $("#step1hdr").addClass("active");
                    $("#step1").addClass("active");
                        
                    $("#step2hdr").removeClass("complete");
                    $("#step2hdr").removeClass("active");
                    $("#step2").removeClass("active");

                    $("#step3hdr").removeClass("complete");
                    $("#step3hdr").removeClass("active");
                    $("#step3").removeClass("active");
                } else if (stepId == 2) {
                    $("#step2hdr").removeClass("complete");
                    $("#step2hdr").addClass("active");
                    $("#step2").addClass("active");

                    $("#step1hdr").removeClass("active");
                    $("#step1hdr").addClass("complete");
                    $("#step1").removeClass("active");

                    $("#step3hdr").removeClass("complete");
                    $("#step3hdr").removeClass("active");
                    $("#step3").removeClass("active");
                }
                else
                {
                    $("#step3hdr").removeClass("complete");
                    $("#step3hdr").addClass("active");
                    $("#step3").addClass("active");

                    $("#step1hdr").removeClass("active");
                    $("#step1hdr").addClass("complete");
                    $("#step1").removeClass("active");

                    $("#step2hdr").removeClass("active");
                    $("#step2hdr").addClass("complete");
                    $("#step2").removeClass("active");
                }
            }
        </script>
    </section>
</asp:Content>
