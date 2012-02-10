using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Adf.Web.Properties;

namespace Adf.Web.UI
{

    #region CodeSample

     //<summary>
     //A <see cref="DataControlField"/> which shows a checkbox in each row to signal the selection of that row.
     //</summary>
     //<example>
     //This example shows the use of this column in a simple <see cref="GridView"/>
     //<code>
     //<![CDATA[
     //<%@ Import Namespace="System.Data" %>
     //<%@ Register TagPrefix="mbrsc" Namespace="Adf.Web.UI.PageControls.RowSelectorColumn" Assembly="Adf.Web" %>
     
     //<html>
     //   <script language="C#" runat="server">
      
     //      ICollection CreateDataSource() 
     //      {
     //         DataTable dt = new DataTable();
     //         DataRow dr;
      
     //         dt.Columns.Add(new DataColumn("IntegerValue", typeof(Int32)));
     //         dt.Columns.Add(new DataColumn("StringValue", typeof(string)));
      
     //         for (int i = 0; i < 5; i++) 
     //         {
     //            dr = dt.NewRow();
      
     //            dr[0] = i;
     //            dr[1] = "Item " + i.ToString();
      
     //            dt.Rows.Add(dr);
     //         }
      
     //         DataView dv = new DataView(dt);
     //         return dv;
     //      }
      
     //      void Page_Load(Object sender, EventArgs e) 
     //      {
      
     //         if (!IsPostBack) 
     //         {
     //            // Load this data only once.
     //            ItemsGrid.DataSource= CreateDataSource();
     //            ItemsGrid.DataBind();
     //         }
     //      }
           
     //      protected void ShowSelections( Object sender, EventArgs e ) {
     //       RowSelectorColumn rsc = ItemsGrid.Columns[0] as RowSelectorColumn;
     //       Message.Text = "Total selected rows = " + rsc.SelectedIndexes.Length.ToString() + "<br>";
     //       foreach( Int32 selectedIndex in rsc.SelectedIndexes ) {
     //           Message.Text += selectedIndex.ToString() + "<br>";
     //       }
     //      }
      
     //   </script>
      
     //<body>
      
     //   <form runat=server>
      
     //      <h3>GridView Example</h3>
      
     //      <asp:GridView id="ItemsGrid"
     //           BorderColor="black"
     //           BorderWidth="1"
     //           CellPadding="3"
     //           AutoGenerateColumns="true"
     //           runat="server">
     
     //         <HeaderStyle BackColor="darkblue" forecolor="white">
     //         </HeaderStyle> 
     //         <Columns>
     //           <mbrsc:RowSelectorColumn allowselectall="true" />
     //         </Columns>
      
     //      </asp:GridView>
           
     //      <asp:Button runat="server" onclick="ShowSelections" text="Show Selections" />
     //      <br>
     //      <asp:Label runat="server" id="Message" />
      
     //   </form>
      
     //</body>
     //</html>
     //]]>
     //</code>
     //</example>
     

    #endregion CodeSample

    /// <summary>
    /// Represents a checkbox column in a gridview control
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
    public class RowSelectorColumn : DataControlField
    {
        #region FindColumns

        /// <summary>
        /// Finds the first <see cref="RowSelectorColumn"/> in the given <see cref="GridView"/>.
        /// </summary>
        /// <param name="grid">The <see cref="GridView"/> to search.</param>
        /// <returns>A <see cref="RowSelectorColumn"/> if found else null.</returns>
        public static RowSelectorColumn FindColumn(GridView grid)
        {
           
            if (grid != null)
            {
                foreach (DataControlField col in grid.Columns)
                {
                     RowSelectorColumn foundCol = col as RowSelectorColumn;
                    if (foundCol != null) return foundCol;
                }
            }
            return null;
        }

