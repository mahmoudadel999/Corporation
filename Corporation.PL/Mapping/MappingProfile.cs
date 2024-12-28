using AutoMapper;
using Corporation.BLL.Models.Departments;
using Corporation.PL.ViewModels.Departments;

namespace Corporation.PL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Department

            CreateMap<DepartmentDetailsDto, DepartmentEditViewModel>()
                /*.ForMember(dest => dest.Name, config => config.MapFrom(src => src.Name))*/;

            CreateMap<DepartmentEditViewModel, UpdatedDepartmentDto>();
            CreateMap<DepartmentEditViewModel, CreatedDepartmentDto>();

            #endregion

            #region Employee

            #endregion

        }
    }
}
