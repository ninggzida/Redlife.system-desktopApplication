using Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Redlife.system_desktopApplication.Form1;

namespace Redlife.system_desktopApplication.t.i
{
    public partial class AcessosEfetuadosDiariamente : Form
    {
        int linhaAtual = 0;
        public AcessosEfetuadosDiariamente()
        {
            InitializeComponent();

        }

        private void AcessosEfetuadosDiariamente_Load(object sender, EventArgs e)
        {

        }
        private void ConfigurarGrid()
        {
            var grid = dataGridView1;

            // 🔒 comportamento
            grid.ReadOnly = true;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.MultiSelect = false;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToResizeRows = false;
            grid.AllowUserToResizeColumns = true;

            // 🎯 aparência geral
            grid.BackgroundColor = Color.Gainsboro;
            grid.BorderStyle = BorderStyle.Fixed3D;
            grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            grid.GridColor = Color.LightGray;

            // 📏 tamanho automático
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

            // 🔤 fontes
            grid.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            // 🎨 cores células
            grid.DefaultCellStyle.ForeColor = Color.FromArgb(40, 40, 40);
            grid.DefaultCellStyle.BackColor = Color.White;
            grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 120, 215); // azul moderno
            grid.DefaultCellStyle.SelectionForeColor = Color.White;

            // 🎨 linhas alternadas (muito importante pra leitura)
            grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);

