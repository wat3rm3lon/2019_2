using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityApi.Dtos
{
    public class EntityDto
    {
        public Guid Id { get; set; }
        public DateTime OperationDate { get; set; }
        public Decimal Amount { get; set; }
    }
}
