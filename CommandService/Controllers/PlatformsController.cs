using System;
using Microsoft.AspNetCore.Mvc;

namespace CommandService.Controllers {
    [Route( "api/c/[controller]" )]
    [ApiController]
    public class PlatformsController : ControllerBase {
        public PlatformsController(){
            
        }
        [HttpPost]
        public ActionResult TestConnection(){
            Console.WriteLine( "--> POST request to Command Service" );
            return Ok( "--> Response to POST request" );
        }
    }
}