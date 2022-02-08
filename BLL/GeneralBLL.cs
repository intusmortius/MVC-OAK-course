using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class GeneralBLL
    {
        GeneralDAO dao = new GeneralDAO();
        AdsDAO adsdao= new AdsDAO();
        CategoryDAO categorydao = new CategoryDAO();
        AddressDAO AddressDAO = new AddressDAO();
        public GeneralDTO GetAllPosts()
        {
            GeneralDTO dto = new GeneralDTO();
            dto.SliderPost = dao.GetSliderPosts();
            dto.BreakingPost = dao.GetBreakingPosts();
            dto.PopularPost = dao.GetPopularPosts();
            dto.MostViewedPost = dao.GetMostViewedPosts();
            dto.Videos = dao.GetVideos();
            dto.Adslist = adsdao.GetAds();
            return dto;
        }

        public GeneralDTO GetPostDetailPageItemsWithID(int ID)
        {
            GeneralDTO dto = new GeneralDTO();
            dto.BreakingPost = dao.GetBreakingPosts();
            dto.Adslist = adsdao.GetAds();
            dto.PostDetail = dao.GetPostDetail(ID);
            return dto;
        }

        public GeneralDTO GetCategoryPostList(string categoryName)
        {
            GeneralDTO dto = new GeneralDTO();
            dto.BreakingPost = dao.GetBreakingPosts();
            dto.Adslist = adsdao.GetAds();
            if (categoryName == "video")
            {
                dto.Videos = dao.GetAllVideos();
                dto.CategoryName = "video";
            }
            else
            {
                List<CategoryDTO> categorylist = categorydao.GetCategories();
                int categoryID = 0;
                foreach (var item in categorylist)
                {
                    if (categoryName==SeoLink.GenerateUrl(item.CategoryName))
                    {
                        categoryID = item.ID;
                        dto.CategoryName = item.CategoryName;
                        break;
                    }
                }
                dto.CategoryPostList = dao.GetCategoryPostList(categoryID);
            }
            return dto;
        }

        public string GetCategoryPostList()
        {
            throw new NotImplementedException();
        }

        public GeneralDTO GetContactPageItems()
        {
            GeneralDTO dto = new GeneralDTO();
            dto.BreakingPost = dao.GetBreakingPosts();
            dto.Adslist = adsdao.GetAds();
            dto.Address = AddressDAO.GetAddresses().First();
            return dto;
        }

        public GeneralDTO GetSearchPosts(string searchText)
        {
            GeneralDTO dto = new GeneralDTO();
            dto.BreakingPost = dao.GetBreakingPosts();
            dto.Adslist = adsdao.GetAds();
            dto.CategoryPostList = dao.GetSearchPost(searchText);
            dto.SearchText = searchText;
            return dto;
        }
    }
}
