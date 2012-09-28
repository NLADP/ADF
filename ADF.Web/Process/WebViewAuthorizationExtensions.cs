using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Adf.Base.Authorization;
using Adf.Core.Authorization;
using Adf.Core.Domain;
using Adf.Web.UI;
using AjaxControlToolkit;

namespace Adf.Web.Process
{
    public static class WebViewAuthorizationExtensions
    {
        #region WebControl Authorization

        public static void SetPermission<T>(this WebControl control, IAction action, bool disable = false)
        {
            if (control == null) return;

#if DEBUG
            control.Style.Add("border", "1px solid Red");
#endif
            if (!AuthorizationManager.IsAllowed(typeof(T).Name, action))
            {
                if (disable)
                    control.Enabled = false;
                else
                    control.Visible = false;
            }
        }

        public static void SetPermission(this WebControl control, Type subject, IAction action, bool disable = false)
        {
            if (control == null) return;

#if DEBUG
            control.Style.Add("border", "1px solid Red");
#endif
            if (!AuthorizationManager.IsAllowed(subject.Name, action))
            {
                if (disable)
                    control.Enabled = false;
                else
                    control.Visible = false;
            }
        }

        #endregion

        #region SmartPanel Authorization

        public static void SetPermission<T>(this SmartPanel control, IAction action)
        {
            if (control == null) return;
#if DEBUG
            control.Style.Add("color", "blue");
#endif
            if (!AuthorizationManager.IsAllowed(typeof (T).Name, action))
            {
                control.Enabled = false;

                if (!AuthorizationManager.IsAllowed(typeof(T).Name, Actions.View))
                {
                    control.Visible = false;
                }
            }
        }

        #endregion

        #region Gridview Authorization

        public static void SetPermission<T>(this GridView control, IAction action)
        {
            SetPermission(control, typeof(T), action);
        }

        public static void SetPermission(this GridView control, Type subject, IAction action) 
        {
            if (control == null) return;
#if DEBUG
            control.Style.Add("border", "1px solid Red!important");
#endif
            if (!AuthorizationManager.IsAllowed(subject.Name, action))
            {
                control.SelectedIndexChanging += (sender, args) => args.Cancel = true;
            }
        }

        #endregion

        #region MessageButton Authorization

        public static void SetPermission<T>(this MessageButton button, IAction action)
        {
            if (button == null) return;
#if DEBUG
            button.Class += " ugly";
#endif
            if (!AuthorizationManager.IsAllowed(typeof(T).Name, action))
            {
                button.Visible = false;
            }
        }

        public static void SetPermission(this MessageButton button, Type subject,IAction action) 
        {
            if (button == null) return;
#if DEBUG
            button.Class += " ugly";
#endif
            if (!AuthorizationManager.IsAllowed(subject.Name, action))
            {
                button.Visible = false;
            }
        }

        public static void SetPermission<T>(this Control control, IAction action)
        {
            if (control == null) return;

            if (!AuthorizationManager.IsAllowed(typeof(T).Name, action))
            {
                if (!(control is ModalPopupExtender))    // throws an exception
                {
                    control.Visible = false;
                }
            }
        }

        #endregion
    }
}
