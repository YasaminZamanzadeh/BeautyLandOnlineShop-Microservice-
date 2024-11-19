using BeautyLand.Infrastructure.Messages.MessagesBus.Dtos.Publisher;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeautyLand.Infrastructure.Messages.MessagesBus.Publisher
{
    public interface IMessageBus
    {
        void Publish(BaseMessageDto messages, string exchange);
    }
}
