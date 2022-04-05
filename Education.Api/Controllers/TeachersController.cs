using Education.Domain.Commons;
using Education.Domain.Configurations;
using Education.Service.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Education.Domain.Enums;
using Education.Service.DTOs.Teachers;
using Education.Domain.Entities.Teachers;

namespace Education.Api.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class TeachersController : ControllerBase
    {
        private readonly ITeacherService teacherService;
        private readonly IWebHostEnvironment env;
        public TeachersController(ITeacherService teacherService, IWebHostEnvironment env)
        {
            this.teacherService = teacherService;
            this.env = env;
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<Teacher>>> Create([FromForm] TeacherForCreationDto teacherDto)
        {
            var result = await teacherService.CreateAsync(teacherDto);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<IEnumerable<Teacher>>>> GetAll([FromQuery] PaginationParams @params)
        {
            var result = await teacherService.GetAllAsync(@params);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<Teacher>>> Get([FromRoute] Guid id)
        {
            var result = await teacherService.GetAsync(p => p.Id == id);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse<Teacher>>> Update(Guid id, [FromForm] TeacherForCreationDto teacherDto)
        {
            var result = await teacherService.UpdateAsync(id, teacherDto);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse<bool>>> Delete(Guid id)
        {
            var result = await teacherService.DeleteAsync(p => p.Id == id && p.State != ItemState.Deleted);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }
    }
}
