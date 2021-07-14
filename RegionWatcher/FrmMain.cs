using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace RegionWatcher
{
    public partial class FrmMain : Form
    {
        private static readonly string splitter = ",";
        private static readonly SoundPlayer soundPlayer = new(Environment.GetFolderPath(Environment.SpecialFolder.Windows) + "\\Media\\tada.wav");
        private static readonly List<FrmMain> formsList = new();
        private static readonly TimeSpan alarmSuppressInterval = TimeSpan.FromSeconds(1.5);
        private static DateTime suppressAlarmUntil = DateTime.MinValue;
        private static bool suppressExit = false;
        private static bool initializing = true;
        private static bool muteApplication = false;

        private readonly BitmapBuffer[] buffers = new BitmapBuffer[2];

        public FrmMain()
        {
            AllocateBuffers();
            InitializeComponent();
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            DisposeBuffers();

            formsList.Remove(this);

            if (!suppressExit && e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit();
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg != NativeMethods.WM_NCHITTEST)
            {
                base.WndProc(ref m);

                return;
            }

            Point pScreen = Helpers.Int32ToPoint(m.LParam.ToInt32());
            Point pClient = PointToClient(pScreen);

            if (pClient.X >= ClientSize.Width - 16 && pClient.Y >= ClientSize.Height - 16)
            {
                m.Result = (IntPtr)NativeMethods.HTBOTTOMRIGHT;
            }
            else
            {
                m.Result = (IntPtr)NativeMethods.HTCLIENT;
            }
        }

        public void BufferSizeChanged(object sender, EventArgs args)
        {
            DisposeBuffers();
            AllocateBuffers();

            StoreSizeAndLocation();
        }

        private void AllocateBuffers()
        {
            for (int i = 0; i < buffers.Length; i++)
                buffers[i] = new BitmapBuffer(DesktopBounds.Width, DesktopBounds.Height);
        }

        private void DisposeBuffers()
        {
            foreach (BitmapBuffer buffer in buffers)
                buffer.Dispose();
        }

        private bool FlipBufferHasSameData()
        {
            buffers[0].CopyFromScreen(DesktopLocation);

            Array.Reverse(buffers); // flip buffers

            return buffers[0].HasSameDataAs(buffers[1]);
        }

        private static void StoreSizeAndLocation()
        {
            if (initializing)
                return;

            Settings.Default.Locations = string.Join(splitter, formsList.Select(f => Helpers.PointToInt32(f.Location).ToString(System.Globalization.CultureInfo.InvariantCulture)));
            Settings.Default.Sizes = string.Join(splitter, formsList.Select(f => Helpers.SizeToInt32(f.Size).ToString(System.Globalization.CultureInfo.InvariantCulture)));

            Settings.Default.Save();
        }

        private void CheckDiffs(object sender, EventArgs args)
        {
            if (muteApplication || DateTime.UtcNow < suppressAlarmUntil)
                return;

            if (formsList.All(x => x.FlipBufferHasSameData()))
                return;

            suppressAlarmUntil = DateTime.UtcNow + alarmSuppressInterval;

            ThreadPool.QueueUserWorkItem(_ => soundPlayer.Play());
        }

        private void ContextMenuStrip_Opening(object sender, CancelEventArgs e) =>
            tsmiRemoveThis.Enabled = formsList.Count > 1;

        private void TsmiRemoveThis_Click(object sender, EventArgs e)
        {
            if (formsList[0] == this)
            {
                // if closing main form - copy the last form and close it instead
                FrmMain frmMain = formsList[^1];
                Location = frmMain.Location;
                Size = frmMain.Size;
                suppressExit = true;
                frmMain.Close();
                suppressExit = false;
            }
            else
            {
                suppressExit = true;
                Close();
                suppressExit = false;
            }

            StoreSizeAndLocation();
        }

        private void TsmiAddRegion_Click(object sender, EventArgs e)
        {
            formsList.Add(new FrmMain());
            formsList[^1].Show();

            StoreSizeAndLocation();
        }

        private static void VerifyPositions(ref Point[] locations, ref Size[] sizes)
        {
            for (int i = 0; i < locations.Length; i++)
            {
                Rectangle rect = new(locations[i], sizes[i]);

                if (!Screen.GetBounds(rect).Contains(rect))
                {
                    locations[i].X = 100 + 100 * i;
                    locations[i].Y = 200 + 100 * i;
                    sizes[i].Width = 300;
                    sizes[i].Height = 200;
                }
            }
        }

        private void FrmMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) // allow left click drag to move windows
                return;

            NativeMethods.ReleaseCapture();
            _ = NativeMethods.SendMessage(this.Handle, NativeMethods.WM_NCLBUTTONDOWN, NativeMethods.HTCAPTION, 0);
        }

        [STAThread]
        private static void Main()
        {
            Application.VisualStyleState = VisualStyleState.ClientAreaEnabled;
            Application.SetCompatibleTextRenderingDefault(false);

            if (!string.IsNullOrWhiteSpace(Settings.Default.Locations))
            {
                Point[] locations = Settings.Default.Locations.Split(splitter[0])
                    .Select(s => int.Parse(s, System.Globalization.CultureInfo.InvariantCulture))
                    .Select(i => Helpers.Int32ToPoint(i))
                    .ToArray();

                Size[] sizes = Settings.Default.Sizes.Split(splitter[0])
                    .Select(s => int.Parse(s, System.Globalization.CultureInfo.InvariantCulture))
                    .Select(i => Helpers.Int32ToSize(i))
                    .ToArray();

                VerifyPositions(ref locations, ref sizes);

                for (int i = 0; i < locations.Length; i++)
                {
                    formsList.Add(new FrmMain());

                    formsList[i].Show();
                    formsList[i].Location = locations[i];
                    formsList[i].Size = sizes[i];

                    if (i == 0)
                    {
                        formsList[0].timerCheck.Enabled = true;
                        formsList[0].notifyIcon.Icon = SystemIcons.Asterisk;
                        formsList[0].notifyIcon.Visible = true;
                    }
                }
            }
            else
            {
                formsList.Add(new FrmMain());
            }

            initializing = false;
            Application.Run(formsList[0]);
        }

        private void TsmiApplicationExit_Click(object sender, EventArgs e) =>
            Application.Exit();

        private void TsmiApplicationMute_Click(object sender, EventArgs e)
        {
            muteApplication = !muteApplication;
            tsmiWindowMute.Checked = muteApplication;
            tsmiTrayMute.Checked = muteApplication;
        }

        // setup menu items to set opacity, from 0% to 100% in 10% increments
        private static void SetupDimMenu(ToolStripMenuItem t)
        {
            if (t.HasDropDownItems)
                return;

            for (int i = 0; i <= 100; i += 10)
            {
                ToolStripMenuItem tsmi = new();
                float opacity = i / 100f;
                tsmi.Text = i + "%";
                tsmi.Click += (s, e) =>
                {
                    foreach (FrmMain f in formsList)
                    {
                        f.Opacity = opacity;
                    }
                };
                t.DropDownItems.Add(tsmi);
            }
        }

        private void FrmMain_Shown(object sender, EventArgs e)
        {
            SetupDimMenu(tsmiContextDim);
            SetupDimMenu(tsmiTrayDim);
        }
    }
}
