﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Login/Login.Master" Inherits="System.Web.Mvc.ViewPage<ResetPasswordModel>" %>
<%@ Import Namespace="xVal.Html"%>
<%@ Import Namespace="Arashi.Web.Areas.Admin.Extensions"%>
<%@ Import Namespace="Arashi.Web.Areas.Admin.Models"%>

<asp:Content ContentPlaceHolderID="head" runat="server">
	<title>Reset Password</title>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
   <div id="loginbox" class="section ui-widget">
      <div id="loginbox-inner" class="ui-widget-content ui-corner-all ui-shadow">
         <div id="loginbox-title" class="ui-corner-all">
	         <h2 class="align-center">
               <img alt="logo" src="/Resources/img/logo/arashi-project-logo-h43.png"/>
	         </h2>
         </div>
         <div class="section-body">
		      <% using (Html.BeginForm("GenerateAndSendNewPassword", "Login", FormMethod.Post, new { id = "loginform", @class = "ui-form-default" })) { %>
			         <ol>
			            <li class="vertical">
			               <p>Please enter your e-mail address. We will send you a new password.</p>
			            </li>
	                  <% if (ViewData["ResetPasswordFailed"] != null) { %>
			               <li class="vertical">
	                        <div class="validation-summary-errors ui-state-error ui-corner-all">
	                           <%= Html.Encode(ViewData["ResetPasswordFailed"].ToString())%>
	                        </div>
	                     </li>
	                  <% } %>
	                  <% if (ViewData["ResetPasswordSuccessfull"] != null) { %>
			               <li class="vertical">
	                        <div class="ui-state-highlight ui-corner-all">
	                           <%= Html.Encode(ViewData["ResetPasswordSuccessfull"].ToString())%>
	                        </div>
	                     </li>
	                  <% } %>
			            <li class="vertical">
				            <label for="Email">Email</label>
				            <%= Html.TextBox("Email", String.Empty, new {@class = "logintext"}) %>
			               <%= Html.ValidationMessage("Email")%>
			            </li>
			            <li class="align-center">
                        <%= Html.AntiForgeryToken() %>
         			      <%= Html.Hidden("ReturnUrl", ViewData["ReturnUrl"]) %>
			               <%= Html.SubmitUI("Send me the new password")%>
			            </li>
			         </ol>
		      <% } %>
	      </div>
      </div>
      <br />
      &nbsp;&nbsp;&nbsp;
      <a href='<%= Url.Action("Index", "Login") %>'>
         Login
      </a>
   </div>
</asp:Content>

