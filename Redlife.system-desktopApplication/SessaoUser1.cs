using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Redlife.system_desktopApplication
{

    

    
}
public static class SessaoUsuario
    {
        public static int Id { get; set; }
        public static string Nome { get; set; }
        public static string Usuario { get; set; }
        public static string Cargo { get; set; }

        public static void Limpar()
        {
            Id = 0;
            Nome = null;
            Usuario = null;
            Cargo = null;
        }
       
        public static void NomeUser()
    {
        string userNomeLabel = SessaoUsuario.Usuario;
    }

    }



public static class AppState
{
    public static string Valorpesquisa1 { get; set; }
    public static bool PesquisarDB { get; set; }
}

internal class SessaoUser1
    {
    }
    public static class ModalHelper
    {
        public static void AbrirModal(Form owner, Form modal)
        {
            Form formBackGround = new Form();

            try
            {
                formBackGround.StartPosition = FormStartPosition.Manual;
                formBackGround.FormBorderStyle = FormBorderStyle.None;
                formBackGround.Opacity = 0.50d;
                formBackGround.BackColor = Color.Black;
                formBackGround.ShowInTaskbar = false;
                formBackGround.WindowState = FormWindowState.Maximized;
                formBackGround.Bounds = owner.Bounds;
                

                formBackGround.Show();

                modal.StartPosition = FormStartPosition.CenterParent;
                modal.Owner = formBackGround;

                modal.ShowDialog();
            }
            finally
            {
                formBackGround.Dispose();
            }
       
        }
    }

