using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppTest01.Models
{
    public class FilmeDAO
    {
        public int Id { get; set; }
        public String Nome { get; set; }
        public int QtdeDisponivel { get; set; }
        public String Sinopse { get; set; }
        public String FaixaEtaria { get; set; }
        public Boolean IsDeleted { get; set; }
    }

    public class FilmeDAOList
    {
        public List<FilmeDAO> Filmes { get; set; }

        public FilmeDAOList()
        {
            Filmes = new List<FilmeDAO>();
        }

        public FilmeDAO GetFilmeNaoDeletadoByID(int Id)
        {
            FilmeDAO filmeRetorno = null;

            foreach (FilmeDAO filme in Filmes)
            {
                if (filme.Id == Id && filme.IsDeleted != true)
                {
                    filmeRetorno = filme;
                    break;
                }
            }
            return filmeRetorno;
        }

        public FilmeDAO GetFilmeByID(int Id)
        {
            return Filmes.First(filme => filme.Id == Id);
        }

        public int GetFilmesIndexByID(int Id)
        {
            return Filmes.FindIndex(filme => filme.Id == Id);
        }
    }
}