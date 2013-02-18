<script type="text/javascript" src="../Script/CheckGrid.js" language="javascript"></script>

<div class="contentheader"><h2><asp:Literal runat="server" Text="<%$ Resources: Search for $Attribute.Name.Lower$ %>" /></h2></div>
<div class="content">
  <sv:SmartView id="grd$Attribute.Name.Pascal$" runat="server" >
    <PagerStyle CssClass="Paging" />
    <Columns>
      <asp:TemplateField>
        <HeaderTemplate>
          <asp:CheckBox ID="chkAll" Runat="server" OnClick="javascript:CheckAll(this)"></asp:CheckBox>
        </HeaderTemplate>
        <ItemTemplate>
          <asp:CheckBox ID="chkSelect" Runat="server"></asp:CheckBox>
        </ItemTemplate>
      </asp:TemplateField>
      <sv:SelectButton />
      <sv:TextButton DataField="Title" Header="$Attribute.Name.Pascal$" />
      <Tobago.Loop(Attribute.Classifier.Attributes, "GridColumns")>
        <Tobago.Loop(Attribute.Classifier.Associations, "Associations")>
        </Columns>
  </sv:SmartView>
</div>
<div class="contentfooter">
  <adf:SmartButton id="lbNew$Attribute.Name.Pascal$" runat="server" OnClick="lbNew$Attribute.Name.Pascal$_Click" Text="New $Attribute.Name.Pascal$" />
  <adf:SmartButton id="lbCancel$Attribute.Name.Pascal$" runat="server" OnClick="lbCancel$Attribute.Name.Pascal$_Click" Text="Cancel" />
</div>

