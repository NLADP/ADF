using System.Web.UI.WebControls;
using Adf.Core.Domain;
using Adf.Core.State;
using Adf.Web.UI.SmartView;

namespace Adf.Web
{
    /// <summary>
    /// Represents the sorting related service of an <see cref="SmartView"/>.
    /// Provides methods to sort the records in an <see cref="SmartView"/> etc.
    /// </summary>
    class BusinessGridViewSorter : IGridService
    {
        /// <summary>
        /// Puts the specified <see cref="SmartView"/> into the 
        /// <see cref="StateManager.Personal"/> i.e. session.
        /// </summary>
        /// <param name="view">The <see cref="SmartView"/> to put.</param>
        /// <param name="p">The parameters to use. Currently not being used.</param>
        public void InitService(SmartView view, params object[] p)
        {
            Grid = view;
        }

        #region Properties

        /// <summary>
        /// Gets the <see cref="SortDirection"/> according to the value stored in the 
        /// <see cref="StateManager.Personal"/> i.e. session for a particular 
        /// <see cref="SmartView"/>.
        /// </summary>
        /// <returns>
        /// The <see cref="SortDirection"/> according to the value stored in the 
        /// <see cref="StateManager.Personal"/> i.e. session for a particular 
        /// <see cref="SmartView"/>.
        /// </returns>
        public SortDirection SortDirection
        {
            get { return (SortAscending) ? SortDirection.Ascending : SortDirection.Descending; }
        }

        /// <summary>
        /// Gets or sets the <see cref="SmartView"/> 
        /// </summary>
        /// <returns>
        /// The <see cref="SmartView"/> 
        /// </returns>
        public SmartView Grid { get; set; }
//        {
//            get { return StateManager.Personal["BusinessGridViewSorter"] as SmartView; }
//            set { StateManager.Personal["BusinessGridViewSorter"] = value; }
//        }

        /// <summary>
        /// Gets or sets the last expression stored in the 
        /// <see cref="StateManager.Personal"/> i.e. session for a particular 
        /// <see cref="SmartView"/>
        /// </summary>
        /// <returns>
        /// The last expression stored in the 
        /// <see cref="StateManager.Personal"/> i.e. session for a particular 
        /// <see cref="SmartView"/>
        /// </returns>
        public string LastExpression
        {
            get { return StateManager.Personal[Grid.ClientID + ".LastExpression"] as string; }
            set { StateManager.Personal[Grid.ClientID + ".LastExpression"] = value; }
        }

        /// <summary>
        /// Gets or sets the value indicating whether the 'SortAscending' indicator for a 
        /// particular <see cref="SmartView"/> is true.
        /// </summary>
        /// <returns>
        /// true if the 'SortAscending' indicator for a particular 
        /// <see cref="SmartView"/> is true; otherwise, false.
        /// </returns>
        public bool SortAscending
        {
            get { return (bool)StateManager.Personal[Grid.ClientID + ".SortAscending"]; }
            set { StateManager.Personal[Grid.ClientID + ".SortAscending"] = value; }
        }

        #endregion

        /// <summary>
        /// Sorts the records in the specified <see cref="SmartView"/>.
        /// </summary>
        /// <param name="action">The 'Sorting' <see cref="GridAction"/> to perform.</param>
        /// <param name="view">The <see cref="SmartView"/> to sort.</param>
        /// <param name="p">The parameters used for sorting.</param>
        public void HandleService(GridAction action, SmartView view, params object[] p)
        {
            if (view == null) return;
            if (!view.AllowSorting) return;
            if (action != GridAction.Sorting) return;

            // Handle sorting
            GridViewSortEventArgs args = p[0] as GridViewSortEventArgs;
            if (args == null) return;

            if (args.SortExpression == LastExpression)
            {
                SortAscending = !SortAscending;
            }
            else
            {
                SortAscending = true;
                LastExpression = args.SortExpression;
            }

            args.SortDirection = SortDirection;

            if (view.DataSource is IDomainCollection)
            {
                IDomainCollection col = (IDomainCollection)view.DataSource;

                col.Sort(args.SortExpression, (args.SortDirection == SortDirection.Ascending) ? SortOrder.Ascending : SortOrder.Descending);

                view.DataSource = col;
            }

            view.DataBind();
        }

    }
}
