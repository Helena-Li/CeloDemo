using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CeloDemo.Models
{
    public class FilterViewModel
    {
        public FilterViewModel()
        {
            this.Number = 10;
        }
        public int Number { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
