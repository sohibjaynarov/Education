using Education.Domain.Commons;
using Education.Domain.Configurations;
using Education.Service.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Education.Domain.Entities.Courses;
using Education.Service.DTOs.Courses;
using Education.Domain.Enums;

namespace Education.Api.Controllers
{
    [ApiController]
    [Route("api/courses")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService courseService;
        private readonly IWebHostEnvironment env;
        public CoursesController(ICourseService courseService, IWebHostEnvironment env)
        {
            this.courseService = courseService;
            this.env = env;
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<Course>>> Create([FromForm] CourseForCreationDto courseDto)
        {
            var result = await courseService.CreateAsync(courseDto);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<IEnumerable<Course>>>> GetAll([FromQuery] PaginationParams @params)
        {
            var result = await courseService.GetAllAsync(@params);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<Course>>> Get([FromRoute] Guid id)
        {
            var result = await courseService.GetAsync(p => p.Id == id);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse<Course>>> Update(Guid id, [FromForm] CourseForCreationDto courseDto)
        {
            var result = await courseService.UpdateAsync(id, courseDto);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse<bool>>> Delete(Guid id)
        {
            var result = await courseService.DeleteAsync(p => p.Id == id && p.State != ItemState.Deleted);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }
    }
}
