using BLL.Services;
using BLL.Shared;
using CORE.DAL;
using CORE.DTO;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Judgments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("AddCategory", Name = "AddCategory")]
        public async Task<ActionResult<APIResponse>> Add(CategoryInput Category)
        {
            try
            {
                var dtos = await _mediator.Send(new AddCategoryCommand() { _CategoryInput = Category });
                return Ok(dtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse { IsError = true, Message = "", Data = ex.Message });
            }
        }
        [HttpPost("UpdateCategory", Name = "UpdateCategory")]
        public async Task<ActionResult<APIResponse>> Update( CategoryInput Category)
        {
            try
            {
                var dtos = await _mediator.Send(new UpdateCategoryCommand() { _CategoryInput = Category });
                return Ok(dtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse { IsError = true, Message = "", Data = ex.Message });
            }
        }
        [HttpGet("GetAllCategory", Name = "GetAllCategory")]
        public async Task<ActionResult<APIResponse>> GetAll()
        {
            try
            {
                var dtos = await _mediator.Send(new GetAllCategoryQuery());
                return Ok(dtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse { IsError = true, Message = "", Data = ex.Message });
            }
        }
        [HttpGet("GetCategory", Name = "GetCategory")]
        public async Task<ActionResult<APIResponse>> Get(int Id)
        {
            try
            {
                var dtos = await _mediator.Send(new GetByIdCategoryQuery() { Id= Id });
                return Ok(dtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse { IsError = true, Message = "", Data = ex.Message });
            }
        }
        [HttpDelete("DeleteCategory", Name = "DeleteCategory")]
        public async Task<ActionResult<APIResponse>> Delete(int Id)
        {
            try
            {
                var dtos = await _mediator.Send(new DeleteCategoryCommand() { Id = Id });
                return Ok(dtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse { IsError = true, Message = "", Data = ex.Message });
            }
        }
    }
}
