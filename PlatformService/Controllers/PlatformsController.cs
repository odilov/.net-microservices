using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.DTOs;
using PlatformService.Models;
using PlatformService.SyncDataServices.Http;

namespace PlatformService.Controllers {
    [Route( "api/[controller]" )]
    [ApiController]
    public class PlatformsController : ControllerBase {
        private readonly IPlatformRepo _repository;
        private readonly IMapper _mapper;
        private readonly ICommandDataClient _commandDataClient;

        public PlatformsController( IPlatformRepo repository, IMapper mapper, ICommandDataClient commandDataClient ){
              _repository = repository;
              _mapper = mapper;
              _commandDataClient = commandDataClient;
        }
        [HttpGet]
        public ActionResult <IEnumerable<PlatformReadDto>> GetPlatforms(){
            Console.WriteLine( "--> Getting Platforms" );
            var platformItems = _repository.GetPlatforms();
            return Ok( _mapper.Map<IEnumerable<PlatformReadDto>>( platformItems ) );
        }
        [HttpGet( "{id}", Name = "GetPlatformById" )]
        public ActionResult <PlatformReadDto> GetPlatformById( int id ){
            var platformItem = _repository.GetPlatformById( id );
            if( platformItem != null ){
                return Ok( _mapper.Map<PlatformReadDto>( platformItem ) );
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<ActionResult<PlatformReadDto>> CreatePlatform( PlatformCreateDto platform ){
            var platformModel = _mapper.Map<Platform>( platform );
            _repository.CreatePlatform( platformModel );
            _repository.SaveChanges();
            var platformReadDto = _mapper.Map<PlatformReadDto>( platformModel );

            try{
               await _commandDataClient.SendToCommandService( platformReadDto );  
            }
            catch( Exception excep ){
                Console.WriteLine( $"--> Couldn't send a sync req : { excep.Message }" );
                throw;
            }

            return CreatedAtRoute( nameof( GetPlatformById ),  new { ID = platformReadDto.ID }, platformReadDto );
        }
    }
}