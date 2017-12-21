<%@ Page Title="" Language="C#" MasterPageFile="~/AppPages/Common/Site.Master" AutoEventWireup="true" CodeBehind="FBTS_PartyMaster.aspx.cs" Inherits="FBTS.View.AppPages.CPanel.FBTS_PartyMaster" %>

<%@ Import Namespace="FBTS.Model.Common" %>
<%@ Register Src="~/UserControls/Common/CustomMessageControl.ascx" TagPrefix="uc1" TagName="CustomMessageControl" %>
<%@ Register Src="~/UserControls/Common/AddressControlHorizontal2Col.ascx" TagPrefix="ezyuc" TagName="AddressControlHorizontal2Col" %>


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

            if (!validateMandatoryField($('#<%=txtCode.ClientID %>'), "Code Cannot be Blank!!")) return false;
            if (!validateMandatoryField($('#<%=txtName.ClientID %>'), "Name Cannot be Blank!!")) return false;
            return true;
        }
    </script>
    <uc1:CustomMessageControl runat="server" ID="CustomMessageControl" />
    <section class="panel">
        <!-- BEGIN PAGE HEADER-->
        <div class="row">
            <div class="col-md-4">
                <header class="panel-heading no-b">
                    <h4 runat="server" id="pageTitle">Manage Users</h4>
                </header>
            </div>
            <div class="col-md-8">
                <asp:UpdatePanel ID="uplActions" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="pull-right panel-body" runat="server" id="divForm" visible="false">
                            <label><b>Created Date :</b></label>
                            <asp:Label ID="lblCreadet" runat="server" Text="" CssClass="control-label"></asp:Label>&nbsp&nbsp&nbsp&nbsp 
                            <asp:LinkButton ID="lnkSave" runat="server" CssClass="btn btn-primary btn-sm  " Text="Login" OnClick="lnkSave_Click" OnClientClick="return jsValidateForm();">
                                <i class="icon-save mr5"></i> Save </asp:LinkButton>
                            <asp:LinkButton ID="lnkBack" runat="server" CssClass="btn btn-sm btn-default" Text="Login" OnClientClick="openTab(1);" OnClick="lnkBack_Click">
                                <i class="icon-backward mr5"></i> Back </asp:LinkButton>
                        </div>
                        <div class="pt15" runat="server" id="divView">
                            <div class="col-md-offset-4 col-md-5">
                                <div class="input-group mb15">
                                    <span class="input-group-addon"><i class="icon-search"></i></span>
                                    <asp:TextBox ID="txtSearch" runat="server" class="form-control" placeholder="Enter a keyword to search"
                                        AutoPostBack="true" OnTextChanged="txtSearch_TextChanged">
                                    </asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="pull-right">
                                    <asp:LinkButton ID="lnkAddNew" runat="server" CssClass="btn btn-sm btn-success" OnClick="lnkAddNew_Click"><i class="icon-plus"></i> Add</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <!-- END PAGE HEADER-->
        <div class="panel-body">
            <!-- BEGIN PAGE CONTENT-->
            <div class="tabbable tabbable-custom boxless">
                <div class="tab-content">
                    <uc1:CustomMessageControl runat="server" ID="CustomMessageControl1" />
                    <div class="tab-pane active" id="tab_1">
                        <%--start tab_1--%>
                        <div class="responsive" data-tablet="span12 fix-offset" data-desktop="span6">
                            <div class="row">
                                <%--start of row2--%>
                                <div class="col-md-12">
                                    <asp:UpdatePanel ID="uplView" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:HiddenField ID="hdnIsUpload" runat="server" />
                                            <asp:GridView ID="GridViewTable" runat="server" CssClass="table table-striped table-bordered table-hover"
                                                AllowPaging="True" FooterStyle="table table-striped table-bordered table-hover" PageSize="15" Width="100%" AutoGenerateColumns="False"
                                                AllowSorting="True" EmptyDataText="No Data is Available" OnRowDataBound="GridViewTable_RowDataBound" OnPageIndexChanging="GridViewTable_PageIndexChanging">
                                                <PagerStyle CssClass="dataTables_pager" HorizontalAlign="Right" VerticalAlign="Middle"></PagerStyle>
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl #">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRowIndex" runat="server" />
                                                            <asp:HiddenField ID="hidSuspent" runat="server"></asp:HiddenField>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="5%"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Code" DataField="Sname">
                                                        <ItemStyle Width="15%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Name" DataField="Name"></asp:BoundField>
                                                    <asp:TemplateField HeaderText="Actions">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEdit" CommandArgument='<%#DataBinder.Eval(Container, "DataItem.Sname") %>' CommandName='<%#Constants.UpdateAction %>' CssClass="btn btn-xs btn-success"
                                                                ToolTip="Click here to Edit" OnClientClick=" openTab(2); " runat="server" OnClick="lnkEdit_Click"><i class="icon-edit"></i>Edit</asp:LinkButton>
                                                            <%--<asp:LinkButton ID="lnkDelete" CssClass="btn btn-xs btn-danger" ToolTip="Click here to Delete" runat="server" ><i class="icon-trash"></i> Delete</asp:LinkButton>--%>
                                                            <asp:LinkButton ID="lnkBan" ToolTip="Click here to Ban" runat="server" CssClass="btn btn-xs btn-default" OnClick="lnkBan_Click"><i class="icon-ban-circle"></i> Ban</asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="15%"></ItemStyle>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <%--end of row2--%>
                            <!-- END EXAMPLE TABLE PORTLET-->
                        </div>
                    </div>
                    <%--end of tab_1--%>
                    <div class="tab-pane" id="tab_2">
                        <%--START of tab_2--%>
                        <div class="responsive" data-tablet="span12 fix-offset" data-desktop="span6">
                            <asp:UpdatePanel ID="uplForm" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:HiddenField ID="hidKey" runat="server" />
                                    <asp:HiddenField ID="hidAction" runat="server" />
                                    <asp:HiddenField ID="hidType" runat="server" />
                                    <asp:HiddenField ID="hidMode" runat="server" />
                                    <asp:HiddenField ID="hidParent" runat="server" />
                                    <asp:HiddenField ID="hidSub" runat="server" />
                                    <asp:HiddenField ID="hidLFgroup" runat="server" />
                                    <div class="row">
                                        <div class="col-md-3">
                                            <div class="form-group-lg">
                                                <label>Code</label>
                                                <div>
                                                    <asp:TextBox ID="txtCode" runat="server" CssClass="form-control text-uppercase" placeholder="Code" MaxLength="15"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-5">
                                            <div class="form-group-lg">
                                                <label>Name</label>
                                                <div>
                                                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Name" MaxLength="120"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group-lg">
                                                <label>Contct Person</label>
                                                <div>
                                                    <asp:TextBox ID="txtcontactperson" runat="server" CssClass="form-control" placeholder="Contact Person" MaxLength="50"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>                                    
                                    <div class="row">
                                        <ezyuc:AddressControlHorizontal2Col runat="server" ID="BasicAddressControl" />
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <%--END of tab_2--%>
                    <div class="tab-pane" id="tab_3">
                        <%--START of tab_3--%>
                        <div class="responsive" data-tablet="span12 fix-offset" data-desktop="span6">
                            <asp:UpdatePanel ID="uplBalkUpload" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-md-8 pl25">
                                            <div class="form-group">
                                                <label class="control-label pl10">Please Select Excel File</label>
                                                <div>
                                                    <div class="col-md-7">
                                                        <ajaxToolkit:AsyncFileUpload runat="server" ID="fileExcelUpload" Width="400px" UploaderStyle="Modern"
                                                            UploadingBackColor="#CCFFFF" OnUploadedComplete="fileExcelUpload_UploadedComplete" CssClass="uploadinput"
                                                            OnClientUploadComplete="uploadcomplete" />
                                                        <asp:HiddenField ID="hidFileName" runat="server" />
                                                    </div>
                                                    <div class="col-md-5 pb15">
                                                        <asp:LinkButton ID="lnkUpload" runat="server" CssClass="btn btn-default btn-sm mr5" OnClick="lnkUpload_Click">
                                                            Upload<i class="ti-upload ml5"></i>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <%--START of tab_3--%>
                </div>
            </div>
        </div>
        <!-- END PAGE CONTENT-->
    </section>
    <script>
        // AjaxControlToolkit.AsyncFileUpload.prototype.newFileName = null;
        function uploadcomplete(sender, e) {
            $('#<%=hidFileName.ClientID%>')[0].value = e._fileName;
        }
        function openTab(tabId) {
            if (tabId == 1) {
                $("#tabhd_1").addClass("active");
                $("#tab_1").addClass("active");

                $("#tabhd_2").removeClass("active");
                $("#tab_2").removeClass("active");
                $("#tabhd_3").removeClass("active");
                $("#tab_3").removeClass("active");
            }
            else if (tabId == 2) {

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
</asp:Content>
