﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV19.Services.Interfaces
{
    internal interface IAsyncDataService
    {
        string GetResult(DateTime time);
    }
}
