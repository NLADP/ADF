using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.UI.WebControls;
using Adf.Core.Domain;
using Adf.Core.Extensions;
using Adf.Core.Identity;
using Adf.Core.Objects;
using Adf.Core.State;
using Adf.Web.UI.Styling;

namespace Adf.Web.UI.SmartView
{
    /// <summary>
    /// Represents a customized <see cref="GridView"/> control.
    /// </summary>
    public class SmartView : GridView
    {
        #region private fields

        private static IStyler _styler = ObjectFactory.BuildUp<IStyler>("BusinessGridViewStyler");
        private bool _autostyle = true;
        private static IEnumerable<IGridService> _services;
        /// <summary>
        /// Gets the collection after addition of element.
        /// </summary>
        private static IEnumerable<IGridService> Services
        {
            get { return _services ?? (_services = ObjectFactory.BuildAll<IGridService>().ToList()); }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initialize the properties of business GridView
        /// </summary>
        public SmartView()
        {
            if (DesignMode) return;

            base.EnableViewState = true;
            base.AutoGenerateColumns = false;
            base.CellPadding = 0;
            base.AllowSorting = true;
            base.AllowPaging = true;

            foreach (IGridService service in Services) service.InitService(this);
        }

        /// <summary>
        /// Set style on page load.
        /// </summary>
        /// <param name="e">Event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            if (!DesignMode && AutoStyle)
                Styler.SetStyles(this);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets client identification of datasource
        /// </summary>
        public override object DataSource
        {
            get
            {
                if (DesignMode) return base.DataSource;

                return StateManager.Personal[ClientID + ".DataSource"];
            }
            set
            {
                if (DesignMode)
                {
                    base.DataSource = value;
                    return;
                }

                var source = value as IDomainObject[];
                if (source != null)
                {
                    PagerSettings.Visible = (source.Length > PageSize);
                    PageIndex = 0;
                }

                StateManager.Personal[ClientID + ".DataSource"] = value;
            }
        }

        /// <summary>
        /// Gets or sets a style of BusinessGrid
        /// </summary>
        [Bindable(true), Category("BusinessGrid"), DefaultValue(true)]
        public bool AutoStyle
        {
            get { return _autostyle; }
            set { _autostyle = value; }
        }

        /// <summary>
        /// Gets or sets a style of type <see cref="IStyler"/>
        /// </summary>
        /// <returns>Returns a style of type <see cref="IStyler"/>.</returns>
        public static IStyler Styler
        {
            get { return _styler; }
            set { _styler = value; }
        }

        /// <summary>
        /// Gets the value of selected index of GridView
        /// </summary>
        public ID Current
        {
            get
            {
                IDomainObject[] source = DataSource as IDomainObject[];
                int index = (PageSize * (PageIndex) + SelectedIndex);
                if (source != null)
                {
                    return source[index].Id;
                }

                //doing it the old way
                return IdManager.New(DataKeys[SelectedIndex].Value.ToString());
            }
            set
            {
                int index = (DataSource as IEnumerable<IDomainObject>).IndexOf(value);

                PageIndex = index / PageSize;
                SelectedIndex = index % PageSize;

                DataBind();  // rebind otherwise the page is not correctly set
            }
        }

        #endregion

        #region Services

        /// <summary>
        /// Provides an override of GridView sorting.
        /// </summary>
        /// <param name="e"><see cref="GridViewSortEventArgs"/></param>
        protected override void OnSorting(GridViewSortEventArgs e)
        {
            //delegate sorting to the gridservice(s)
            foreach (IGridService service in Services)
                service.HandleService(GridAction.Sorting, this, e);
        }

        /// <summary>
        /// Provides an override of GridView paging.
        /// </summary>
        /// <param name="e"><see cref="GridViewPageEventArgs"/></param>
        protected override void OnPageIndexChanging(GridViewPageEventArgs e)
        {
            //delegate paging to the gridservice(s)
            foreach (IGridService service in Services)
                service.HandleService(GridAction.Paging, this, e);
        }

        #endregion

        #region EmptyTable

        /// <summary>
        /// Get or sets an empty table
        /// </summary>
        [Description("Enable or disable generating an empty table with headers if no data rows in source"), Category("Misc"), DefaultValue("true")]
        public bool ShowEmptyTable
        {
            get
            {
                object o = ViewState["ShowEmptyTable"];
                return (o == null || (bool)o);
            }
            set
            {
                ViewState["ShowEmptyTable"] = value;
            }
        }


        /// <summary>
        /// Provides the creation of child control of table. It is an CreateChildControls override of <see cref="System.Web.UI.WebControls.GridView"/>
        /// </summary>
        /// <param name="dataSource"><see cref="System.Web.UI.WebControls.GridView"/></param>
        /// <param name="dataBinding"><see cref="System.Web.UI.WebControls.GridView"/></param>
        /// <returns></returns>
        protected override int CreateChildControls(IEnumerable dataSource, bool dataBinding)
        {
            int numRows = base.CreateChildControls(dataSource, dataBinding);

            //no data rows created, create empty table if enabled
            if (numRows == 0 && ShowEmptyTable)
            {
                //create table
                Table table = new Table {ID = ID};

                //create a new header row
                GridViewRow row = base.CreateRow(-1, -1, DataControlRowType.Header, DataControlRowState.Normal);

                //convert the exisiting columns into an array and initialize
                DataControlField[] fields = new DataControlField[Columns.Count];
                Columns.CopyTo(fields, 0);
                InitializeRow(row, fields);
                table.Rows.Add(row);

                Controls.Add(table);
            }
            return numRows;
        }

        #endregion EmptyTable
    }
}