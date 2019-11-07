﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ImageHUB.Repositories;
using ImageHUB.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ImageHUB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService profileService;
        private DatabaseContext context;

        public ProfileController(IProfileService profileService, DatabaseContext context)
        {
            this.profileService = profileService;
            this.context = context;
        }

        [HttpGet]
        public Repositories.Profile Get()
        {
            string userName = User.FindFirstValue(ClaimTypes.Name);
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string email = User.FindFirstValue(ClaimTypes.Email);

            var profile = this.profileService.GetProfileByID(this.context, id, userName);
            profile.Avatar = id;
            profile.Email = email;

            return profile;
        }
    }
}