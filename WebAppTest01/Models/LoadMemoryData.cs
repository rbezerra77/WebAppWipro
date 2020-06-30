using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WebAppTest01.Models
{
    public class LoadMemoryData
    {
        public ClienteDAOList ListaDeClientes { get; set; }
        public FilmeDAOList ListaDeFilmes { get; set; }
        public LocacaoDAOList ListaDeLocacoes { get; set; }

        public LoadMemoryData()
        {
            //IF arquivo nao existe
            ListaDeClientes = new ClienteDAOList();
            ListaDeFilmes = new FilmeDAOList();
            ListaDeLocacoes = new LocacaoDAOList();

            for (int i = 0; i < 10; i++)
            {
                ListaDeClientes.Clientes.Add(new ClienteDAO()
                {
                    Id = 10 * i,
                    Cpf = "653CPF00" + i + "-00",
                    Nome = "Cliente_0" + i,
                    IsDeleted = false
                });

                ListaDeFilmes.Filmes.Add(new FilmeDAO()
                {
                    Id = i,
                    Nome = "Filme_0" + i,
                    FaixaEtaria = "10+",
                    QtdeDisponivel = 5,
                    Sinopse = "Sinopse 0" + i
                });

                ListaDeLocacoes.Locacoes.Add(new LocacaoDAO()
                {
                    IdCliente = 10 * i,
                    IdFilme = i,
                    DataLocacao = DateTime.Parse("2020-07-01"),
                    DataDevolucao = DateTime.Parse("2020-07-01").AddDays(5),
                    DiasLocacao = 5,
                    IsDeleted = false
                });
            }

            // Atraso
            ListaDeLocacoes.Locacoes.Add(new LocacaoDAO()
            {
                IdCliente = 10,
                IdFilme = 1,
                DataLocacao = DateTime.Parse("2020-06-10"),
                DataDevolucao = null,
                DiasLocacao = 5,
                IsDeleted = false
            });

            // Repetir locador
            ListaDeLocacoes.Locacoes.Add(new LocacaoDAO()
            {
                IdCliente = 20,
                IdFilme = 2,
                DataLocacao = DateTime.Parse("2020-06-30"),
                DiasLocacao = 5,
                IsDeleted = false
            });

            // Filme nao disponivel
            ListaDeFilmes.Filmes.ElementAt(ListaDeFilmes.GetFilmesIndexByID(3)).QtdeDisponivel = 0;

            // Filme marcado como deletado
            ListaDeFilmes.Filmes.ElementAt(ListaDeFilmes.GetFilmesIndexByID(9)).IsDeleted = true;

            // Cliente marcado como deletado
            ListaDeClientes.Clientes.ElementAt(ListaDeClientes.GetClienteIndexByID(90)).IsDeleted = true;
            
            //Locação em Aberto = Sem data de devolução
            ListaDeLocacoes.Locacoes.Add(new LocacaoDAO()
            {
                IdCliente = 70,
                IdFilme = 7,
                DataLocacao = DateTime.Parse("2020-06-30"),
                DataDevolucao = null,
                DiasLocacao = 5,
                IsDeleted = false
            });

            XmlSerializer xmlSer = new XmlSerializer(ListaDeClientes.GetType());
        }
    }
}