using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Redlife.system_desktopApplication
{
    public partial class FormLoading : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     
            int nTopRect,     
            int nRightRect,    
            int nBottomRect,   
            int nWidthEllipse, 
            int nHeightEllipse 
        );
        public FormLoading()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        private void FormLoading_Shown(object sender, EventArgs e)
        {
            this.Refresh();
            ProgressBar1.Minimum = 0;
            ProgressBar1.Maximum = 100;
            for (int i = 1; i < 100; i++) { 
                ProgressBar1.Value = i;
                Application.DoEvents();
                Thread.Sleep(9);

            
            }
            Thread.Sleep(95);
            this.Close();
        }

        private void kryptonPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
