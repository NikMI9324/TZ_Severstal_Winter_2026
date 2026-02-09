using MediatR;
using Microsoft.AspNetCore.Mvc;
using Severstal.Application.Rolls.Commands.AddRoll;
using Severstal.Application.Rolls.Commands.GetFilteredData;
using Severstal.Application.Rolls.Commands.GetStatistics;
using Severstal.Application.Rolls.Commands.RemoveRoll;
using Severstal.Application.Rolls.Dtos;

namespace Severstal.Api.Endpoints
{
    public static class RollsEndpoints
    {
        public static void AddRollsEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/rolls");

            group.MapPost("add", async ([FromBody] AddRollCommand command, ISender mediator,
                CancellationToken ct) =>
            {
                var rollCreated = await mediator.Send(command, ct);
                return Results.Created($"/api/rolls/{rollCreated.Id}", rollCreated);
            })
            .Produces<CreateRollResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest);

            group.MapPut("remove/{id:int}", async ([FromRoute] int id, ISender mediator, CancellationToken ct) =>
            {
                var result = await mediator.Send(new RemoveRollCommand { Id = id }, ct);
                return Results.Ok(result);
            })
            .Produces<RemoveRollResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status409Conflict)
            .ProducesProblem(StatusCodes.Status400BadRequest);

            group.MapGet("statistics", async ([AsParameters] GetStatisticsCommand command, 
                ISender mediator,
                CancellationToken ct) =>
            {
                var getStatisticsResponse = await mediator.Send(command, ct);
                return Results.Ok(getStatisticsResponse);
            })
            .Produces<RollStatisticsDto>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest);

            group.MapGet("", async ([AsParameters] GetFilteredDataCommand command, 
                ISender mediator,
                CancellationToken ct) =>
            {
                var rollsResponse = await mediator.Send(command, ct);
                return Results.Ok(rollsResponse);
            })
            .Produces<RollDto>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest);


        }
    }
}
