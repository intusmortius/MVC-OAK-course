using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class AdsDAO : PostContext
    {
        public int AddAds(Ad ads)
        {
            try
            {
                db.Ads.Add(ads);
                db.SaveChanges();
                return ads.ID;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<AdsDTO> GetAds()
        {
            List<AdsDTO> list = new List<AdsDTO>();
            List<Ad> adslist = db.Ads.Where(x => x.isDeleted == false).OrderBy(x => x.AddDate).ToList();
            foreach (var item in adslist)
            {
                AdsDTO dto = new AdsDTO();
                dto.ID = item.ID;
                dto.Name = item.Name;
                dto.ImagePath = item.ImagePath;
                dto.Link = item.Link;
                dto.Imagesize = item.Size;
                list.Add(dto);
            }
            return list;
        }

        public string UpdateAds(AdsDTO model)
        {
            try
            {
                Ad ads = db.Ads.First(x => x.ID == model.ID);
                string oldImagePath = ads.ImagePath;
                ads.Name = model.Name;
                ads.Link = model.Link;
                if (model.ImagePath != null)
                {
                    ads.ImagePath = model.ImagePath;
                }
                ads.Size = model.Imagesize;
                ads.LastUpdateDate = DateTime.Now;
                ads.LastUpdateUserID = UserStatic.UserID;
                db.SaveChanges();
                return oldImagePath;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public string DeleteAds(int ID)
        {
            try
            {
                Ad ad = db.Ads.First(x => x.ID == ID);
                string imagepath = ad.ImagePath;
                ad.isDeleted = true;
                ad.DeletedDate = DateTime.Now;
                ad.LastUpdateUserID = UserStatic.UserID;
                ad.LastUpdateDate = DateTime.Now;
                db.SaveChanges();
                return imagepath;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public object GetAdsWithID(int ID)
        {
            Ad ad = db.Ads.First(x => x.ID == ID);
            AdsDTO dto = new AdsDTO();
            dto.ID = ad.ID;
            dto.Name = ad.Name;
            dto.Link = ad.Link;
            dto.ImagePath = ad.ImagePath;
            dto.Imagesize = ad.Size;
            return dto;
        }
    }
}
