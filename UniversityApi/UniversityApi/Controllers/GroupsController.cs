using Microsoft.AspNetCore.Mvc;
using Services.Services.Interfaces;
using UniversityApi.Dtos;
using System;
using System.Collections.Generic;

namespace UniversityApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupsController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpGet("")]
        public ActionResult<List<GroupGetDto>> GetGroups()
        {
            try
            {
                return Ok(_groupService.GetAll());
            }
            catch (Exception ex)
            {
               
                return StatusCode(500, "An unknown error occurred");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<GroupGetDto> GetById(int id)
        {
            try
            {
                return Ok(_groupService.GetById(id));
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, "An unknown error occurred");
            }
        }

        [HttpPost("")]
        public ActionResult Create(CreateDto createDto)
        {
            try
            {
                var id = _groupService.Create(createDto);
                return StatusCode(201, new { id });
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, "An unknown error occurred");
            }
        }

        [HttpPut("{id}")]
        public ActionResult Edit(int id, EditDto editDto)
        {
            try
            {
                var groupId = _groupService.Edit(id, editDto);
                return StatusCode(201, new { id = groupId });
            }
            catch (Exception ex)
            {
               
                return StatusCode(500, "An unknown error occurred");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var groupId = _groupService.Delete(id);
                return StatusCode(201, new { id = groupId });
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, "An unknown error occurred");
            }
        }
    }
}
