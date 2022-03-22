using ArcheProjectWebApp.Helper;
using ArcheProjectWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
//using Microsoft.Office.Interop.Word;
//using Microsoft.AspNetCore.Http;
using System;
using System.Data;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Linq;
using DocumentFormat.OpenXml;
using Xceed.Words.NET;
//using Microsoft.AspNetCore.Authorization;
//using DevExpress.XtraRichEdit;
//using DevExpress.XtraRichEdit.API.Native
//using RepositoryLayer;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.AspNetCore;

namespace ArcheProjectWebApp.Controllers
{
    public class StudentController : Controller
    {
        //private readonly DemoAPIDbContext _context;
        //public StudentController(DemoAPIDbContext context)
        //{
        //    _context = context;
        //}
        private readonly StudentApi _api = new StudentApi();
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
                HttpResponseMessage message = client.PostAsJsonAsync("/api/Student/UpdateStudent", model).Result;
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
       
        [HttpGet]
        public IActionResult WordFileUpload()
        {
            try
            {
                string path = Path.GetFullPath("Files/Haritest.docx");
                //string filename = Path.GetFileName("~/Files/Haritest.docx");
                using (var docs = WordprocessingDocument.Open(path, true))
                {
                    DataTable dt = new DataTable();
                    int rowCount = 0;
                    Table table = docs.MainDocumentPart.Document.Body.Elements<Table>().First();
                    IEnumerable<TableRow> rows = table.Elements<TableRow>();
                    foreach (TableRow row in rows)
                    {
                        if (rowCount == 0)
                        {
                            foreach (TableCell cell in row.Descendants<TableCell>())
                            {
                                dt.Columns.Add(cell.InnerText);
                            }
                            rowCount += 1;
                        }
                        else
                        {
                        //dt.Rows.Add(row);
                        dt.Rows.Add();
                            int i = 0;
                            foreach (TableCell cell in row.Descendants<TableCell>())
                            {
                                // dt.Rows.Add(row);
                                
                            
                            dt.Rows[dt.Rows.Count - 1][i] = cell.InnerText;
                            i++;
                                //c++;
                            }

                        }
                    }
                    docs.Close();
                    DemoAPIDbContext context = new DemoAPIDbContext();
                    Wordfile wordfile = new Wordfile();
                    wordfile.FullName = dt.Rows[0][1].ToString();
                    wordfile.SelfMobileNomber = dt.Rows[1][1].ToString();
                    wordfile.FamilyMobileNumber = dt.Rows[2][1].ToString();
                    wordfile.FriendMobileNumber = dt.Rows[3][1].ToString();
                    wordfile.Email = dt.Rows[4][1].ToString();
                    context.Add(wordfile);
                    context.SaveChanges();

                    return View(dt);
                    
                }
            }
            catch (Exception ex)
            {

                return View(ex.Message);
            }
        }   
        
       public IActionResult GenerateDocx()
        {
            try
            {
                string filePath = @"C:\Intel\DataFile4.docx";
                var docmn = DocX.Create(filePath);
                docmn.InsertParagraph("Record").Bold();
                docmn.Save();
                using (var docx = WordprocessingDocument.Open(filePath, true))
                {
                    var doc = docx.MainDocumentPart.Document;
                    Table table = new Table();
                    DemoAPIDbContext context = new DemoAPIDbContext();
                    var model = context.Wordfiles.ToList();
                    var tr1 = new TableRow();
                    TableCell tc11 = new TableCell(new Paragraph(new Run(new Text("FullName: "))));
                    TableCell tc12 = new TableCell(new Paragraph(new Run(new Text(model[0].FullName))));
                    tr1.Append(tc11, tc12);
                    var tr2 = new TableRow();
                    TableCell tc21 = new TableCell(new Paragraph(new Run(new Text("SelfMobileNomber"))));
                    TableCell tc22 = new TableCell(new Paragraph(new Run(new Text(model[0].SelfMobileNomber))));
                    tr2.Append(tc21, tc22);
                    var tr3 = new TableRow();
                    TableCell tc31 = new TableCell(new Paragraph(new Run(new Text("FamilyMobileNumber"))));
                    TableCell tc32 = new TableCell(new Paragraph(new Run(new Text(model[0].FamilyMobileNumber))));
                    tr3.Append(tc31, tc32);
                    var tr4 = new TableRow();
                    TableCell tc41 = new TableCell(new Paragraph(new Run(new Text("FriendMobileNumber"))));
                    TableCell tc42 = new TableCell(new Paragraph(new Run(new Text(model[0].FriendMobileNumber))));
                    tr4.Append(tc41, tc42);
                    var tr5 = new TableRow();
                    TableCell tc51 = new TableCell(new Paragraph(new Run(new Text("Email"))));
                    TableCell tc52 = new TableCell(new Paragraph(new Run(new Text(model[0].Email))));
                    tr5.Append(tc51, tc52);
                    table.Append(tr1, tr2, tr3, tr4, tr5);
                    doc.Body.Append(table);
                    doc.Save();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return ViewBag(ex.Message);
            }
        }

    }
}
