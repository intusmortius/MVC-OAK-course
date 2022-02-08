using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;

namespace BLL
{
    public class UserBLL
    {
        UserDAO dao = new UserDAO();
        public UserDTO GetUserWithUsernameAndPassword(UserDTO model)
        {
            return dao.GetUserWithUsernameAndPassword(model); ;
        }

        public bool AddUser(UserDTO model)
        {
            T_User user = new T_User();
            user.AddDate = DateTime.Now;
            user.Username = model.Username;
            user.Password = model.Password;
            user.Email = model.Email;
            user.ImagePath = model.Imagepath;
            user.NameSurname = model.Name;
            user.isAdmin = model.isAdmin;
            user.isDeleted = false;
            user.LastUpdateUserID = UserStatic.UserID;
            user.LastUpdateDate = DateTime.Now;
            int ID = dao.AddUser(user);
            LogBLL.AddLog(General.ProcessType.UserAdd, General.TableName.User, ID);
            return true;
        }

        public List<UserDTO> GetUsers()
        {
            return dao.GetUsers();
        }

        public UserDTO GetUserWithID(int ID)
        {
            return dao.GetUserWithID(ID);
        }

        public string UpdateUser(UserDTO model)
        {
            string oldImagePath = dao.UpdateUser(model);
            LogDAO.AddLog(General.ProcessType.UserUpdate, General.TableName.User, model.ID);
            return oldImagePath;
        }

        public string DeleteUser(int ID)
        {
            string imagepath = dao.DeleteUser(ID);
            LogDAO.AddLog(General.ProcessType.UserDelete, General.TableName.User, ID);
            return imagepath;
        }
    }
}
