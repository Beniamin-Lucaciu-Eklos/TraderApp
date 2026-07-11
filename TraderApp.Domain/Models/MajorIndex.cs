using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraderApp.Domain.Models
{
    public class MajorIndex
    {
        public double Price { get; set; }

        public double Changes { get; set; }

        public MajorIndexType Type { get; set; }
    }
}
