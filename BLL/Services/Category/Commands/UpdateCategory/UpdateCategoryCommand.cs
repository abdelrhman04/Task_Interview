using BLL.Shared;
using CORE.DTO;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BLL.Services
{
    public class UpdateCategoryCommand : IRequest<APIResponse>
    {
        public CategoryInput _CategoryInput { get; set; }
    }
}
