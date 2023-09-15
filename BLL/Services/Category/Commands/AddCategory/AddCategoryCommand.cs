
using BLL.Shared;
using CORE.DTO;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BLL.Services
{
    public class AddCategoryCommand : IRequest<APIResponse>
    {
        [Required]
        public CategoryInput _CategoryInput { get; set; }

    }
}
