using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class SocialMediaBLL
    {
        SocialMediaDAO dao = new SocialMediaDAO();
        public bool AddSocialMedia(SocialMediaDTO model)
        {
            SocialMedia media = new SocialMedia();
            media.Name = model.Name;
            media.Link = model.Link;
            media.ImagePath = model.ImagePath;
            media.AddDate = DateTime.Now;
            media.LastUpdateUserID = UserStatic.UserID;
            media.LastUpdateDate = DateTime.Now;
            int ID = dao.AddSocialMedia(media);
            LogBLL.AddLog(General.ProcessType.SocialAdd, General.TableName.Social, ID);
            return true;
        }

        public List<SocialMediaDTO> GetSocialMedias()
        {
            return dao.GetSocialMedias();
        }

        public SocialMediaDTO GetSocialMediaWithID(int ID)
        {
            return dao.GetSocialMediaWithID(ID);
        }

        public string UpdateSocialMedia(SocialMediaDTO model)
        {
            string oldImagepath = dao.UpdateSocialMedia(model);
            LogDAO.AddLog(General.ProcessType.SocialUpdate, General.TableName.Social, model.ID);
            return oldImagepath;
        }

        public string DeleteSocialMedia(int ID)
        {
            string imagepath = dao.DeleteSocialMedia(ID);
            LogDAO.AddLog(General.ProcessType.SocialDelete, General.TableName.Social, ID);
            return imagepath;
        }
    }
}
