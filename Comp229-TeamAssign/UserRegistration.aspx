<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="UserRegistration.aspx.cs" Inherits="Comp229_TeamAssign.Registration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="container">
<div class="row">
    <div class="col-xs-12 col-sm-8 col-md-6 col-sm-offset-2 col-md-offset-3">
        <br/>
		<div class="register">
			<h2>Sign Up <small>Let's make some changes!</small></h2>
			<hr class="colorgraph">
			<div class="row">
				<div>
					<div class="form-group">
                         <asp:TextBox ID = "username" runat="server" class="form-control input-lg" placeholder="Username"></asp:TextBox>
                    <asp:RequiredFieldValidator ID = "usernameRequiredVal" runat="server" ErrorMessage="Cannot Be Empty" ValidationGroup="InsertValidation" ControlToValidate="username" ForeColor="Red"></asp:RequiredFieldValidator>
					</div>
				</div>
			</div>
            <div class="row">
			<div class="form-group">
				<asp:TextBox ID = "email" runat="server" class="form-control input-lg" placeholder="Email Address"></asp:TextBox>
                    <asp:RequiredFieldValidator ID = "emailRequiredVal" runat="server" ErrorMessage="Cannot Be Empty" ValidationGroup="InsertValidation" ControlToValidate="email" ForeColor="Red"></asp:RequiredFieldValidator>
			</div>
                </div>
			<div class="row">
				<div>
					<div class="form-group">
                        <asp:TextBox ID = "password" type="password" runat="server" class="form-control input-lg" placeholder="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID = "passwordRequiredVal" runat="server" ErrorMessage="Cannot Be Empty" ValidationGroup="InsertValidation" ControlToValidate="password" ForeColor="Red"></asp:RequiredFieldValidator>
					</div>
				</div>
                </div>
            <div class="row">
                    <div>
					<div class="form-group">
						<asp:TextBox ID = "confirmpwd" type="password" runat="server" class="form-control input-lg" placeholder="Confirm Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID = "confirmpwdRequiredVal" runat="server" ErrorMessage="Cannot Be Empty" ValidationGroup="InsertValidation" ControlToValidate="confirmpwd" ForeColor="Red"></asp:RequiredFieldValidator>
                     <p><asp:CompareValidator id="comparepwd" 
              runat="server"
              ControlToCompare="password"
              ControlToValidate="confirmpwd"
              ErrorMessage="Your password does not match! Please enter again"
               SetFocusOnError="true" 
               ForeColor ="red"/></p>
					</div>
				</div>
			
			<hr class="colorgraph">
			<div class="row">
				<div class="col-xs-12 col-md-6">
                <asp:Button ID = "submitBtn" runat="server" Text="Register" CssClass="btn btn-primary btn-block btn-lg"  ValidationGroup="InsertValidation" OnClick="submitBtn_Click" /></div>
				<div class="col-xs-12 col-md-6"><a href="MainTracking.aspx" class="btn btn-danger btn-block btn-lg">Cancel</a></div>

			</div>
		</div>
        </div>
    </div>
</div>
	</div>
</asp:Content>
