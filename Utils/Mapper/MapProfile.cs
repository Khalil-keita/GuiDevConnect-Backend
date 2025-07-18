using AutoMapper;
using backEnd.Src.Dtos;
using backEnd.Src.Models;

namespace backEnd.Utils.Mapper
{
    public class MapProfile: Profile
    {
        public MapProfile()
        {
            CreateMap<UserBookmark, BookMarkDto>();
            CreateMap<UserActivity, UserActivityDto>();
            CreateMap<UserPreference, UserPreferencesDto>();
            CreateMap<UserStatistic, UserStatisticsDto>();
            CreateMap<User, UserDto>();
        }
    }
}
