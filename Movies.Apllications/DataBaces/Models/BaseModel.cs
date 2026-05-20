using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Applications.DataBaces.Models
{
    public class BaseModel
    {
        public bool IsDeleted { get; set; } = false;
        public Guid Id { get; init; }
    }
}
