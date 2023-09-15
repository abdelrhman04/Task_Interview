using AutoMapper;
using BLL.Shared;
using CORE.DAL;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BLL.Services
{
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, APIResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public AddProductCommandHandler(IUnitOfWork _unitOfWork, IMapper _mapper)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
        }
        public async Task<APIResponse> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Product post = mapper.Map<Product>(request._ProductInput);
                post = await unitOfWork._Product.AddAsync(post, cancellationToken, KeyCaching.None.ToString());
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
