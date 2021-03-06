﻿using Reviso.Application.Dtos;
using System.Collections.Generic;

namespace Reviso.Application
{
    public interface IRegistrationService
    {
        int AddRegistration(RegistrationDto registration);
        IEnumerable<RegistrationDto> GetRegistrationsForProject(int projectId);
        RegistrationDto GetRegistration(int id);
        void DeleteRegistration(int registration);
    }
}