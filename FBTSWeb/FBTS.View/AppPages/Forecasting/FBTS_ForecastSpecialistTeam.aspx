<%@ Page Title="" Language="C#" MasterPageFile="~/AppPages/Common/Site.Master" AutoEventWireup="true" CodeBehind="FBTS_ForecastSpecialistTeam.aspx.cs" Inherits="FBTS.View.AppPages.Forecasting.FBTS_ForecastSpecialistTeam" %>

<%@ Import Namespace="FBTS.Model.Common" %>
<%@ Register Src="~/UserControls/Common/CustomMessageControl.ascx" TagPrefix="uc1" TagName="CustomMessageControl" %>
<%@ Register Src="~/UserControls/Common/TransactionStageControl.ascx" TagPrefix="uc1" TagName="TransactionStageControl" %>
<%@ Register Src="~/UserControls/ForecastingCommon/ForecastingGridViewListControl.ascx" TagPrefix="uc1" TagName="ForecastingGridViewListControl" %>
<%@ Register Src="~/UserControls/ForecastingCommon/ForecastingControl.ascx" TagPrefix="uc1" TagName="ForecastingControl" %> 
<%@ Register Src="~/UserControls/ForecastingCommon/ForecastingPartControl.ascx" TagPrefix="uc1" TagName="ForecastingPartControl" %> 
<%@ Register Src="~/UserControls/ForecastingCommon/RefrenceControl.ascx" TagPrefix="uc1" TagName="RefrenceControl" %> 

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
      <script type="text/javascript">
          // Retains the calender after Postback

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
              if (!jsValidateHeader<%=ForecastingControlId.ClientID%>()) return false;
                if (!validateGrid<%=ForecastingPartControlId.ClientID%>()) return false;

              var ref = "<%=divref1.ClientID%>";
              if ((document.getElementById(ref)) != null)
                  if (!jsValidateReferance<%=RefrenceControlId1.ClientID%>()) return false;

              ref = "<%=divref2.ClientID%>";
              if (document.getElementById(ref) != null)
                  if (!jsValidateReferance<%=RefrenceControlId2.ClientID%>()) return false;

              ref = "<%=divref3.ClientID%>";
              if (document.getElementById(ref) != null)
                  if (!jsValidateReferance<%=RefrenceControlId3.ClientID%>()) return false;

              ref = "<%=divref4.ClientID%>";
              if (document.getElementById(ref) != null)
                  if (!jsValidateReferance<%=RefrenceControlId4.ClientID%>()) return false;

              ref = "<%=divref5.ClientID%>";
              if (document.getElementById(ref) != null)
                  if (!jsValidateReferance<%=RefrenceControlId5.ClientID%>()) return false;

              ref = "<%=divref6.ClientID%>";
              if (document.getElementById(ref) != null)
                  if (!jsValidateReferance<%=RefrenceControlId6.ClientID%>()) return false;
                return true;
            }
    </script>
<div class="row">
    <div class="col-md-12">
        <div class="tabbable tabbable-custom">
            <uc1:TransactionStageControl runat="server" id="TransactionStageControlId" />
            <uc1:CustomMessageControl runat="server" id="CustomMessageControl" />

            <div class="tab-content">
                <div class="row">
                    <asp:UpdatePanel ID="uplActions" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="col-md-6">
                                <header class="panel-heading no-b">                                        
                                    <h4 runat="server" id="pageTitle"><b>Forecasting</b></h4>
                                </header>
                            </div>
                            <div class="col-md-6 pt20" id="divSave" runat="server" visible="false">
                                <div class="pull-right">
                                    <asp:LinkButton ID="lnkSave" runat="server" CssClass="btn btn-primary btn-sm " Text="Login" OnClientClick="return jsValidateForm();" OnClick="lnkSave_Click">
                                        <i class="icon-save mr5"></i> Save </asp:LinkButton>
                                    <asp:LinkButton ID="lnkBack" runat="server" CssClass="btn btn-sm btn-default" Text="Login"  OnClientClick="openTab(1);" OnClick="lnkBack_Click">
                                        <i class="icon-backward mr5"></i> Back </asp:LinkButton>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div> 
                <div class="tab-pane active" id="tab1">
                    <asp:UpdatePanel ID="uplForm" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <uc1:ForecastingGridViewListControl runat="server" id="ForecastingGridViewListControlId" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="tab-pane" id="tab2">   
                                         
                    <asp:UpdatePanel ID="uplView" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:HiddenField ID="hidAction" runat="server" />
                                    <uc1:ForecastingControl runat="server" id="ForecastingControlId" />
                                </div>                                
                            </div>                            
                            <div class="row">
                                <div class="col-md-12">
                                    <uc1:ForecastingPartControl runat="server" id="ForecastingPartControlId" />
                                </div>                                
                            </div>   
                            <div class="row">
                                <div class="col-md-4" runat="server" id="divref1" visible="false">
                                    <uc1:RefrenceControl runat="server" id="RefrenceControlId1"/>
                                </div>    
                                <div class="col-md-4"  runat="server" id="divref2" visible="false">
                                    <uc1:RefrenceControl runat="server" id="RefrenceControlId2"/>
                                </div>
                                <div class="col-md-4" runat="server" id="divref3" visible="false">
                                    <uc1:RefrenceControl runat="server" id="RefrenceControlId3"/>
                                </div>                            
                            </div>   
                            <div class="row">
                                <div class="col-md-4" runat="server" id="divref4" visible="false">
                                    <uc1:RefrenceControl runat="server" id="RefrenceControlId4" />
                                </div>    
                                <div class="col-md-4" runat="server" id="divref5" visible="false">
                                    <uc1:RefrenceControl runat="server" id="RefrenceControlId5" />
                                </div>
                                <div class="col-md-4" runat="server" id="divref6" visible="false">
                                    <uc1:RefrenceControl runat="server" id="RefrenceControlId6" />
                                </div>                            
                            </div>  

                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
</div> 
    <script type="text/javascript">
     function openTab(tabId) {
                if (tabId == 1) {
                    $("#tabhd1").addClass("active");
                    $("#tab1").addClass("active");

                    $("#tabhd2").removeClass("active");
                    $("#tab2").removeClass("active");
                }
                else {
                    $("#tabhd2").addClass("active");
                    $("#tab2").addClass("active");

                    $("#tabhd_1").removeClass("active");
                    $("#tab1").removeClass("active");
                }
            }
    </script>
</asp:Content>
