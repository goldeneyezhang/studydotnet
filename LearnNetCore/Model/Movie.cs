﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnNetCore.Model
{
    public class Movie
    {
		public int ID { get; set; }
		public string Title { get; set; }
		public DateTime ReleaseDate { get; set; }
		public string Genre { get; set; }
		public decimal Price { get; set; }
    }
}
