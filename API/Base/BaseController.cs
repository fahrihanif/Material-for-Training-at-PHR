using API_CodeFirst.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API_CodeFirst.Base;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class BaseController<IRepository, Entity, Key> : ControllerBase
    where Entity : class
    where IRepository : IGeneralRepository<Entity, Key>
{
    private readonly IRepository repository;

    public BaseController(IRepository repository)
    {
        this.repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult> GetAllAsync()
    {
        var results = await repository.GetAllAsync();
        if (results.Count() is 0)
        {
            return NotFound(new
            {
                statusCode = 404,
                message = "Data Not Found!"
            });
        }
        return Ok(new
        {
            statusCode = 200,
            message = "Data Found!",
            data = results
        });
    }

    [HttpGet("{key}")]
    public async Task<ActionResult> GetByIdAsync(Key key)
    {
        var result = await repository.GetByIdAsync(key);
        if (result is null)
        {
            return NotFound(new
            {
                statusCode = 404,
                message = "Data Not Found!"
            });
        }
        return Ok(new
        {
            statusCode = 200,
            message = "Data Found!",
            data = result
        });
    }

    [HttpPost]
    public async Task<ActionResult> InsertAsync(Entity entity)
    {
        var result = await repository.InsertAsync(entity);
        if (result is 0)
        {
            return Conflict(new
            {
                statusCode = HttpStatusCode.Conflict,
                message = "Data Fail to Insert!"
            });
        }
        return Ok(new
        {
            statusCode = 200,
            message = "Data Saved Successfully!"
        });
    }

    [HttpPut("{key}")]
    public async Task<ActionResult> UpdateAsync(Entity entity, Key key)
    {
        var result = await repository.IsDataExist(key);
        if (!result)
        {
            return NotFound(new
            {
                statusCode = 404,
                message = "Data Not Found!"
            });
        }

        var update = await repository.UpdateAsync(entity);
        if (update is 0)
        {
            return Conflict(new
            {
                statusCode = 409,
                message = "Data Fail to Update!"
            });
        }

        return Ok(new
        {
            statusCode = 200,
            message = "Data Updated!"
        });
    }

    [HttpDelete("{key}")]
    public async Task<ActionResult> Delete(Key key)
    {
        var result = await repository.DeleteAsync(key);

        if (result is 0)
        {
            return Conflict(new
            {
                statusCode = 409,
                message = "Data Fail to Delete!"
            });
        }

        return Ok(new
        {
            statusCode = 200,
            message = "Data Deleted!"
        });
    }
}
