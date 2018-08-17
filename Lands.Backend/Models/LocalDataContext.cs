using Lands.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lands.Backend.Models
{
    //Este Datacontext lo usamos para el manejo de migraciones
    //y hereda del que esta en Domain
    public class LocalDataContext:DataContext
    {
        public System.Data.Entity.DbSet<Lands.Domain.User> Users { get; set; }
    }
}