using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class FavBLL
    {
        FavDAO dao = new FavDAO();
        public object GetFav()
        {
            return dao.GetFav();
        }

        public FavDTO UpdateFav(FavDTO model)
        {
            FavDTO dto = dao.UpdateFav(model);
            LogDAO.AddLog(General.ProcessType.IconUpdate, General.TableName.Icon, dto.ID);
            return dto;
        }
    }
}
