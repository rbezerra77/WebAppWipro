using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppTest01.Models
{
    public class ClienteDAO
    {
        public String Cpf { get; set; }
        public String Nome { get; set; }
        public int Id { get; set; }
        public Boolean IsDeleted { get; set; }
    }
            

    public class ClienteDAOList
    {
        public List<ClienteDAO> Clientes { get; set; }

        public ClienteDAOList()
        {
            Clientes = new List<ClienteDAO>();
        }

        public ClienteDAO GetClienteNaoDeletadoByID(int Id)
        {
            ClienteDAO clienteRetorno = null;

            foreach (ClienteDAO cliente in Clientes)
            {
                if(cliente.Id == Id && cliente.IsDeleted != true)
                {
                    clienteRetorno = cliente;
                    break;
                }
            }
            return clienteRetorno;
        }

        public ClienteDAO GetClienteByID(int Id)
        {
            return Clientes.First(cliente => cliente.Id == Id);
        }

        public int GetClienteIndexByID(int Id)
        {
            return Clientes.FindIndex(cliente => cliente.Id == Id);
        }
    }
}