using Api.Data;
using Api.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    public class GoalsController : ControllerBase
    {
        private readonly ILogger<GoalsController> _logger;
        private readonly ApplicationDbContext _context;

        public GoalsController(ILogger<GoalsController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("api/GetGoals")]
        public IEnumerable<Goal> GetGoals()
        {
            var goals = _context.Goals.ToList();
            return goals;
        }

        [HttpPost("api/CreateGoal/{title}/{description}/{date}")]
        public ActionResult<string> CreateGoal(string title, string description, DateTime date)
        {
            _context.Goals.Add(new Goal { Title = title, Description = description, Date = date });
            try
            {
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("api/EditGoal/{id}/{title}/{description}/{date}")]
        public ActionResult<string> EditGoal(int id, string title, string description, DateTime date)
        {
            var goal = _context.Goals.FirstOrDefault(x => x.Id == id);
            goal.Title = title;
            goal.Description = description;
            goal.Date = date;
            try
            {
                _context.SaveChanges();
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("api/DeleteGoal/{id}")]
        public ActionResult<string> DeleteGoal(int id)
        {
            var goal = _context.Goals.SingleOrDefault(x => x.Id == id);
            _context.Goals.Remove(goal);
            try
            {
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}