        /// <summary>
        /// Finds the first <see cref="RowSelectorColumn"/> in the given <see cref="GridView"/> after or at the given column index.
        /// </summary>
        /// <param name="grid">The <see cref="GridView"/> to search.</param>
        /// <param name="startIndex">The index of the column to start the search.</param>
        /// <returns>A <see cref="RowSelectorColumn"/> if fouund or else null.</returns>
        public static RowSelectorColumn FindColumn(GridView grid, Int32 startIndex)
        {
            if (grid != null)
            {
                for (Int32 i = startIndex; i < grid.Columns.Count; i++)
                {
                    RowSelectorColumn foundCol = grid.Columns[i] as RowSelectorColumn;
                    if (foundCol != null)
                        return foundCol;
                }
            }
            return null;
        }

        #endregion

        #region Cell Creation

        /// <summary>
        /// This member overrides <see cref="DataControlField.InitializeCell"/>.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods"), SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId = "System.Web.UI.WebControls.TableCell.set_Text(System.String)")]
        public override void InitializeCell(DataControlFieldCell cell, DataControlCellType cellType, DataControlRowState rowState, int rowIndex)
        {
            base.InitializeCell(cell, cellType, rowState, rowIndex);

            switch (rowState)
            {
                case DataControlRowState.Edit:
                case DataControlRowState.Normal:
                case DataControlRowState.Alternate:
                case DataControlRowState.Selected:


                    if (SelectionMode == ListSelectionMode.Multiple)
                    {
                        ParticipantCheckBox selector = new ParticipantCheckBox();
                        selector.ID = "RowSelectorColumnSelector";
                        selector.AutoPostBack = AutoPostBack;
                        cell.Controls.Add(selector);
                        if (AllowSelectAll)
                        {
                            RegisterForSelectAll(selector);
                        }
                        selector.ServerChange += new EventHandler(selector_ServerChange);
                    }
                    else
                    {
                        ParticipantRadioButton selector = new ParticipantRadioButton();
                        selector.Name = "RowSelectorColumnSelector";
                        selector.ID = "RowSelectorColumnSelector";
                        selector.AutoPostBack = AutoPostBack;
                        cell.Controls.Add(selector);
                        selector.DataBinding += new EventHandler(selectorDataBinding);
                        selector.ServerChange += new EventHandler(selector_ServerChange);
                    }
                    break;
            }

            if (cellType == DataControlCellType.Header)
            {
                if (AllowSelectAll && SelectionMode == ListSelectionMode.Multiple)
                {
                    selectAllControl = new SelectAllCheckBox();
                    selectAllControl.ID = "RowSelectorColumnAllSelector";
                    selectAllControl.AutoPostBack = AutoPostBack;
                    RegisterSelectAllScript();

                    String currentText = (cell == null) ? "" : cell.Text;
                    if (!string.IsNullOrEmpty(currentText))
                    {
                        cell.Text = "";
                    }
                    cell.Controls.Add(selectAllControl);
                    if (!string.IsNullOrEmpty(currentText))
                    {
                        cell.Controls.Add(new LiteralControl(currentText));
                    }
                }
            }
        }

        /// <summary>
        /// Provides the creation of child control.
        /// </summary>
        /// <returns><see cref="RowSelectorColumn"/></returns>
        protected override DataControlField CreateField()
        {
            //throw new Exception("The method or operation is not implemented.");

            return this;
        }

        /// <summary>
        /// Sets the value of the given selector to the row index.
        /// </summary>
        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Int32.ToString"), SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        protected virtual void SetIndexValue(HtmlInputRadioButton radioSelector)
        {
            GridViewRow row = radioSelector.NamingContainer as GridViewRow;
            if (row != null)
            {
                radioSelector.Value = row.RowIndex.ToString();
            }
        }


