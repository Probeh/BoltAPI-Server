using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.DTOs;
using Server.Models;

namespace Server.Controllers
{
    [Route("api/Profiles")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        private readonly DataContext context;

        public ProfilesController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetProfiles()
        {
            return Ok(await this.context.Profiles.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> CreateProfile(ProfileDTO profile)
        {
            try
            {
                await this.context.Profiles.AddAsync(new Profile()
                {
                    Title = profile.Title,
                        FirstName = profile.FirstName,
                        LastName = profile.LastName,
                        Email = profile.Email,
                        Phone = profile.Phone,
                        Date = DateTime.Parse(profile.Date),
                });
                await this.context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            var result = await this.context.Profiles.FirstAsync(x => x.FirstName == profile.FirstName && x.LastName == profile.LastName);
            if (result != null)
            {
                return Ok(new ProfileDTO()
                {
                    Title = result.Title,
                    FirstName = result.FirstName,
                    LastName = result.LastName,
                    Phone = result.Phone,
                    Email = result.Email,
                    Date = result.Date.ToString(),
                });
            }
            else { return BadRequest(); }
        }
    }
}