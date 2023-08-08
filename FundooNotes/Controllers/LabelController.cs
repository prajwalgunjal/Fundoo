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
    public class LabelController : ControllerBase
    {
        private ILabelBusiness ilabelBusiness;
        public LabelController(ILabelBusiness ilabelBusiness)
        {
            this.ilabelBusiness = ilabelBusiness;
        }
        [HttpPost]
        [Route("AddLabel")]
        public IActionResult AddLabel(LabelModel labelModel, int noteID)
        {
            int userID = Convert.ToInt32(this.User.FindFirst("UserId").Value);

            var result = ilabelBusiness.AddLabel(labelModel, userID, noteID);
            if (result != null)
            {
                return Ok(new ResponseModel<List<LabelEntity>> { Success = true, Message = "label added"});

            }
            else {
                return NotFound(new ResponseModel<List<LabelEntity>> { Success = false, Message = "label not added", Data = null });

            }
        }
        [HttpPost]
        [Route("DisplayByLabel")]
        public IActionResult DisplayByLabel(string label)
        {
            int userID = Convert.ToInt32(this.User.FindFirst("UserId").Value);
            var result =ilabelBusiness.DisplayByLabel(label, userID);
            if (result != null)
            {
                return Ok(new ResponseModel<List<NoteEntity>> { Success = true, Message = "Data Displayed", Data = result });

            }
            else
            {
                return BadRequest(new ResponseModel<List<NoteEntity>> { Success = false, Message = "Data not Found", Data = null });

            }
        }

        [HttpPost]
        [Route("GetLabels")]
        public IActionResult GetLabels(string label)
        {
            int userID = Convert.ToInt32(this.User.FindFirst("UserId").Value);

            var result = ilabelBusiness.GetLabels(label, userID);
            if (result != null)
            {
                return Ok(new ResponseModel<List<LabelEntity>> { Success = true, Message = "label Displayed", Data = result});

            }
            else
            {
                return BadRequest(new ResponseModel<List<LabelEntity>> { Success = false, Message = "label not Found", Data = null });

            }
        }
    }
}
