using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Adf.Web.UI.SmartView
{
//    public class TestButtonEventArgs : EventArgs
//    {
//        public object Item { get; set; }
//    }
//
//    public class TestButtonItemTemplate : ITemplate
//    {
//        private readonly string _datafield;
//        public event EventHandler DataBinding;
//        public string ToolTip { get; set; }
//
//        public TestButtonItemTemplate(string datafield)
//        {
//            _datafield = datafield;
//        }
// 
//        public void InstantiateIn(Control container)
//        {
//            var b = new LinkButton();
//
//            b.DataBinding += BDataBinding;
//            
//            container.Controls.Add(b);
//        }
//
//        void BDataBinding(object sender, EventArgs e)
//        {
//            var b = (LinkButton)sender;
//            var row = (GridViewRow)b.NamingContainer;
//
//            DataBinding.Invoke(sender, new TestButtonEventArgs { Item = row.DataItem });
//
//            b.Text = DataBinder.Eval(row.DataItem, _datafield).ToString();
//            b.ToolTip = ToolTip ?? b.ToolTip;
//        }
//    }
//
//    public class TestButton : TemplateField, INamingContainer
//    {
//        public event EventHandler DataBinding;
//        public string DataField { get; set; }
//        public string ToolTip { set { (ItemTemplate as TestButtonItemTemplate).ToolTip = value; } }
//
//        public IDataItemContainer BindingContainer
//        {
//            get { return null; }
//        }
//
//        public Control NamingContainer
//        {
//            get { return Control; }
//        }
//
//        public override void InitializeCell(DataControlFieldCell cell, DataControlCellType cellType, DataControlRowState rowState, int rowIndex)
//        {
//            base.InitializeCell(cell, cellType, rowState, rowIndex);
//
//            if (cellType == DataControlCellType.DataCell)
//            {
//                var testButtonItemTemplate = new TestButtonItemTemplate(DataField);
//
//                testButtonItemTemplate.DataBinding += (sender, args) => DataBinding.Invoke(this, args);
//
//                ItemTemplate = testButtonItemTemplate;
//            }
//        }
//    }
}
