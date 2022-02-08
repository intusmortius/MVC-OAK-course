using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class PostDAO
    {
        public int AddPost(Post post)
        {
            try
            {
                using (PostdataEntities db = new PostdataEntities())
                {
                    db.Posts.Add(post);
                    db.SaveChanges();
                }
                return post.ID;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int AddImage(PostImage item)
        {
            try
            {
                using (PostdataEntities db = new PostdataEntities())
                {
                    db.PostImages.Add(item);
                    db.SaveChanges();
                }

                return item.ID;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int AddTag(PostTag tag)
        {
            try
            {
                using (PostdataEntities db = new PostdataEntities())
                {
                    db.PostTags.Add(tag);
                    db.SaveChanges();
                }

                return tag.ID;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<PostDTO> GetPost()
        {
            List<PostDTO> dtolist = new List<PostDTO>();
            using (PostdataEntities db = new PostdataEntities())
            {
                var postlist = (from p in db.Posts.Where(x => x.isDeleted == false)
                                join c in db.Categories on p.CategoryID equals c.ID
                                select new
                                {
                                    ID = p.ID,
                                    Title = p.Title,
                                    categoryName = c.CategoryName,
                                    AddDate = p.AddDate
                                }).OrderByDescending(x => x.AddDate).ToList();

                foreach (var item in postlist)
                {
                    PostDTO dto = new PostDTO();
                    dto.Title = item.Title;
                    dto.ID = item.ID;
                    dto.CategoryName = item.categoryName;
                    dto.AddDate = item.AddDate;
                    dtolist.Add(dto);
                }
            }

            return dtolist;
        }

        public CountDTO GetAllCounts()
        {
            CountDTO dto = new CountDTO();
            using (PostdataEntities db = new PostdataEntities())
            {
                dto.PostCount = db.Posts.Where(x => x.isDeleted == false).Count();
                dto.CommentCount = db.Comments.Where(x => x.isDeleted == false).Count();
                dto.MessageCount = db.Contacts.Where(x => x.isDeleted == false).Count();
                dto.ViewCount = db.Posts.Where(x => x.isDeleted == false).Sum(x => x.ViewCount);
            }

            return dto;
        }

        public List<CommentDTO> GetAllComments()
        {
            List<CommentDTO> dtolist = new List<CommentDTO>();
            using (PostdataEntities db = new PostdataEntities())
            {
                var list = (from c in db.Comments.Where(x => x.isDeleted == false)
                            join p in db.Posts on c.PostID equals p.ID
                            select new
                            {
                                ID = c.ID,
                                PostTitle = p.Title,
                                Email = c.Email,
                                Content = c.CommentContent,
                                AddDate = c.AddDate,
                                isApproved = c.isApproved
                            }).OrderBy(x => x.AddDate).ToList();
                foreach (var item in list)
                {
                    CommentDTO dto = new CommentDTO();
                    dto.ID = item.ID;
                    dto.PostTitle = item.PostTitle;
                    dto.Email = item.Email;
                    dto.CommentContent = item.Content;
                    dto.AddDate = item.AddDate;
                    dto.isApproved = item.isApproved;
                    dtolist.Add(dto);
                }
            }

            return dtolist;
        }

        public void DeleteComment(int ID)
        {
            using (PostdataEntities db = new PostdataEntities())
            {
                Comment cmt = db.Comments.First(x => x.ID == ID);
                cmt.isDeleted = true;
                cmt.DeletedDate = DateTime.Now;
                cmt.LastUpdateDate = DateTime.Now;
                cmt.LastUpdateUserID = UserStatic.UserID;
                db.SaveChanges();
            }
        }

        public void ApproveComment(int ID)
        {
            using (PostdataEntities db = new PostdataEntities())
            {
                Comment comment = db.Comments.First(x => x.ID == ID);
                comment.isApproved = true;
                comment.ApproveUserID = UserStatic.UserID;
                comment.ApproveDate = DateTime.Now;
                comment.LastUpdateDate = DateTime.Now;
                comment.LastUpdateUserID = UserStatic.UserID;
                db.SaveChanges();
            }
        }

        public List<CommentDTO> GetComments()
        {
            List<CommentDTO> dtolist = new List<CommentDTO>();
            using (PostdataEntities db = new PostdataEntities())
            {
                var list = (from c in db.Comments.Where(x => x.isDeleted == false && x.isApproved == false)
                            join p in db.Posts on c.PostID equals p.ID
                            select new
                            {
                                ID = c.ID,
                                PostTitle = p.Title,
                                Email = c.Email,
                                Content = c.CommentContent,
                                AddDate = c.AddDate
                            }).OrderBy(x => x.AddDate).ToList();
                foreach (var item in list)
                {
                    CommentDTO dto = new CommentDTO();
                    dto.ID = item.ID;
                    dto.PostTitle = item.PostTitle;
                    dto.Email = item.Email;
                    dto.CommentContent = item.Content;
                    dto.AddDate = item.AddDate;
                    dtolist.Add(dto);
                }
            }
            return dtolist;
        }

        public List<PostDTO> GetHotNews()
        {
            List<PostDTO> dtolist = new List<PostDTO>();
            using (PostdataEntities db = new PostdataEntities())
            {
                var postlist = (from p in db.Posts.Where(x => x.isDeleted == false && x.Area1 == true)
                                join c in db.Categories on p.CategoryID equals c.ID
                                select new
                                {
                                    ID = p.ID,
                                    Title = p.Title,
                                    categoryName = c.CategoryName,
                                    AddDate = p.AddDate,
                                    seolink = p.SeoLink
                                }).OrderByDescending(x => x.AddDate).Take(8).ToList();
                foreach (var item in postlist)
                {
                    PostDTO dto = new PostDTO();
                    dto.Title = item.Title;
                    dto.ID = item.ID;
                    dto.CategoryName = item.categoryName;
                    dto.AddDate = item.AddDate;
                    dto.SeoLink = item.seolink;
                    dtolist.Add(dto);
                }
            }
            return dtolist;
        }

        public int GetCommentCount()
        {
            int count = 0;
            using (PostdataEntities db = new PostdataEntities())
            {
                count = db.Comments.Where(x => x.isDeleted == false && x.isApproved == false).Count();
            }
            return count;
        }

        public int GetMessageCount()
        {
            int count = 0;
            using (PostdataEntities db = new PostdataEntities())
            {
                count = db.Contacts.Where(x => x.isDeleted == false && x.isRead == false).Count();
            }
            return count;
        }

        public void AddComment(Comment comment)
        {
            try
            {
                using (PostdataEntities db = new PostdataEntities())
                {
                    db.Comments.Add(comment);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public PostDTO GetPostWithID(int ID)
        {
            PostDTO dto = new PostDTO();
            using (PostdataEntities db = new PostdataEntities())
            {
                Post post = db.Posts.First(x => x.ID == ID);

                dto.ID = post.ID;
                dto.Title = post.Title;
                dto.ShortContent = post.ShortContent;
                dto.PostContent = post.PostContent;
                dto.Language = post.LanguageName;
                dto.Notification = post.Notification;
                dto.SeoLink = post.SeoLink;
                dto.Slider = post.Slider;
                dto.Area1 = post.Area1;
                dto.Area2 = post.Area2;
                dto.Area3 = post.Area3;
                dto.CategoryID = post.CategoryID;
            }
            return dto;
        }

        public List<PostImageDTO> GetPostImagesWithPostID(int PostID)
        {
            List<PostImageDTO> dtolist = new List<PostImageDTO>();
            using (PostdataEntities db = new PostdataEntities())
            {
                List<PostImage> list = db.PostImages.Where(x => x.isDeleted == false && x.PostID == PostID).ToList();

                foreach (var item in list)
                {
                    PostImageDTO dto = new PostImageDTO();
                    dto.ID = item.ID;
                    dto.ImagePath = item.ImagePath;
                    dtolist.Add(dto);
                }
            }
            return dtolist;
        }

        public List<PostTag> GetPostTagWithPostID(int PostID)
        {
            List<PostTag> list = new List<PostTag>();
            using (PostdataEntities db = new PostdataEntities())
            {
                list = db.PostTags.Where(x => x.isDeleted == false && x.PostID == PostID).ToList();
            }
            return list;
        }

        public void UpdatePost(PostDTO model)
        {
            using (PostdataEntities db = new PostdataEntities())
            {
                Post post = db.Posts.First(x => x.ID == model.ID);
                post.Title = model.Title;
                post.Area1 = model.Area1;
                post.Area2 = model.Area2;
                post.Area3 = model.Area3;
                post.CategoryID = model.CategoryID;
                post.LanguageName = model.Language;
                post.LastUpdateDate = DateTime.Now;
                post.LastUpdateUserID = UserStatic.UserID;
                post.Notification = model.Notification;
                post.PostContent = model.PostContent;
                post.SeoLink = model.SeoLink;
                post.ShortContent = model.ShortContent;
                post.Slider = model.Slider;
                db.SaveChanges();
            }
        }

        public string DeletePostImage(int ID)
        {
            try
            {
                string imagepath = "";
                using (PostdataEntities db = new PostdataEntities())
                {
                    PostImage img = db.PostImages.First(x => x.ID == ID);
                    imagepath = img.ImagePath;
                    img.isDeleted = true;
                    img.DeletedDate = DateTime.Now;
                    img.LastUpdateUserID = UserStatic.UserID;
                    img.LastUpdateDate = DateTime.Now;
                    db.SaveChanges();
                }
                return imagepath;
            }
            catch (Exception ex)
            {

                throw ex;
            };
        }

        public List<PostImageDTO> DeletePost(int ID)
        {
            List<PostImageDTO> dtolist = new List<PostImageDTO>();
            using (PostdataEntities db = new PostdataEntities())
            {
                Post post = db.Posts.First(x => x.ID == ID);
                post.isDeleted = true;
                post.DeletedDate = DateTime.Now;
                post.LastUpdateDate = DateTime.Now;
                post.LastUpdateUserID = UserStatic.UserID;
                db.SaveChanges();
                List<PostImage> imagelist = db.PostImages.Where(x => x.PostID == ID).ToList();
                
                foreach (var item in imagelist)
                {
                    PostImageDTO dto = new PostImageDTO();
                    dto.ImagePath = item.ImagePath;
                    item.isDeleted = true;
                    item.DeletedDate = DateTime.Now;
                    item.LastUpdateUserID = UserStatic.UserID;
                    item.LastUpdateDate = DateTime.Now;
                    dtolist.Add(dto);
                }
                db.SaveChanges();
            }
            return dtolist;
        }

        public void DeleteTags(int PostID)
        {
            using (PostdataEntities db = new PostdataEntities())
            {
                List<PostTag> list = db.PostTags.Where(x => x.isDeleted == false && x.PostID == PostID).ToList();
                foreach (var item in list)
                {
                    item.isDeleted = true;
                    item.DeletedDate = DateTime.Now;
                    item.LastUpdateUserID = UserStatic.UserID;
                    item.LastUpdateDate = DateTime.Now;
                }
                db.SaveChanges();
            }
        }
    }
}
