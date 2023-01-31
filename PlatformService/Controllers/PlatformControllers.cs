using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using PlatformService.Data;
using System.Collections.Generic;
using PlatformService.Dtos;
using System;
using PlatformService.Models;
using Microsoft.AspNetCore.Http;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController: ControllerBase
    {
        private readonly IPlatformRepo _repository;
        private readonly IMapper _mapper;

        public PlatformsController(IPlatformRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            Console.WriteLine("--> Geting Platforms....");

            var platformItems = _repository.GetAllPlatforms();

            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platformItems));
        }

        [HttpGet("{id}", Name = "GetPlatformById")]
        public ActionResult<PlatformReadDto> GetPlatform(int id)
        {
            var platformItem = _repository.GetPlatformById(id);

            if(platformItem != null)
            {
                return Ok(_mapper.Map<PlatformReadDto>(platformItem));
            }
            else{
                return NotFound();
            }
        }

        [HttpPost]
        public ActionResult<PlatformReadDto> CreatePlatform(PlatformCreateDto platformCreateDto)
        {
            var platformModel = _mapper.Map<Platform>(platformCreateDto);

            _repository.CreatePlatform(platformModel);
            _repository.SaveChanges();

            var platformReadDto = _mapper.Map<PlatformReadDto>(platformModel);

            return CreatedAtRoute("GetPlatformById", new { id = platformReadDto.Id }, platformReadDto);
        }
    }
}