using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Redlife.system_desktopApplication
{
    class CadastrarBancoPaciente
    {

        private static string DbPath = Path.Combine(Application.StartupPath, "ClientUsersDataBase");

        // 🔗 String de conexão
        private static string ConnectionString = $"Data Source={DbPath};Version=3;";
    }
    public partial class CadastroDeDoadores : Form
    {
        public CadastroDeDoadores()
        {
            InitializeComponent();
        }


        

        private void guna2HtmlLabel6_Click(object sender, EventArgs e)
        {

        }

        private void kryptonTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel9_Click(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            checkBox2.Checked = false;
            

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            checkBox4.Checked = false;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            checkBox3.Checked = false;
        }

        private void guna2HtmlLabel11_Click(object sender, EventArgs e)
        {

        }

        private void kryptonTextBox2_TextChanged(object sender, EventArgs e)
        {
            var txt = kryptonTextBox2;

            int cursor = txt.SelectionStart;

            string numeros = new string(txt.Text.Where(char.IsDigit).ToArray());

            if (numeros.Length > 11)
                numeros = numeros.Substring(0, 11);

            string formatado = "";

            if (numeros.Length > 0)
                formatado = numeros.Substring(0, Math.Min(3, numeros.Length));

            if (numeros.Length > 3)
                formatado += "." + numeros.Substring(3, Math.Min(3, numeros.Length - 3));

            if (numeros.Length > 6)
                formatado += "." + numeros.Substring(6, Math.Min(3, numeros.Length - 6));

            if (numeros.Length > 9)
                formatado += "-" + numeros.Substring(9, Math.Min(2, numeros.Length - 9));

            txt.TextChanged -= kryptonTextBox2_TextChanged;
            txt.Text = formatado;
            txt.SelectionStart = txt.Text.Length; 
            txt.TextChanged += kryptonTextBox2_TextChanged;
        }

        private void kryptonTextBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void kryptonTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void panel96_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void CadastroDeDoadores_Load(object sender, EventArgs e)
        {

        }
    }
}
