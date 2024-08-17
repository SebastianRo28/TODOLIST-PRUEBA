using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ToDoListPruebaTecnica.Models;

namespace ToDoListNuevo.Controllers
{
    public class TaskController : Controller
    {
        private ToDoListDbContext db = new ToDoListDbContext();

        public ActionResult Index()
        {
            var tasks = db.Tarea.ToList();
            return View(tasks);
        }

        [HttpPost]
        public ActionResult Create(Tarea task)
        {
            var userId = Convert.ToInt32(Request.Headers["UserID"]);
            if (ModelState.IsValid)
            {
                task.Id = userId;
                db.Tarea.Add(task);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    // Ver el mensaje de error
                    System.Diagnostics.Debug.WriteLine(error.ErrorMessage);
                }
            }

            return View(task);
        }

        [HttpPost]
        public ActionResult Edit(int id, string titulo, string descripcion)
        {
            var task = db.Tarea.Find(id);
            task.Title = titulo;
            task.Description = descripcion;
            if (task != null)
            {

                db.Entry(task).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true });
           

            }

            

            return View(task);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var task = db.Tarea.Find(id);
            db.Tarea.Remove(task);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult MarkAsComplete(int id)
        {
            var task = db.Tarea.Find(id);
            if (task != null)
            {
                task.IsCompleted = true;
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false, message = "Tarea no encontrada." });
        }
    }
}
