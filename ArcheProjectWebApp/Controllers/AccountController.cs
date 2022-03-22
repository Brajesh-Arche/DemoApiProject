using ArcheProjectWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Net.Mail;

namespace ArcheProjectWebApp.Controllers
{
    public class AccountController : Controller
    {
        DemoAPIDbContext db = new DemoAPIDbContext();
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(UserLogin model)
        {
            bool isValid = db.UserTable.Any(x => x.UserName == model.UserName && x.Password == model.Password);
            if(isValid==true)
            {
                HttpContext.Response.Cookies.Append("UserName", model.UserName);
                return RedirectToAction("Index", "Student");
            }
            return View();
        }
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(UserLogin model)
        {
            if(ModelState.IsValid)
            {
                db.UserTable.Add(model);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            return View();
        }
        public IActionResult ChangePassword()
        {

            return View();
        }
        [HttpPost]
        public IActionResult ChangePassword(AccountViewModel model)
        {
            var login = db.UserTable.Where(x => x.Password.Equals(model.OldPassword)).FirstOrDefault();
            if(login.Password==model.OldPassword)
            {
                if(model.ConfirmPassword==model.NewPassword)
                {
                    login.ConfirmPassword = model.ConfirmPassword;
                    login.Password = model.NewPassword;
                    db.Entry(login).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();
                    TempData["msg"] = "<script>alert('Password has been changed successfully !!!');</script>";
                }
            }
            else
            {
                TempData["msg"] = "<script>alert('Old password not match !!! Please check entered old password');;</ script > ";
            }
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete("UserName");
            return RedirectToAction("Login");
        }
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ForgetPassword(string EmailId)
        {
            var message = "";
            bool isvalid = db.UserTable.Any(x => x.EmailId.Equals(EmailId));
            if (isvalid == true)
            {
                string ResetCode = Guid.NewGuid().ToString();

                //var verifyURL = "/Account/ResetPassword" + ResetCode;
                string FilePath = "C:\\ProjectDemo\\ArcheProjectDemoApi\\ArcheProjectWebApp\\EmailTemplate\\ResetPwd.html";
                StreamReader str = new StreamReader(FilePath);
                string MailText = str.ReadToEnd();
                str.Close();
                MailText = MailText.Replace("[newusername]", EmailId);
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("yourmail@archesoftronix.com");
                mail.To.Add(new MailAddress(EmailId));
                mail.Subject = "test";
                var lnkHref = "<a href='" + Url.Action("ResetPassword", "Account", new { Resetcode = ResetCode, email = EmailId }, "https") + "'>Reset Password</a>";
                MailText = MailText.Replace("[link]", lnkHref);
                mail.Body = MailText;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "your.host.com";
                smtp.Port = portnumber;
                smtp.EnableSsl = false;
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = new System.Net.NetworkCredential("Yourmail@archesoftronix.com", "yourpassword", "");
                //smtp.Connect(_appSettings.SmtpHost, _appSettings.SmtpPort, SecureSocketOptions.StartTls);
                //smtp.Authenticate(_appSettings.SmtpUser, _appSettings.SmtpPass);
                smtp.Send(mail);
                message = "Reset Password Link Sent successfully";
                var user = db.UserTable.Where(x => x.EmailId == EmailId).FirstOrDefault();
                if (user != null)
                {
                    user.ResetCode = ResetCode;
                    db.SaveChanges();
                }
                // smtp.Disconnect(true);
                return RedirectToAction("Login");
                //return View();
            }
            else
            {
                message = "EmailId is Not Registere Plz Singup First...!";
            }
            ViewBag.Message = message;
            return View();
        }
        [HttpGet]
        public IActionResult ResetPassword(string resetcode,string email)
        {
            var message = "";
            var mail = db.UserTable.Where(x => x.EmailId==email).FirstOrDefault();
            if(mail!=null)
            {
                ResetPasswordModel model = new ResetPasswordModel();
                model.Code = resetcode;
                return View(model);
            }
            else
            {
                message = "Something Invalid";
                return ViewBag.Message = message;
            }
        }
        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordModel model)
        {
            var message = "";
            if(ModelState.IsValid)
            {
                var user = db.UserTable.Where(a => a.ResetCode == model.Code).FirstOrDefault();
                if(user!=null)
                {
                    user.Password = model.NewPassword;
                    user.ConfirmPassword = model.ConfirmPassword;
                    user.ResetCode = "0";
                    db.SaveChanges();
                    message = "New Password updated successfully";
                }
            }
            else
            {
                message = "Something Invalid";
            }
            ViewBag.Message = message;
            return View("Login");
        }
    }
}
