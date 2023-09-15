using AutoMapper;
using BLL.Shared;
using CORE.DAL;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BLL.Services
{
    public class AddCategoryCommandHandler : IRequestHandler<AddCategoryCommand, APIResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public AddCategoryCommandHandler(IUnitOfWork _unitOfWork, IMapper _mapper)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
        }
        public async Task<APIResponse> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Category post = mapper.Map<Category>(request._CategoryInput);
                post = await unitOfWork._Category.AddAsync(post, cancellationToken, KeyCaching.None.ToString());
                return new APIResponse
                {
                    IsError = false,
                    Code = 200,
                    Message = "",
                    Data = post,
                };
            }
            catch (Exception ex)
            {
                return new APIResponse
                {
                    IsError = true,
                    Message = ex.Message,
                };
            }


        }
    }
}
