using BLL.Shared;
using MediatR;
namespace BLL.Services
{
    public class GetByIdProductQuery : IRequest<APIResponse>
    {
        public int Id { get; set; }
    }
}
