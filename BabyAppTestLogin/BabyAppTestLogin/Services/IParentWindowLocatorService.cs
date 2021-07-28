using System;
using System.Collections.Generic;
using System.Text;

namespace BabyAppTestLogin.Services
{
    /// <summary>
    /// Simple platform specific service that is responsible for locating a 
    /// </summary>
    public interface IParentWindowLocatorService
    {
        object GetCurrentParentWindow();
    }
}

