using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using NetCore2.Models;

namespace NetCore2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
        private readonly ApplicationSettings _appSettings;
        public IConfiguration Configuration { get; }
        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IOptions<ApplicationSettings> appSettings, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appSettings = appSettings.Value;
            Configuration = configuration;
        }

        [HttpPost]
        [Route("Register")]
        //POST : /api/ApplicationUser/Register
        public async Task<Object> PostApplicationUser(AppUserModel model)
        {

            var applicationUser = new AppUser()
            {
                UserName = model.UserName,
                Email = model.Email,
                FullName = model.FullName,
                DormId = model.DormId,
                Room = model.Room

            };
            Role role = new Role { Name = "User", NormalizedName = "USER" };

            if (_userManager.Users.SingleOrDefault(u => u.UserName == model.UserName) == null)
            {
                try
                {
                    var result = await _userManager.CreateAsync(applicationUser, model.Password);
                    if (model.Role != null) await _userManager.AddToRoleAsync(applicationUser, model.Role.Name);
                    else await _userManager.AddToRoleAsync(applicationUser, "User");
                    if (result.Succeeded)
                    {
                        // генерация токена для пользователя
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(applicationUser);
                        var callbackUrl = Url.Action(
                            "ConfirmEmail",
                            "Auth",
                            new { userId = applicationUser.Id, code = code },
                            protocol: HttpContext.Request.Scheme);
                      //  EmailService emailService = new EmailService();
                        await SendEmailAsync(model.Email, "Подтвердите адрес",
                           $"Перейдите по ссылке для завершения регистрации <a href='{callbackUrl}'>-ссылка-</a>");

                        return result;
                    }
                    return result;
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            else
            {
                return BadRequest();
            }

           
        }



        public async Task SendEmailAsync(string email,  string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Сервисная служба общежития Московского Политеха", "login@service-mospolytech.ru"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (mysender, certificate, chain, sslPolicyErrors) => { return true; };
                client.CheckCertificateRevocation = false;
                await client.ConnectAsync("mail.service-mospolytech.ru", 25, false);

                await client.AuthenticateAsync("login@service-mospolytech.ru", "12345678Ss");
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }

        [HttpGet]
        [AllowAnonymous]        
        public async Task<Object> ConfirmEmail(string userId, string code)
        {


            if (userId == null || code == null)
            {
                return "email не подтвержден";
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return "email не подтвержден";
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                ViewBag.url = Configuration["ApplicationSettings:ClientURL"];
                return View("Index");
            }
            else
                return "email не подтвержден";
        }



        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var user = _userManager.Users.Single(u=>u.UserName == model.UserName);
            IdentityOptions _options = new IdentityOptions();
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var role = await _userManager.GetRolesAsync(user);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]{
                        new Claim("UserID", user.Id),
                        new Claim(_options.ClaimsIdentity.RoleClaimType, role.FirstOrDefault())
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWTSecret)),
                    SecurityAlgorithms.HmacSha256)


                };
                if (await _userManager.IsEmailConfirmedAsync(user))
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                    var token = tokenHandler.WriteToken(securityToken);
                    return Ok(new { token });
                }
                else return Unauthorized(new { message = "Email не подтвержден" });

            }
            else
                return BadRequest(new { message = "Username or password is incorrect." });
        }
    }
}

