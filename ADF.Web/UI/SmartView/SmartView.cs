using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Adf.Core.Domain;
using Adf.Core.Extensions;
using Adf.Core.Identity;
using Adf.Core.Resources;
using Adf.Core.State;
using Adf.Core.Styling;

namespace Adf.Web.UI.SmartView
{
    // Class is sealed so that we can safely initialize virtual fields in the constructor
    public sealed class SmartView : GridView
    {
        public string PageSizes { get; set; }

        private string _lastexpression
        {
            get { return ViewState[ClientID + "LastExpression"].ToString(); }
            set { ViewState[ClientID + "LastExpression"] = value; }
        }

        private bool _ascending
        {
            get { return Convert.ToBoolean(ViewState[ClientID + "Ascending"]); }
            set { ViewState[ClientID + "Ascending"] = value; }
        }

        public string Source { get; set; }
        
        private ID _currentId = IdManager.Empty();
        public ID Current
        {
            get { return _currentId; }
            set { SetIndex(value); _currentId = value; }
        }

        public bool ShowEmptyTable
        {
            get { var o = ViewState["ShowEmptyTable"]; return (o == null || (bool) o); }
            set { ViewState["ShowEmptyTable"] = value; }
        }

        public override object DataSource
        {
            get
            {
                return (Source.IsNullOrEmpty()) 
                    ? StateManager.Personal[ClientID + ".DataSource"]
                    : (base.DataSource = PropertyHelper.GetValue(Page, Source));
            }
            set
            {
                PageIndex = 0;
                
                if (Source.IsNullOrEmpty())
                {
                    StateManager.Personal[ClientID + ".DataSource"] = value;
                }
                else
                {
                    base.DataSource = value;
                }

                OnDataPropertyChanged();
            }
        }

        public SmartView()
        {
            PageSizes = StateManager.Settings.GetOrDefault("SmartView.PageSizes", "10,20,50");
            PageSize = StateManager.Settings.GetOrDefault("SmartView.DefaultPageSize", 20);
            
            AllowSorting = true;
            AllowPaging = true;
            EmptyDataText = "No items found.";
            EnableViewState = true;
            AutoGenerateColumns = false;
            CellPadding = 0;

            // Assign empty eventhandler so that we can safely call base.OnPageIndexChanging in the overridden method
            PageIndexChanging += SmartView_PageIndexChanging;
            // Assign empty eventhandler so that we can safely call base.OnSorting in the overridden method
            Sorting += SmartView_Sorting;
        }

        // Empty eventhandler so that we can safely call base.OnPageIndexChanging in the overridden method
        private static void SmartView_Sorting(object sender, GridViewSortEventArgs e) { }

        // Assign empty eventhandler so that we can safely call base.OnSorting in the overridden method
        private static void SmartView_PageIndexChanging(object sender, GridViewPageEventArgs e) { }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PagerSettings.Mode = PagerButtons.NumericFirstLast;
            PagerSettings.PageButtonCount = 10;
            PagerSettings.Position = PagerPosition.Bottom;

            _lastexpression = string.Empty;
            _ascending = true;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (Source.IsNullOrEmpty()) return;

            DataSource = PropertyHelper.GetValue(Page, Source);
            DataKeyNames = new[] { "ID" };
        }


        protected override void OnRowCommand(GridViewCommandEventArgs e)
        {
            ID id;

            if (this.TryGetId(e, out id)) _currentId = id;

            base.OnRowCommand(e);
        }

        protected override void OnPageIndexChanging(GridViewPageEventArgs e)
        {
            base.OnPageIndexChanging(e);

            if (e.Cancel) return;

            PageIndex = e.NewPageIndex;
            SelectedIndex = -1;

            DataBind();
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            PagerSettings.Visible = DataSource != null && (DataSource as IEnumerable).Count() > PageSize;

            if (!Source.IsNullOrEmpty()) DataBind();
        }

        private void SetIndex(ID value)
        {
            var index = (DataSource as IEnumerable<IDomainObject>).IndexOf(value);

            PageIndex = index/PageSize;
            SelectedIndex = index%PageSize;

            DataBind(); // rebind otherwise the page is not correctly set
        }

        #region Services

        /// <summary>
        /// Provides an override of GridView sorting.
        /// </summary>
        /// <param name="e"><see cref="GridViewSortEventArgs"/></param>
        protected override void OnSorting(GridViewSortEventArgs e)
        {
            base.OnSorting(e);

            if (e.Cancel) return;

            if (e.SortExpression == _lastexpression)
            {
                _ascending = !_ascending;
            }
            else
            {
                _ascending = true;
                _lastexpression = e.SortExpression;
            }

            if (DataSource is IDomainCollection)
            {
                var source = (IDomainCollection) DataSource;

                source.Sort(e.SortExpression, (_ascending) ? SortOrder.Ascending : SortOrder.Descending);

                DataSource = source;
            }

            DataBind();
        }

        #endregion

        protected override int CreateChildControls(IEnumerable dataSource, bool dataBinding)
        {
            int numberofrows = base.CreateChildControls(dataSource, dataBinding);

            if (numberofrows == 0 && ShowEmptyTable) { CreateEmptyRow(); }

            CreateFooter();

            StyleManager.Style(StyleType.Grid, this);

            return numberofrows;
        }

        private void CreateEmptyRow()
        {
            //create table
            var table = new Table { ID = ID };

            //create a new header row
            var row = CreateRow(-1, -1, DataControlRowType.Header, DataControlRowState.Normal);

            //convert the exisiting columns into an array and initialize
            var fields = new DataControlField[Columns.Count];

            Columns.CopyTo(fields, 0);
            InitializeRow(row, fields);
            table.Rows.Add(row);

            if (EmptyDataTemplate != null || !string.IsNullOrEmpty(EmptyDataText))
            {
                var emptyRow = new GridViewRow(-1, -1, DataControlRowType.EmptyDataRow, DataControlRowState.Normal) { CssClass = "SmartViewRow"}; 
                var cell = new TableCell { ColumnSpan = fields.Length };

                if (EmptyDataTemplate != null)
                {
                    EmptyDataTemplate.InstantiateIn(cell);
                }
                else
                {
                    cell.Controls.Add(new LiteralControl(ResourceManager.GetString(EmptyDataText)));
                }

                emptyRow.Cells.Add(cell);
                table.Rows.Add(emptyRow);
            }

            Controls.Clear(); // Remove the current empty row and stuff
            Controls.Add(table);
        }


        private void CreateFooter()
        {
            var footer = new SmartViewFooter {Owner = this, Visible = true, PageSizes = PageSizes };

            Controls.Add(footer);
        }
    }
}
