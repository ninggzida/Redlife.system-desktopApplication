using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using static Redlife.system_desktopApplication.Form1;


namespace Redlife.system_desktopApplication
{
    
    public partial class CadastroFuncionarios_User : Form
    {
        FormLoading formLoading1 = new FormLoading();
        bool entradaIo = false;
        bool CriandoUser;
        bool UserExist;
        bool UserEdit;
        int IdSelecionado = 0;

        public CadastroFuncionarios_User()
        {
            InitializeComponent();
            this.AutoScaleMode = AutoScaleMode.Font;
            Form1 form1 = new Form1();
            
        }

        private void CadastroFuncionarios_User_Load(object sender, EventArgs e)
        {
           
            
            dataGridViewUsuarios.ReadOnly = true;
            dataGridViewUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewUsuarios.AllowUserToAddRows = false;
            dataGridViewUsuarios.AllowUserToResizeRows = false;

            
            dataGridViewUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

          
            dataGridViewUsuarios.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
            dataGridViewUsuarios.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
            dataGridViewUsuarios.ForeColor = Color.DimGray;

            
            dataGridViewUsuarios.RowTemplate.Height = 35;

           
            dataGridViewUsuarios.EnableHeadersVisualStyles = false;
            dataGridViewUsuarios.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 30, 30);
            dataGridViewUsuarios.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            
            dataGridViewUsuarios.BackgroundColor = Color.Gainsboro;
        }

        private void CadastroFuncionarios_User_Shown(object sender, EventArgs e)
        {
            dataGridViewUsuarios.ClearSelection();
            AtivarCampos(this, false);
            btnEditUser1.Enabled = false;
        }

