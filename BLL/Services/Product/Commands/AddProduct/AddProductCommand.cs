
using BLL.Shared;
using CORE.DTO;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BLL.Services
{
    public class AddProductCommand : IRequest<APIResponse>
    {
        [Required]
        public ProductInput _ProductInput { get; set; }

    }
}
