using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiAgendamientoWeb.Filters;
using CrudNetcore.Models;
using CrudNetcore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrudNetcore.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiKeyAuth]
    [Produces("application/json")]
    [ApiController]
    public class CrudController : ControllerBase
    {
        CrudService Service = new CrudService();
        [HttpPost("Insert")]
        public async Task<Response> insert(UsuarioModel input)
        {
            var response = await Service.Insert(input);
            return response;
        }

        [HttpGet("List")]
        public async Task<UserResponse> Show()
        {
            var response = await Service.UsuarioShow();
            return response;
        }

        [HttpDelete("Delete")]
        public async Task<Response> DeleteUser(int idusuario)
        {
            var response = await Service.Deleteuser(idusuario);
            return response;
        }

        [HttpPut("Edit")]
        public async Task<Response> EditUser(UsuarioEdit input)
        {
            var response = await Service.updateuser(input);
            return response;
        }
    }


}
