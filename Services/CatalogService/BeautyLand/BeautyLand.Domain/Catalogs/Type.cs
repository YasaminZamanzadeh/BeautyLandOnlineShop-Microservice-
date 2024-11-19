using BeautyLand.Domain.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyLand.Domain.Catalogs
{
    [AudiTable]
    public class Type
    {
        public Type()
        {
                Items = new List<Item>();   
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<Item> Items { get; set; }
    }
}