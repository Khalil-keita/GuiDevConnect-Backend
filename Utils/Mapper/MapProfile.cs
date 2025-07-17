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
            CreateMap<UserPreferences, UserPreferencesDto>();
            CreateMap<UserStatistics, UserStatisticsDto>();
            CreateMap<User, UserDto>();
        }
    }
}
