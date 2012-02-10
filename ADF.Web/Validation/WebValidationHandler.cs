using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Adf.Core.Extensions;
using Adf.Core.Validation;
using Adf.Web.Helper;

namespace Adf.Web
{
    /// <summary>
    /// Provides methods to parse validation result collection and reflect the exception on the UI.
    /// </summary>
    public class WebValidationHandler : IValidationHandler
    {
        /// <summary>
        /// Shows exception message on the UI
        /// </summary>
        /// <param name="validationResults">Validation results.</param>
        /// <param name="p">Variable no of objects.</param>
        public void Handle(ValidationResultCollection validationResults, params object[] p)
        {
            Page page = HttpContext.Current.Handler as Page;

            if (page == null) return;

            var control = ControlHelper.Find(page, "ExceptionControl") as UserControl;
            if (control != null) control.Visible = true;

            Label l = ControlHelper.Find(page, "lblException") as Label;
            if (l != null) l.Text = validationResults.ToArray().ConvertToString("<br>");
        }
    }
}