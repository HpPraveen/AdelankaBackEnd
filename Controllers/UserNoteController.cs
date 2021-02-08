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
    public class UserNoteController : ControllerBase
    {
        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.Name);

        private IGenericRepository<UserNote> _userNoteRepository;

        public UserNoteController(IGenericRepository<UserNote> userNoteRepository)
        {
            _userNoteRepository = userNoteRepository;
        }

        [HttpGet]
        [Route("AllUserNoteDetails")]
        public IActionResult GetAllUserNoteDetails()
        {
            _logger.InfoFormat("Request with verb [{0}] and Uri [{1}] executed", Request.Method, Request.Path);
            _logger.InfoFormat("Action GetAllUserNoteDetails started");
            try
            {
                var userDetailList = _userNoteRepository.Get().ToList();
                _logger.InfoFormat("Action GetAllUserNoteDetails completed");
                return Ok(userDetailList);
            }
            catch (Exception ex)
            {
                _logger.InfoFormat("Action GetAllUserNoteDetails Failed");
                return new JsonResult(ex);
            }
        }

        [HttpPost]
        [Route("NewUserNoteDetails")]
        public IActionResult AddUserNoteDetails([FromBody] UserNote userNote)
        {
            _logger.InfoFormat("Request with verb [{0}] and Uri [{1}] executed", Request.Method, Request.Path);
            _logger.InfoFormat("Action AddUserNoteDetails started");
            try
            {
                _userNoteRepository.Insert(userNote);
                _userNoteRepository.Save();
                return Ok(true);
            }
            catch (Exception ex)
            {
                _logger.InfoFormat("Action AddUserNoteDetails Failed");
                return new JsonResult(ex);
            }
        }

        [HttpPut]
        [Route("UpdateUserNoteDetails")]
        public IActionResult UpdateUserNoteDetails([FromBody] UserNote userNote)
        {
            _logger.InfoFormat("Request with verb [{0}] and Uri [{1}] executed", Request.Method, Request.Path);
            _logger.InfoFormat("Action UpdateUserNoteDetails started");
            try
            {
                //var noteDetails = _userNoteRepository.Get(e => e.Id == userNoteId);

                //var userNote = new UserNote
                //{
                //    Comment = comment,
                //    Username = noteDetails.FirstOrDefault().Username,
                //    Title = noteDetails.FirstOrDefault().Title,
                //    Note = noteDetails.FirstOrDefault().Note,
                //    ModifiedDate = noteDetails.FirstOrDefault().ModifiedDate,
                //};
                _userNoteRepository.Update(userNote);
                _userNoteRepository.Save();
                return Ok(true);
            }
            catch (Exception ex)
            {
                _logger.InfoFormat("Action UpdateUserNoteDetails Failed");
                return new JsonResult(ex);
            }
        }
    }
}