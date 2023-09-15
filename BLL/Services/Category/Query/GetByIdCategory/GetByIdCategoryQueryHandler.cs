using AutoMapper;
using BLL.Shared;
using CORE.DAL;
using MediatR;

namespace BLL.Services
{
    public class GetByIdCategoryQueryHandler : IRequestHandler<GetByIdCategoryQuery, APIResponse>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public GetByIdCategoryQueryHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<APIResponse> Handle(GetByIdCategoryQuery request, CancellationToken cancellationToken)
        {
            try
            {
               
                var Result = await _uow._Category.GetByIdAsync(cancellationToken, KeyCaching.None.ToString(), x=>x.Id==request.Id);
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
                Result = _mapper.Map<Category>(Result);
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
