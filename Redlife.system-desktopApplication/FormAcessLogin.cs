using Krypton.Toolkit;
using System;
using System.Data.SQLite;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static Redlife.system_desktopApplication.Form1;

namespace Redlife.system_desktopApplication
{
    public partial class FormAcessLogin : Form
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

        public bool LoginSucesso { get; private set; } = false;

        public FormAcessLogin()
        {
            InitializeComponent();
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.AutoScaleMode = AutoScaleMode.Font;
            this.FormBorderStyle = FormBorderStyle.None;
            Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 12, 12));
        }

        private void BtnExit1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextboxUser.Text) ||
                string.IsNullOrWhiteSpace(TextboxPassword.Text))
            {
                var telaAviso = new SearchWindon01();
                telaAviso.label2.Text = "Usuario ou senha vazios!";
                telaAviso.Searchtextbox1.Visible = false;
                telaAviso.button1.Visible = false;
                telaAviso.button2.Text = "OK";
                telaAviso.button2.TextAlign = ContentAlignment.MiddleCenter;
                telaAviso.button2.ImageIndex = -1;
                ModalHelper.AbrirModal(this, telaAviso);
                TextboxUser.Clear();
                TextboxPassword.Clear();
                TextboxUser.Focus();
                return;
            }

            try
            {
                using (SQLiteConnection conn = Conexao.Abrir())
                {
                    string sql = @"SELECT id, NomeCivil, NomeUser, senha, Status, Cargo 
                                   FROM [User] 
                                   WHERE NomeUser = @user";

                    SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@user", TextboxUser.Text);

                    SQLiteDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        string senhaBanco = reader["senha"].ToString();
                        string status = reader["Status"].ToString();

                        
                        if (TextboxPassword.Text == senhaBanco)
                        {
                            
                            if (status == "Ativo")
                            {
                                
                                SessaoUsuario.Id = Convert.ToInt32(reader["id"]);
                                SessaoUsuario.Nome = reader["NomeCivil"].ToString();
                                SessaoUsuario.Usuario = reader["NomeUser"].ToString();
                                SessaoUsuario.Cargo = reader["Cargo"].ToString();

                                LogAcesso.Registrar(SessaoUsuario.Usuario, "login");

                                LoginSucesso = true;
                                this.Close();
                            }
                            else
                            {
                                
                                var telaAviso = new SearchWindon01();
                                telaAviso.label2.Text = "Usuario Desligado - Sem acesso.";
                                telaAviso.Searchtextbox1.Visible = false;
                                telaAviso.button1.Visible = false;
                                telaAviso.button2.Text = "OK";
                                telaAviso.button2.TextAlign = ContentAlignment.MiddleCenter;
                                telaAviso.button2.ImageIndex = -1;
                                ModalHelper.AbrirModal(this, telaAviso);


                            }
                        }
                        else
                        {
                            var telaAviso = new SearchWindon01();
                            telaAviso.label2.Text = "Senha Incorreta!";
                            telaAviso.Searchtextbox1.Visible = false;
                            telaAviso.button1.Visible = false;
                            telaAviso.button2.Text = "OK";
                            telaAviso.button2.TextAlign = ContentAlignment.MiddleCenter;
                            telaAviso.button2.ImageIndex = -1;
                            ModalHelper.AbrirModal(this, telaAviso);
                            TextboxPassword.Clear();
                            TextboxPassword.Focus();
                        }
                    }
                    else
                    {
                        var telaAviso = new SearchWindon01();
                        telaAviso.label2.Text = "Usuario não encontrado!";
                        telaAviso.Searchtextbox1.Visible = false;
                        telaAviso.button1.Visible = false;
                        telaAviso.button2.Text = "OK";
                        telaAviso.button2.TextAlign = ContentAlignment.MiddleCenter;
                        telaAviso.button2.ImageIndex = -1;
                        ModalHelper.AbrirModal(this, telaAviso);
                        TextboxUser.Clear();
                        TextboxPassword.Clear();
                        TextboxUser.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao acessar o banco: " + ex.Message);
            }
        }

        private void TextboxUser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextboxPassword.Focus();
                
            }
        }

        private void TextboxPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                kryptonButton1.PerformClick();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
          
            var telaEmail = new SearchWindon01();
            telaEmail.Searchtextbox1.Visible = false;
            telaEmail.label2.Text = "deseja receber e-mail de recuperação?";


            ModalHelper.AbrirModal(this, telaEmail);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void FormAcessLogin_Load(object sender, EventArgs e)
        {
            guna2ShadowForm1.SetShadowForm(this);
            guna2ShadowForm1.ShadowColor = Color.Black;
            guna2ShadowForm1.BorderRadius = 20;
            
        }

        private void FormAcessLogin_Shown(object sender, EventArgs e)
        {
           
        }
    }
}