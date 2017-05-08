using AcessoBancoDados;
using ObjetoTransferencia;
using System;
using System.Data;
namespace Negocio
{
    public class ClienteNegocios
    {
        AcessoDadosSql acessoDadosSql = new AcessoDadosSql();

        public string Inserir(Cliente cliente)
        {
            try
            {
                acessoDadosSql.LimparParametros();
                acessoDadosSql.AdicionaParamtros("@Nome", cliente.Nome);
                acessoDadosSql.AdicionaParamtros("@DataNasc", cliente.DataNascimento);
                acessoDadosSql.AdicionaParamtros("@Sexo", cliente.Sexo);
                acessoDadosSql.AdicionaParamtros("@LimiteCompra", cliente.LimiteCompra);
                acessoDadosSql.AdicionaParamtros("@Telefone", cliente.Telefone);
                acessoDadosSql.AdicionaParamtros("@Email", cliente.Email);

                // A porta de saída do método é uma string
                // Na procedure de inserção foi feito um retorno de um inteiro
                // caso aconteça alguma "erro" na questão dos parametros na procedure 
                // ele me retorna o erro convertido em string
                // por esso motivo o executamanipulação foi atribuido para uma string 
                // e concvertio no final da linha de código 
                string IdCliente = acessoDadosSql.ExecutarManipulação(CommandType.StoredProcedure, "uspClienteInserirAll").ToString();
                return IdCliente;
            }
            catch (Exception exception)
            {
                // EXCEÇÃO TRATADA, NESSE CASO NÃO FOI FEITO UM THROW PARA LANÇAR
                // ESSA EXCEÇÃO 
                return exception.Message;
            }
           }
        public string Alterar(Cliente cliente)
        {
            try
            {
                acessoDadosSql.LimparParametros();
                acessoDadosSql.AdicionaParamtros("@IdCliente", cliente.IdCliente);
                acessoDadosSql.AdicionaParamtros("@Nome", cliente.Nome);
                acessoDadosSql.AdicionaParamtros("@DataNasc", cliente.DataNascimento);
                acessoDadosSql.AdicionaParamtros("@Sexo", cliente.Sexo);
                acessoDadosSql.AdicionaParamtros("@LimiteCompra", cliente.LimiteCompra);
                acessoDadosSql.AdicionaParamtros("@Telefone", cliente.Telefone);
                acessoDadosSql.AdicionaParamtros("@Email",cliente.Email);
                // A porta de saída do método é uma string
                // Na procedure de inserção foi feito um retorno de um inteiro
                // caso aconteça alguma "erro" na questão dos parametros na procedure 
                // ele me retorna o erro convertido em string
                // por esso motivo o executamanipulação foi atribuido para uma string 
                // e concvertio no final da linha de código 
                string IdCliente = acessoDadosSql.ExecutarManipulação(CommandType.StoredProcedure, "uspClienteAlterar").ToString();
                return IdCliente;
            }
            catch (Exception exception)
            {
                // EXCEÇÃO TRATADA, NESSE CASO NÃO FOIO FEITO UM THROW PARA LANÇAR
                // ESSA EXCEÇÃO 
                return exception.Message;
            }
        }
        public string Deletar(Cliente cliente)
        {

            try
            {
                acessoDadosSql.LimparParametros();
                acessoDadosSql.AdicionaParamtros("@IdCliente", cliente.IdCliente);
                string IdCliente = acessoDadosSql.ExecutarManipulação(CommandType.StoredProcedure, "uspClienteDelete").ToString();
                return IdCliente;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        public ClienteCollection Consultar(string nome)
        {
            try
            {
                ClienteCollection colecao = new ClienteCollection();
                acessoDadosSql.LimparParametros();
                acessoDadosSql.AdicionaParamtros("@Nome", nome);
                DataTable datatable = acessoDadosSql.ExecutarConsulta(CommandType.StoredProcedure, "uspClienteConsultaNome");


                foreach (DataRow linha in datatable.Rows)
                {
                    Cliente cliente = new Cliente();

                    cliente.IdCliente = Convert.ToInt32(linha["IdCliente"]);
                    cliente.Nome = Convert.ToString(linha["Nome"]);
                    cliente.DataNascimento = Convert.ToDateTime(linha["DataNasc"]);
                    cliente.Sexo = Convert.ToBoolean(linha["Sexo"]);
                    cliente.LimiteCompra = Convert.ToDecimal(linha["LimiteCompra"]);
                    cliente.Telefone = Convert.ToString(linha["Telefone"]);
                    cliente.Email = Convert.ToString(linha["Email"]);
                    colecao.Add(cliente);
                }
                return colecao;
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível localizar o cliente" + ex.Message);
            }
            
        }
        public ClienteCollection consultarPorId(string IdCliente)
        {
            try
            {
                ClienteCollection colecao = new ClienteCollection();
                acessoDadosSql.LimparParametros();
                acessoDadosSql.AdicionaParamtros("@IdCliente",IdCliente);
                DataTable datatable = acessoDadosSql.ExecutarConsulta(CommandType.StoredProcedure, "uspClienteConsultaId");

                foreach (DataRow linha in datatable.Rows)
                {
                    Cliente cliente = new Cliente();
                    cliente.IdCliente      = Convert.ToInt32   (linha["IdCliente"]);
                    cliente.Nome           = Convert.ToString  (linha["Nome"]);
                    cliente.DataNascimento = Convert.ToDateTime(linha["DataNasc"]);
                    cliente.Sexo           = Convert.ToBoolean (linha["Sexo"]);
                    cliente.LimiteCompra   = Convert.ToDecimal (linha["LimiteCompra"]);

                    colecao.Add(cliente);

                }

                return colecao;
            }
            catch (Exception ex)
            {
                throw new Exception("O cliente informado não foi localizado" + ex.Message);
            }

        }
    }
}
