using AutoMapper;
using BLL.Shared;
using CORE.DAL;
using MediatR;
using Microsoft.Extensions.Hosting;

namespace BLL.Services
{
    public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQuery, APIResponse>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public GetByIdProductQueryHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<APIResponse> Handle(GetByIdProductQuery request, CancellationToken cancellationToken)
        {
            try
            {
               
                var Result = await _uow._Product.GetByIdAsync(cancellationToken, KeyCaching.None.ToString(), x=>x.Id==request.Id);

                if (Result == null)
                {
                    return new APIResponse
                    {
                        IsError = false,
                        Code = 404,
                        Message = "",
                        Data = Result,
                    };
                }
                Result = _mapper.Map<Product>(Result);
                return new APIResponse
                {
                    IsError = true,
                    Code = 200,
                    Message = "",
                    Data = Result
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
