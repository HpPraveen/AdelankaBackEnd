using System;
using System.Linq;
using System.Reflection;
using Adelanka.DAL;
using Adelanka.DAL.Models;
using log4net;
using Microsoft.AspNetCore.Mvc;

namespace Adelanka.Controllers
{
    [Route("")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.Name);

        private IGenericRepository<SystemUser> _systemUserRepository;

        public UserController(IGenericRepository<SystemUser> systemUserRepository)
        {
            _systemUserRepository = systemUserRepository;
        }

        [HttpGet]
        [Route("UserDetails/{userName}/{password}")]
        public IActionResult GetUserDetailsByUserName(string userName, string password)
        {
            _logger.InfoFormat("Request with verb [{0}] and Uri [{1}] executed", Request.Method, Request.Path);
            _logger.InfoFormat("Action GetUserDetailsByUserName started");
            try
            {
                var userDetailList = _systemUserRepository.Get(e => e.IsActive && e.UserName == userName && e.Password == password).ToList();
                _logger.InfoFormat("Action GetUserDetailsByUserName completed");
                return Ok(userDetailList);
            }
            catch (Exception ex)
            {
                _logger.InfoFormat("Action GetUserDetailsByUserName Failed");
                return new JsonResult(ex);
            }
        }

        [HttpPost]
        [Route("NewUserDetails")]
        public IActionResult AddUserDetails([FromBody] SystemUser systemUserDetails)
        {
            _logger.InfoFormat("Request with verb [{0}] and Uri [{1}] executed", Request.Method, Request.Path);
            _logger.InfoFormat("Action AddUserDetails started");
            try
            {
                var alreadySavedUserDetails = _systemUserRepository.Get(e => e.UserName == systemUserDetails.UserName).ToList();
                if (alreadySavedUserDetails.Count == 0)
                {
                    _systemUserRepository.Insert(systemUserDetails);
                    _systemUserRepository.Save();
                    return Ok(true);
                }
                else
                {
                    return Ok(false);
                }
            }
            catch (Exception ex)
            {
                _logger.InfoFormat("Action AddUserDetails Failed");
                return new JsonResult(ex);
            }
        }
    }
}