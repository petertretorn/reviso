using Reviso.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace Reviso.Domain.Entities
{

    public class Project : IEntity
    {
        public Project()
        {
            _timeRegistrations = new List<TimeRegistration>();
        }

        public DateTime Start{ get; set; }
        public DateTime End { get; set; }
        public Invoice Invoice { get; set; }
        public Contract Contract { get; set; }
        public string Name { get; set; }

        private List<TimeRegistration> _timeRegistrations;
        public IReadOnlyList<TimeRegistration> TimeRegistrations
        {
            get
            {
                return _timeRegistrations;
            }
        }
           
        public bool IsActive => End == DateTime.MinValue;

        public void Close()
        {
            if (!IsActive) throw new ArgumentException("Project already closed");

            End = DateTime.Now.Date;
        }

        public void AddTimeRegistration(TimeRegistration timeRegistration)
        {
            if (!IsActive) throw new ArgumentException("Cannot add timeregistration to closed project");

            _timeRegistrations.Add(timeRegistration);
        }
    }
}
