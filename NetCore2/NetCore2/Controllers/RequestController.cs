using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Fiver.Mvc.FileUpload.Models.Home;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NetCore2.Models;


namespace NetCore2.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {

        AuthDbContext _context;
        private UserManager<AppUser> _userManager;
        private IHostingEnvironment _hostingEnvironment;
        private int ReqId;

        public IConfiguration Configuration { get; }
        public RequestController(AuthDbContext context, UserManager<AppUser> userManager, IHostingEnvironment hostingEnvironment, IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _hostingEnvironment = hostingEnvironment;
            Configuration = configuration;
        }


        [HttpGet]
        [Route("Dorms")]
        public Object GetDorms()
        {

            return _context.Dorm.ToArray();
        }


        [HttpDelete("{id}")]
        public Object DeleteRequest(int id)
        {
            try
            {
                Lifecycle lifecycle = _context.Lifecycle.SingleOrDefault(u => u.Id == id);
                _context.Lifecycle.Remove(lifecycle);
                _context.SaveChanges();
                string folder = _hostingEnvironment.WebRootPath + "\\Upload\\" + id;
                if (Directory.Exists(folder))
                {
                    Directory.Delete(folder, true);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpPost]
        public Object AddReq(Request request)
        {
            string executorId;
            if (request.Executor.UserName != "" && request.Executor.UserName != null)
            {
                executorId = _userManager.Users.SingleOrDefault(u => u.UserName == request.Executor.UserName).Id;
            }
            else
            {
                executorId = null;
            }

            Request req = new Request()
            {
                PriorityId = request.PriorityId,
                DormId = request.DormId,
                Room = request.Room,
                CreatorId = request.CreatorId,
                Title = request.Title,
                Description = request.Description,
                ExecutorId = executorId,
                CategoryId = request.CategoryId,
                Lifecycle = new Lifecycle { Opened = DateTime.Now },
                StatusId = request.StatusId

            };

            _context.Requests.Add(req);
            _context.SaveChanges();

            string newPath = Path.Combine(_hostingEnvironment.WebRootPath + "\\Upload\\", req.id.ToString() + "\\");
           
            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }
            Directory.CreateDirectory(_hostingEnvironment.WebRootPath + "\\Upload\\" + request.CreatorId);
            var files = Directory.GetFiles(_hostingEnvironment.WebRootPath + "\\Upload\\" + request.CreatorId);
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    string fileName = file.Split("\\")[8];
                    string fullPath = Path.Combine(newPath, file);
                    System.IO.File.Move(file, newPath + fileName);
                }

            }
            Directory.Delete(_hostingEnvironment.WebRootPath + "\\Upload\\" + request.CreatorId);
            return req.id;

        }

        [HttpGet]
        [Authorize(Roles = "Admin, Operator")]
        public Object GetRequests()
        {
            return _context.Requests
                .Include(r => r.Executor)
                .Include(r => r.Category)
                .Include(r => r.Lifecycle)
                .Include(r => r.Status)
                .Include(r => r.Creator).ToArray();
        }

        [HttpGet]
        [Route("User")]
    //    [Authorize(Roles = "User, Executor")]
        public Object GetRequestsForUser()
        {
            string userId = User.Claims.FirstOrDefault(x => x.Type == "UserID").Value;
            return _context.Requests.Where(r => r.CreatorId == userId || r.ExecutorId == userId)
                .Include(r => r.Executor)
                .Include(r => r.Category)
                .Include(r => r.Lifecycle)
                .Include(r => r.Status)
                .Include(r => r.Creator).ToArray();
        }


        [HttpPost("UploadFiles"), DisableRequestSizeLimit]
        public ActionResult UploadFile()
        {
            try
            {

                var id = Request.Form.Keys.FirstOrDefault();
                var files = Request.Form.Files;
                string webRootPath = _hostingEnvironment.WebRootPath;
                string newPath = Path.Combine(webRootPath + "/Upload", id);
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        string fullPath = Path.Combine(newPath, fileName);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                    }
                }

                return Ok("Upload Successful.");
            }
            catch (System.Exception ex)
            {

                return BadRequest("Upload Failed: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("getRequestInfo/{id}")]
        public Object getRequestInfo(int id)
        {
            return _context.Requests.Include(r => r.Executor)
                .Include(r => r.Status)
                .Include(r => r.Lifecycle)
                .SingleOrDefault(r => r.id == id);
        }

        [HttpGet]
        [Route("getRequestFiles/{id}")]
        public Object getRequestFiles(string id)
        {
            List<string> directories = new List<string>();
            string webRootPath = _hostingEnvironment.WebRootPath;
            string newPath = Path.Combine(webRootPath + "\\Upload", id);
            DirectoryInfo dir = new DirectoryInfo(newPath);
            try
            {
                foreach (var item in dir.GetFiles())
                {
                    directories.Add(Configuration["ApplicationSettings:CurrentURL"] + "/Upload/" + id + "/" + item.Name);
                }
                return directories;
            }
            catch
            {
                return Ok();
            }
        }

      


        [HttpPost]
        [Route("deleteFile")]
        public Object deleteFile(imgUrlObject imgUrl)
        {
            string url = imgUrl.url;
            string webRootPath = _hostingEnvironment.WebRootPath;
            string id = url.Split('/')[4];
            string name = url.Split('/')[5];
            string[] Files = Directory.GetFiles(webRootPath + "\\Upload\\" + id, name);
            foreach (string file in Files)
            {

                System.IO.File.Delete(file);

            }

            return Files;
        }


        [HttpPut]
        public async Task<Object> updateRequest(Request request)
        {
            var req = _context.Requests.SingleOrDefault(r => r.id == request.id);
            var lifecycle = _context.Lifecycle.SingleOrDefault(l => l.Id == req.LifecycleId);
            if (req != null)
            {
                req.PriorityId = request.PriorityId;
                req.Room = request.Room;
                req.Title = request.Title;
                if (request.ExecutorId != null && req.ExecutorId == null) { lifecycle.Distributed = DateTime.Now; } else if(request.Executor.UserName == null) { lifecycle.Distributed = null; }
                if (request.ExecutorId == "") { req.ExecutorId = null; }
                else { req.ExecutorId = request.ExecutorId; }
                req.DormId = request.DormId;
                req.Description = request.Description;
                req.CategoryId = request.CategoryId;
                if (request.StatusId != req.StatusId && request.StatusId == 2) { lifecycle.Closed = DateTime.Now; }
                req.StatusId = request.StatusId;
                _context.Update(req);
                _context.Update(lifecycle);
                var result = await _context.SaveChangesAsync();
                return result;
            }
            else return BadRequest();
        }

    }

    public class imgUrlObject
    {
        public string url { get; set; }
    }
}