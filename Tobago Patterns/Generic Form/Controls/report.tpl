<div class="contentheader"><h2><asp:Literal runat="server" Text="<%$ Resources: Report %>" /></h2></div>

<div class="content">
  <Controls:ReportView id="report$Attribute.Name.Pascal$" runat="server" />
</div>
<div class="contentfooterleft">
  
</div>
<div class="contentfooterright">
  <adf:SmartButton id="lbCancel$Attribute.Name.Pascal$" runat="server" OnClick="lbCancel$Attribute.Name.Pascal$_Click" Text="Cancel" />
</div>
<br style="clear: both;"/>
