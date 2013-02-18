<%@ Control Language="C#" AutoEventWireup="true" CodeFile="$Actor.Name.Pascal$Menu.ascx.cs" Inherits="Controls_$Actor.Name.Pascal$Menu" %>
<div class="selectheader">
    <h2>$Actor.Name$ Menu</h2>
</div>
<div class="select">
    <ul>
		<li><asp:LinkButton ID="lbHome" runat="server" OnClick="lbHome_Click">Home</asp:LinkButton></li>
<Tobago.Loop(Actor.NavigableAssociations, Associations)>
	</ul>
</div>
<div class="selectfooter">
</div>

