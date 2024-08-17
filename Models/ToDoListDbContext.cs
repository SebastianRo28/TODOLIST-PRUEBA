using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ToDoListNuevo.Models;

namespace ToDoListPruebaTecnica.Models
{
    public class ToDoListDbContext : DbContext
    {
        public DbSet<Tarea> Tarea { get; set; }
        public DbSet<User> Users { get; set; }

        public ToDoListDbContext() : base("name=DefaultConnection") {
            
        }

    }
}