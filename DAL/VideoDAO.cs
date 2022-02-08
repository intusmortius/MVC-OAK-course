using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class VideoDAO : PostContext
    {
        public int AddVideo(Video video)
        {
            try
            {
                db.Videos.Add(video);
                db.SaveChanges();
                return video.ID;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<VideoDTO> GetVideos()
        {
            List<Video> list = db.Videos.Where(x => x.isDeleted == false).OrderBy(x => x.AddDate).ToList();
            List<VideoDTO> videolist = new List<VideoDTO>();
            foreach (Video video in list)
            {
                VideoDTO dto = new VideoDTO();
                dto.ID = video.ID;
                dto.Title = video.Title;
                dto.VideoPath = video.ViedoPath;
                dto.OriginalVideoPath = video.OriginalVideoPath;
                dto.AddDate = video.AddDate;
                videolist.Add(dto);
            }
            return videolist;
        }

        public void UpdateVideo(VideoDTO model)
        {
            try
            {
                Video video = db.Videos.First(x => x.ID == model.ID);
                video.ViedoPath = model.VideoPath;
                video.OriginalVideoPath = model.OriginalVideoPath;
                video.Title = model.Title;
                video.LastUpdateDate = DateTime.Now;
                video.LastUpdateUserID = UserStatic.UserID;
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void DeleteVideo(int ID)
        {
            try
            {
                Video video = db.Videos.First(x => x.ID == ID);
                video.isDeleted = true;
                video.DeletedDate = DateTime.Now;
                video.LastUpdateUserID = UserStatic.UserID;
                video.LastUpdateDate = DateTime.Now;
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public VideoDTO GetVideoWithID(int ID)
        {
            Video video = db.Videos.First(x => x.ID == ID);
            VideoDTO dto = new VideoDTO();
            dto.ID = video.ID;
            dto.OriginalVideoPath = video.OriginalVideoPath;
            dto.Title = video.Title;
            return dto;
        }
    }
}
