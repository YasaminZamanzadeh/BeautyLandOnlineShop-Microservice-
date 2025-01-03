﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeautyLand.Infrastructure.Messages.MessagesBus.Dtos.Publisher;

namespace BeautyLand.Infrastructure.Messages.MessagesBus.Publisher
{
    public interface IMessageBus
    {
        void Publish(BaseMessageDto message, string quesueName);
    }
}
