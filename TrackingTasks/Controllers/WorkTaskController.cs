using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrackingTasks.Models;

namespace TrackingTasks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkTaskController : ControllerBase
    {

        private readonly WorkTaskDbContext _workTaskDbContext;

        public WorkTaskController(WorkTaskDbContext workTaskDbContext)
        {
            _workTaskDbContext = workTaskDbContext;
        }

        [HttpGet]   
        [Route("GetWorkTask")]
        public async Task<ActionResult<IEnumerable<WorkTask>>> GetWorkTasks()
        {
            var response = await _workTaskDbContext.WorkTask.ToListAsync();
            return response != null ? Ok(response) : NotFound();   
        }

        [HttpPost]
        [Route("AddWorkTask")]
        public async Task<WorkTask> AddWorkTask(WorkTask objWorkTask)
        {
            _workTaskDbContext.WorkTask.Add(objWorkTask);
            await _workTaskDbContext.SaveChangesAsync();
            return objWorkTask;
        }

        [HttpPatch]
        [Route("UpdateWorkTask/{id}")]

        public async Task<WorkTask> UpdateWorkTask(WorkTask objWorkTask)
        {
            _workTaskDbContext.Entry(objWorkTask).State = EntityState.Modified;
            await _workTaskDbContext.SaveChangesAsync();    
            return objWorkTask;
        }

        [HttpDelete]
        [Route("DeleteWorkTask/{id}")]

        public bool DeleteWorkTask(int id)
        {
            bool dTask = false;
            var workTask = _workTaskDbContext.WorkTask.Find(id);
            if (workTask != null)
            {
                dTask = true;
                _workTaskDbContext.Entry(workTask).State = EntityState.Deleted;
                _workTaskDbContext.SaveChanges();
            }
            else
            {
                dTask = false;
            }
            return dTask;
        }

    }
}
