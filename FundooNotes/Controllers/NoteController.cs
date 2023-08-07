using BusinessLayer.Interfaces;
using CommonLayer.Models;
using CommonLayer.RequestModels;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;
using RepositoryLayer.Migrations;
using System;
using System.Collections.Generic;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private INoteBusiness iNoteBusiness;
        public NoteController(INoteBusiness iNoteBusiness)
        {
            this.iNoteBusiness = iNoteBusiness;

        }
        [HttpPost]
        // request url:-  localhost/Controller_name/MethodRoute
        [Route("AddNote")]
        public ActionResult takeANote(TakeANoteModel takeANoteModel)
        {
            int userID = Convert.ToInt32(this.User.FindFirst("UserId").Value);
            var result = iNoteBusiness.TakeANote(takeANoteModel, userID);
            if (result != null)
            {
                return Ok(new ResponseModel<NoteEntity> { Success = true, Message = "Note added Successfull", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<NoteEntity> { Success = false, Message = "Not Registred", Data = null });
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
        [HttpGet("Display")]
        public IActionResult Display(string emailid)
        {
            var result = iNoteBusiness.GetAll(emailid);

            if (result != null)
            {
                return Ok(new ResponseModel<List<NoteEntity>> { Success = true, Message = "Display successfull", Data = result });
            }
            else
            {
                return NotFound(new ResponseModel<List<NoteEntity>> { Success = false, Message = "No notes found", Data = null });
            }
        }

        [HttpPost]
        [Route("Edit")]
        public IActionResult EditANote(TakeANoteModel takeANoteModel,int userID, int noteId)
        {
            var result = iNoteBusiness.EditANote(takeANoteModel,userID, noteId);

            if (result != null)
            {
                return Ok(new ResponseModel<NoteEntity> { Success = true, Message = "Note edited successfully", Data = result });
            }
            else
            {
                return NotFound(new ResponseModel<NoteEntity> { Success = false, Message = "Note not found", Data = null });
            }
        }


        [HttpPut]
        [Route("IsPin")]
        public IActionResult isPin(int noteId)
        {
            var result = iNoteBusiness.isPin(noteId);
            if (result)
            {
                return Ok(new ResponseModel<bool> { Success = true, Message = "Pinned" ,Data = result });
            }
            else
            {
                return NotFound(new ResponseModel<bool> { Success = false, Message = "unPinned" , Data = result });
            }
        }


        [HttpPut]
        [Route("Archive")]
        public IActionResult IsArchive(int noteId)
        {
            var result = iNoteBusiness.IsArchive(noteId);
            if (result)
            {
                return Ok(new ResponseModel<bool> { Success = true, Message = "Archived", Data = result });
            }
            else
            {
                return NotFound(new ResponseModel<bool> { Success = false, Message = "UnArchived", Data = result });
            }
        }


        [HttpPut]
        [Route("TrashNotes")]
        public IActionResult TrashNotes(int noteId)
        {
            var result = iNoteBusiness.TrashNotes(noteId);
            if (result)
            {
                return Ok(new ResponseModel<bool> { Success = true, Message = "Trashed", Data = result });
            }
            else
            {
                return NotFound(new ResponseModel<bool> { Success = false, Message = "Not Trashed", Data = result });
            }
        }


        [HttpDelete]
        [Route("Delete")]
        public IActionResult Delete_A_Note(int userID)
        {
            var result = iNoteBusiness.Delete_A_note(userID);

            if (result != null)
            {
                return Ok(new ResponseModel<NoteEntity> { Success = true, Message = "Note Delete successfully"});
            }
            else
            {
                return NotFound(new ResponseModel<NoteEntity> { Success = false, Message = "Note not found", Data = null });
            }
        }
    }
}
