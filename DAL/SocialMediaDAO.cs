using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SocialMediaDAO : PostContext
    {
        public int AddSocialMedia(SocialMedia media)
        {
            try
            {
                db.SocialMedias.Add(media);
                db.SaveChanges();
                return media.ID;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<SocialMediaDTO> GetSocialMedias()
        {
            try
            {
                List<SocialMediaDTO> list = new List<SocialMediaDTO>();
                List<SocialMedia> medias = db.SocialMedias.Where(x => x.isDeleted == false).ToList();
                foreach (SocialMedia media in medias)
                {
                    SocialMediaDTO newmedia = new SocialMediaDTO();
                    newmedia.ID = media.ID;
                    newmedia.Name = media.Name;
                    newmedia.Link = media.Link;
                    newmedia.ImagePath = media.ImagePath;
                    list.Add(newmedia);
                }   
                return list;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public string UpdateSocialMedia(SocialMediaDTO model)
        {
            try
            {
                SocialMedia media = db.SocialMedias.First(x => x.ID == model.ID);
                string oldImagePath = media.ImagePath;
                if (model.ImagePath != null)
                {
                    media.ImagePath = model.ImagePath;
                }
                media.Name = model.Name;
                media.Link = model.Link;
                media.LastUpdateDate = DateTime.Now;
                media.LastUpdateUserID = UserStatic.UserID;
                db.SaveChanges();
                return oldImagePath;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public string DeleteSocialMedia(int ID)
        {
            try
            {
                SocialMedia social = db.SocialMedias.First(x => x.ID == ID);
                string imagepath = social.ImagePath;
                social.isDeleted = true;
                social.DeletedDate = DateTime.Now;
                social.LastUpdateUserID = UserStatic.UserID;
                social.LastUpdateDate = DateTime.Now;
                db.SaveChanges();
                return imagepath;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public SocialMediaDTO GetSocialMediaWithID(int ID)
        {
            SocialMedia media = db.SocialMedias.First(x => x.ID == ID);
            SocialMediaDTO dto = new SocialMediaDTO();
            dto.ID = ID;
            dto.Name = media.Name;
            dto.Link = media.Link;
            dto.ImagePath = media.ImagePath;
            return dto;
        }
    }
}
