using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Areas.Admin.Controllers
{
    public class FavController : BaseController
    {
        FavBLL bll = new FavBLL();
        public ActionResult UpdateFav()
        {
            return View(bll.GetFav());
        }

        [HttpPost]
        public ActionResult UpdateFav(FavDTO model)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.ProcessType = General.Messages.EmptyArea;
            }
            else
            {
                if (model.FavImage != null)
                {
                    string filename = "";
                    HttpPostedFileBase postedfile = model.FavImage;
                    Bitmap FavImage = new Bitmap(postedfile.InputStream);
                    Bitmap resizefavImage = new Bitmap(FavImage, 100, 100);
                    string ext = Path.GetExtension(postedfile.FileName);
                    if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".ico")
                    {
                        string uniquenumber = Guid.NewGuid().ToString();
                        filename = uniquenumber + postedfile.FileName;
                        resizefavImage.Save(Server.MapPath("~/Areas/Admin/Content/FavImage/" + filename));
                        model.Fav = filename;
                    }
                    else
                    {
                        ViewBag.ProcessState = General.Messages.ExtensionError;
                    }
                }
                if (model.LogoImage != null)
                {
                    string filename = "";
                    HttpPostedFileBase postedfile = model.LogoImage;
                    Bitmap LogoImage = new Bitmap(postedfile.InputStream);
                    Bitmap resizefavImage = new Bitmap(LogoImage, 100, 100);
                    string ext = Path.GetExtension(postedfile.FileName);
                    if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".ico")
                    {
                        string uniquenumber = Guid.NewGuid().ToString();
                        filename = uniquenumber + postedfile.FileName;
                        resizefavImage.Save(Server.MapPath("~/Areas/Admin/Content/FavImage/" + filename));
                        model.Logo = filename;
                    }
                    else
                    {
                        ViewBag.ProcessState = General.Messages.ExtensionError;
                    }
                }
                FavDTO returndto= bll.UpdateFav(model);
                if (model.FavImage != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/FavImage/" + returndto.Fav)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/FavImage/" + returndto.Fav));
                    }
                }

                if (model.LogoImage != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/FavImage/" + returndto.Logo)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/FavImage/" + returndto.Logo));
                    }
                }

                ViewBag.ProcessState = General.Messages.UpdateSuccess;
            }
            return View(model);
        }
    }
}
