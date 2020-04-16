using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Npgsql;

/// <summary>
/// Descripción breve de DAOPersona
/// </summary>
public class DAOPersona
{
    public EPersona Login_Vasbor(EPersona user)
    {
        using (var db = new Mapeo())
        {

            return db.persona.Where(x => x.Username.Equals(user.Username) && x.Clave.Equals(user.Clave)).FirstOrDefault();
        }
    }
    public void InsertarUsuario(EPersona user)
    {
        using (var db = new Mapeo())
        {
            db.persona.Add(user);
            db.SaveChanges();
            
        }
        
    }
   
    public List<EPersona> obtenerUsuarios()
    {
        using (var db = new Mapeo())
        {
            return (from uu in db.persona
                    join rol in db.rol on uu.Id_rol equals rol.Id

                    select new
                    {
                        uu,
                        rol
                    }).ToList().Select(m => new EPersona
                    {
                        Identificacion = m.uu.Identificacion,
                        Nombre = m.uu.Nombre,
                        Apellido = m.uu.Apellido,
                        Telefono = m.uu.Telefono,
                        Username = m.uu.Username,
                        Clave = m.uu.Clave,
                        Correo = m.uu.Correo,
                        Descripcion_Rol = m.rol.Descripcion_rol,
                        Id_rol = m.uu.Id_rol



                    }).ToList();
        }
    }
}