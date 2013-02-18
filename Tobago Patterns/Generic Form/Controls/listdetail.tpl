<div class="content">
  <table border="0" cellpadding="0" cellspacing="0">
    <tr >
      <td class="listdetail" width="350px">
        <div class="contentheader">
          <h2>
            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources: Select $Attribute.Name.Lower$ %>" /></h2>
        </div>
        <sv:SmartView id="grd$Attribute.Name.Pascal.Plural$" runat="server" OnSelectedIndexChanged="grd$Attribute.Name.Pascal.Plural$_SelectedIndexChanged" >
          <PagerStyle CssClass="Paging" />
          <Columns>
            <sv:SelectButton />
            <sv:TextButton DataField="Title" Header="Order Type" />
          </Columns>
        </sv:SmartView>        
        <br/>
      </td>
      <td class="listdetail">
        <div class="contentheader">
          <h2>
            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources: Edit $Attribute.Name.Lower$ %>" /></h2>
        </div>
        <ajax:TabContainer ID="TabContainer1" runat="server">
          <ajax:TabPanel ID="TabPanel1" runat="server" HeaderText="<%$ Resources: General %>">
            <ContentTemplate>
              <panels:Panel$Attribute.Name.Pascal$ id="panel$Attribute.Name.Pascal$" runat="server" Editable="true" />
            </ContentTemplate>
          </ajax:TabPanel>
        </ajax:TabContainer>
        <div class="contentfooterleft">
          <adf:SmartButton id="lbNew$Attribute.Name.Pascal$" runat="server" OnClick="lbNew$Attribute.Name.Pascal$_Click" Text="New" />
          <adf:SmartButton id="lbSave$Attribute.Name.Pascal$" runat="server" OnClick="lbSave$Attribute.Name.Pascal$_Click" Text="Save" />
          <adf:SmartButton id="lbRemove$Attribute.Name.Pascal$" runat="server" OnClick="lbRemove$Attribute.Name.Pascal$_Click" Message="Are you sure?" Text="Remove" />
        </div>
      </td>
    </tr>
  </table>
</div>
<div class="contentfooterright">
</div>
<br style="clear: both;"/>

