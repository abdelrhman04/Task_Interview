using BLL.Shared;
using MediatR;
namespace BLL.Services
{
    public class GetByIdCategoryQuery : IRequest<APIResponse>
    {
        public int Id { get; set; }
    }
}
