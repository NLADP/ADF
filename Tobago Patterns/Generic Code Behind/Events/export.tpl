		
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        lbDownload.OnClientClick = DownloadManager.GenerateUrl;
    }

    protected void lbCancelExport_Click(object sender, EventArgs e)
    {
        MyTask.Cancel();
    }

    protected void lbDownloadExport_Click(object sender, EventArgs e)
    {
        var messageDefinitionType = downloadMessageDefinitionControl.NameList.SelectedItem.Value;

        if (messageDefinitionType.IsNullOrEmpty())
        {
            ValidationManager.AddError("Please select a file definition.");
            DownloadManager.CancelDownload();
			return;
        }

        MyTask.Export$Attribute.Name.Pascal$(messageDefinitionType);
        
		DownloadManager.QueueDownload(MyTask.ExportFile, "text/csv", "export.csv");
    
	    MyTask.OK();
    }
