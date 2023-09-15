using AutoMapper;
using BLL.Shared;
using CORE.DAL;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BLL.Services
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, APIResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public UpdateProductCommandHandler(IUnitOfWork _unitOfWork, IMapper _mapper)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
        }
        public async Task<APIResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            try
            {

                Product post = await unitOfWork._Product.GetByIdAsync(cancellationToken,
                      KeyCaching.None.ToString(),
                    x =>x.Id== request._ProductInput.Id);

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
                post = mapper.Map<Product>(request._ProductInput);

                post = await unitOfWork._Product.
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
