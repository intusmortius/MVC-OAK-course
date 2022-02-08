using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class MetaDAO : PostContext
    {
        public int AddMeta(Meta meta)
        {
            try
            {
                db.Metas.Add(meta);
                db.SaveChanges();
                return meta.ID;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<MetaDTO> GetMetaData()
        {
            List<MetaDTO> model = new List<MetaDTO>();
            List<Meta> list = db.Metas.Where(x => x.isDeleted == false).OrderBy(x => x.AddDate).ToList();

            foreach (var item in list)
            {
                MetaDTO meta = new MetaDTO();
                meta.MetaID = item.ID;
                meta.Name = item.Name;
                meta.MetaContent = item.MetaContent;
                model.Add(meta);
            }
            return model;
        }

        public MetaDTO GetMetaWithID(int ID)
        {
            try
            {
                MetaDTO model = new MetaDTO();
                Meta meta = db.Metas.First(x => x.ID == ID);
                model.Name = meta.Name;
                model.MetaContent = meta.MetaContent;
                model.MetaID = ID;
                return model;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void DeleteMeta(int ID)
        {
            try
            {
                Meta meta = db.Metas.First(x=>x.ID == ID);
                meta.isDeleted = true;
                meta.DeletedDate = DateTime.Now;
                meta.LastUpdateUserID = UserStatic.UserID;
                meta.LastUpdateDate = DateTime.Now;
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void UpdateMeta(MetaDTO model)
        {
            try
            {
                Meta meta = db.Metas.First(x => x.ID == model.MetaID);
                meta.Name = model.Name;
                meta.MetaContent = model.MetaContent;
                meta.LastUpdateDate = DateTime.Now;
                meta.LastUpdateUserID = UserStatic.UserID;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