            // 📌 header
            grid.EnableHeadersVisualStyles = false;
            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 30, 30);
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grid.ColumnHeadersHeight = 40;
            grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            // 📏 altura linha
            grid.RowTemplate.Height = 32;

            // 🚫 tira aquele quadrado feio do canto
            grid.RowHeadersVisible = false;

            // 🖱️ cursor mais moderno
            grid.Cursor = Cursors.Hand;

            // 🧾 nomes amigáveis (se existirem)
            if (grid.Columns.Contains("usuario"))
                grid.Columns["usuario"].HeaderText = "Usuário";

            if (grid.Columns.Contains("dataHora"))
                grid.Columns["dataHora"].HeaderText = "Data / Hora";

            if (grid.Columns.Contains("acao"))
                grid.Columns["acao"].HeaderText = "Ação";

            // 🙈 esconder ID
            if (grid.Columns.Contains("id"))
                grid.Columns["id"].Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {


        }
        private void CarregarAcessos()
        {
            try
            {
                string path = System.IO.Path.Combine(Application.StartupPath, "DataHoraDataBase.db");
                string connectionString = $"Data Source={path};Version=3;";

                using (var conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();

                    string query = @"SELECT id, usuario, dataHora, acao
                             FROM HoraAcesso
                             WHERE dataHora LIKE @data";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@data",
                            guna2DateTimePicker1.Value.ToString("yyyy-MM-dd") + "%");

                        using (SQLiteDataAdapter da = new SQLiteDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            dataGridView1.DataSource = dt;
                        }
                    }
                }

                ConfigurarGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar os acessos:\n" + ex.Message);
            }
        }

        

        private void AcessosEfetuadosDiariamente_Shown(object sender, EventArgs e)
        {
            ConfigurarGrid();
        }

        private void guna2GradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            listarAcessos();
        }
        private void listarAcessos()
        {
            try
            {
                string path = System.IO.Path.Combine(Application.StartupPath, "DataHoraDataBase.db");
                string connectionString = $"Data Source={path};Version=3;";

                using (var conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();

                    string query = @"SELECT id, usuario, dataHora, acao FROM HoraAcesso";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        using (SQLiteDataAdapter da = new SQLiteDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            dataGridView1.DataSource = dt;
                        }
                    }
                }

                ConfigurarGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar os acessos:\n" + ex.Message);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            CarregarAcessos();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            try
            {
                dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

                PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();

                printDocument1.DefaultPageSettings.Landscape = true;
                printDocument1.DefaultPageSettings.Margins = new Margins(30, 30, 30, 30);

                // evita duplicar evento
                printDocument1.PrintPage -= printDocument1_PrintPage;
                printDocument1.PrintPage += printDocument1_PrintPage;

                // 🔥 verifica se existe alguma impressora instalada
                PrinterSettings settings = new PrinterSettings();

                if (!settings.IsValid)
                {
                    MessageBox.Show("Impressora indisponível. Usando PDF.");

                    printDocument1.PrinterSettings.PrinterName = "Microsoft Print to PDF";
                }
                else
                {
                    printDocument1.PrinterSettings = settings;
                }

                printPreviewDialog.Document = printDocument1;

                ((Form)printPreviewDialog).WindowState = FormWindowState.Maximized;
                ((Form)printPreviewDialog).ShowIcon = false;
                ((Form)printPreviewDialog).Text = "Relatório de usuarios do sistema";

                printPreviewDialog.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao abrir visualização:\n" + ex.Message);
            }
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            // Configurações de layout (Estilo Excel)
            float x = e.MarginBounds.Left;
            float y = e.MarginBounds.Top;

            Font fontTitulo = new Font("Segoe UI", 14, FontStyle.Bold);
            Font fontCabecalho = new Font("Segoe UI", 10, FontStyle.Bold);
            Font fontCorpo = new Font("Segoe UI", 9, FontStyle.Regular);
            Pen penBorda = new Pen(Color.Black, 0.1f);

            // 🔥 Título
            e.Graphics.DrawString(
                "Relatório De Acessos Efetuados Dia - " + guna2DateTimePicker1.Value.ToString("dd/MM/yyyy") + " - REDLIFE",
                fontTitulo,
                Brushes.Black,
                x,
                y
            );

            y += 35;

            // 🔥 calcular proporção
            float larguraTotalGrid = 0;
            foreach (DataGridViewColumn col in dataGridView1.Columns)
                if (col.Visible) larguraTotalGrid += col.Width;

            float larguraDisponivel = e.MarginBounds.Width;
            float proporcao = larguraDisponivel / larguraTotalGrid;

            // 🔥 CABEÇALHO (repete em todas páginas)
            float currentX = x;

            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                if (!col.Visible) continue;

                float larguraCol = col.Width * proporcao;
                RectangleF rect = new RectangleF(currentX, y, larguraCol, 25);

                e.Graphics.FillRectangle(Brushes.LightGray, rect);
                e.Graphics.DrawRectangle(penBorda, rect.X, rect.Y, rect.Width, rect.Height);

                StringFormat sf = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };

                e.Graphics.DrawString(col.HeaderText, fontCabecalho, Brushes.Black, rect, sf);

                currentX += larguraCol;
            }

            y += 25;

            // 🔥 LINHAS COM PAGINAÇÃO
            for (; linhaAtual < dataGridView1.Rows.Count; linhaAtual++)
            {
                var row = dataGridView1.Rows[linhaAtual];
                if (row.IsNewRow) continue;

                currentX = x;
                float alturaLinha = 22;

                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (!dataGridView1.Columns[cell.ColumnIndex].Visible) continue;

                    float larguraCol = dataGridView1.Columns[cell.ColumnIndex].Width * proporcao;
                    RectangleF rect = new RectangleF(currentX, y, larguraCol, alturaLinha);

                    e.Graphics.DrawRectangle(penBorda, rect.X, rect.Y, rect.Width, rect.Height);

                    string valor = cell.Value?.ToString() ?? "";

                    RectangleF rectTexto = new RectangleF(currentX + 3, y, larguraCol - 5, alturaLinha);

                    StringFormat sfCorpo = new StringFormat
                    {
                        LineAlignment = StringAlignment.Center,
                        FormatFlags = StringFormatFlags.NoWrap
                    };

                    e.Graphics.DrawString(valor, fontCorpo, Brushes.Black, rectTexto, sfCorpo);

                    currentX += larguraCol;
                }

                y += alturaLinha;

                // 🔥 QUEBRA DE PÁGINA
                if (y + alturaLinha > e.MarginBounds.Bottom)
                {
                    linhaAtual++; // continua da próxima linha
                    e.HasMorePages = true;
                    return;
                }
            }

            // 🔥 terminou tudo
            linhaAtual = 0;
            e.HasMorePages = false;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox1.Text == "")
                {
                    var telaAviso = new SearchWindon01();
                    telaAviso.Searchtextbox1.Visible = false;
                    telaAviso.button1.Visible = false;
                    telaAviso.BackColor = Color.White;
                    telaAviso.label2.ForeColor = Color.DimGray;
                    telaAviso.label2.Text = "Campo vazio, digite o usuário!";

                    telaAviso.ShowDialog();
                }
                else { FiltrarPorUsuario(textBox1.Text); }
                



            }
        }
        private void FiltrarPorUsuario(string nomeUser)
        {
            try
            {
                string path = System.IO.Path.Combine(Application.StartupPath, "DataHoraDataBase.db");
                string connectionString = $"Data Source={path};Version=3;";

                using (var conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();

                    string query = @"SELECT id, usuario, dataHora, acao
                             FROM HoraAcesso
                             WHERE usuario LIKE @usuario";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@usuario", "%" + nomeUser + "%");

                        using (SQLiteDataAdapter da = new SQLiteDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            if (dt.Rows.Count == 0)
                            {
                                var telaAviso = new SearchWindon01();
                                telaAviso.Searchtextbox1.Visible = false;
                                telaAviso.button1.Visible = false;
                                telaAviso.BackColor = Color.White;
                                telaAviso.label2.Text = "Usuário não encontrado!";
                                telaAviso.label2.ForeColor = Color.DimGray;
                                telaAviso.ShowDialog();

                                dataGridView1.DataSource = null;
                                return;
                            }

                            dataGridView1.DataSource = dt;
                        }
                    }
                }

                ConfigurarGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao filtrar:\n" + ex.Message);
            }
        }
    }
}
