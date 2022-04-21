﻿using Microsoft.AspNetCore.Mvc;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.ServiceLayer;
using TheNewPanelists.MotoMoto.BusinessLayer;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataStoreEntities;
using Microsoft.AspNetCore.Cors;

namespace TheNewPanelists.MotoMoto.WebServices.UserManagement.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagementController : ControllerBase
    { 
        private readonly UserManagementDataAccess _userManagementDataAccess = new UserManagementDataAccess();
        [RequireHttps]
        [HttpOptions]
        public IActionResult PreFlightRoute()
        {
            return NoContent();
        }

        [RequireHttps]
        [HttpGet]
        public IActionResult GetUserAccounts(string username)
        {
            UserManagementService service = new UserManagementService(_userManagementDataAccess);
            UserManagementManager manager = new UserManagementManager(service);

            try
            {
                ISet<AccountModel> retrieveAllAccounts = manager.RetrieveAllUsers(username);
                return Ok(retrieveAllAccounts);
            }
            catch 
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [RequireHttps]
        [HttpPost]
        public IActionResult RetrieveAllUsers(DataStoreUser user)
        {
            UserManagementService service = new UserManagementService(_userManagementDataAccess);
            UserManagementManager manager = new UserManagementManager(service);

            try
            {
                ISet<AccountModel> acct = manager.RetrieveAllUsers(user!._username!);
                return Ok(acct);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
        }

        [RequireHttps]
        [HttpDelete]
        public IActionResult DeleteAccount(string _username, string _password)
        {
            UserManagementService service = new UserManagementService(_userManagementDataAccess);
            UserManagementManager manager = new UserManagementManager(service);

            try
            {
                var deleteAccountModel = new DeleteAccountModel()
                {
                    Username = _username,
                    VerifiedPassword = _password
                };
                bool result = manager.PerminateDeleteAccountManager(deleteAccountModel);
                return Ok();
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}


/*
 [HttpDelete]
        public IActionResult DeleteAccount(string _username, string _password)
        {
            UserManagementService service = new UserManagementService(_userManagementDataAccess);
            UserManagementManager manager = new UserManagementManager(service);

            try
            {
                var deleteAccountModel = new DeleteAccountModel()
                {
                    username = _username,
                    verifiedPassword = _password
                };
                bool result = manager.PerminateDeleteAccountManager(deleteAccountModel);
                return Ok();
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
 */