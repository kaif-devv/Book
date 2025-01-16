using Microsoft.AspNetCore.Mvc;

namespace HomePage.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class Controller : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public IActionResult GetHomePage()
        {
            var htmlContent = @"
                <div style='font-family:monospace;text-align:center;margin-top:20%;'>
                    <h1 style='font-size:72px;'>Books API is Ready!</h1>
                    <p>For navigating to the swagger documentation, click <a href='/swagger'>here</a></p>
                </div>";
            return Content(htmlContent, "text/html");
        }
    }
}