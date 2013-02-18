    <div class="contentheader">
        <h2>
            <asp:Literal runat="server" Text="<%$ Resources: Download %>" /></h2>
    </div>
    <div class="content">
        <panels:DownloadMessageDefinitionControl ID="downloadMessageDefinitionControl" runat="server" />
    </div>
    <div class="contentfooter">
        <div class="contentfooterleft">
        </div>
        <div class="contentfooterright">
          <adf:SmartButton ID="lbDownload" runat="server" OnClick="lbDownloadExport_Click" Text="Download" />
          <adf:SmartButton ID="lbCancelExport" runat="server" OnClick="lbCancelExport_Click" Text="Cancel" />
        </div>
    </div>