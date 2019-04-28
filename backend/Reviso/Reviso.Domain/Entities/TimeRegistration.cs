using Reviso.Domain.Interfaces;
using System;

namespace Reviso.Domain.Entities
{
    public class TimeRegistration : IEntity
    {
        public int Hours { get; set; }
        public int ProjectId { get; set; }
        public DateTime Date { get; set; }
    }
}
