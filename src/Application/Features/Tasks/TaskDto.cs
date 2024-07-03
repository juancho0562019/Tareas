using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Tasks
{
    public class TaskDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Customer { get; set; }
    }

    public class TaskWithTimesDto : TaskDto
    {
        public List<TimeDto>? Times { get; set; }
    }

    public class TimeDto
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public double SpentTime { get; set; }
    }

}
