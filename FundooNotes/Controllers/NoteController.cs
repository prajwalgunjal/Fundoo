using BusinessLayer.Interfaces;
using CommonLayer.Models;
using CommonLayer.RequestModels;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;
using RepositoryLayer.Migrations;
using System;

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
    }
}
