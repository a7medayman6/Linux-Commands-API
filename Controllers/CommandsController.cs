using System;
using System.Collections.Generic;
using API.Data;
using API.Dtos;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        
        private readonly IAPIRepo _repo;
        private readonly IMapper _mapper;

        public CommandsController(IAPIRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        
        // GET api/commands
        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands()
        {
            
            var commandItems = _repo.GetAllCommands();

            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
        }

        // GET api/commands/{id}
        [HttpGet("{id}", Name="GetCommandById")]
        public ActionResult<CommandReadDto> GetCommandById(int id)
        {
            var command_item = _repo.GetCommandById(id);
            if(command_item != null)
                return Ok(_mapper.Map<CommandReadDto>(command_item));
            return NotFound();
        }


        // POST api/commands
        [HttpPost]
        public ActionResult <CommandReadDto> CreateCommand(CommandCreateDto commandCreateDto)
        {
            // Convert the DTO to Command
            var commandModel = _mapper.Map<Command>(commandCreateDto);
            // Add the new command to the database
            _repo.CreateCommand(commandModel);
            // Commit the new command to the database
            _repo.SaveChanges();

            var commandReadDto = _mapper.Map<CommandReadDto>(commandModel);
            return CreatedAtRoute(nameof(GetCommandById), new {Id = commandReadDto.Id}, commandReadDto);      
        }
    }
}
