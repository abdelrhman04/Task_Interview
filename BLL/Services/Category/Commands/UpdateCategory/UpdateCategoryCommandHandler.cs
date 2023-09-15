using AutoMapper;
using BLL.Shared;
using CORE.DAL;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BLL.Services
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, APIResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public UpdateCategoryCommandHandler(IUnitOfWork _unitOfWork, IMapper _mapper)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
        }
        public async Task<APIResponse> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Category post = await unitOfWork._Category.GetByIdAsync(cancellationToken,
                      KeyCaching.None.ToString(),
                    x =>x.Id== request._CategoryInput.Id);
               if(post == null)
                {
                    return new APIResponse
                    {
                        IsError = false,
                        Code = 404,
                        Message = "",
                        Data = post,
                    };
                }
                post = mapper.Map<Category>(request._CategoryInput);
                post = await unitOfWork._Category.
                    UpdateAsync(post, cancellationToken, KeyCaching.None.ToString());
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