        private void AtivarCampos(Control parent, bool ativo)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is TextBox || ctrl is ComboBox)
                    ctrl.Enabled = ativo;

                if (ctrl.HasChildren)
                    AtivarCampos(ctrl, ativo);
            }
        }

        private void btnBusca1_Click(object sender, EventArgs e)
        {
            ModalHelper.AbrirModal(this, new SearchWindon01());

            if (AppState.PesquisarDB &&
                !string.IsNullOrWhiteSpace(AppState.Valorpesquisa1))
            {
                BuscarData();
            }
        }

        public void btnNovoUser1_Click(object sender, EventArgs e)
        {
            CriandoUser = true;
            UserEdit = false;

            AtivarCampos(this, true);

            btnDeleteUser.Enabled = false;
            btnEditUser1.Enabled = false;
            btnBusca1.Enabled = false;
            btnNovoUser1.Enabled = false;
            btnClear1.Enabled = false;
        }

        private void btnEditUser1_Click(object sender, EventArgs e)
        {
            UserEdit = true;
            CriandoUser = false;

            AtivarCampos(this, true);
        }

        private void btnClear1_Click(object sender, EventArgs e)
        {
            TextBoxUsuario1.Clear();
            TextBoxNomecivil1.Clear();
            textBoxEndereco1.Clear();
            TextBoxCpf1.Clear();
            textBoxRg1.Clear();
            TextBoxMatricula1.Clear();
            TextBoxContato1.Clear();
            textBoxEmailCorp1.Clear();
            TextBoxSenha1.Clear();
            comboBoxAcess1.ResetText();
            comboBoxStatus1.ResetText();

            dataGridViewUsuarios.DataSource = null;

            btnEditUser1.Enabled = false;
        }

        private void btnCancel1_Click(object sender, EventArgs e)
        {
            if (CriandoUser)
            {
                var aviso = new SearchWindon01();

                aviso.ModoConfirmacaoCancelUser = true;
                aviso.label2.Text = "Deseja cancelar sem salvar?";
                aviso.button1.Text = "Sim";
                aviso.button2.Text = "Não";
                aviso.Searchtextbox1.Visible = false;

                ModalHelper.AbrirModal(this, aviso);

                if (!aviso.ConfirmadoCancelCREateUser)
                    return;

                LimparCampos();
                CriandoUser = false;
            }

            AtivarCampos(this, false);

            btnDeleteUser.Enabled = true;
            btnBusca1.Enabled = true;
            btnNovoUser1.Enabled = true;
            btnClear1.Enabled = true;
        }

        private void LimparCampos()
        {
            TextBoxUsuario1.Clear();
            TextBoxNomecivil1.Clear();
            textBoxEndereco1.Clear();
            TextBoxCpf1.Clear();
            textBoxRg1.Clear();
            TextBoxMatricula1.Clear();
            TextBoxContato1.Clear();
            textBoxEmailCorp1.Clear();
            TextBoxSenha1.Clear();
            comboBoxAcess1.ResetText();
            comboBoxStatus1.ResetText();

            dataGridViewUsuarios.DataSource = null;
        }

        private void BuscarData()
        {
            using (var conn = Conexao.Abrir())
            {
                string query = @"SELECT id, NomeCivil, NomeUser, EmailCorp, endereco, Cpf, Rg, contato, senha, Status, Cargo 
                                 FROM [User] 
                                 WHERE NomeUser LIKE @user";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@user", "%" + AppState.Valorpesquisa1 + "%");

                    SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridViewUsuarios.DataSource = dt;

                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];
                        IdSelecionado = Convert.ToInt32(row["id"]);

                        TextBoxNomecivil1.Text = row["NomeCivil"].ToString();
                        TextBoxUsuario1.Text = row["NomeUser"].ToString();
                        textBoxEmailCorp1.Text = row["EmailCorp"].ToString();
                        textBoxEndereco1.Text = row["endereco"].ToString();
                        TextBoxCpf1.Text = row["Cpf"].ToString();
                        textBoxRg1.Text = row["Rg"].ToString();
                        TextBoxContato1.Text = row["contato"].ToString();
                        TextBoxSenha1.Text = row["senha"].ToString();
                        comboBoxAcess1.Text = row["Cargo"].ToString();
                        comboBoxStatus1.Text = row["Status"].ToString();

                        btnEditUser1.Enabled = true;
                        btnNovoUser1.Enabled = true;
                    }
                }
            }
        }

        private void Savedb()
        {
            using (var conn = Conexao.Abrir())
            {
                string query = @"INSERT INTO [User]
                (NomeCivil, NomeUser, EmailCorp, endereco, Cpf, Rg, contato, senha, Status, Cargo)
                VALUES
                (@nome, @user, @email, @endereco, @cpf, @rg, @contato, @senha, @status, @cargo)";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nome", TextBoxNomecivil1.Text);
                    cmd.Parameters.AddWithValue("@user", TextBoxUsuario1.Text);
                    cmd.Parameters.AddWithValue("@email", textBoxEmailCorp1.Text);
                    cmd.Parameters.AddWithValue("@endereco", textBoxEndereco1.Text);
                    cmd.Parameters.AddWithValue("@cpf", TextBoxCpf1.Text);
                    cmd.Parameters.AddWithValue("@rg", textBoxRg1.Text);
                    cmd.Parameters.AddWithValue("@contato", TextBoxContato1.Text);
                    cmd.Parameters.AddWithValue("@senha", TextBoxSenha1.Text);
                    cmd.Parameters.AddWithValue("@status", comboBoxStatus1.Text);
                    cmd.Parameters.AddWithValue("@cargo", comboBoxAcess1.Text);

                    cmd.ExecuteNonQuery();
                }
            }
            ModalHelper.AbrirModal(this, formLoading1);
            MostrarAviso("Usuário criado com sucesso!");
        }

        private void updateUser()
        {
            using (var conn = Conexao.Abrir())
            {
                string query = @"UPDATE [User] SET 
            NomeCivil = @nome,
            NomeUser = @user,
            EmailCorp = @email,
            endereco = @endereco,
            Cpf = @cpf,
            Rg = @rg,
            contato = @contato,
            senha = @senha,
            Status = @status,
            Cargo = @cargo
            WHERE id = @id";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", IdSelecionado); 
                    cmd.Parameters.AddWithValue("@nome", TextBoxNomecivil1.Text);
                    cmd.Parameters.AddWithValue("@user", TextBoxUsuario1.Text);
                    cmd.Parameters.AddWithValue("@email", textBoxEmailCorp1.Text);
                    cmd.Parameters.AddWithValue("@endereco", textBoxEndereco1.Text);
                    cmd.Parameters.AddWithValue("@cpf", TextBoxCpf1.Text);
                    cmd.Parameters.AddWithValue("@rg", textBoxRg1.Text);
                    cmd.Parameters.AddWithValue("@contato", TextBoxContato1.Text);
                    cmd.Parameters.AddWithValue("@senha", TextBoxSenha1.Text);
                    cmd.Parameters.AddWithValue("@status", comboBoxStatus1.Text);
                    cmd.Parameters.AddWithValue("@cargo", comboBoxAcess1.Text);

                    cmd.ExecuteNonQuery();
                }
            }
            

            MostrarAviso("Usuário atualizado com sucesso!");
            
        }

        private void btnSaveUser1_Click(object sender, EventArgs e)
        {
            if (TextBoxNomecivil1.Text == "" ||
                TextBoxUsuario1.Text == "" ||
                textBoxEmailCorp1.Text == "" ||
                textBoxEndereco1.Text == "" ||
                TextBoxCpf1.Text == "" ||
                textBoxRg1.Text == "" ||
                TextBoxContato1.Text == "" ||
                TextBoxSenha1.Text == "" ||
                comboBoxStatus1.Text == "" ||
                comboBoxAcess1.Text == "")
            {
                MostrarAviso("Preencha todos os campos!");
                return;
            }

            if (CriandoUser && UsuarioJaExiste())
            {
                MostrarAviso("Dados já existentes!");
                AtivarCampos(this, true);
                return;
            }

            if (CriandoUser)
            {
                Savedb();
                CriandoUser = false;
            }
            else if (UserEdit)
            {
                updateUser();
                UserEdit = false;
            }
            AtivarCampos(this, false);
            BuscarData();
            
        }

        private bool UsuarioJaExiste()
        {
            using (var conn = Conexao.Abrir())
            {
                string query = @"SELECT COUNT(*) FROM [User]
                WHERE NomeUser = @user 
                OR EmailCorp = @email
                OR endereco = @endereco
                OR Cpf = @cpf
                OR Rg = @rg
                OR contato = @contato";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@user", TextBoxUsuario1.Text);
                    cmd.Parameters.AddWithValue("@email", textBoxEmailCorp1.Text);
                    cmd.Parameters.AddWithValue("@endereco", textBoxEndereco1.Text);
                    cmd.Parameters.AddWithValue("@cpf", TextBoxCpf1.Text);
                    cmd.Parameters.AddWithValue("@rg", textBoxRg1.Text);
                    cmd.Parameters.AddWithValue("@contato", TextBoxContato1.Text);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        private void MostrarAviso(string mensagem)
        {
            var tela = new SearchWindon01();

            tela.label2.Text = mensagem;
            tela.Searchtextbox1.Visible = false;
            tela.button1.Visible = false;

            tela.button2.Text = "OK";
            tela.button2.TextAlign = ContentAlignment.MiddleCenter;
            tela.button2.ImageIndex = -1;

            ModalHelper.AbrirModal(this, tela);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f is ReapotTelaControle)
                {
                    f.BringToFront(); 
                    f.WindowState = FormWindowState.Maximized; 
                    return;
                }
            }

            
            ReapotTelaControle reapotTelaControle = new ReapotTelaControle();
            reapotTelaControle.Show();
            
        }

        private void dataGridViewUsuarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

