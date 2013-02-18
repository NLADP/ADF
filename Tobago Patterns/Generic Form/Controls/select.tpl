<div class="contentheader"><h2><asp:Literal runat="server" Text="<%$ Resources: Select $Attribute.Name.Lower$ %>" /></h2></div>

<div class="content">
  <sv:SmartView id="grd$Attribute.Name.Plural.Pascal$" width="100%" runat="server" OnSelectedIndexChanged="grd$Attribute.Name.Plural.Pascal$_SelectedIndexChanged" >
    <PagerStyle CssClass="Paging" />
    <Columns>
      <sv:TextButton DataField="Title" Header="$Attribute.Name.Pascal$" />
      <Tobago.Loop(Attribute.Classifier.Attributes, "GridColumns")>
        <Tobago.Loop(Attribute.Classifier.Associations, "Associations")>
        </Columns>
  </sv:SmartView>
</div>
<div class="contentfooterleft">
  <adf:SmartButton id="lbNew$Attribute.Name.Pascal$" runat="server" OnClick="lbNew$Attribute.Name.Pascal$_Click" Text="New $Attribute.Name.Pascal$" />
</div>
<div class="contentfooterright">
  <adf:SmartButton id="lbCancel$Attribute.Name.Pascal$" runat="server" OnClick="lbCancel$Attribute.Name.Pascal$_Click" Text="Cancel" />
</div>
<br style="clear: both;"/>
