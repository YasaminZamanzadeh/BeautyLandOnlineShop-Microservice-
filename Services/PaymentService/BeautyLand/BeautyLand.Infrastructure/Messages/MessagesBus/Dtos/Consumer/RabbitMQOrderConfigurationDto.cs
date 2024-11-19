using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyLand.Infrastructure.Messages.MessagesBus.Dtos.Consumer
{
    public class RabbitMQOrderConfigurationDto
    {
            public string HostName { get; set; }
            public string OrderQueueName { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
        
    }
}
