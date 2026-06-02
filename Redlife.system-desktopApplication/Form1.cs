using Krypton.Navigator;
using Redlife.system_desktopApplication.MessagesPanels;
using Redlife.system_desktopApplication.t.i;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static Redlife.system_desktopApplication.EscolhaDeEstoqueAoAcacao;

namespace Redlife.system_desktopApplication
{

    public partial class Form1 : Form
    {
        public Form1()
        {

            InitializeComponent();
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.AutoScaleMode = AutoScaleMode.Font;
            MessageFormGenericButtons messageFormGenericButtons = new MessageFormGenericButtons();

        }

        public string telaExistente;

        private void Form1_Load(object sender, EventArgs e)

        {
            toolTip1.SetToolTip(LogoutBtnPic, "Clique aqui para fazer logout");
            


            
        }



        public class Conexao
        {
            private static string connectionString = "Data Source=DataBase.db;Version=3;BusyTimeout=5000;Journal Mode=WAL;";

            public static SQLiteConnection Abrir()
            {
                var conn = new SQLiteConnection(connectionString);
                conn.Open();
                return conn;
            }
        }
        

        public void AbrirNoPanel(Form tela)
        {
           foreach(Control ctrl in panelMdi1.Controls)
            {
                if(ctrl.GetType() == tela.GetType())
                {
                    ctrl.BringToFront();
                    return;
                }
            }

            tela.TopLevel = false;
            tela.Dock = DockStyle.Fill;

            panelMdi1.Controls.Add(tela);
            tela.Show();
        }

        public void acaotelaLoginShown()
        {
            using (var login = new FormAcessLogin())
            {
                

                login.ShowDialog();

                if (login.LoginSucesso)
                {
                    kryptonPanel1.Visible = false;
                    var loading = new FormLoading();
                    ModalHelper.AbrirModal(this, loading);


                    


                }
                else
                {
                    Application.Exit();
                }
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {

            acaotelaLoginShown();



        }
        
        
        private void cadastroDeFuncionáriosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            

            AbrirNoPanel(new CadastroFuncionarios_User());
        }

        private void materiaisComunsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirNoPanel(new TelaEstoqueMaterialComun());
        }

        private void folowUPSolicitaçõesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirNoPanel(new Follow_Up_solicitts());
        }

        private void cadastrarItensNoEstoqueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var telaAviso = new EscolhaDeEstoqueAoAcacao();
            ModalHelper.AbrirModal(this, telaAviso);
        }

        public void retornoResposta()
        {
            if(EstadoSistema.AbrirEstoque == true)
            {
                

                AbrirNoPanel(new CasdatroMaterialcomumNoEstoquegeral01());
                EstadoSistema.AbrirEstoque = false;
            }
        }

        private void configuraçõesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void cadastroDePacientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirNoPanel(new CadastroDeDoadores());
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {
             
        }

        private void LogoutBtnPic_Click(object sender, EventArgs e)
        {


            using (var telaLogout = new MessageFormGenericButtons())
            {
               
                ModalHelper.AbrirModal(this, telaLogout);

                
                if (telaLogout.RealizarLogout == true)
                {
                   
                    LogAcesso.Registrar(SessaoUsuario.Usuario, "Saida");

                    
                    var telasAbertas = panelMdi1.Controls.OfType<Form>().ToList();
                    foreach (Form tela in telasAbertas)
                    {
                        tela.Close();
                        tela.Dispose();
                    }

                   
                    panelMdi1.Controls.Clear();
                    kryptonPanel1.Visible = true;
                    acaotelaLoginShown();
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        
        private void acessosDiáriosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            var acessDiario = new AcessosEfetuadosDiariamente();
            ModalHelper.AbrirModal(this, acessDiario);
        }

        private void painelDeControleTIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirNoPanel(new PanelControlTI());
        }

        private void aberturaDeChamadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirNoPanel(new AberturaDeChamadosTi());
        }
    }
    }

    
  

