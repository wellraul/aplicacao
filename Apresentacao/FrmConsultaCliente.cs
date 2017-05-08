using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocio;
using ObjetoTransferencia;
namespace Apresentacao
{
    public partial class FrmCadClientes : Form
    {
        public FrmCadClientes()
        {
            InitializeComponent();
            atualizar();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
           
            int n;
            bool ehUmNumero = int.TryParse(txtPesquisa.Text, out n);
            if (ehUmNumero == true)
            {
                atualizarId();
            }
            else if (ehUmNumero == false)
            {
                atualizar();
            }
           

        }

        private void atualizarId()
        {
            ClienteNegocios cliente = new ClienteNegocios();
            ClienteCollection clientecollection = cliente.consultarPorId(txtPesquisa.Text);
            dataGridView1.DataSource = clientecollection;
            dataGridView1.Update();
            dataGridView1.Refresh();
        }
        private void atualizar()
        {
            ClienteNegocios clienteNegocio = new ClienteNegocios();

            ClienteCollection clientecollection = clienteNegocio.Consultar(txtPesquisa.Text);
            dataGridView1.DataSource = clientecollection;
            dataGridView1.Update();
            dataGridView1.Refresh();
        }
      

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {   // cria a caixa de dialogo construtor 7
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Nenhum cliente selecionado");
            }

            DialogResult resultado = MessageBox.Show("Deseja realmente Apagar esse cliente?", "Pergunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //verifica se o resultado da pergunta é sim ou não
            if(resultado == DialogResult.No)
            {
                return;
            }
            //pegar o cliente selecionado no GRID
            Cliente clienteSelecionado = (dataGridView1.SelectedRows[0].DataBoundItem as Cliente);

            ClienteNegocios clienteNegocios = new ClienteNegocios();
            // Chama o método excluir e apaga o cliente selecionado
            string retorno = clienteNegocios.Deletar(clienteSelecionado);

            try
            {
                int IdCliente = Convert.ToInt32(retorno);
                MessageBox.Show("Cliente Excluído com sucesso!!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                atualizar();
            }
            catch
            {
                MessageBox.Show("Cliente não foi excluido!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void btnInserir_Click_1(object sender, EventArgs e)
        {
            FrmCadCliente frmCadCliente = new FrmCadCliente(AcaoNaTela.Inserir, null);
            
            DialogResult dialogResult = frmCadCliente.ShowDialog();
            if (dialogResult == DialogResult.Yes)
            {
                atualizar();
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Nenhum cliente selecionado");
            }
            //pegar o cliente selecionado no GRID
            Cliente clienteSelecionado = (dataGridView1.SelectedRows[0].DataBoundItem as Cliente);


            FrmCadCliente frmCadCliente = new FrmCadCliente(AcaoNaTela.Alterar, clienteSelecionado);
            DialogResult resultado = frmCadCliente.ShowDialog();
            if (resultado == DialogResult.Yes)
            {
                atualizar();
            }

        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Nenhum cliente selecionado");
            }
            //pegar o cliente selecionado no GRID

            Cliente clienteSelecionado = (dataGridView1.SelectedRows[0].DataBoundItem as Cliente);
            FrmCadCliente frmCadCliente = new FrmCadCliente(AcaoNaTela.Consultar, clienteSelecionado);
            frmCadCliente.ShowDialog();

        }

        private void FrmCadClientes_Load(object sender, EventArgs e)
        {

        }
    }
}
