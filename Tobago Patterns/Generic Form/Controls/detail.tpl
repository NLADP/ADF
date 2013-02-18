<br style="clear: both;" />
<div class="contentheader">
  <h2><asp:Literal runat="server" Text="<%$ Resources: All $Attribute.Name.Lower.Plural$ %>" /></h2>
</div>
<div class="content">
  <sv:SmartView id="grd$Attribute.Name.Plural.Pascal$" runat="server" OnSelectedIndexChanged="grd$Attribute.Name.Plural.Pascal$_SelectedIndexChanged" RowDeleting="grd$Attribute.Name.Plural.Pascal$_RowDeleting" >
    <PagerStyle CssClass="Paging" />
    <Columns>
      <sv:TextButton DataField="Title" Header="$Attribute.Name.Pascal$" />
      <Tobago.Loop(Attribute.Classifier.Attributes, "GridColumns")>
      <Tobago.Loop(Attribute.Classifier.Associations, "Associations")>
        <sv:DeleteButton />  
    </Columns>
  </sv:SmartView>
</div>
<div class="contentfooter">
  <adf:SmartButton id="lbNew$Attribute.Name.Pascal$" runat="server" OnClick="lbNew$Attribute.Name.Pascal$_Click" Text="New $Attribute.Name.Pascal$" />
</div>
