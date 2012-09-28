using System.Web.UI.WebControls;
using Adf.Core.State;
using Adf.Web.UI.SmartView;

namespace Adf.Web
{
    /// <summary>
    /// Represents the pager related service of an <see cref="SmartView"/>.
    /// Provides methods to set the pager related properties of an <see cref="SmartView"/>.
    /// </summary>
    class BusinessGridViewPager : IGridService
    {
        public int DefaultPageSize = 10;
        public PagerMode DefaultPagerMode = PagerMode.NumericPages;

        /// <summary>
        /// Initializes an instance of the <see cref="Adf.Web.BusinessGridViewPager"/> class.
        /// Sets the DefaultPageSize and DefaultPagerMode of the <see cref="Adf.Web.BusinessGridViewPager"/>.
        /// </summary>
        public BusinessGridViewPager()
        {
            int pagesize;
            if(int.TryParse(StateManager.Settings["BusinessGridView.DefaultPageSize"] as string, out pagesize))
                DefaultPageSize = pagesize;
            
            if (StateManager.Settings["BusinessGridView.DefaultPagerMode"].ToString() == "NextPrev") 
                DefaultPagerMode = PagerMode.NextPrev;
        }

        /// <summary>
        /// Sets the pager related properties of the specified <see cref="SmartView"/>.
        /// </summary>
        /// <remarks>
        /// The pager related properties like PageSize, PagerSettings.Mode, 
        /// PagerSettings.PageButtonCount and PagerSettings.Position are set.
        /// </remarks>
        /// <param name="view">The <see cref="SmartView"/>, the pager 
        /// related properties of which are to set.</param>
        /// <param name="p">The parameters used to set the properties. Currently not being used.</param>
        public void InitService(SmartView view, params object[] p)
        {
            view.PageSize = (view.PageSize == 10) ? DefaultPageSize : view.PageSize;
//            view.PagerSettings.Visible = view.AllowPaging;

            if (DefaultPagerMode == PagerMode.NumericPages)
                view.PagerSettings.Mode = PagerButtons.NumericFirstLast;

            if (DefaultPagerMode == PagerMode.NextPrev)
                view.PagerSettings.Mode = PagerButtons.NextPreviousFirstLast;
            
            view.PagerSettings.PageButtonCount = 10;
            view.PagerSettings.Position = PagerPosition.Bottom;
        }

        /// <summary>
        /// Sets the new page as the current page of the specified <see cref="SmartView"/>.
        /// </summary>
        /// <param name="action">The 'Paging' <see cref="GridAction"/> to perform.</param>
        /// <param name="view">The <see cref="SmartView"/>, the current page of 
        /// which is to set.</param>
        /// <param name="p">The parameters used to set the current page.</param>
        public void HandleService(GridAction action, SmartView view, params object[] p)
        {
            if (view == null) return;
            if (!view.AllowPaging) return;
            if (action != GridAction.Paging) return;

            GridViewPageEventArgs args = p[0] as GridViewPageEventArgs;
            if (args == null) return;
            
            view.PageIndex = args.NewPageIndex;
            view.SelectedIndex = -1;
            view.DataBind();
        }
    }
}
