using BLL.Shared;
using MediatR;

namespace BLL.Services
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, APIResponse>
    {
        private readonly IUnitOfWork uow;
        public DeleteCategoryCommandHandler(IUnitOfWork _uow)
        {
            uow = _uow;
        }
        public async Task<APIResponse> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
               
                var DeleteItem = await uow._Category.GetByIdAsync(cancellationToken,
                   KeyCaching.None.ToString()
                    , x => x.Id == request.Id);
                if (DeleteItem == null)
                {
                    return new APIResponse
                    {
                        IsError = false,
                        Code = 404,
                        Message = "Element Not Found"
                    };
                }
                await uow._Category.DeleteAsync(DeleteItem, cancellationToken
                    , KeyCaching.None.ToString());
                return new APIResponse
                {
                    IsError = true,
                    Code = 200,
                    Message = "Element Deleted"
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
