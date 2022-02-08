using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Areas.Admin.Controllers
{
    public class AddressController : BaseController
    {

        AddressBLL bll = new AddressBLL();
        public ActionResult AddressList()
        {
            return View(bll.GetAdresses());
        }
        public ActionResult AddAddress()
        {
            return View(new AddressDTO());
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddAddress(AddressDTO model)
        {
            if (ModelState.IsValid)
            {
                if (bll.AddAddress(model))
                {
                    ViewBag.ProcessState = General.Messages.AddSuccess;
                    ModelState.Clear();
                    model = new AddressDTO();
                }
            }
            else
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }
            return View(model);
        }
        public ActionResult UpdateAddress(int ID)
        {
            return View(bll.GetAdresses().First(x => x.ID == ID));
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateAddress(AddressDTO model)
        {
            if (ModelState.IsValid)
            {
                if (bll.UpdateAddress(model))
                {
                    ViewBag.ProcessState = General.Messages.UpdateSuccess;
                } 
                else
                    ViewBag.ProcessState = General.Messages.GenralError;
            }
            else
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }
            return View(model);
        }
        public JsonResult DeleteAddress(int ID)
        {
            bll.DeleteAddress(ID);
            return Json("");
        }
    }
}