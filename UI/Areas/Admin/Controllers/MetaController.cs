using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Areas.Admin.Controllers
{
    public class MetaController : BaseController
    {
        // GET: Admin/Meta
        public ActionResult Index()
        {
            return View();
        }

        MetaBLL bll = new MetaBLL();

        public ActionResult AddMeta()
        {
            return View(new MetaDTO());
        }

        [HttpPost]
        public ActionResult AddMeta(MetaDTO model)
        {
            ViewBag.ProcessState = General.Messages.EmptyArea;

            if (ModelState.IsValid)
            {
                ViewBag.ProcessState = General.Messages.GenralError;

                if (bll.AddMeta(model))
                {
                    ViewBag.ProcessState = General.Messages.AddSuccess;
                    ModelState.Clear();
                }
            }
            return View(new MetaDTO());
        }

        public ActionResult MetaList()
        {
            return View(bll.GetMetaData());
        }

        public ActionResult UpdateMeta(int ID)
        {
            return View(bll.GetMetaWithID(ID));
        }
        [HttpPost]
        public ActionResult UpdateMeta(MetaDTO model)
        {
            ViewBag.ProcessState = General.Messages.EmptyArea;

            if (ModelState.IsValid)
            {
                ViewBag.ProcessState = General.Messages.GenralError;

                if (bll.UpdateMeta(model))
                {
                    ViewBag.ProcessState = General.Messages.UpdateSuccess;
                }
            }
            return View(model);
        }
        public JsonResult DeleteMeta(int ID)
        {
            bll.DeleteMeta(ID);
            return Json("");
        }
    }
}