using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ToDoListNuevo.Models;

namespace ToDoListPruebaTecnica.Models
{
    public class Tarea
    {
        [Key]
        public int TaskID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool IsCompleted { get; set; }




        public int Id { get; set; }


        public virtual User User { get; set; }
    }
}