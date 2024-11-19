using BeautyLand.Subscription.Messages.MessagesBus.Dtos.Publisher;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace BeautyLand.Subscription.Messages.MessagesBus.Publisher
{
    public interface IMessagePublisher
    {
        void Publish(BaseMessageDto message, string queueName);
    }
}
