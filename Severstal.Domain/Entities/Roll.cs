using Severstal.Domain.Exeptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Severstal.Domain.Entities
{
    public class Roll
    {
        public int Id { get; set; }
        public double Length { get; private set; }
        public double Weight { get; private set; }
        public DateTime AddedDate { get; set; }
        public DateTime? RemovedDate { get; set; }
        public bool IsRemoved => RemovedDate.HasValue;
        public Roll() { }

        public Roll(double length, double weight)
        {
            if (length <= 0) throw new InvalidDataRollException("Длина должна быть больше нуля");
            if (weight <= 0) throw new InvalidDataRollException("Вес должен быть больше нуля");
            Length = length;
            Weight = weight;
            AddedDate = DateTime.Today;
        }
    }
}
