﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ImageHUB.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ImageHUB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendController : ControllerBase
    {
        private readonly IProfileService profileService;

        public FriendController(IProfileService profileService)
        {
            this.profileService = profileService;
        }

        [HttpPost]
        [Route("AddFriend")]
        public void AddFriend(string id)
        {
            string userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            this.profileService.AddFriend(userId, id);
        }

        [HttpPost]
        [Route("AcceptFriend")]
        public void AcceptFriend(string id)
        {
            string userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            this.profileService.AcceptFriend(userId, id);
        }

        [HttpDelete]
        [Route("DeleteFriend")]
        public void DeleteFriend(string id)
        {
            string userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            this.profileService.DeleteFriend(userId, id);
        }
    }
}