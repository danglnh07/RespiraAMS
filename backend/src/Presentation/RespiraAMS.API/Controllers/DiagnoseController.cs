using BuildingBlocks.Dtos;
using Microsoft.AspNetCore.Mvc;
using RespiraAMS.Application.Features.Diagnose;
using Wolverine;

namespace RespiraAMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DiagnoseController(IMessageBus bus) : ControllerBase
{
    [HttpPost]
    public async Task<ApiResponse<DiagnoseResult>> Diagnose([FromBody] DiagnoseQuery request)
    {
        var result = await bus.InvokeAsync<DiagnoseResult>(request);
        return ApiResponse<DiagnoseResult>.Ok(result);
    }
}