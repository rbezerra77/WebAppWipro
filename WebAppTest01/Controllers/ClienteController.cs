using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAppTest01.Models;

namespace WebAppTest01.Controllers
{
    public class ClienteController : ApiController
    {
        LoadMemoryData memory = new LoadMemoryData();

        // GET: api/Cliente
        public List<ClienteDAO> Get()
        {            
            return memory.ListaDeClientes.Clientes;
        }

        // GET: api/Cliente/5
        public ClienteDAO Get(int id)
        {
            return memory.ListaDeClientes.GetClienteNaoDeletadoByID(id);
        }

        // POST: api/Cliente
        public void Post([FromBody] ClienteDAO cliente)
        {
            int index = -1;
            index = memory.ListaDeClientes.GetClienteIndexByID(cliente.Id);

            if (index != -1) //Cliente encontrado: Funcao usada na pesquisa é FindIndex, retorna -1 caso nao encontre
            {                
                memory.ListaDeClientes.Clientes.ElementAt(index).Cpf = cliente.Cpf;
                memory.ListaDeClientes.Clientes.ElementAt(index).Nome = cliente.Nome;
                memory.ListaDeClientes.Clientes.ElementAt(index).IsDeleted = cliente.IsDeleted;
            }
            else
            {
                memory.ListaDeClientes.Clientes.Add(cliente);
            }

        }

        // DELETE: api/Cliente/5
        public void Delete(int id)
        {
            int index = -1;
            index = memory.ListaDeClientes.GetClienteIndexByID(id);

            if(index != -1)
            {
                memory.ListaDeClientes.Clientes.ElementAt(index).IsDeleted = true;
            }
        }
    }
}
