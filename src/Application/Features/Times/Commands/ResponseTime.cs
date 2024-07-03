using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Times.Commands
{
    public class ResponseTimeDto
    {
        public string Description { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime? EndDate { get; set; }

    }
}
