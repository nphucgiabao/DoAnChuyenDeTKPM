﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageQueue.Interfaces
{
    public interface IQueueSubcribercs
    {
        Task ProcessQueue();
    }
}
