<div class="contentheader">
  <h2><asp:Literal runat="server" Text="<%$ Resources: Define $Attribute.Name.Lower$ %>" /></h2>
</div>
<div class="content">
  <panels:Panel$Attribute.Type.Pascal$ id="panel$Attribute.Name.Pascal$" runat="server" Editable="true" />
</div>
<div class="contentfooter">
  <adf:SmartButton id="lbSave$Attribute.Name.Pascal$" runat="server" OnClick="lbSave$Attribute.Name.Pascal$_Click" Text="Save" />
  <adf:SmartButton id="lbCancel$Attribute.Name.Pascal$" runat="server" OnClick="lbCancel$Attribute.Name.Pascal$_Click" Text="Cancel" />
</div>
