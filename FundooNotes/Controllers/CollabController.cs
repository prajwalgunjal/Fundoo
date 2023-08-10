
using BusinessLayer.Interfaces;
using CommonLayer.Models;
using CommonLayer.RequestModels;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;
using RepositoryLayer.Migrations;
using System;
using System.Collections.Generic;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollabController :ControllerBase
    {
        ICollabBusiness icollabBusiness;
        public CollabController(ICollabBusiness icollabBusiness)
        {
            this.icollabBusiness = icollabBusiness;
        }
        [HttpPost]
        [Route("AddColab")]

        public IActionResult AddCollab(int NoteId, CollabModel collabModel)
        {
            int userID = Convert.ToInt32(this.User.FindFirst("UserId").Value);

            var result = icollabBusiness.AddCollab(userID, NoteId, collabModel);
            if (result != null)
            {
                return Ok(new ResponseModel<CollabEntity> { Success = true, Message = "Collab added", Data= result});

            }
            else
            {
                return NotFound(new ResponseModel<CollabEntity> { Success = false, Message = "Collab not added", Data = null });

            }
        }

        [HttpGet]
        [Route("GetColab")]
        public IActionResult GetCollabEntities(int noteid)
        {
            int userID = Convert.ToInt32(this.User.FindFirst("UserId").Value);
            var result = icollabBusiness.GetCollabEntities(userID,noteid);
            if (result != null)
            {
                return Ok(new ResponseModel<List<CollabEntity> >{ Success = true, Message = "Displayed", Data = result });

            }
            else
            {
                return NotFound(new ResponseModel<List<CollabEntity>> { Success = false, Message = "Not Displayed", Data = null });
            }
        }
        [HttpDelete]
        [Route("RemoveCollab")]
        public IActionResult RemoveCollab(int collabID, int noteID)
        {
            int userID = Convert.ToInt32(this.User.FindFirst("UserId").Value);
            var result = icollabBusiness.RemoveCollab(userID,collabID, noteID);
            if (result != null)
            {
                return Ok(new ResponseModel<CollabEntity> { Success = true, Message = "Removed", Data = result });

            }
            else
            {
                return NotFound(new ResponseModel<CollabEntity>{ Success = false, Message = "Not Displayed", Data = null });
            }
        }
        [HttpGet]
        [Route("GetAllCollabs")]
        public IActionResult Get_All_Collabs()
        {
            int userID = Convert.ToInt32(this.User.FindFirst("UserId").Value);
            var result = icollabBusiness.Get_All_Collabs(userID);
            if (result != null)
            {
                return Ok(new ResponseModel<List<CollabEntity>> { Success = true, Message = "Displayed", Data = result });

            }
            else
            {
                return NotFound(new ResponseModel<List<CollabEntity>> { Success = false, Message = "Not Displayed", Data = null });
            }
        }
        [HttpGet]
        [Route("GetAllNoteForOneCollab")]
        public IActionResult Get_All_Note_For_One_Collab(int CollabID)
        {
            int userID = Convert.ToInt32(this.User.FindFirst("UserId").Value);
            var result = icollabBusiness.Get_All_Note_For_One_Collab(userID,CollabID);
            if (result != null)
            {
                return Ok(new ResponseModel<List<NoteEntity>> { Success = true, Message = "Displayed", Data = result });

            }
            else
            {
                return NotFound(new ResponseModel<List<NoteEntity>> { Success = false, Message = "Not Displayed", Data = null });
            }
        }

    }
}
