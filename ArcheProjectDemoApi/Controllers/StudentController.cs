using DomainLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer;

namespace ArcheProjectDemoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IService _service;
        public StudentController(IService service)
        {
            _service = service;
        }
        [HttpGet(nameof(GetAllStudent))]
        public IActionResult GetAllStudent()
        {
            var res=_service.GetAllStudents();
            return Ok(res);
        }
        [HttpGet(nameof(GetStudent))]
        public IActionResult GetStudent(int id)
        {
            var res = _service.GetStudent(id);
            return Ok(res);
        }
        [HttpPost(nameof(AddStudent))]
        public IActionResult AddStudent(Student student)
        {
            _service.AddStudent(student);
            return Ok("Student Details Added...");
        }
        [HttpPut(nameof(UpdateStudent))]
        public IActionResult UpdateStudent(Student student)
        {
            _service.UpdateStudent(student);
            return Ok("Data Updated..");
        }
        [HttpDelete(nameof(DeleteStudent))]
        public IActionResult DeleteStudent(int id)
        {
            _service.DeleteStudent(id);
            return Ok("Student Data Deleted...");
        }
    }
}
