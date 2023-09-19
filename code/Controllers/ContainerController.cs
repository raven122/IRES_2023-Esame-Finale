using Ires2023Exam.DataTransferObjects;
using Ires2023Exam.DbContexts;
using Ires2023Exam.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;

namespace Ires2023Exam.Controllers;

[ApiController]
[Route("[controller]")]
public class ContainerController : ControllerBase
{
    private readonly AppDbContext _db;

    public ContainerController() {
        _db = new AppDbContext(null);
    }

    [HttpGet("add-container")]
    public async Task<AddContainerOutputDto> AddContainer(AddContainerInputDto dto) {
        var spots = await _db.Spots
            .OrderBy(s => s.X)
                .ThenBy(s => s.Y)
            .ToListAsync();

        var grid = spots
            .GroupBy(s => s.X)
            .Select(g => g
                .OrderBy(x => x.Y)
                .ToArray()
            )
            .ToArray();

        // trying horizontally
        for (var i = 0; i < grid.Length; i++) {
            for (var j = 0; j < grid[i].Length - dto.SpotLength + 1; j++) {
                bool fit = true;
                for (int k = 0; k < dto.SpotLength; k++) {
                    if (grid[i][j+k].ContainerId != null) {
                        fit = false;
                        break;
                    }
                }
                if (fit) {
                    var container = new ContainerEntity {
                        Code = dto.Code,
                        Length = dto.SpotLength,
                        Type = dto.ContentType,

                    };

                    // 4 aggiunta validazioni su lunghezza container e tipo
                    if (container.Length > 3)
                    {
                        return new AddContainerOutputDto { Error = "Container length exceeds the maximum length allowed" };
                    }
                    if (container.Type == null)
                    {
                        return new AddContainerOutputDto { Error = "You must provide a type for the container" };
                    }
                    _db.Containers.Add(container);
                    await _db.SaveChangesAsync();

                    for (int k = 0; k < dto.SpotLength; k++) {
                        grid[i][j+k].ContainerId = container.Id;
                    }
                    await _db.SaveChangesAsync();
                    return new AddContainerOutputDto { InsertedId = container.Id };
                }
            }
        }

        // trying vertically - omitted for brevity

        return new AddContainerOutputDto { Error = "No space for the new container!" };

        


    }

    // 5
    [HttpGet("get-empty-spots")]
    public async Task<List<AvailableSpotDto>> GetEmptySpots()
    {
        return await _db.Spots
            .Where(s => s.ContainerId == null)
            .OrderBy(s => s.X)
                .ThenBy(s => s.Y)
            .Select(s => new AvailableSpotDto { X = s.X, Y = s.Y })
            .ToListAsync();
    }

}
