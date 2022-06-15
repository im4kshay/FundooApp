﻿using BusinessLogicLayer.Interface;
using CommonLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FundooApp.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class NoteController : Controller
    {
        private readonly INoteManager manager;
        public NoteController(INoteManager manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        [Route("createnote")]
        public async Task<IActionResult> CreateNote([FromBody] NoteModel mynotes)
        {
            try
            {
                var result = this.manager.CreateNote(mynotes);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Note Created Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string> { Status = false, Message = "Note Creation Failed" });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet]
        [Route("getnotes")]
        public IActionResult GetNote(int userId)
        {
            try
            {
                var result = this.manager.GetNote(userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Note recived", Data = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string> { Status = false, Message = "Note not recived" });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPut]
        [Route("editnote")]
        public async Task<IActionResult> EditNote([FromBody] NoteModel note)
        {
            try
            {
                var result = this.manager.EditNote(note);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Note Edited", Data = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string> { Status = false, Message = "Cannot edit Note" });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPut]
        [Route("changecolour")]
        public async Task<IActionResult> ChangeColour([FromBody] NoteModel note)
        {
            try
            {
                var result = this.manager.ChangeColour(note);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Colour changed", Data = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string> { Status = false, Message = "Cannot change colour " });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPut]
        [Route("archivenote")]
        public async Task<IActionResult> NoteArchive(int noteId)
        {
            try
            {
                string result = await this.manager.NoteArchive(noteId);
                if (result.Equals("Archived") || result.Equals("Unarchived"))
                {
                    return this.Ok(new { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string> { Status = false, Message = result });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPut]
        [Route("pinningNote")]
        public async Task<IActionResult> Pinning(int noteId)
        {
            try
            {
                string result = await this.manager.Pinning(noteId);
                if (result.Equals("Note pinned") || result.Equals("Note unpinned") || result.Equals("Note unarchived and pinned"))
                {
                    return this.Ok(new { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string> { Status = false, Message = result });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet]
        [Route("getArchiveNotes")]
        public IActionResult GetArchiveNote(int userId)
        {
            try
            {
                var result = this.manager.GetArchiveNote(userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Here is your Note", Data = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string> { Status = false, Message = "Cannot read notes" });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet]
        [Route("gettrashnotes")]
        public IActionResult GetTrashNote(int userId)
        {
            try
            {
                var result = this.manager.GetTrashNote(userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Here is your Note", Data = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string> { Status = false, Message = "Cannot read notes" });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpDelete]
        [Route("movetotrash")]
        public async Task<IActionResult> DeleteNote(int noteId)
        {
            try
            {
                string result = await this.manager.DeleteNote(noteId);
                if (result.Equals("Note unpinned and Trashed") || result.Equals("Note Trashed"))
                {
                    return this.Ok(new { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string> { Status = false, Message = result });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPut]
        [Route("retriveNote")]
        public async Task<IActionResult> RetriveFromTrash(int noteId)
        {
            try
            {
                string result = await this.manager.RetriveFromTrash(noteId);
                if (result.Equals("Note Restored"))
                {
                    return this.Ok(new { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string> { Status = false, Message = result });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [HttpPut]
        [Route("deleteTrash")]
        public async Task<IActionResult> DeleteFromTrash(int notesId)
        {
            try
            {
                string result = await this.manager.DeleteFromTrash(notesId);
                if (result.Equals("Note Deleted Permanently"))
                {
                    return this.Ok(new { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string> { Status = false, Message = result });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPut]
        [Route("addReminder")]
        public async Task<IActionResult> AddReminder(int notesId, string remind)
        {
            try
            {
                string result = await this.manager.AddReminder(notesId, remind);
                if (result.Equals("Remind me"))
                {
                    return this.Ok(new { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string> { Status = false, Message = result });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPut]
        [Route("deleteReminder")]
        public async Task<IActionResult> DeleteReminder(int noteId)
        {
            try
            {
                string result = await this.manager.DeleteReminder(noteId);
                if (result.Equals("Reminder Deleted"))
                {
                    return this.Ok(new { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string> { Status = false, Message = result });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet]
        [Route("getReminder")]
        public IActionResult GetReminder(int noteId)
        {
            try
            {
                var result = this.manager.GetReminder(noteId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Here is your Reminder", Data = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string> { Status = false, Message = "Note does not exist" });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPut]
        [Route("addImages")]
        public async Task<IActionResult> AddingImage(int noteId)//Add this (, IFormFile form) when cloudinary work 
        {
            try
            {
                string result = await this.manager.AddingImage(noteId);
                if (result.Equals("Image added Successfully"))
                {
                    return this.Ok(new { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string> { Status = false, Message = result });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPut]
        [Route("removeImage")]
        public async Task<IActionResult> RemovingImage(int noteId)
        {
            try
            {
                string result = await this.manager.RemovingImage(noteId);
                if (result.Equals("Removed Image Successfully"))
                {
                    return this.Ok(new { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string> { Status = false, Message = result });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
