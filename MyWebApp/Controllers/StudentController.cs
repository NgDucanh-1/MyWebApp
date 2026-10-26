using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyWebApp.Models;

namespace MyWebApp.Controllers
{
    public class StudentController : Controller
    {
        private static List<Student> listStudents = new List<Student>();
        private readonly IWebHostEnvironment _env;

        public StudentController(IWebHostEnvironment env)
        {
            _env = env;
            if (listStudents.Count == 0)
            {
                listStudents = new List<Student>()
                {
                    new Student() { Id = 101, Name = "Hải Nam", Branch = Branch.IT,
                        Gender = Gender.Male, IsRegular = true,
                        Address = "A1-2018", Email = "nam@gmail.com", DateOfBorth = new DateTime(2002, 1, 1), Score = 8.5 },
                    new Student() { Id = 102, Name = "Minh Tú", Branch = Branch.BE,
                        Gender = Gender.Female, IsRegular = true,
                        Address = "A1-2019", Email = "tu@gmail.com", DateOfBorth = new DateTime(2003, 5, 12), Score = 9.0 }
                };
            }
        }

        [Route("Admin/Student/List", Name = "StudentList")]
        public IActionResult Index()
        {
            return View(listStudents);
        }

        [HttpGet]
        [Route("Admin/Student/Add", Name = "StudentAdd")]
        public IActionResult Create()
        {
            ViewBag.AllGenders = Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList();
            ViewBag.AllBranches = new List<SelectListItem>()
            {
                new SelectListItem { Text = "IT", Value = "1" },
                new SelectListItem { Text = "BE", Value = "2" },
                new SelectListItem { Text = "CE", Value = "3" },
                new SelectListItem { Text = "EE", Value = "4" }
            };
            return View();
        }

        [HttpPost]
        [Route("Admin/Student/Add", Name = "StudentAddPost")]
        public async Task<IActionResult> Create(Student s, IFormFile? avatar)
        {
            if (ModelState.IsValid)
            {
                s.Id = listStudents.Count > 0 ? listStudents.Last().Id + 1 : 101;

                if (avatar != null && avatar.Length > 0)
                {
                    var ext = Path.GetExtension(avatar.FileName).ToLowerInvariant();
                    var allowed = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
                    if (allowed.Contains(ext))
                    {
                        var folder = Path.Combine(_env.WebRootPath, "uploads");
                        Directory.CreateDirectory(folder);
                        var fileName = $"{s.Id}_{Guid.NewGuid():N}{ext}";
                        using (var stream = new FileStream(Path.Combine(folder, fileName), FileMode.Create))
                        {
                            await avatar.CopyToAsync(stream);
                        }
                        s.AvatarUrl = "/uploads/" + fileName;
                    }
                }

                listStudents.Add(s);
                return RedirectToAction("Index");
            }

            // Nếu dữ liệu vi phạm Validation -> Nạp lại ViewBag và hiển thị lại Form lỗi
            ViewBag.AllGenders = Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList();
            ViewBag.AllBranches = new List<SelectListItem>()
            {
                new SelectListItem { Text = "IT", Value = "1" },
                new SelectListItem { Text = "BE", Value = "2" },
                new SelectListItem { Text = "CE", Value = "3" },
                new SelectListItem { Text = "EE", Value = "4" }
            };
            return View(s);
        }
    }
}