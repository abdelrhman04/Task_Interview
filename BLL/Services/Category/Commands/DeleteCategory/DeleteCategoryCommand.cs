
using BLL.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class DeleteCategoryCommand : IRequest<APIResponse>
    {
        public int Id { get; set; }
    }
}
