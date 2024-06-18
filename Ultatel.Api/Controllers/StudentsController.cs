using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Ultatel.Core.Dtos;
using Ultatel.Core.Entities;
using Ultatel.Services.Services;

namespace Ultatel.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public StudentsController(IStudentService studentService, UserManager<ApplicationUser> userManager,IMapper mapper)
        {
            _studentService = studentService;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            var userId = _userManager.GetUserId(User);
            var students = await _studentService.GetStudentsAsync(userId);
            return Ok(students);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudent(int id)
        {
            var userId = _userManager.GetUserId(User);
            var student = await _studentService.GetStudentByIdAsync(id, userId);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent([FromBody] StudentCreateDto student)
        {
            var userId = _userManager.GetUserId(User);
           
           var StudentCreated= _mapper.Map<Student>(student);
            StudentCreated.AddedByUserId = userId;

            await _studentService.AddStudentAsync(StudentCreated);
            return Ok(StudentCreated);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, Student student)
        {
            var userId = _userManager.GetUserId(User);
            if (id != student.Id || userId != student.AddedByUserId)
            {
                return BadRequest();
            }
            await _studentService.UpdateStudentAsync(student);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var userId = _userManager.GetUserId(User);
            await _studentService.DeleteStudentAsync(id, userId);
            return NoContent();
        }
    }

}
