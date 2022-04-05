using Education.Domain.Commons;
using Education.Domain.Configurations;
using Education.Domain.Entities.Students;
using Education.Domain.Enums;
using Education.Service.DTOs.Students;
using Education.Service.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using SystemIO = System.IO;
using System.Threading.Tasks;

namespace Education.Api.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService studentService;
        private readonly IWebHostEnvironment env;
        public StudentsController(IStudentService studentService, IWebHostEnvironment env)
        {
            this.studentService = studentService;
            this.env = env;
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<Student>>> Create([FromForm] StudentForCreationDto studentDto)
        {
            var result = await studentService.CreateAsync(studentDto);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<IEnumerable<Student>>>> GetAll([FromQuery] PaginationParams @params)
        {
            var result = await studentService.GetAllAsync(@params);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<Student>>> Get([FromRoute] Guid id)
        {
            var result = await studentService.GetAsync(p => p.Id == id);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse<Student>>> Update(Guid id, [FromForm] StudentForCreationDto studentDto)
        {
            var result = await studentService.UpdateAsync(id, studentDto);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse<bool>>> Delete(Guid id)
        {
            var result = await studentService.DeleteAsync(p => p.Id == id && p.State != ItemState.Deleted);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }
    }
}
