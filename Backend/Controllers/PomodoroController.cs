using Microsoft.AspNetCore.Mvc;  // Controller, HTTP, and routing functionality
using Pomodoro.Models;  // Imports all models

namespace Pomodoro.Controllers;  // Defines the logical location of this class

[ApiController]  // Tells ASP.NET this controller is a REST API controller
[Route("api/pomodoro")]  // Sets the base URL path for this controller


public class PomodoroController : ControllerBase // ControllerBase is the base class for API controllers
{
    private static PomodoroTimer _timer = new();  // Creates a single shared timer for the application

   [HttpGet]  // [HttpGet] responds to GET requests at /api/pomodoro
    public ActionResult<object> Get()  // Returns an anonymous object
    {
        return new
        {
            timeLeft = _timer.GetTimeLeft(),  // Calculated seconds left
            isRunning = _timer.IsRunning,  // Whether the timer is running
            isBreak = _timer.IsBreak  // Whether it is break time
        };
    }

    [HttpPost("start")] // Responds to POST /api/pomodoro/start
    public IActionResult Start()
    {
        _timer.Start();  // Sets IsRunning = true and records start time
        return Ok(_timer);  // Returns HTTP 200 with the timer object as JSON
    }

    [HttpPost("pause")] // Responds to POST /api/pomodoro/pause
    public IActionResult Pause()
    {
        _timer.Pause();  // Sets IsRunning = false and updates TimeLeft
        return Ok(_timer);
    }
    

    [HttpPost("reset")]  // Responds to POST /api/pomodoro/reset
    public IActionResult Reset()
    {
        _timer.Reset();  // Resets timer and sets IsRunning = false.
        return Ok(_timer);
    }

    [HttpPost("skip")]
    public IActionResult Skip()
        {
            _timer.Skip();
            return Ok(_timer);
        }
}
