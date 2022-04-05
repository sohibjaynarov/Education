using Education.Domain.Commons;
using Education.Domain.Configurations;
using Education.Service.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Education.Domain.Entities.Groups;
using Education.Domain.Enums;
using Education.Service.DTOs.Groups;

namespace Education.Api.Controllers
{
    [ApiController]
    [Route("api/groups")]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupService groupService;
        private readonly IWebHostEnvironment env;
        public GroupsController(IGroupService groupService, IWebHostEnvironment env)
        {
            this.groupService = groupService;
            this.env = env;
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<Group>>> Create(GroupForCreationDto groupDto)
        {
            var result = await groupService.CreateAsync(groupDto);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<IEnumerable<Group>>>> GetAll([FromQuery] PaginationParams @params)
        {
            var result = await groupService.GetAllAsync(@params);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<Group>>> Get([FromRoute] Guid id)
        {
            var result = await groupService.GetAsync(p => p.Id == id);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse<Group>>> Update(Guid id, GroupForCreationDto groupDto)
        {
            var result = await groupService.UpdateAsync(id, groupDto);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse<bool>>> Delete(Guid id)
        {
            var result = await groupService.DeleteAsync(p => p.Id == id && p.State != ItemState.Deleted);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }
    }
}
