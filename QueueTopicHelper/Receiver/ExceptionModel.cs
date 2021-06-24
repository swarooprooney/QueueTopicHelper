using System;

namespace QueueTopicHelper.Receiver
{
    public class ExceptionModel
    {
        public Exception Exception { get; set; }
        public string Action { get; set; }
        public string Endpoint { get; set; }
        public string EntityPath { get; set; }

        public string ClientId { get; set; }


    }
}
