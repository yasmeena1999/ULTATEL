using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Claims;
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

        public StudentsController(IStudentService studentService, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _studentService = studentService;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<StudentDto>>> GetAllStudents()
        {
            var userId = User.FindFirst("UserId")?.Value;
            var students = await _studentService.GetAllAsync();
            var userStudents = students.Where(s => s.AddedByUserId == userId);
            var studentDtos = _mapper.Map<IEnumerable<StudentDto>>(userStudents);
            return Ok(studentDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDto>> GetStudentById(int id)
        {
            var student = await _studentService.GetByIdAsync(id);
            if (student == null || student.AddedByUserId != _userManager.GetUserId(User))
            {
                return NotFound();
            }
            var studentDto = _mapper.Map<StudentDto>(student);
            return Ok(studentDto);
        }

        [HttpPost]
        public async Task<ActionResult<StudentDto>> AddStudent(StudentCreateDto studentDto)
        {
            var userId = User.FindFirst("UserId")?.Value;
            var student = _mapper.Map<Student>(studentDto);
            student.AddedByUserId = userId;
            student.CreatedAt = DateTime.UtcNow;
            student.UpdatedAt = DateTime.UtcNow;
            await _studentService.AddAsync(student);
            return CreatedAtAction(nameof(GetStudentById), new { id = student.Id }, studentDto);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> EditStudent(int id, StudentDto studentDto)
        {
            var student = await _studentService.GetByIdAsync(id);
            if (student == null || student.AddedByUserId != _userManager.GetUserId(User))
            {
                return NotFound();
            }

            _mapper.Map(studentDto, student);
            student.UpdatedAt = DateTime.UtcNow;

            await _studentService.UpdateAsync(student);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _studentService.GetByIdAsync(id);
            if (student == null || student.AddedByUserId != _userManager.GetUserId(User))
            {
                return NotFound();
            }

            await _studentService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<StudentDto>>> SearchStudents([FromQuery] StudentSearchDto searchDto)
        {
            var userId = User.FindFirst("UserId")?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var students = await _studentService.SearchAsync(searchDto, userId);

            if (students == null || !students.Any())
            {
                return NotFound();
            }

            var studentDtos = _mapper.Map<IEnumerable<StudentDto>>(students);

            return Ok(studentDtos);
        }
    }
}
