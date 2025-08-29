using Microsoft.AspNetCore.Mvc;

namespace projetoGloboClima.Shared.OutPut
{
    public interface IOutputPort
    {
        IActionResult InvalidRequest(string message);
        IActionResult InvalidRequest();
        IActionResult Success(object obj);
        IActionResult Ok();
        IActionResult NotFound(string message);
    }
}
