using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Adf.Base.Domain;
using Adf.Core.Domain;
using Adf.Core.Identity;
using Adf.Core.Objects;
using Adf.Core.Resources;
using Adf.Core.State;

namespace Adf.Web.UI
{
    public class DropDownTreeView : WebControl
    {
        public bool AutoImage = true;
        public bool ExpandToSelectedNode = true;
        protected readonly string _imageFormat;
        protected readonly string _defaultText;
        protected readonly string _emptyText;

        public string SelectedValue { get { return _button.CommandArgument; }}

        public event CommandEventHandler ItemSelected;

        public void OnItemSelected(CommandEventArgs e)
        {
            var handler = ItemSelected;

            if (handler != null) handler(this, e);
        }

        protected LinkButton _button;
        protected TreeView _tree;
        protected Panel _panel;
        protected string _type;

        public DropDownTreeView()
        {
            base.CssClass = "AdfTreeView";
            
            _imageFormat = StateManager.Settings["DropDownTreeView.ImageFormat"].ToString();
            _defaultText = ResourceManager.GetString(StateManager.Settings["DropDownTreeView.DefaultText"].ToString());
            _emptyText = ResourceManager.GetString(StateManager.Settings["DropDownTreeView.EmptyText"].ToString());
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            var cssclass = String.Format("{0}", CssClass);

            _button = new LinkButton {Enabled = true, CssClass = string.Format("{0}{1}", cssclass, "Button"), Width = Width };
            _tree = new TreeView { Visible = true, Enabled = true, CssClass = cssclass, ShowLines = true, Width = Width, NodeWrap = true };
            _panel = new Panel { Visible = false, CssClass = string.Format("{0}{1}", cssclass, "Panel"), Width = Width };

            _tree.SelectedNodeChanged += TreeOnSelectedNodeChanged;
            _tree.SelectedNodeStyle.CssClass = string.Format("{0}{1}", cssclass, "SelectedNode");
            _tree.NodeStyle.CssClass = string.Format("{0}{1}", cssclass, "Node");

            _button.Click += ButtonOnClick;

            _panel.Controls.Add(_tree);

            Controls.Add(_button);
            Controls.Add(_panel);

            Visible = Enabled = AutoImage = true;
        }

        private void ButtonOnClick(object sender, EventArgs eventArgs)
        {
            _panel.Visible = !_panel.Visible;
        }

        private void TreeOnSelectedNodeChanged(object sender, EventArgs eventArgs)
        {
            var tree = sender as TreeView;
            if (tree == null) return;

            _panel.Visible = false;
            _button.Text = tree.SelectedNode.Text;
            _button.CommandArgument = tree.SelectedNode.Value;

            OnItemSelected(new CommandEventArgs("ItemSelected", tree.SelectedValue));
        }

        private static bool IsRoot(IDomainHierarchy target, ICollection<IDomainHierarchy> items)
        {
            if (target.IsNullOrEmpty()) return false;

            var parent = target.GetParent();

            if (parent.IsNullOrEmpty()) return true;
            return !items.Contains(parent);
        }

        public void Bind(ICollection<IDomainHierarchy> items, IDomainHierarchy target, bool addEmpty = false)
        {
            if (items == null) throw new ArgumentNullException("items");

            _type = items.First().GetType().Name;
            _button.Text = target.IsNullOrEmpty() ? string.Format(_defaultText, _type) : target.Title;
            _button.CommandArgument = target.IsNullOrEmpty() ? string.Empty : target.Id.ToString();

            AddEmpty(_tree, target, addEmpty);

            using (new ObjectScope<IDomainHierarchy>(items.ToList()))
            {
                var roots = items.Where(i => IsRoot(i, items));

                foreach (var root in roots) Bind(_tree, root, target);
            }
        }

        private void AddEmpty(TreeView tree, IDomainHierarchy target, bool addEmpty)
        {
            if (!addEmpty) return;

            var node = new TreeNode {Text = string.Format(_emptyText, _type), Value = IdManager.Empty().ToString(), Selected = false};

            SetImage(node, target, true);

            tree.Nodes.Add(node);
        }

        private void SetImage(TreeNode node, IDomainHierarchy item, bool usedefault = false)
        {
            if (!AutoImage) return;

            var imageprovider = item as IImageProvider;

            node.ImageUrl = (imageprovider == null) ? string.Format(_imageFormat, _type) : usedefault ? imageprovider.DefaultImageUrl : imageprovider.ImageUrl;
        }

        private void Bind(TreeView tree, IDomainHierarchy root, IDomainHierarchy target)
        {
            var node = new TreeNode {Text = root.Title, Value = root.Id.ToString(), Selected = root.Equals(target)};

            SetImage(node, root);

            tree.Nodes.Add(node);

            foreach (var child in root.GetChildren()) Bind(node, child as IDomainHierarchy, target);
        }

        private void Bind(TreeNode parent, IDomainHierarchy item, IDomainHierarchy target)
        {
            var node = new TreeNode {Text = item.Title, Value = item.Id.ToString(), Selected = item.Equals(target)};

            SetImage(node, item);

            parent.ChildNodes.Add(node);

            if (node.Selected && ExpandToSelectedNode)
            {
                ExpandUp(node);
            }
            
            foreach (var child in item.GetChildren()) Bind(node, child as IDomainHierarchy, target);
        }

        public TreeNode GetRootNode(TreeNode current)
        {
            return current.Parent == null ? current : GetRootNode(current.Parent);
        }

        public TreeNode GetRootNode()
        {
            return GetRootNode(_tree.SelectedNode);
        }

        private static void ExpandUp(TreeNode node)
        {
            if (node.Parent == null) return;

            node.Parent.Expand();

            ExpandUp(node.Parent);
        }

        public void Clear()
        {
            _tree.Nodes.Clear();
        }
    }
}
