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
using System.IO;

namespace Redlife.system_desktopApplication
{
    public static class LogAcesso
    {
        private static string DbPath = Path.Combine(Application.StartupPath, "DataHoraDataBase.db");

        // 🔗 String de conexão
        private static string ConnectionString = $"Data Source={DbPath};Version=3;";

        // 🚀 Método principal
        public static void Registrar(string usuario, string acao)
        {
            try
            {
                using (var conn = new SQLiteConnection(ConnectionString))
                {
                    conn.Open();

                    // 🔥 Garante que a tabela existe
                    CriarTabelaSeNaoExistir(conn);

                    // 🔥 Insert
                    string query = @"INSERT INTO HoraAcesso (usuario, dataHora, acao)
                                     VALUES (@usuario, @dataHora, @acao)";

                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@usuario", usuario);
                        cmd.Parameters.AddWithValue("@dataHora", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        cmd.Parameters.AddWithValue("@acao", acao);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao registrar log:\n" + ex.Message);
            }
        }

        // 🛠️ Cria tabela automaticamente se não existir
        private static void CriarTabelaSeNaoExistir(SQLiteConnection conn)
        {
            string sql = @"
            CREATE TABLE IF NOT EXISTS HoraAcesso (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                usuario TEXT NOT NULL,
                dataHora TEXT NOT NULL,
                acao TEXT NOT NULL
            );";

            using (var cmd = new SQLiteCommand(sql, conn))
            {
                cmd.ExecuteNonQuery();
            }
        }
    }
}
