﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QueueTopicHelper.Receiver
{
    public interface IReceiver
    {
        Task ReadMessageAsync();
        Task CloseQueueAsync();
        event EventHandler<string> RaiseMessageReadyEvent;
        event EventHandler<ExceptionModel> RaiseExceptionEvent;
    }

}
