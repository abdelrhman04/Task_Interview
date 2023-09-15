using AutoMapper;
using CORE.DAL;
using CORE.DTO;

namespace BLL.Mapper;

public class Mapping :Profile
{
    public Mapping()
    {
        CreateMap<ProductOutput, Product>().ReverseMap();
        CreateMap<ProductInput, Product>().ReverseMap();

        CreateMap<CategoryOutput, Category>().ReverseMap();
        CreateMap<CategoryInput, Category>().ReverseMap();
    }
}
