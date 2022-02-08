using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace DAL
{
    public class UserDAO : PostContext
    {
        public UserDTO GetUserWithUsernameAndPassword(UserDTO model)
        {
            UserDTO dto = new UserDTO();
            T_User user = db.T_User.FirstOrDefault(x => x.Username == model.Username && x.Password == model.Password);
            if(user != null && user.ID != 0)
            {
                dto.ID = user.ID;
                dto.Username = user.Username;
                dto.Password = user.Password;
                dto.Email = user.Email;
                dto.Imagepath = user.ImagePath;
                dto.Name = user.NameSurname;
                dto.isAdmin = user.isAdmin;
            }
            return dto;
        }

        public int AddUser(T_User user)
        {
            try
            {
                db.T_User.Add(user);
                db.SaveChanges();
                return user.ID;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<UserDTO> GetUsers()
        {
            List<T_User> list = db.T_User.Where(x => x.isDeleted == false).OrderBy(x => x.AddDate).ToList();
            List<UserDTO> userList = new List<UserDTO>();
            foreach (var item in list)
            {
                UserDTO dto = new UserDTO();
                dto.ID = item.ID;
                dto.Name = item.NameSurname;
                dto.Username = item.Username;
                dto.Imagepath = item.ImagePath;
                userList.Add(dto);
            }
            return userList;
        }

        public string DeleteUser(int ID)
        {
            try
            {
                T_User user = db.T_User.First(x => x.ID == ID);
                user.isDeleted = true;
                user.DeletedDate = DateTime.Now;
                user.LastUpdateUserID = UserStatic.UserID;
                user.LastUpdateDate = DateTime.Now;
                db.SaveChanges();
                return user.ImagePath;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public string UpdateUser(UserDTO model)
        {
            T_User user = db.T_User.First(x => x.ID == model.ID);
            string oldImagePath = user.ImagePath;
            if (model.Imagepath != null)
                user.ImagePath = model.Imagepath;
            user.NameSurname = model.Name;
            user.Username = model.Username;
            user.Email = model.Email;
            user.Password = model.Password;
            user.LastUpdateDate = DateTime.Now;
            user.LastUpdateUserID = UserStatic.UserID;
            user.isAdmin = model.isAdmin;
            db.SaveChanges();
            return oldImagePath;
        }

        public UserDTO GetUserWithID(int ID)
        {
            UserDTO model = new UserDTO();
            T_User user = db.T_User.First(x => x.ID == ID);
            model.Username = user.Username;
            model.ID = user.ID;
            model.Name = user.NameSurname;
            model.Password = user.Password;
            model.isAdmin = user.isAdmin;   
            model.Email = user.Email;
            model.Imagepath = user.ImagePath;
            return model;
        }

        public UserDTO GetUserwithID(object iD)
        {
            throw new NotImplementedException();
        }
    }
}
