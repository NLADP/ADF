<div class="contentheader"><h2><asp:Literal runat="server" Text="<%$ Resources: Enter search arguments to search for $Attribute.Name.Lower.Plural$ %>" /></h2>
</div>
<div class="content">
  <asp:Panel runat="server" ID="pnlSearch" DefaultButton="lbSearch$Attribute.Name.Pascal$">
    <panels:SearchPanel$Attribute.Type.Pascal$ id="searchPanel$Attribute.Name.Pascal$" runat="server" />
  </asp:Panel>
</div>
<div class="contentfooter">
  <adf:SmartButton id="lbSearch$Attribute.Name.Pascal$" runat="server" OnClick="lbSearch$Attribute.Name.Pascal$_Click" Text="Search" />
</div>
<div class="contentheader"><h2><asp:Literal runat="server" Text="<%$ Resources: $Attribute.Name.Lower.Plural$ %>" /></h2></div>
<div class="content">
  <sv:SmartView id="grd$Attribute.Name.Plural.Pascal$" runat="server" OnSelectedIndexChanged="grd$Attribute.Name.Plural.Pascal$_SelectedIndexChanged" >
    <PagerStyle CssClass="Paging" />
    <Columns>
      <sv:TextButton DataField="Title" Header="$Attribute.Name.Pascal$" />
      <Tobago.Loop(Attribute.Classifier.Attributes, "GridColumns")>
        <Tobago.Loop(Attribute.Classifier.Associations, "Associations")>
        </Columns>
  </sv:SmartView>
</div>
<div class="contentfooterleft">
  <adf:SmartButton id="lbNew$Attribute.Name.Pascal$" runat="server" OnClick="lbNew$Attribute.Name.Pascal$_Click" Text="New" />
</div>
<div class="contentfooterright">
  <adf:SmartButton id="lbCancel$Attribute.Name.Pascal$" runat="server" OnClick="lbCancel$Attribute.Name.Pascal$_Click" Text="Cancel" />
</div>
<br style="clear: both;"/>