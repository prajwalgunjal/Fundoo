using BusinessLayer.Interfaces;
using CommonLayer.Models;
using CommonLayer.RequestModels;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RepositoryLayer.Entity;
using RepositoryLayer.Migrations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
 
    public class NoteController : ControllerBase
    {
        private INoteBusiness iNoteBusiness;
        private ILogger<UserController> logger;
        private readonly IDistributedCache distributedCache;
        public NoteController(INoteBusiness iNoteBusiness, ILogger<UserController> logger, IDistributedCache distributedCache)
        {
            this.iNoteBusiness = iNoteBusiness;
            this.logger = logger;
            this.distributedCache = distributedCache;   
        }
        [HttpPost]
        // request url:-  localhost/Controller_name/MethodRoute
        [Route("AddNote")]
        public ActionResult takeANote(TakeANoteModel takeANoteModel)
        {
            try
            {
               // int userID = Convert.ToInt32(this.User.FindFirst("UserId").Value);
                int userID = (int)HttpContext.Session.GetInt32("UserId");
                var result = iNoteBusiness.TakeANote(takeANoteModel, userID);
                if (result != null)
                {
                    logger.LogInformation("Note added Successfully");
                    return Ok(new ResponseModel<NoteEntity> { Success = true, Message = "Note added Successfull", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<NoteEntity> { Success = false, Message = "Not Registred", Data = null });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return BadRequest(ex.ToString());
            }
        }

        /* [HttpGet]
         [Route("Display")]
         public ActionResult DisplayNote()
         {
             int userID = Convert.ToInt32(this.User.FindFirst("UserId").Value);

             List<NoteEntity> userNotes = iNoteBusiness.DisplayNote(userID);

             if (userNotes.Count > 0)
             {
                 return Ok(new ResponseModel<List<NoteEntity>> { Success = true, Message = "Notes retrieved successfully", Data = userNotes });
             }
             else
             {
                 return NotFound(new ResponseModel<List<NoteEntity>> { Success = false, Message = "No notes found for the user", Data = null });
             }
         }*/

        [HttpGet]
        [Route("GetAllNotesUsingRedis")]
        public async Task<IActionResult> GetAllNotesUsingRedis()
        {
            try
            {
                var CacheKey = "NotesList";
                List<NoteEntity> NoteList;
                byte[] RedishNoteList = await distributedCache.GetAsync(CacheKey);
                if (RedishNoteList != null)
                {
                    logger.LogDebug("Getting the list from redis cache");
                    var SerializedNoteList = Encoding.UTF8.GetString(RedishNoteList);
                    NoteList = JsonConvert.DeserializeObject<List<NoteEntity>>(SerializedNoteList);
                }
                else
                {
                    logger.LogDebug("Setting the list to cache which is requested for the first time");
                    NoteList = (List<NoteEntity>)iNoteBusiness.Get_All_Notes_Without_Login();
                    var SerializedNoteList = JsonConvert.SerializeObject(NoteList);
                    var redishNoteList = Encoding.UTF8.GetBytes(SerializedNoteList);
                    var options = new DistributedCacheEntryOptions().SetAbsoluteExpiration(DateTime.Now.AddMinutes(10)).SetSlidingExpiration(TimeSpan.FromMinutes(1));
                    await distributedCache.SetAsync(CacheKey, redishNoteList, options);
                }
                logger.LogInformation("Got the notes list successfully from redish");
                return Ok(NoteList);
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Exception thrown...");
                return BadRequest(new { Success = false, Message = ex.Message });
            }


        }

        [HttpGet("Get_All_Notes_Without_Login")]
            public IActionResult Get_All_Notes_Without_Login()
            {
            try
            {
                var result = iNoteBusiness.Get_All_Notes_Without_Login();

                if (result != null)
                {
                    logger.LogInformation("Display successfull");
                    return Ok(new ResponseModel<List<NoteEntity>> { Success = true, Message = "Display successfull", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<List<NoteEntity>> { Success = false, Message = "No notes found", Data = null });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return BadRequest(ex.ToString());
            }

        }

        [HttpGet("Display")]
            public IActionResult Display()
            {
            try
            {
                int userID = Convert.ToInt32(this.User.FindFirst("UserId").Value);

                var result = iNoteBusiness.GetAll(userID);

                if (result != null)
                {
                    logger.LogInformation("Display successfull");
                    return Ok(new ResponseModel<List<NoteEntity>> { Success = true, Message = "Display successfull", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<List<NoteEntity>> { Success = false, Message = "No notes found", Data = null });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return BadRequest(ex.ToString());
            }
            
        }
        [HttpPatch]
        [Route("ChangeColour")]
        public IActionResult ChangeColour(int noteId, string colour)
        {

            try
            {
                int userID = Convert.ToInt32(this.User.FindFirst("UserId").Value);
                var result = iNoteBusiness.ChangeColour(noteId, colour, userID);
                if (result != null)
                {
                    logger.LogInformation("Display successfull");
                    return Ok(new ResponseModel<NoteEntity> { Success = true, Message = "Colour changed successfull" });
                }
                else
                {
                    return BadRequest(new ResponseModel<NoteEntity> { Success = false, Message = "colour not changed", Data = null });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return BadRequest(ex.ToString());
            }
            
        }

        [HttpPatch]
        [Route("SetReminder")]
        public IActionResult SetReminder(int noteId,DateTime dateTime)
        {

            try
            {
                int userID = Convert.ToInt32(this.User.FindFirst("UserId").Value);

                var result = iNoteBusiness.SetReminder(noteId, dateTime, userID);

                if (result == dateTime)
                {
                    logger.LogInformation("Display successfull");
                    return Ok(new ResponseModel<NoteEntity> { Success = true, Message = "Reminder set successfull" });
                }
                else
                {
                    return BadRequest(new ResponseModel<NoteEntity> { Success = false, Message = "Reminder not set successfull", Data = null });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return BadRequest(ex.ToString());
            }
            
        }

        [HttpPost]
        [Route("Edit")]
        public IActionResult EditANote(TakeANoteModel takeANoteModel, int noteId)
        {

            try
            {
                int userID = Convert.ToInt32(this.User.FindFirst("UserId").Value);
                var result = iNoteBusiness.EditANote(takeANoteModel, userID, noteId);

                if (result != null)
                {
                    logger.LogInformation("Display successfull");
                    return Ok(new ResponseModel<NoteEntity> { Success = true, Message = "Note edited successfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<NoteEntity> { Success = false, Message = "Note not found", Data = null });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return BadRequest(ex.ToString());
            }
           
        }
       

        [HttpPut]
        [Route("IsPin")]
        public IActionResult isPin(int noteId)
        {

            try
            {
                int userID = Convert.ToInt32(this.User.FindFirst("UserId").Value);

                var result = iNoteBusiness.isPin(noteId, userID);
                //throw new Exception("Error");

                if (result)
                {
                    logger.LogInformation("Pinned Note successfully");
                    return Ok(new ResponseModel<bool> { Success = true, Message = "Pinned", Data = result });
                }
                
                else
                {
                    return BadRequest(new ResponseModel<bool> { Success = false, Message = "unPinned", Data = result });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return BadRequest(ex.ToString());
            }
           
        }


        [HttpPut]
        [Route("Archive")]
        public IActionResult IsArchive(int noteId)
        {
            try
            {
                int userID = Convert.ToInt32(this.User.FindFirst("UserId").Value);

                var result = iNoteBusiness.IsArchive(noteId, userID);
                if (result)
                {
                    logger.LogInformation("Display successfull");
                    return Ok(new ResponseModel<bool> { Success = true, Message = "Archived", Data = result });
                }
                else
                {
                    return NotFound(new ResponseModel<bool> { Success = false, Message = "UnArchived", Data = result });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return BadRequest(ex.ToString());
            }
            
        }


        [HttpPut]
        [Route("TrashNotes")]
        public IActionResult TrashNotes(int noteId)
        {
            try
            {
                int userID = Convert.ToInt32(this.User.FindFirst("UserId").Value);
                var result = iNoteBusiness.TrashNotes(noteId,userID);
                if (result)
                {
                    logger.LogInformation("Display successfull");

                    return Ok(new ResponseModel<bool> { Success = true, Message = "Trashed", Data = result });
                }
                else
                {
                    return NotFound(new ResponseModel<bool> { Success = false, Message = "Not Trashed", Data = result });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return BadRequest(ex.ToString());
            }
            
        }


        [HttpDelete]
        [Route("Delete")]
        public IActionResult Delete_A_Note(int noteID)
        {
            try
            {
                int userID = Convert.ToInt32(this.User.FindFirst("UserId").Value);

                var result = iNoteBusiness.Delete_A_note(userID,noteID);

                if (result != null)
                {
                    logger.LogInformation("Display successfull");

                    return Ok(new ResponseModel<NoteEntity> { Success = true, Message = "Note Delete successfully", Data = result});
                }
                else
                {
                    return NotFound(new ResponseModel<NoteEntity> { Success = false, Message = "Note not found", Data = null });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return BadRequest(ex.ToString());
            }
            
        }

        [HttpPost]
        [Route("UploadImage")]
        public IActionResult UploadImage(string filePath, long notesId)
        {
            try
            {
                int userID = Convert.ToInt32(this.User.FindFirst("UserId").Value);

                var result = iNoteBusiness.UploadImage(filePath,notesId,userID);

                if (result != null)
                {
                    logger.LogInformation("Display successfull");

                    return Ok(new ResponseModel<string> { Success = true, Message = "Image Uploaded", Data = result });
                }
                else
                {
                    return NotFound(new ResponseModel<string> { Success = false, Message = "Image not Uploaded", Data = null });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return BadRequest(ex.ToString());
            }
        }
    }
}
