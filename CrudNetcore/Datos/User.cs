using System;
using System.Collections.Generic;

namespace CrudNetcore.Datos
{
    public partial class User
    {
        public uint IdUser { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Rut { get; set; }
        public string Password { get; set; }
    }
}
