using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAppTest01.Models;

namespace WebAppTest01.Controllers
{
    public class LocacaoController : ApiController
    {
        LoadMemoryData memory = new LoadMemoryData();

        // GET: api/Locacao
        public List<LocacaoDAO> Get()
        {
            return memory.ListaDeLocacoes.Locacoes;
        }

        // GET: api/Locacao/5
        public LocacaoDAO Get(int id)
        {
            return memory.ListaDeLocacoes.GetLocacaoByID(id);
        }

        // POST: api/Locacao
        public String Post([FromBody] LocacaoDAO locacao)
        {
            int filmeIndex = memory.ListaDeFilmes.GetFilmesIndexByID(locacao.IdFilme);
            int clienteIndex = memory.ListaDeClientes.GetClienteIndexByID(locacao.IdCliente);

            //Cliente ou Filme nao existe
            if (filmeIndex == -1 || clienteIndex == -1)
            {
                return "Cliente ou Filme nao encontrado na base de dados";
            }

            //Cliente ou filme marcado como deletado
            if (memory.ListaDeFilmes.Filmes.ElementAt(filmeIndex).IsDeleted || memory.ListaDeClientes.Clientes.ElementAt(clienteIndex).IsDeleted)
            {
                return "Cliente ou Filme nao encontrado na base de dados";
            }

            //verificando se é um aluguel ou uma devolução
            if (locacao.DataDevolucao == null) // ==null : LOCACAO
            {

                //Previnindo aluguel de filme indisponível
                if (memory.ListaDeFilmes.Filmes.ElementAt(filmeIndex).QtdeDisponivel < 1)
                {
                    return "Filme Solicitado nao disponivel";
                }

                //Previnindo aluguel em duplicidade
                foreach (LocacaoDAO loc in memory.ListaDeLocacoes.Locacoes)
                {
                    if (!loc.IsDeleted)
                    {
                        if (loc.IdCliente == locacao.IdCliente && loc.DataDevolucao == null) //Cliente encontrado e data de devolução nulla
                        {
                            return "Cliente com aluguel já em andamento com o filme: " + memory.ListaDeFilmes.GetFilmeByID(loc.IdFilme).Nome;
                        }
                    }
                }

                //Adcionando locação
                memory.ListaDeLocacoes.Locacoes.Add(locacao);

                return "Locação realizada com sucesso!";
            }
            else //!= null : Devoluçao
            {
                String msg = String.Empty;

                if (locacao.DataLocacao.AddDays(locacao.DiasLocacao) > DateTime.Today)
                {
                    msg = "ATENÇÃO: Filme Devolvido com atraso.  Calcular dias excedentes no pagamento.\n"; 
                }
                memory.ListaDeLocacoes.Locacoes.ElementAt(
                memory.ListaDeLocacoes.GetLocacaoIndexByID(locacao.Id)
                ).DataDevolucao = locacao.DataDevolucao;

                return msg + "Devolução feita com sucesso!";
            }

        }

        // DELETE: api/Locacao/5
        public void Delete(int id)
        {
            int index = -1;
            index = memory.ListaDeLocacoes.GetLocacaoIndexByID(id);

            if (index != -1)
            {
                memory.ListaDeLocacoes.Locacoes.ElementAt(index).IsDeleted = true;
            }
        }
    }
}
