
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


    }
}
