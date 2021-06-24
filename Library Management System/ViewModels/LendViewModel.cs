using Library_Management_System.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Management_System.ViewModels
{
    public class LendViewModel
    {
        public Book Book { get; set; }
        public IEnumerable<Customer> customers { get; set; }

    }
}
