using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyLand.Infrastructure.Messages.MessagesBus.Dtos.Publisher
{
    public class ItemChangesDto: BaseMessageDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
