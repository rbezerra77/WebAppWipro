using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppTest01.Models
{
    public class LocacaoDAO
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public int IdFilme { get; set; }
        public DateTime DataLocacao { get; set; }
        public DateTime? DataDevolucao { get; set; }
        public int DiasLocacao { get; set; }
        public Boolean IsDeleted { get; set; }
    }

    public class LocacaoDAOList
    {
        public List<LocacaoDAO> Locacoes { get; set; }

        public LocacaoDAOList()
        {
            Locacoes = new List<LocacaoDAO>();
        }


        public LocacaoDAO GetLocacaoNaoDeletadoByID(int id)
        {
            LocacaoDAO LocacaoRetorno = null;

            foreach (LocacaoDAO Locacao in Locacoes)
            {
                if (Locacao.Id == id && Locacao.IsDeleted != true)
                {
                    LocacaoRetorno = Locacao;
                    break;
                }
            }
            return LocacaoRetorno;
        }

        public LocacaoDAO GetLocacaoByID(int id)
        {
            return Locacoes.First(Locacao => Locacao.Id == id);
        }

        public int GetLocacaoIndexByID(int id)
        {
            return Locacoes.FindIndex(Locacao => Locacao.Id == id);
        }

        public List<LocacaoDAO> GetLocacoesByCliente(int IdCliente)
        {
            List<LocacaoDAO> listRetorno = new List<LocacaoDAO>();
            foreach (LocacaoDAO locacao in Locacoes)
            {
                if (!locacao.IsDeleted && locacao.IdCliente == IdCliente)
                {
                    listRetorno.Add(locacao);
                }
            }
            return listRetorno;
        }
    }
}