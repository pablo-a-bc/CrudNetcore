using ApiAgendamientoWeb.Servicios;
using CrudNetcore.Datos;
using CrudNetcore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace CrudNetcore.Services
{
    public class CrudService
    {
        public async Task<Response> Insert(UsuarioModel input)
        {
            Response response = new Response();
            ValidatorService valService = new ValidatorService();
            try
            {
                await Task.Run(() =>
                 {
                     using (crudContext db = new crudContext())
                     {
                         string rut = input.Rut;
                         string email = input.Email;
                         if (valService.ValidaRut(rut))
                         {
                             if (valService.IsValidEmail(email))
                             {
                                 var val = db.
                                 User.
                                 FirstOrDefault(x => x.Rut == rut);
                                 if (val == null)
                                 {
                                     string _secure_pass = valService.Encriptar(input.password);
                                     User user = new User()
                                     {
                                         Name = input.Nombre,
                                         Password = _secure_pass,
                                         Email = email,
                                         Rut = rut

                                     };
                                     db.User.Add(user);
                                     db.SaveChanges();
                                     response.Code = 200;
                                     response.Status = true;
                                     response.Message = "Ok realizado";
                                 }
                                 else
                                 {
                                     response.Code = 407;
                                     response.Status = false;
                                     response.Message = "Usuario ya existe en el sistema";
                                 }
                             }
                             else
                             {
                                 response.Code = 406;
                                 response.Status = false;
                                 response.Message = "Email no válido";
                             }
                         }
                         else
                         {
                             response.Code = 405;
                             response.Status = false;
                             response.Message = "Rut no válido";
                         }

                     };
                 });
            }
            catch (Exception ex)
            {
                response.Code = 500;
                response.Status = false;
                response.Message = "Ha ocurrido una excepción " + ex;
            }

            return response;
        }


        public async Task<UserResponse> UsuarioShow()
        {
            UserResponse response = new UserResponse();
            try
            {
                await Task.Run(() =>
                {
                    using (crudContext db = new crudContext())
                    {
                        var list = db.User.ToList();
                        if(list.Count > 0)
                        {
                            List <UsuarioShow> lista = new List<UsuarioShow>();
                            foreach(var bus in list)
                            {
                                UsuarioShow user = new UsuarioShow
                                {
                                    id = (int)bus.IdUser,
                                    Nombre = bus.Name,
                                    Email= bus.Email,
                                    Rut = bus.Rut,

                                };
                                lista.Add(user);

                            }
                            response.Data = lista;
                            response.Code = 200;
                            response.Status = true;
                            response.Message = "Ok realizado";
                        }
                        else
                        {
                            response.Code = 404;
                            response.Status = false;
                            response.Message = "No hay usuarios";
                        }

                    };

                });
            }
            catch (Exception ex)
            {
                response.Code = 500;
                response.Status = false;
                response.Message = "Ha ocurrido una excepción " + ex;

            }
            return response;
        }


        public async Task<Response> Deleteuser(int id)
        {
            Response response = new Response();
            try
            {
                await Task.Run(() =>
                {
                    using (crudContext db = new crudContext())
                    {
                        var search = db.
                        User.
                        FirstOrDefault(x => x.IdUser == id);
                        if(search!=null)
                        {
                            db.User.Remove(search);
                            db.SaveChanges();
                            response.Code = 200;
                            response.Status = true;
                            response.Message = "Ok realizado";
                        }
                        else
                        {
                            response.Code = 404;
                            response.Status = false;
                            response.Message = "Id no encontrado";
                        }
    
                     };
                });
            }
            catch (Exception ex)
            {
                response.Code = 500;
                response.Status = false;
                response.Message = "Ha ocurrido una excepción " + ex;

            }

            return response;
        }

        public async Task<Response> updateuser(UsuarioEdit input)
        {
            Response response = new Response();
            ValidatorService valService = new ValidatorService();
            try
            {
                await Task.Run(() =>
                {
                    using (crudContext db = new crudContext())
                    {
                        var search = db.User.FirstOrDefault(x => x.IdUser == input.id);
                        if(search!=null)
                        {
                            if (valService.IsValidEmail(input.Email))
                            {
                                search.Name = input.Nombre.Trim() == "" ? search.Name : input.Nombre;
                                search.Email = input.Email;
                                search.Password = input.Nombre.Trim() == "" ? search.Name : valService.Encriptar(input.password);
                                db.Entry(search).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                db.SaveChanges();
                                response.Code = 200;
                                response.Status = true;
                                response.Message = "Realizado ok";


                            }
                            else
                            {
                                response.Code = 404;
                                response.Status = false;
                                response.Message = "Email no válido";
                            }

                        }
                        else
                        {
                            response.Code = 404;
                            response.Status = false;
                            response.Message = "No existe usuario";
                        }

                    };
                });
            }
            catch (Exception ex)
            {
                response.Code = 500;
                response.Status = false;
                response.Message = "Ha ocurrido una excepción " + ex;

            }

            return response;

        }

    }
}
