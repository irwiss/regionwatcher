using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace RegionWatcher
{
    public partial class FrmMain : Form
    {
        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
            {
                this.components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.cmsWindowMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiAddRegion = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRemoveThis = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiContextDim = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiWindowMute = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiWindowExit = new System.Windows.Forms.ToolStripMenuItem();
            this.timerCheck = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.cmsTrayMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiTrayDim = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTrayMute = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiTrayExit = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsWindowMenu.SuspendLayout();
            this.cmsTrayMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmsWindowMenu
            // 
            this.cmsWindowMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsWindowMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAddRegion,
            this.tsmiRemoveThis,
            this.tsmiContextDim,
            this.toolStripSeparator1,
            this.tsmiWindowMute,
            this.toolStripSeparator2,
            this.tsmiWindowExit});
            this.cmsWindowMenu.Name = "contextMenuStrip";
            this.cmsWindowMenu.Size = new System.Drawing.Size(207, 136);
            this.cmsWindowMenu.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuStrip_Opening);
            // 
            // tsmiAddRegion
            // 
            this.tsmiAddRegion.Name = "tsmiAddRegion";
            this.tsmiAddRegion.Size = new System.Drawing.Size(206, 24);
            this.tsmiAddRegion.Text = "Add a region";
            this.tsmiAddRegion.Click += new System.EventHandler(this.TsmiAddRegion_Click);
            // 
            // tsmiRemoveThis
            // 
            this.tsmiRemoveThis.Name = "tsmiRemoveThis";
            this.tsmiRemoveThis.Size = new System.Drawing.Size(206, 24);
            this.tsmiRemoveThis.Text = "Remove this region";
            this.tsmiRemoveThis.Click += new System.EventHandler(this.TsmiRemoveThis_Click);
            // 
            // tsmiContextDim
            // 
            this.tsmiContextDim.Name = "tsmiContextDim";
            this.tsmiContextDim.Size = new System.Drawing.Size(206, 24);
            this.tsmiContextDim.Text = "Dim to >";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(203, 6);
            // 
            // tsmiWindowMute
            // 
            this.tsmiWindowMute.Name = "tsmiWindowMute";
            this.tsmiWindowMute.Size = new System.Drawing.Size(206, 24);
            this.tsmiWindowMute.Text = "Mute";
            this.tsmiWindowMute.Click += new System.EventHandler(this.TsmiApplicationMute_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(203, 6);
            // 
            // tsmiWindowExit
            // 
            this.tsmiWindowExit.Name = "tsmiWindowExit";
            this.tsmiWindowExit.Size = new System.Drawing.Size(206, 24);
            this.tsmiWindowExit.Text = "Exit";
            this.tsmiWindowExit.Click += new System.EventHandler(this.TsmiApplicationExit_Click);
            // 
            // timerCheck
            // 
            this.timerCheck.Interval = 500;
            this.timerCheck.Tick += new System.EventHandler(this.CheckDiffs);
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.cmsTrayMenu;
            this.notifyIcon.Text = "RegionWatcher";
            // 
            // cmsTrayMenu
            // 
            this.cmsTrayMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsTrayMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiTrayDim,
            this.tsmiTrayMute,
            this.toolStripSeparator3,
            this.tsmiTrayExit});
            this.cmsTrayMenu.Name = "cmsTrayMenu";
            this.cmsTrayMenu.Size = new System.Drawing.Size(139, 82);
            // 
            // tsmiTrayDim
            // 
            this.tsmiTrayDim.Name = "tsmiTrayDim";
            this.tsmiTrayDim.Size = new System.Drawing.Size(138, 24);
            this.tsmiTrayDim.Text = "Dim to >";
            // 
            // tsmiTrayMute
            // 
            this.tsmiTrayMute.Name = "tsmiTrayMute";
            this.tsmiTrayMute.Size = new System.Drawing.Size(138, 24);
            this.tsmiTrayMute.Text = "Mute";
            this.tsmiTrayMute.Click += new System.EventHandler(this.TsmiApplicationMute_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(135, 6);
            // 
            // tsmiTrayExit
            // 
            this.tsmiTrayExit.Name = "tsmiTrayExit";
            this.tsmiTrayExit.Size = new System.Drawing.Size(138, 24);
            this.tsmiTrayExit.Text = "Exit";
            this.tsmiTrayExit.Click += new System.EventHandler(this.TsmiApplicationExit_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(400, 400);
            this.ContextMenuStrip = this.cmsWindowMenu;
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(16, 16);
            this.Name = "FrmMain";
            this.Opacity = 0.25D;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Shown += new System.EventHandler(this.FrmMain_Shown);
            this.LocationChanged += new System.EventHandler(this.BufferSizeChanged);
            this.SizeChanged += new System.EventHandler(this.BufferSizeChanged);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMain_MouseDown);
            this.cmsWindowMenu.ResumeLayout(false);
            this.cmsTrayMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private IContainer components = null;
        private ContextMenuStrip cmsWindowMenu;
        private ToolStripMenuItem tsmiAddRegion;
        private ToolStripMenuItem tsmiRemoveThis;
        private Timer timerCheck;
        private NotifyIcon notifyIcon;
        private ToolStripMenuItem tsmiContextDim;
        private ContextMenuStrip cmsTrayMenu;
        private ToolStripMenuItem tsmiTrayExit;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem tsmiWindowExit;
        private ToolStripMenuItem tsmiWindowMute;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem tsmiTrayMute;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem tsmiTrayDim;
    }
}
