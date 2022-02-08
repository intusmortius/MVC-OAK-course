﻿using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class GeneralDAO : PostContext
    {
        public List<PostDTO> GetSliderPosts()
        {
            List<PostDTO> dtolist = new List<PostDTO>();
            var list = (from p in db.Posts.Where(x => x.Slider == true && x.isDeleted == false)
                        join c in db.Categories on p.CategoryID equals c.ID
                        select new
                        {
                            postID = p.ID,
                            title = p.Title,
                            categoryName = c.CategoryName,
                            seolink = p.SeoLink,
                            viewcount = p.ViewCount,
                            AddDate = p.AddDate
                        }).OrderByDescending(x => x.AddDate).Take(8).ToList();
            foreach (var item in list)
            {
                PostDTO dto = new PostDTO();
                dto.ID = item.postID;
                dto.Title = item.title;
                dto.CategoryName = item.categoryName;
                dto.ViewCount = item.viewcount;
                dto.AddDate = item.AddDate;
                dto.SeoLink = item.seolink;
                PostImage img = db.PostImages.First(x => x.PostID == item.postID && x.isDeleted == false);
                dto.ImagePath = img.ImagePath;
                dto.CommentCount = db.Comments.Where(x => x.isDeleted == false && x.PostID == item.postID && x.isApproved == true).Count();
                dtolist.Add(dto);
            }
            return dtolist;
        }

        public List<VideoDTO> GetAllVideos()
        {
            List<VideoDTO> dtolist = new List<VideoDTO>();
            List<Video> list = db.Videos.Where(x => x.isDeleted == false).OrderByDescending(x => x.AddDate).ToList();
            foreach (var item in list)
            {
                VideoDTO dto = new VideoDTO();
                dto.ID = item.ID;
                dto.Title = item.Title;
                dto.VideoPath = item.ViedoPath;
                dto.OriginalVideoPath = item.OriginalVideoPath;
                dto.AddDate = item.AddDate;
                dtolist.Add(dto);
            }
            return dtolist;
        }

        public List<PostDTO> GetCategoryPostList(int categoryID)
        {
            List<PostDTO> dtolist = new List<PostDTO>();
            var list = (from p in db.Posts.Where(x=> x.isDeleted == false && x.CategoryID == categoryID)
                        join c in db.Categories on p.CategoryID equals c.ID
                        select new
                        {
                            postID = p.ID,
                            title = p.Title,
                            categoryName = c.CategoryName,
                            seolink = p.SeoLink,
                            viewcount = p.ViewCount,
                            AddDate = p.AddDate
                        }).OrderByDescending(x => x.AddDate).ToList();
            foreach (var item in list)
            {
                PostDTO dto = new PostDTO();
                dto.ID = item.postID;
                dto.Title = item.title;
                dto.CategoryName = item.categoryName;
                dto.ViewCount = item.viewcount;
                dto.AddDate = item.AddDate;
                dto.SeoLink = item.seolink;
                PostImage img = db.PostImages.First(x => x.PostID == item.postID && x.isDeleted == false);
                dto.ImagePath = img.ImagePath;
                dto.CommentCount = db.Comments.Where(x => x.isDeleted == false && x.PostID == item.postID && x.isApproved == true).Count();
                dtolist.Add(dto);
            }
            return dtolist;
        }

        public List<PostDTO> GetSearchPost(string searchText)
        {
            List<PostDTO> dtolist = new List<PostDTO>();
            var list = (from p in db.Posts.Where(x => x.isDeleted == false && (x.Title.Contains(searchText) ||  x.PostContent.Contains(searchText)))
                        join c in db.Categories on p.CategoryID equals c.ID
                        select new
                        {
                            postID = p.ID,
                            title = p.Title,
                            categoryName = c.CategoryName,
                            seolink = p.SeoLink,
                            viewcount = p.ViewCount,
                            AddDate = p.AddDate
                        }).OrderByDescending(x => x.AddDate).ToList();
            foreach (var item in list)
            {
                PostDTO dto = new PostDTO();
                dto.ID = item.postID;
                dto.Title = item.title;
                dto.CategoryName = item.categoryName;
                dto.ViewCount = item.viewcount;
                dto.AddDate = item.AddDate;
                dto.SeoLink = item.seolink;
                PostImage img = db.PostImages.First(x => x.PostID == item.postID && x.isDeleted == false);
                dto.ImagePath = img.ImagePath;
                dto.CommentCount = db.Comments.Where(x => x.isDeleted == false && x.PostID == item.postID && x.isApproved == true).Count();
                dtolist.Add(dto);
            }
            return dtolist;
        }

        public PostDTO GetPostDetail(int ID)
        {
            Post post = db.Posts.First(x => x.ID == ID);
            post.ViewCount++;
            db.SaveChanges();
            PostDTO dto = new PostDTO();
            dto.ID = post.ID;
            dto.Title = post.Title;
            dto.ShortContent = post.ShortContent;
            dto.PostContent = post.PostContent;
            dto.Language = post.LanguageName;
            dto.SeoLink = post.SeoLink;
            dto.CategoryID = post.CategoryID;
            dto.CategoryName = (db.Categories.First(x => x.ID == dto.CategoryID)).CategoryName;
            List<PostImage> images = db.PostImages.Where(x => x.isDeleted == false && x.PostID == ID).ToList();
            List<PostImageDTO> imagelist = new List<PostImageDTO>();
            foreach (var item in images)
            {
                PostImageDTO img = new PostImageDTO();
                img.ID = item.ID;
                img.ImagePath = item.ImagePath;
                imagelist.Add(img);
            }
            dto.PostImages = imagelist;
            dto.CommentCount = db.Comments.Where(x => x.isDeleted == false && x.PostID == ID && x.isApproved).Count();
            List<Comment> comments = db.Comments.Where(x => x.isDeleted == false && x.PostID == ID && x.isApproved).ToList();
            List<CommentDTO> commentdtolist = new List<CommentDTO>();
            foreach (var item in comments)
            {
                CommentDTO cdto = new CommentDTO();
                cdto.ID = item.ID;
                cdto.AddDate = item.AddDate;
                cdto.CommentContent = item.CommentContent;
                cdto.Name = item.NameSurname;
                cdto.Email = item.Email;
                commentdtolist.Add(cdto);
            }
            dto.CommentList = commentdtolist;
            List<PostTag> tags = db.PostTags.Where(x=>x.isDeleted == false && x.PostID == ID).ToList();
            List<TagDTO> taglist = new List<TagDTO>();
            foreach (var item in tags)
            {
                TagDTO tdto = new TagDTO();
                tdto.ID = item.ID;
                tdto.TagContent = item.TagContent;
                taglist.Add(tdto);
            }
            dto.TagList = taglist;
            return dto;
        }

        public List<VideoDTO> GetVideos()
        {
            List<VideoDTO> dtolist = new List<VideoDTO>();
            List<Video> list = db.Videos.Where(x => x.isDeleted == false).OrderByDescending(x => x.AddDate).Take(3).ToList();
            foreach (var item in list)
            {
                VideoDTO dto = new VideoDTO();
                dto.ID = item.ID;
                dto.Title = item.Title;
                dto.VideoPath = item.ViedoPath;
                dto.OriginalVideoPath = item.OriginalVideoPath;
                dto.AddDate = item.AddDate;
                dtolist.Add(dto);
            }
            return dtolist;
        }

        public List<PostDTO> GetMostViewedPosts()
        {
            List<PostDTO> dtolist = new List<PostDTO>();
            var list = (from p in db.Posts.Where(x => x.isDeleted == false)
                        join c in db.Categories on p.CategoryID equals c.ID
                        select new
                        {
                            postID = p.ID,
                            title = p.Title,
                            categoryName = c.CategoryName,
                            seolink = p.SeoLink,
                            viewcount = p.ViewCount,
                            AddDate = p.AddDate
                        }).OrderByDescending(x => x.AddDate).Take(5).ToList();
            foreach (var item in list)
            {
                PostDTO dto = new PostDTO();
                dto.ID = item.postID;
                dto.Title = item.title;
                dto.CategoryName = item.categoryName;
                dto.ViewCount = item.viewcount;
                dto.AddDate = item.AddDate;
                dto.SeoLink = item.seolink;
                PostImage img = db.PostImages.First(x => x.PostID == item.postID && x.isDeleted == false);
                dto.ImagePath = img.ImagePath;
                dto.CommentCount = db.Comments.Where(x => x.isDeleted == false && x.PostID == item.postID && x.isApproved == true).Count();
                dtolist.Add(dto);
            }
            return dtolist;
        }

        public List<PostDTO> GetPopularPosts()
        {
            List<PostDTO> dtolist = new List<PostDTO>();
            var list = (from p in db.Posts.Where(x => x.Area2 == true && x.isDeleted == false)
                        join c in db.Categories on p.CategoryID equals c.ID
                        select new
                        {
                            postID = p.ID,
                            title = p.Title,
                            categoryName = c.CategoryName,
                            seolink = p.SeoLink,
                            viewcount = p.ViewCount,
                            AddDate = p.AddDate
                        }).OrderByDescending(x => x.AddDate).Take(5).ToList();
            foreach (var item in list)
            {
                PostDTO dto = new PostDTO();
                dto.ID = item.postID;
                dto.Title = item.title;
                dto.CategoryName = item.categoryName;
                dto.ViewCount = item.viewcount;
                dto.AddDate = item.AddDate;
                dto.SeoLink = item.seolink;
                PostImage img = db.PostImages.First(x => x.PostID == item.postID && x.isDeleted == false);
                dto.ImagePath = img.ImagePath;
                dto.CommentCount = db.Comments.Where(x => x.isDeleted == false && x.PostID == item.postID && x.isApproved == true).Count();
                dtolist.Add(dto);
            }
            return dtolist;
        }

        public List<PostDTO> GetBreakingPosts()
        {

            List<PostDTO> dtolist = new List<PostDTO>();
            var list = (from p in db.Posts.Where(x => x.isDeleted == false)
                        join c in db.Categories on p.CategoryID equals c.ID
                        select new
                        {
                            postID = p.ID,
                            title = p.Title,
                            categoryName = c.CategoryName,
                            seolink = p.SeoLink,
                            viewcount = p.ViewCount,
                            AddDate = p.AddDate
                        }).OrderByDescending(x => x.AddDate).Take(5).ToList();
            foreach (var item in list)
            {
                PostDTO dto = new PostDTO();
                dto.ID = item.postID;
                dto.Title = item.title;
                dto.CategoryName = item.categoryName;
                dto.ViewCount = item.viewcount;
                dto.AddDate = item.AddDate;
                dto.SeoLink = item.seolink;
                PostImage img = db.PostImages.First(x => x.PostID == item.postID && x.isDeleted == false);
                dto.ImagePath = img.ImagePath;
                dto.CommentCount = db.Comments.Where(x => x.isDeleted == false && x.PostID == item.postID && x.isApproved == true).Count();
                dtolist.Add(dto);
            }
            return dtolist;

        }
    }
}
