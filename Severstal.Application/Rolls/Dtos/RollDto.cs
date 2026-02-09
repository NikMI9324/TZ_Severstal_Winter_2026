using System;
using System.Collections.Generic;
using System.Text;

namespace Severstal.Application.Rolls.Dtos
{
    public record RollDto
    {
        public int Id { get; init; }
        public double Length { get; init; }
        public double Weight { get; init; }
        public DateTime AddedDate { get; init; }
        public DateTime? RemovedDate { get; init; }
    }
}
