using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Windows.Input;
using System.Diagnostics.Eventing.Reader;


namespace Redlife.system_desktopApplication
{
    public partial class SearchWindon01 : Form
    {
        public static class FormExtensions
        {
            
        }
        public bool ConfirmadoCancelCREateUser { get; private set; }
        public bool ModoConfirmacaoCancelUser { get; set; } = false;

        public SearchWindon01()
        {
            InitializeComponent();
        }

        private void SearchWindon01_Load(object sender, EventArgs e)
        {
            guna2ShadowForm1.SetShadowForm(this);
            guna2ShadowForm1.ShadowColor = Color.Black;
            guna2ShadowForm1.BorderRadius = 20;
            Searchtextbox1.Focus();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            if (Searchtextbox1.Text == "")
            {
                Searchtextbox1.BackColor = Color.LightYellow;

            }else {
                AppState.Valorpesquisa1 = Searchtextbox1.Text;
                 this.Close();
                 
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void kryptonPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

       
        private void Searchtextbox1_TextChanged(object sender, EventArgs e)
        {
            Searchtextbox1.BackColor = Color.White;
        }

        private void Searchtextbox1_Enter(object sender, EventArgs e)
        {
            
        }

        private void Searchtextbox1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                button1.PerformClick();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (ModoConfirmacaoCancelUser)
            {
                ConfirmadoCancelCREateUser = true;
                this.Close();
                return;
            }

           
            if (Searchtextbox1.Text == "")
            {
                Searchtextbox1.BackColor = Color.LightYellow;
                return;
            }

            AppState.Valorpesquisa1 = Searchtextbox1.Text;
            AppState.PesquisarDB = true;
            this.Close();
            


        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            ConfirmadoCancelCREateUser = false;
            CadastroFuncionarios_User cadastroFuncionarios = new CadastroFuncionarios_User();
            cadastroFuncionarios.Opacity = 100;
            AppState.PesquisarDB = false;
            this.Close();
        }

        private void SearchWindon01_Shown(object sender, EventArgs e)
        {
            Searchtextbox1.Focus();
            
        }

        private void kryptonPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel14_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
