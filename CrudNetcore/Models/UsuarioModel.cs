using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudNetcore.Models
{
    public class UsuarioModel
    {
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Rut { get; set; }
        public string password { get; set; }

    }

    public class Response
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; }
    }

    public class UsuarioEdit
    {
        public int id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string password { get; set; }

    }

    public class UsuarioShow
    {
        public int id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Rut { get; set; }
    }

    public class UserResponse
    {
        public List<UsuarioShow> Data { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; }
    }
}
