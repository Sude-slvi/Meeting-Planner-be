using AutoMapper;
using MeetingPlanning.Application.DTOs;
using MeetingPlanning.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingPlanning.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Meeting, ListMeetingDto>()
                .ForMember(dest => dest.MeetingRoomName,
                    opt => opt.MapFrom(src => src.MeetingRoom != null ? src.MeetingRoom.Name : "Unknown Room"))
                .ForMember(dest => dest.UserName,
                    opt => opt.MapFrom(src => src.User != null ? src.User.FullName : "Unknown User"))
                .ForMember(dest => dest.InvitedUsers,
                    opt => opt.MapFrom(src => src.MeetingInvitations.Select(mi => new ListUsersDto
                    {
                        Id = mi.User.Id,
                        FullName = mi.User.FullName,
                        TeamName = mi.User.Team.Name,
                        Status = mi.Status
                        
                    })));

            CreateMap<User, ListUsersDto>()
                .ForMember(dest => dest.TeamName,
                    opt => opt.MapFrom(src => src.Team != null ? src.Team.Name : "Unknown Team"));

            CreateMap<MeetingInvitation, ListMeetingInvitationDto>()
                .ForMember(dest => dest.UserFullName,
                    opt => opt.MapFrom(src => src.User != null ? src.User.FullName : "Unknown User"))
                .ForMember(dest => dest.MeetingTitle,
                    opt => opt.MapFrom(src => src.Meeting != null ? src.Meeting.Title : "Unknown Meeting"))
                .ForMember(dest => dest.StartTime,
                    opt => opt.MapFrom(src => src.Meeting != null ? src.Meeting.StartTime : DateTime.MinValue))
                .ForMember(dest => dest.Duration,
                    opt => opt.MapFrom(src => src.Meeting != null ? src.Meeting.Duration : TimeSpan.Zero))
                .ForMember(dest => dest.EndTime,
                    opt => opt.MapFrom(src => src.Meeting != null ? src.Meeting.EndTime : DateTime.MinValue))
                .ForMember(dest => dest.MeetingRoomName,
                    opt => opt.MapFrom(src => src.Meeting != null ? src.Meeting.MeetingRoom.Name : "Unknown Meeting Room"));
        }
    }
}
