using ArcheProjectWebApp.Helper;
using ArcheProjectWebApp.Models;
using DomainLayer;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ArcheProjectWebApp.Controllers
{
    public class StudentController : Controller
    {
        StudentApi _api = new StudentApi();
        public async Task<IActionResult> Index()
        {
            List<StudentViewModel> students = new List<StudentViewModel>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("/api/Student/GetAllStudent");
            if(res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                students = JsonConvert.DeserializeObject<List<StudentViewModel>>(result);
            }
            return View(students);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public  IActionResult Create(StudentViewModel model)
        {
            if (ModelState.IsValid)
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpClient client = _api.Initial();
                HttpResponseMessage message = client.PostAsJsonAsync("/api/Student/AddStudent", model).Result;
                if (message.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
        public async Task<IActionResult> Edit(int id)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage response = await client.GetAsync("/api/Student/GetStudent?id=" + id);
            var res =response.Content.ReadAsStringAsync().Result;
            var students = JsonConvert.DeserializeObject<StudentViewModel>(res);
            return View(students);
        }
        [HttpPost]
        public IActionResult Edit(StudentViewModel model)
        {
            if (ModelState.IsValid)
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpClient client = _api.Initial();
                HttpResponseMessage message = client.PostAsJsonAsync("/api/Student/AddStudent", model).Result;
                if (message.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
        public async Task<IActionResult> Delete(int id)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage response =await client.DeleteAsync("/api/Student/DeleteStudent?id=" + id);

            if(response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
