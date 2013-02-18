	<div class="contentheader"><h2><asp:Literal runat="server" Text="<%$ Resources: $Attribute.Name$ %>" /></h2></div>
    <div class="content">
      <ajax:TabContainer runat="server">
        <ajax:TabPanel runat="server" HeaderText="<%$ Resources: Default %>">
          <ContentTemplate>
            <panels:Panel$Attribute.Type.Pascal$ id="panel$Attribute.Name.Pascal$" runat="server" Editable="true" />
          </ContentTemplate>
        </ajax:TabPanel>
      </ajax:TabContainer>
    </div>
	<div class="contentfooterleft">
    <adf:SmartButton id="lbSelect$Attribute.Name.Pascal$" runat="server" OnClick="lbSelect$Attribute.Name.Pascal$_Click" Text="Select" />
    <adf:SmartButton id="lbDelete$Attribute.Name.Pascal$" runat="server" OnClick="lbDelete$Attribute.Name.Pascal$_Click" Message="Are you sure?" Text="Remove" />
  </div>
  <div class="contentfooterright">
    <adf:SmartButton id="lbSave$Attribute.Name.Pascal$" runat="server" OnClick="lbSave$Attribute.Name.Pascal$_Click" Text="Save" />		
		<adf:SmartButton id="lbCancel$Attribute.Name.Pascal$" runat="server" OnClick="lbCancel$Attribute.Name.Pascal$_Click" Text="Cancel" />
	</div>
