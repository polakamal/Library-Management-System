﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Management_System.ViewModels
{
    public class HomeViewModel
    {
        public int CustomerCount { get; set; }
        public int AuthorCount { get; set; }
        public int BookCount { get; set; }
        public int LendBookCount { get; set; }
    }
}
