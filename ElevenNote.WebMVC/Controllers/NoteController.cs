using ElevenNote.Models;
using ElevenNote.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElevenNote.WebMVC.Controllers
{
    [Authorize]
    public class NoteController : Controller
    {
        // GET: Note
        public ActionResult Index()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new NoteService(userID);
            var model = service.GetNotes();

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NoteCreate model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var service = CreateNoteService();

            if (service.CreateNote(model))
            {
                TempData["SaveResult"] = "Note Created Successfully.";
                return RedirectToAction("index");
            };

            ModelState.AddModelError("", "Note could not be created.");

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var service = CreateNoteService();
            var model = service.GetNoteById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateNoteService();
            var detail = service.GetNoteById(id);
            var model =
                new NoteEdit
                {
                    NoteID = detail.NoteID,
                    Title = detail.Title,
                    Content = detail.Content
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, NoteEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.NoteID != id)
            {
                ModelState.AddModelError("", "ID Mismatch");
                return View(model);
            }

            var service = CreateNoteService();

            if (service.UpdateNote(model))
            {
                TempData["SaveResult"] = "Note Updated.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Note could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var service = CreateNoteService();
            var model = service.GetNoteById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateNoteService();
            service.DeleteNote(id);
            TempData["SaveResult"] = "Note Deleted.";
            return RedirectToAction("Index");
        }

        private NoteService CreateNoteService()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new NoteService(userID);
            return service;
        }
    }
}