        /// <summary>
        /// Gets the checkbox appearing in the header row which controls the other checkboxes.
        /// </summary>
        /// <remarks>The checkbox if <see cref="AllowSelectAll"/> is true, otherwise null.</remarks>
        [Description("Gets the checkbox appearing in the header row which controls the other checkboxes.")]
        protected virtual HtmlInputCheckBox SelectAllControl
        {
            get { return selectAllControl; }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the behavior of including a checkbox in the Header area which selects all the row checkboxes based on its value.
        /// </summary>
        /// <returns>Returns True if the header area checkbox for selecting all row checkboxes is included, else False.</returns>
        /// <remarks>This behavior will only exist on browsers supporting javascript and the W3C DOM.</remarks>
        [DefaultValue(false), Description("Gets or sets the behavior of including a checkbox in the Header area which selects all the row checkboxes based on its value.")]
        public virtual Boolean AllowSelectAll
        {
            get
            {
                object savedState = ViewState["AllowSelectAll"];
                if (savedState != null)
                {
                    return (Boolean) savedState;
                }
                return false;
            }
            set { ViewState["AllowSelectAll"] = value; }
        }

        /// <summary>
        /// Gets or sets the selection mode of the <see cref="RowSelectorColumn"/>.
        /// </summary>
        /// <returns>Returns the selection mode of the <see cref="RowSelectorColumn"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Value should be "Single" or "Multiple"</exception>
        /// <remarks>
        /// Use the SelectionMode property to specify the mode behavior of the <see cref="RowSelectorColumn"/>.
        /// Setting this property to ListSelectionMode.Single indicates only a single item can be selected, while ListSelectionMode.Multiple specifies multiple items can be selected.
        /// </remarks>

        [DefaultValue(ListSelectionMode.Multiple), Description("Gets or sets the selection mode of the RowSelectorColumn.")]
        public virtual ListSelectionMode SelectionMode
        {
            get
            {
                object savedState = ViewState["SelectionMode"];
                if (savedState != null)
                {
                    return (ListSelectionMode) savedState;
                }
                return ListSelectionMode.Multiple;
            }
            set
            {
                if (!Enum.IsDefined(typeof (ListSelectionMode), value))
                {
                    throw new ArgumentOutOfRangeException("value");
                }
                ViewState["SelectionMode"] = value;
            }
        }

        /// <summary>
        /// Gets or sets if the column's controls will postback automaticly when the selection changes.
        /// </summary>
        /// <returns>Returns True if column's controls postback upon selection changes, else False.</returns>
        [Description("Gets or sets if the column's controls will postback automaticly when the selection changes."), DefaultValue(false),]
        public virtual Boolean AutoPostBack
        {
            get
            {
                object savedState = ViewState["AutoPostBack"];
                if (savedState != null)
                {
                    return (Boolean) savedState;
                }
                return false;
            }
            set { ViewState["AutoPostBack"] = value; }
        }

        /// <summary>
        /// Gets or sets an array of the GridViewRow indexes which are marked as selected.
        /// </summary>
        /// <returns>Returns an array of integers corresponding to RowIndex of all the selected rows</returns>
        /// <remarks>The index can be used to get, for example, in the DataKeys collection to get the keys for the selected rows.</remarks>
        [SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays"), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Description("Gets an array of the GridViewRow indexes which are marked as selected.")]
        public virtual Int32[] SelectedIndexes
        {
            get
            {
                ArrayList selectedIndexList = new ArrayList();
                //				Int32 thisCellIndex = this.Owner.Columns.IndexOf(this);
                foreach (GridViewRow item in ((GridView) Control).Rows)
                {
                    Control foundControl = item.FindControl("RowSelectorColumnSelector");
                    HtmlInputCheckBox Checkselector = foundControl as HtmlInputCheckBox;
                    HtmlInputRadioButton radioselector = foundControl as HtmlInputRadioButton;
                    if (Checkselector != null && Checkselector.Checked)
                    {
                        selectedIndexList.Add(item.RowIndex);
                    }
                    else if (radioselector != null && radioselector.Checked)
                    {
                        selectedIndexList.Add(item.RowIndex);
                    }
                }
                return (Int32[]) selectedIndexList.ToArray(typeof (Int32));
            }
            set
            {
                foreach (GridViewRow item in ((GridView) Control).Rows)
                {
                    Boolean checkIt = false;
                    for (Int32 i = 0; i < value.Length; i++)
                    {
                        if (value[i] == item.RowIndex)
                        {
                            checkIt = true;
                            break;
                        }
                    }

                    Control foundControl = item.FindControl("RowSelectorColumnSelector");
                    HtmlInputCheckBox checkselector = foundControl as HtmlInputCheckBox;
                    HtmlInputRadioButton radioselector = foundControl as HtmlInputRadioButton;

                    if (checkselector != null)
                    {
                        checkselector.Checked = checkIt;
                    }
                    else if (radioselector != null)
                    {
                        radioselector.Checked = checkIt;
                    }
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Checks In all row selecter CheckBoxes found in a GridView control.
        /// </summary>
        public void SelectAll()
        {
            if (SelectionMode.Equals(ListSelectionMode.Multiple) && AllowSelectAll)
            {
                foreach (GridViewRow item in ((GridView) Control).Rows)
                {
                    HtmlInputCheckBox checkselector = item.FindControl("RowSelectorColumnSelector") as HtmlInputCheckBox;
                    checkselector.Checked = true;
                }
                selectAllControl.Checked = true;
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// The event that is raised when the selection has changed.
        /// </summary>
        public event EventHandler SelectionChanged;

        /// <summary>
        /// Raises the SelectionChanged event.
        /// </summary>
        protected virtual void OnSelectionChanged(EventArgs e)
        {
            if (SelectionChanged != null)
            {
                SelectionChanged(this, e);
            }
        }

        /// <summary>
        /// The SelectionChanged event.
        /// </summary>
        private void selector_ServerChange(object sender, EventArgs e)
        {
            OnSelectionChanged(EventArgs.Empty);
        }

        #endregion

        #region SelectAll Script

        /// <summary>
        /// Emits the script library neccessary for the SelectAll behavior.
        /// </summary>
        [SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes"), SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId = "System.Exception.#ctor(System.String)"), Description("Emits the script library neccessary for the SelectAll behavior.")]
        protected virtual void RegisterSelectAllScript()
        {
            if (Control.Page == null) return;

            if (Control.Page.ClientScript.IsClientScriptBlockRegistered(GetType(), "rowselectorcolumn")) return;

            Control.Page.ClientScript.RegisterClientScriptBlock(GetType(), "rowselectorcolumn", Resources.ResourceManager.GetString("rowselectorcolumn"));
        }


        /// <summary>
        /// Registers the given checkbox as being bound to the SelectAll checkbox.
        /// </summary>
        /// <param name="selector">The checkbox being bound.</param>
        [Description("Registers the given checkbox as being bound to the SelectAll checkbox.")]
        protected virtual void RegisterForSelectAll(ParticipantCheckBox selector)
        {
            selector.Master = selectAllControl;
        }

        #endregion

        #region Private

        /// <summary>
        /// Set the value of given selector.
        /// </summary>
        /// <param name="sender"><see cref="System.Object"/></param>
        /// <param name="e"><see cref="System.EventArgs"/></param>
        private void selectorDataBinding(Object sender, EventArgs e)
        {
            ParticipantRadioButton radio = sender as ParticipantRadioButton;
            if (radio != null)
            {
                SetIndexValue(radio);
            }
        }

        private SelectAllCheckBox selectAllControl;

        #endregion

        #region Participant Controls

        /// <summary>
        /// Represents checkbox in the header for select-all.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
        public class SelectAllCheckBox : HtmlInputCheckBox
        {
            /// <summary>
            /// Overrides <see cref="HtmlControl.RenderAttributes"/>
            /// </summary>
            protected override void RenderAttributes(HtmlTextWriter writer)
            {
                String finalOnClick = Attributes["onclick"];
                if (finalOnClick == null)
                {
                    finalOnClick = "";
                }

                finalOnClick += " MetaBuilders_RowSelectorColumn_SelectAll( this );";
                if (AutoPostBack)
                {
                    finalOnClick += Page.ClientScript.GetPostBackEventReference(this, null);
                }
                Attributes["onclick"] = finalOnClick;

                base.RenderAttributes(writer);
            }

            /// <summary>
            /// Gets or sets if the control will postback after being changed.
            /// </summary>
            /// <returns>Returns True if control postbacks upon selection changes, else False.</returns>
            public Boolean AutoPostBack
            {
                get
                {
                    Object savedState = ViewState["AutoPostBack"];
                    if (savedState != null)
                    {
                        return (Boolean) savedState;
                    }
                    return false;
                }
                set { ViewState["AutoPostBack"] = value; }
            }
        }

        /// <summary>
        /// Represent a checkbox that appear in each cell of a <see cref="RowSelectorColumn"/> when <see cref="RowSelectorColumn.SelectionMode"/> is set to <see cref="ListSelectionMode.Multiple"/>.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
        public class ParticipantCheckBox : HtmlInputCheckBox
        {
            /// <summary>
            /// Gets or sets if the control will postback after being changed.
            /// </summary>
            public Boolean AutoPostBack
            {
                get
                {
                    Object savedState = ViewState["AutoPostBack"];
                    if (savedState != null)
                    {
                        return (Boolean) savedState;
                    }
                    return false;
                }
                set { ViewState["AutoPostBack"] = value; }
            }


            /// <summary>
            /// Overrides <see cref="HtmlControl.Render"/>.
            /// </summary>
            protected override void Render(HtmlTextWriter writer)
            {
                base.Render(writer);
                if (master != null)
                {
                    LiteralControl script = new LiteralControl("\r\n<script language='javascript'>\r\nMetaBuilders_RowSelectorColumn_Register('" + master.ClientID + "', '" + ClientID + "')\r\n</script>");
                    script.RenderControl(writer);
                }
            }

            /// <summary>
            /// Gets or sets the checkbox which controls the checked state of this checkbox.
            /// </summary>
            public Control Master
            {
                get { return master; }
                set { master = value; }
            }

            private Control master;


            /// <summary>
            /// Overrides <see cref="HtmlControl.RenderAttributes"/>
            /// </summary>
            protected override void RenderAttributes(HtmlTextWriter writer)
            {
                if (master != null)
                {
                    String originalOnClick = Attributes["onclick"];
                    if (originalOnClick != null)
                    {
                        Attributes.Remove("onclick");
                    }
                    else
                    {
                        originalOnClick = "";
                    }
                    Attributes["onclick"] = "MetaBuilders_RowSelectorColumn_CheckChildren( '" + Master.ClientID + "' ); " + originalOnClick;
                }

                if (AutoPostBack)
                {
                    String originalOnClick = Attributes["onclick"];
                    if (originalOnClick != null)
                    {
                        Attributes.Remove("onclick");
                    }
                    else
                    {
                        originalOnClick = "";
                    }
                    Attributes["onclick"] = originalOnClick + " " + Page.ClientScript.GetPostBackEventReference(this, null);
                }

                base.RenderAttributes(writer);
            }
        }

        /// <summary>
        /// The radiobutton which appears in each cell of a <see cref="RowSelectorColumn"/> when <see cref="RowSelectorColumn.SelectionMode"/> is set to <see cref="ListSelectionMode.Single"/>.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
        public class ParticipantRadioButton : HtmlInputRadioButton, IPostBackDataHandler
        {
            #region IPostBackDataHandler Implementation

            /// <summary>
            /// This doesn't differ from the original implementaion... 
            /// except now i'methodName using my own RenderednameAttribute instead of the InputControl implementation.
            /// </summary>
            bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
            {
                bool result = false;

                string postedValue = postCollection[RenderedNameAttribute];
                if (postedValue != null && postedValue == Value)
                {
                    if (Checked)
                    {
                        return result;
                    }
                    Checked = true;
                    result = true;
                }
                else if (Checked)
                {
                    Checked = false;
                }
                return result;
            }

            /// <summary>
            /// No change from the InputControl implementation
            /// </summary>
            void IPostBackDataHandler.RaisePostDataChangedEvent()
            {
                OnServerChange(EventArgs.Empty);
            }

            #endregion

            /// <summary>
            /// Gets or sets if the control will postback after being changed.
            /// </summary>
            public Boolean AutoPostBack
            {
                get
                {
                    Object savedState = ViewState["AutoPostBack"];
                    if (savedState != null)
                    {
                        return (Boolean) savedState;
                    }
                    return false;
                }
                set { ViewState["AutoPostBack"] = value; }
            }

            /// <summary>
            /// Overrides <see cref="HtmlInputRadioButton.RenderAttributes"/>.
            /// </summary>
            /// <remarks>Customized to use this implementation of RenderedNameAttribute</remarks>
            protected override void RenderAttributes(HtmlTextWriter writer)
            {
                writer.WriteAttribute("value", Value);
                Attributes.Remove("value");
                writer.WriteAttribute("name", RenderedNameAttribute);
                Attributes.Remove("name");
                if (ID != null)
                {
                    writer.WriteAttribute("id", ClientID);
                }
                if (AutoPostBack)
                {
                    String finalOnClick = Attributes["onclick"];
                    if (finalOnClick != null)
                    {
                        finalOnClick += " " + Page.ClientScript.GetPostBackEventReference(this, null);
                        Attributes.Remove("onclick");
                    }
                    else
                    {
                        finalOnClick = Page.ClientScript.GetPostBackEventReference(this, null);
                    }
                    writer.WriteAttribute("onclick", finalOnClick);
                }

                Attributes.Render(writer);
                writer.Write(" /");
            }


            /// <summary>
            /// Gets the final rendering of the Name attribute.
            /// </summary>
            /// <remarks>
            /// Differs from the standard RenderedNameAttribute to use the column as the logical naming container instead of the row.
            /// </remarks>
            [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Int32.ToString")]
            protected virtual String RenderedNameAttribute
            {
                get
                {
                    DiscoverContainers();
                    if (parentCell == null || parentGridViewItem == null || parentGridView == null)
                    {
                        return Name;
                    }
                    else
                    {
                        StringBuilder groupContainer = new StringBuilder();
                        groupContainer.Append(parentGridViewItem.UniqueID);
                        groupContainer.Append(":Column");
                        groupContainer.Append(parentGridViewItem.Cells.GetCellIndex(parentCell).ToString());
                        groupContainer.Append(":");
                        groupContainer.Append(Name);

                        return groupContainer.ToString();
                    }
                }
            }

            private TableCell parentCell;
            private GridViewRow parentGridViewItem;
            private GridView parentGridView;

            /// <summary>
            /// Looks up the control heirarchy to discover the container controls for this radiobutton
            /// </summary>
            [SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
            protected virtual void DiscoverContainers()
            {
                if (parentCell == null || parentGridViewItem == null || parentGridView == null)
                {
                    Control tempControl = NamingContainer;
                    if (tempControl is GridViewRow)
                    {
                        parentGridViewItem = (GridViewRow) tempControl;
                        parentGridView = (GridView) parentGridViewItem.NamingContainer;
                    }

                    tempControl = Parent;
                    while (tempControl.Parent.UniqueID != parentGridViewItem.UniqueID)
                    {
                        tempControl = tempControl.Parent;
                    }
                    if (tempControl is TableCell)
                    {
                        parentCell = (TableCell) tempControl;
                    }
                }
            }
        }

        #endregion
    }
}