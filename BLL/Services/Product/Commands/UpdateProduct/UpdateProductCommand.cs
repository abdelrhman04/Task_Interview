using BLL.Shared;
using CORE.DTO;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BLL.Services
{
    public class UpdateProductCommand : IRequest<APIResponse>
    {
        public ProductInput _ProductInput { get; set; }
    }
}
