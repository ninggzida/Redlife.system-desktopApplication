using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Redlife.system_desktopApplication.Form1;
using System.Drawing.Printing;


namespace Redlife.system_desktopApplication
{
    public partial class ReapotTelaControle : Form
    {
        public ReapotTelaControle()
        {
            InitializeComponent();
        }

        private void ReapotTelaControle_Load(object sender, EventArgs e)
        {
            CarregarUsuarios();
        }

        private void ReapotTelaControle_Shown(object sender, EventArgs e)
        {
            CarregarUsuarios();
            dataGridView1.ClearSelection();
        }

        private void CarregarUsuarios()
        {
            try
            {
                using (var conn = Conexao.Abrir())
                {
                    string query = @"SELECT id, NomeCivil, NomeUser, EmailCorp, endereco, Cpf, Rg, contato, Status, Cargo 
                             FROM [User]";

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
                MessageBox.Show("Erro ao carregar usuários:\n" + ex.Message);
            }
        }




        private void ConfigurarGrid()
        {
          
            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToResizeRows = false;

            
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

           
            dataGridView1.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
            dataGridView1.ForeColor = Color.DimGray;

          
            dataGridView1.RowTemplate.Height = 35;

         
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 30, 30);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

           
            dataGridView1.BackgroundColor = Color.Gainsboro;

            
            if (dataGridView1.Columns.Contains("id"))
                dataGridView1.Columns["id"].Visible = false;
        }

        private void btnCancel1_Click_1(object sender, EventArgs e)
        {
            CarregarUsuarios();
            dataGridView1.ClearSelection();
        }

        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            var row = dataGridView1.Rows[e.RowIndex];

            if (row.Cells["Status"].Value != null)
            {
                string status = row.Cells["Status"].Value.ToString();

                if (status == "Desligado")
                {
                    row.DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#FF5A5A");
                    row.DefaultCellStyle.ForeColor = Color.White;
                }
            }
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

            PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
            printDocument1.DefaultPageSettings.Landscape = true;

            
            printDocument1.DefaultPageSettings.Margins = new Margins(30, 30, 30, 30);

            printDocument1.PrintPage += printDocument1_PrintPage;
            printPreviewDialog.Document = printDocument1;

           
            ((Form)printPreviewDialog).WindowState = FormWindowState.Maximized;
            ((Form)printPreviewDialog).ShowIcon = false;
            ((Form)printPreviewDialog).Text = "Relatório de usuarios do sistema";

            printPreviewDialog.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            
            float x = e.MarginBounds.Left;
            float y = e.MarginBounds.Top;

            System.Drawing.Font fontCabecalho = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
            System.Drawing.Font fontCorpo = new System.Drawing.Font("Segoe UI", 9, FontStyle.Regular);
            Pen penBorda = new Pen(Color.Black, 0.1f); // Linha fina estilo Excel

            
            e.Graphics.DrawString("RELATÓRIO DE USUÁRIOS - REDLIFE", new System.Drawing.Font("Segoe UI", 14, FontStyle.Bold), Brushes.Black, x, y);
            y += 35; 
            
            float larguraTotalGrid = 0;
            foreach (DataGridViewColumn col in dataGridView1.Columns)
                if (col.Visible) larguraTotalGrid += col.Width;

            float larguraDisponivelPapel = e.MarginBounds.Width;
            float proporcao = larguraDisponivelPapel / larguraTotalGrid;

            

            
            float currentX = x;
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                if (!col.Visible) continue;

                float larguraCol = col.Width * proporcao;
                RectangleF rect = new RectangleF(currentX, y, larguraCol, 25);

               
                e.Graphics.FillRectangle(Brushes.LightGray, rect);
                e.Graphics.DrawRectangle(penBorda, rect.X, rect.Y, rect.Width, rect.Height);

               
                StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                e.Graphics.DrawString(col.HeaderText, fontCabecalho, Brushes.Black, rect, sf);

                currentX += larguraCol;
            }

            y += 25; 

            
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
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
                        Trimming = StringTrimming.None,         
                        FormatFlags = StringFormatFlags.NoWrap  
                    };

                    e.Graphics.DrawString(valor, fontCorpo, Brushes.Black, rectTexto, sfCorpo);

                    currentX += larguraCol; 
                }

                y += alturaLinha; 

                
                if (y > e.MarginBounds.Bottom) break;
            }
        }

        private void btnfilter1_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            if (dataGridView1.SelectedRows.Count > 0)
            {
                
                var idUsuario = dataGridView1.SelectedRows[0].Cells["id"].Value.ToString();
                var nomeUsuario = dataGridView1.SelectedRows[0].Cells["NomeCivil"].Value.ToString();

                
                DialogResult confirmacao = MessageBox.Show(
                    $"Deseja realmente excluir o registro de {nomeUsuario}?\nEsta ação não pode ser desfeita.",
                    "Atenção",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirmacao == DialogResult.Yes)
                {
                    try
                    {
                        
                        using (var conn = Conexao.Abrir())
                        {
                            string query = "DELETE FROM [User] WHERE id = @id";

                            using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                            {
                                
                                cmd.Parameters.AddWithValue("@id", idUsuario);

                                int resultado = cmd.ExecuteNonQuery();

                                if (resultado > 0)
                                {
                                    

                                    MessageBox.Show("Usuário excluído com sucesso!");

                                    
                                    CarregarUsuarios();
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao excluir usuário:\n" + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione uma linha inteira na tabela para excluir.");
            }
        }

        
    }
}
