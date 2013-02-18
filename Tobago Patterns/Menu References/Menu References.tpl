<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReferencesMenu.ascx.cs" Inherits="Controls_ReferencesMenu" %>
<div class="selectheader">
    <h2>References Menu</h2>
</div>
<div class="select">
    <ul>
	<Tobago.Loop(Model.Classes, Classes)>
	</ul>
</div>
<div class="selectfooter">
</div>