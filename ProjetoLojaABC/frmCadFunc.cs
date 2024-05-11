using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using MySql.Data.MySqlClient;
namespace ProjetoLojaABC
{
    public partial class frmCadFunc : Form
    {
        //Criando variáveis para controle do menu
        const int MF_BYCOMMAND = 0X400;
        [DllImport("user32")]
        static extern int RemoveMenu(IntPtr hMenu, int nPosition, int wFlags);
        [DllImport("user32")]
        static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        [DllImport("user32")]
        static extern int GetMenuItemCount(IntPtr hWnd);
        public frmCadFunc()
        {
            InitializeComponent();
            desabilitarCampos();
        }


        private void btnVoltar_Click(object sender, EventArgs e)
        {
            frmMenuPrincipal abrir = new frmMenuPrincipal();
            abrir.Show();
            this.Hide();
        }
        public void desabilitarCampos()
        {
            txtCodigo.Enabled = false;
            txtNome.Enabled = false;
            txtSenha.Enabled = false;
            txtrepSenha.Enabled = false;


        }
        public void habilitarCampos()
        {
            txtCodigo.Enabled = true;
            txtSenha.Enabled = true;
            txtrepSenha.Enabled = true;
            txtNome.Enabled = true;
            txtNome.Focus();
        }
        public void limparCampos()
        {

            txtNome.Clear();
            txtSenha.Clear();
            txtrepSenha.Clear();
        }



        private void btnNovo_Click_1(object sender, EventArgs e)
        {

            habilitarCampos();
            txtCodigo.Enabled = false;
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {


            if (txtSenha.Text != txtrepSenha.Text)
            {
                MessageBox.Show("As senha estão diferentes",
                    "Mensagem do sistema", MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);
            }
            else
            {
                if (/*txtCodigo.Text.Equals("") || */ txtNome.Text.Equals("") || txtSenha.Text.Equals(""))

                {
                    MessageBox.Show("Favor inserir valores válidos!!!",
                    "Mensagem do sistema", MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);

                }
                else
                {

                    cadastrarFuncionarios();
                    desabilitarCampos();
                    btnNovo.Enabled = true;

                }
            }
        }

    private void btnPesquisar_Click(object sender, EventArgs e)
        {
            frmPesquisar abrir = new frmPesquisar();
            abrir.Show();
            this.Hide();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            limparCampos();
            txtNome.Focus();
        }
        public void cadastrarFuncionarios()
        {
            MySqlCommand comm = new MySqlCommand();
            comm.CommandText = "insert into tbusuario" +
                "(nome,senha)values(@nome,@senha)";
            comm.CommandType = CommandType.Text;

            comm.Parameters.Clear();
            comm.Parameters.Add("@nome", MySqlDbType.VarChar, 30).Value = txtNome.Text;
            comm.Parameters.Add("@senha", MySqlDbType.VarChar, 10).Value = txtSenha.Text;
            comm.Connection = Conexao.obterConexao();


            int res = comm.ExecuteNonQuery();

            MessageBox.Show("Cadastrado com sucesso");
            limparCampos();
            Conexao.fecharConexao();

        }


    }
}
