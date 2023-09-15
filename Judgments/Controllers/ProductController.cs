using BLL.Services;
using BLL.Shared;
using CORE.DTO;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Judgments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("AddProduct", Name = "AddProduct")]
        public async Task<ActionResult<APIResponse>> Add(ProductInput Product)
        {
            try
            {
                var dtos = await _mediator.Send(new AddProductCommand() { _ProductInput = Product });
                return Ok(dtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse { IsError = true, Message = "", Data = ex.Message });
            }
        }
        [HttpPost("UpdateProduct", Name = "UpdateProduct")]
        public async Task<ActionResult<APIResponse>> Update(ProductInput Product)
        {
            try
            {
                var dtos = await _mediator.Send(new UpdateProductCommand() { _ProductInput = Product });
                return Ok(dtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse { IsError = true, Message = "", Data = ex.Message });
            }
        }
        [HttpGet("GetAllProduct", Name = "GetAllProduct")]
        public async Task<ActionResult<APIResponse>> GetAll()
        {
            try
            {
                var dtos = await _mediator.Send(new GetAllProductQuery());
                return Ok(dtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse { IsError = true, Message = "", Data = ex.Message });
            }
        }
        [HttpGet("GetProduct", Name = "GetProduct")]
        public async Task<ActionResult<APIResponse>> Get(int Id)
        {
            try
            {
                var dtos = await _mediator.Send(new GetByIdProductQuery() { Id = Id });
                return Ok(dtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse { IsError = true, Message = "", Data = ex.Message });
            }
        }
        [HttpDelete("DeleteProduct", Name = "DeleteProduct")]
        public async Task<ActionResult<APIResponse>> Delete(int Id)
        {
            try
            {
                var dtos = await _mediator.Send(new DeleteProductCommand() { Id = Id });
                return Ok(dtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse { IsError = true, Message = "", Data = ex.Message });
            }
        }
    }
}

