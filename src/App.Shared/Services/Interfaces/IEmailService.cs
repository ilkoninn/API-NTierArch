﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Shared.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendNewContactNotificationAsync(string webUser);
    }
}
