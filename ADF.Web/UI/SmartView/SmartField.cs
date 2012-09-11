using System;
using System.Web.UI.WebControls;
using Adf.Core.Resources;
using Adf.Core.Extensions;

namespace Adf.Web.UI.SmartView
{
    public abstract class SmartField : DataControlField, IDisposable
    {
        public string ChildId { get; set; }
        public string HeadStyle { get; set; }
        public string FieldStyle { get; set; }
        public string FootStyle { get; set; }
        public string Width { get; set; }
        public bool HideOnEmpty { get; set; }

        private bool _controlsDisposed;

        private int _maxcharacters = 50;
        public int MaxCharacters
        {
            get { return _maxcharacters; } 
            set { _maxcharacters = value; }
        }

        public string Header
        {
            get { return HeaderText; }
            set { HeaderText = ResourceManager.GetString(value); }
        }

        private string _tooltipformat;
        public string ToolTipFormat
        {
            get { return _tooltipformat; }
            set { _tooltipformat = ResourceManager.GetString(value); }
        }
        public string ToolTipField { get; set; }

        private string _datafield;
        public string DataField
        {
            get { return _datafield; }
            set { _datafield = value; if (SortExpression.IsNullOrEmpty()) SortExpression = value; }
        }
        private string _dataformat;

        public string DataFormat
        {
            get { return _dataformat; }
            set { _dataformat = ResourceManager.GetString(value); }
        }

        public string Icon { get; set; }
        public string IconFormat { get; set; }

        public string IsEnabledField { get; set; }

        protected SmartField()
        {
            HideOnEmpty = false;
        }

        public override void InitializeCell(DataControlFieldCell cell, DataControlCellType cellType, DataControlRowState rowState, int rowIndex)
        {
            base.InitializeCell(cell, cellType, rowState, rowIndex);

            switch (cellType)
            {
                case DataControlCellType.Header:
                    HeaderStyle.Set(HeadStyle).Set(FieldStyle);
                break;
                case DataControlCellType.Footer:
                    FooterStyle.Set(FootStyle).Set(FieldStyle);
                break;
                default:
                    ItemStyle.Set(FieldStyle);

                    if (Width != null) ItemStyle.Width = Unit.Parse(Width);

                    InitializeControls(cell, cellType, rowState, rowIndex);

                    if (cellType == DataControlCellType.DataCell) cell.DataBinding += ItemDataBinding;
                break;
            }
        }

        protected abstract void InitializeControls(DataControlFieldCell cell, DataControlCellType cellType, DataControlRowState rowState, int rowIndex);
        protected abstract void DisposeControls();

        protected abstract void ItemDataBinding(object sender, EventArgs e);

        protected override DataControlField CreateField()
        {
            return new BoundField();
        }

        public void Dispose()
        {
            if (!_controlsDisposed) DisposeControls();

            _controlsDisposed = true;

            GC.SuppressFinalize(this);
        }
    }
}
