using Microsoft.AspNetCore.Mvc;

namespace projetoGloboClima.Shared.OutPut
{
    public class OutPutPort : ControllerBase, IOutputPort
    {
        public IActionResult InvalidRequest(string message)
        {
            return BadRequest(message);
        }

        public IActionResult InvalidRequest()
        {
            return BadRequest();
        }

        public IActionResult NotFound(string message)
        {
            return base.NotFound(message);
        }

        public IActionResult Success(object obj)
        {
            return Ok(obj);
        }

        public IActionResult Ok()
        {
            return Ok();
        }
    }
}
