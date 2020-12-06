using System.Collections.Generic;
using VideoGameLibrary;
using VideoGameStore.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace VideoGameStore.Controllers
{
    [Route("v1/")]
    //[ApiController]
    public class VideoGameStoreController : ControllerBase
    {
        //private readonly IDatingServices services;
        private VideoGameStoreServices services = new VideoGameStoreServices();

        [EnableCors("AllowAny")]
        [HttpPost]
        [Route("Login")]
        public User Login(string username, string password)
        {
            return services.Login(username, password);
        }

        //[EnableCors("AllowAny")]
        //[HttpPost]
        //[Route("AddMember")]
        //public int AddMember([FromBody]Member member)
        //{
        //    return services.AddMember(member);
        //}

        //[EnableCors("AllowAny")]
        //[HttpPost]
        //[Route("AddProfile")]
        //public bool AddProfile([FromBody]Profile profile)
        //{
        //    return services.AddProfile(profile);
        //}

        //[EnableCors("AllowAny")]
        //[HttpPut]
        //[Route("UpdateProfile")]
        //public bool UpdateProfile([FromBody]Profile profile)
        //{
        //    return services.UpdateProfile(profile);
        //}

        //[EnableCors("AllowAny")]
        //[HttpPut]
        //[Route("DisableProfile")]
        //public bool DisableProfile([FromBody]int profileId)
        //{
        //    return services.DisableProfile(profileId);
        //}

        //[EnableCors("AllowAny")]
        //[HttpPut]
        //[Route("EnableProfile")]
        //public bool EnableProfile([FromBody]int profileId)
        //{
        //    return services.EnableProfile(profileId);
        //}

        //[EnableCors("AllowAny")]
        //[HttpGet]
        //[Route("GetProfilesByGender")]
        //public List<Profile> GetProfilesByGender(string gender)
        //{
        //    return services.GetProfilesByGender(gender);
        //}

        //[EnableCors("AllowAny")]
        //[HttpGet]
        //[Route("GetProfilesByCityAndState")]
        //public List<Profile> GetProfilesByCityAndState(string city, string state)
        //{
        //    return services.GetProfilesByCityAndState(city, state);
        //}

        //[EnableCors("AllowAny")]
        //[HttpGet]
        //[Route("GetProfileById")]
        //public Profile GetProfileById(int profileId)
        //{
        //    return services.GetProfileById(profileId);
        //}

        //[EnableCors("AllowAny")]
        //[HttpDelete]
        //[Route("DeleteProfile")]
        //public bool DeleteProfile([FromBody]int profileId)
        //{
        //    return services.DeleteProfile(profileId);
        //}
    }

}