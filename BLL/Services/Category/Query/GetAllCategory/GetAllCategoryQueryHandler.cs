using AutoMapper;
using BLL.Shared;
using MediatR;

namespace BLL.Services
{
    public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQuery, APIResponse>
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper _mapper;
        public GetAllCategoryQueryHandler(IUnitOfWork _uow, IMapper mapper)
        {
            uow = _uow;
            _mapper = mapper;
        }
        public async Task<APIResponse> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
        {
            try
            {
                
                var Result = (await uow._Category.ListAllAsync
                    (cancellationToken, KeyCaching.None.ToString())).Select(
                    x=>new
                    { x.Name ,x.Id}
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
