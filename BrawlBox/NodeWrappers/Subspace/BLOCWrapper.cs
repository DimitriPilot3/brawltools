﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrawlLib.SSBB.ResourceNodes;
using System.Windows.Forms;
using System.ComponentModel;
using BrawlLib.SSBBTypes;
using BrawlLib;

namespace BrawlBox
{
    [NodeWrapper(ResourceType.BLOC)]
    class BLOCWrapper : GenericWrapper
    {
        #region Menu

        private static ContextMenuStrip _menu;
        static BLOCWrapper()
        {
            _menu = new ContextMenuStrip();
            _menu.Items.Add(new ToolStripMenuItem("Ne&w", null,
            new ToolStripMenuItem("GSND Archive", null, NewGSNDAction)));

            _menu.Items.Add(new ToolStripMenuItem("&Export", null, ExportAction, Keys.Control | Keys.E));
            _menu.Items.Add(new ToolStripMenuItem("&Replace", null, ReplaceAction, Keys.Control | Keys.R));
            _menu.Items.Add(new ToolStripMenuItem("Res&tore", null, RestoreAction, Keys.Control | Keys.T));
            _menu.Items.Add(new ToolStripSeparator());
            _menu.Items.Add(new ToolStripMenuItem("Move &Up", null, MoveUpAction, Keys.Control | Keys.Up));
            _menu.Items.Add(new ToolStripMenuItem("Move D&own", null, MoveDownAction, Keys.Control | Keys.Down));
            _menu.Items.Add(new ToolStripMenuItem("Re&name", null, RenameAction, Keys.Control | Keys.N));
            _menu.Items.Add(new ToolStripSeparator());
            _menu.Items.Add(new ToolStripMenuItem("&Delete", null, DeleteAction, Keys.Control | Keys.Delete));
            _menu.Opening += MenuOpening;
            _menu.Closing += MenuClosing;
        }
        protected static void NewGSNDAction(object sender, EventArgs e) { GetInstance<BLOCWrapper>().NewGSND(); }
        private static void MenuClosing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            _menu.Items[8].Enabled = true;
        }
        private static void MenuOpening(object sender, CancelEventArgs e)
        {
            BLOCWrapper w = GetInstance<BLOCWrapper>();

            _menu.Items[8].Enabled = w.Parent != null;
        }
        #endregion

        public override string ExportFilter { get { return "BLOC Adventure Archive (*.BLOC)|*.bloc"; } }

        public BLOCWrapper() { ContextMenuStrip = _menu; }
        public GSNDNode NewGSND()
        {
            GSNDNode node = new GSNDNode() { Name = _resource.FindName("NewGSND")};
            _resource.AddChild(node);

            BaseWrapper w = this.FindResource(node, false);
            w.EnsureVisible();
            w.TreeView.SelectedNode = w;
            return node;
        }

        public override void OnExport(string outPath, int filterIndex)
        { 
            ((BLOCNode)_resource).Export(outPath);
        }
    }
}