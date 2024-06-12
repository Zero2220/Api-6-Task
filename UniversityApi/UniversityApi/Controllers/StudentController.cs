using Microsoft.AspNetCore.Mvc;
using Services.Exceptions;
using Services.Services.Implementations;
using Services.Services.Interfaces;
using UniversityApi.Dtos;

namespace UniversityApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("")]
        public IActionResult GetStudents()
        {
            try
            {
                var students = _studentService.GetAll();
                return Ok(students);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, "An error occurred while retrieving students.");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetStudent([FromForm] int id)
        {
            try
            {
                var student = _studentService.GetById(id);
                return Ok(student);
            }
            catch (NullReferenceException)
            {
                return NotFound($"Student with ID {id} not found.");
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, "An error occurred while retrieving the student.");
            }
        }

        [HttpPost("")]
        public IActionResult CreateStudent([FromBody] CreateStudentDto createDto)
        {
            try
            {
                var studentId = _studentService.Create(createDto);
                return CreatedAtAction(nameof(GetStudent), new { id = studentId }, null);
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, "An error occurred while creating the student.");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int id, [FromBody] EditStudentDto editDto)
        {
            try
            {
                _studentService.Edit(id, editDto);
                return NoContent();
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, "An error occurred while updating the student.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            try
            {
                _studentService.Delete(id);
                return NoContent();
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
               
                return StatusCode(500, "An error occurred while deleting the student.");
            }
        }
    }
}
