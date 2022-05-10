﻿using Microsoft.AspNetCore.Mvc;
using TheNewPanelists.MotoMoto.BusinessLayer;

namespace TheNewPanelists.MotoMoto.WebServices.NoteDashboard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteDeleteController : Controller
    {
        [HttpOptions]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Web API call to delete a specified note from the database
        /// </summary>
        /// <param name="username"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("DeleteNote")]
        public IActionResult DeleteNotes(string username, string title)
        {
            try
            {
                NoteDashboardManager manager = new NoteDashboardManager();
                return Ok(manager.DeleteNotes(username, title));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
