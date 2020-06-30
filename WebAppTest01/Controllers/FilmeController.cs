using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAppTest01.Models;

namespace WebAppTest01.Controllers
{
    public class FilmeController : ApiController
    {
        LoadMemoryData memory = new LoadMemoryData();

        // GET: api/Filme
        public List<FilmeDAO> Get()
        {
            return memory.ListaDeFilmes.Filmes;
        }

        // GET: api/Filme/5
        public FilmeDAO Get(int id)
        {
            return memory.ListaDeFilmes.GetFilmeNaoDeletadoByID(id);
        }

        // POST: api/Filme
        public void Post([FromBody] FilmeDAO filme)
        {
            int index = -1;
            index = memory.ListaDeFilmes.GetFilmesIndexByID(filme.Id);

            if (index != -1) //Filme encontrado: Funcao usada na pesquisa é FindIndex, retorna -1 caso nao encontre
            {
                memory.ListaDeFilmes.Filmes.ElementAt(index).Nome = filme.Nome;
                memory.ListaDeFilmes.Filmes.ElementAt(index).QtdeDisponivel = filme.QtdeDisponivel;
                memory.ListaDeFilmes.Filmes.ElementAt(index).Sinopse = filme.Sinopse;
                memory.ListaDeFilmes.Filmes.ElementAt(index).IsDeleted = filme.IsDeleted;
            }
            else
            {
                memory.ListaDeFilmes.Filmes.Add(filme);
            }
        }

        // DELETE: api/Filme/5
        public void Delete(int id)
        {
            int index = -1;
            index = memory.ListaDeFilmes.GetFilmesIndexByID(id);

            if (index != -1) 
            {
                memory.ListaDeFilmes.Filmes.ElementAt(index).IsDeleted = true;
            }
        }
    }
}
