using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Redlife.system_desktopApplication
{
    public partial class EscolhaDeEstoqueAoAcacao : Form
    {
        public EscolhaDeEstoqueAoAcacao()
        {
            InitializeComponent();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.ActiveControl = null;

        }

        private void EscolhaDeEstoqueAoAcacao_Load(object sender, EventArgs e)
        {
            treeView1.ExpandAll();
            this.ActiveControl = null;
        }

        private void treeView1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void treeView1_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            var principalform = (Form1)Application.OpenForms["Form1"];
            switch (e.Node.Name) { 
            case "EstoqueMaterialComum":
                    
                    EstadoSistema.AbrirEstoque = true;
                break;
            }
            if(principalform != null)
            {
                principalform.retornoResposta();
                

              
                this.Close();
                
            }
        }

        public static class EstadoSistema
        {
            public static bool AbrirEstoque = false;
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }
    } }
