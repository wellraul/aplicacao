using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ObjetoTransferencia;
using Negocio;
namespace Apresentacao

{
    public partial class FrmCadCliente : Form
    {
        AcaoNaTela acaoSelecionada;
        
         
            public FrmCadCliente(AcaoNaTela acaoNaTela, Cliente cliente)
        {
            InitializeComponent();
            

           acaoSelecionada = acaoNaTela;
            if ( acaoNaTela.Equals(AcaoNaTela.Inserir))
            {
                this.Text ="Inserir Cliente ";

            }
            else if (acaoNaTela.Equals(AcaoNaTela.Consultar))
            {
                this.Text = "Consultar Cliente ";
                this.txtCodigo.Text = cliente.IdCliente.ToString();
                this.txtNome.Text = cliente.Nome;
                this.dateTimePicker1.Value = cliente.DataNascimento;

                if (cliente.Sexo == true)
                {
                    this.radioMasculino.Checked = true;
                }
                else this.radioFeminino.Checked = true;
                this.txtLimiteCompra.Text = cliente.LimiteCompra.ToString();
                // deixando tela somente leitura 
                
                this.txtNome.ReadOnly = true;
                this.txtNome.TabStop = false;
                this.dateTimePicker1.Enabled = false;
                this.radioMasculino.Enabled = false;
                this.radioFeminino.Enabled = false;
                this.txtLimiteCompra.ReadOnly = true;
                this.txtLimiteCompra.TabStop = false;
                this.btnSalvar.Visible = false;
                this.btnCancelar.Text = "Fechar";
                this.btnCancelar.Focus();

            }

            else if (acaoNaTela.Equals(AcaoNaTela.Alterar)) 
            {
                this.Text = "Alterar Cliente ";
                this.txtCodigo.Text = cliente.IdCliente.ToString();
                this.txtNome.Text = cliente.Nome;
                this.dateTimePicker1.Value = cliente.DataNascimento;

                if (cliente.Sexo == true)
                {
                    this.radioMasculino.Checked = true;
                }
                else this.radioFeminino.Checked = false;
                this.txtLimiteCompra.Text = cliente.LimiteCompra.ToString();
                this.mtxtTelefone.Text = cliente.Telefone;
                this.txtEmail.Text = cliente.Email;
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmCadCliente_Load(object sender, EventArgs e)
        {

        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (acaoSelecionada == AcaoNaTela.Inserir)
            {
                ClienteNegocios clientNegocios = new ClienteNegocios();
                Cliente clienteInserir = new Cliente();
                clienteInserir.Nome = txtNome.Text;
                clienteInserir.DataNascimento = dateTimePicker1.Value;
                if (radioMasculino.Checked == true)
                
                    clienteInserir.Sexo = true;
                 else
                    clienteInserir.Sexo = false;
                clienteInserir.LimiteCompra = Convert.ToDecimal(txtLimiteCompra.Text);
                clienteInserir.Telefone = mtxtTelefone.Text;
                clienteInserir.Email = txtEmail.Text;

                string retorno = clientNegocios.Inserir(clienteInserir);

                try
                {
                    int IdCliente = Convert.ToInt32(retorno);
                    MessageBox.Show("Cliente inserido com sucesso, Código"+ IdCliente.ToString());
                    this.DialogResult = DialogResult.Yes;
                }
                catch
                {
                    MessageBox.Show("Não foi possivel inserir o cliente, Detalhes:"+ retorno ,"ERRO!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.DialogResult = DialogResult.No;
                }
            }
            else if (acaoSelecionada == AcaoNaTela.Alterar)
            {
                ClienteNegocios clientNegocios = new ClienteNegocios();
                Cliente clienteAlterar = new Cliente();
                clienteAlterar.IdCliente = Convert.ToInt32(txtCodigo.Text);
                clienteAlterar.Nome = txtNome.Text;
                clienteAlterar.DataNascimento = dateTimePicker1.Value;
                if (radioMasculino.Checked == true)
           
                    clienteAlterar.Sexo = true;
                
                else
                    clienteAlterar.Sexo = false;
                clienteAlterar.LimiteCompra = Convert.ToDecimal(txtLimiteCompra.Text);
                clienteAlterar.Telefone = mtxtTelefone.Text;
                clienteAlterar.Email = txtEmail.Text;


                string retorno = clientNegocios.Alterar(clienteAlterar);

                try
                {
                    int IdCliente = Convert.ToInt32(retorno);
                    MessageBox.Show("Cliente Alterado com sucesso, Código" + IdCliente.ToString());
                    this.DialogResult = DialogResult.Yes;
                }
                catch
                {
                    MessageBox.Show("Não foi possivel Alterar o cliente, Detalhes:" + retorno, "ERRO!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.DialogResult = DialogResult.No;
                }
            }
        }

        private void mtxtTelefone_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
    }
}
