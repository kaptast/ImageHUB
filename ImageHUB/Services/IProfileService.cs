using ImageHUB.Repositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageHUB.Services
{
    public interface IProfileService
    {
        Profile GetProfileByID(DatabaseContext context, string id, string userName);
    }
}
