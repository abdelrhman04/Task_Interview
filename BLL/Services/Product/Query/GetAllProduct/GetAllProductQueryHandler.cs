using AutoMapper;
using BLL.Shared;
using MediatR;

namespace BLL.Services
{
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQuery, APIResponse>
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper _mapper;
        public GetAllProductQueryHandler(IUnitOfWork _uow, IMapper mapper)
        {
            uow = _uow;
            _mapper = mapper;
        }
        public async Task<APIResponse> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            try
            {
                
                var Result = (await uow._Product.ListAllAsync
                    (cancellationToken, KeyCaching.None.ToString())).Select(
                    x=>new
                    { x.Name ,x.Id,x.Category_Id}
                    );
                return new APIResponse
                {
                    IsError = true,
                    Code = 200,
                    Message = "",
                    Data = Result.ToList()
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
