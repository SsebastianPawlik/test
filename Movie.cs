using System;
using System.Collections.Generic;
using System.Text;

namespace Apibaza
{
    public class Movie
    {
        public string Title { get; set; }
        public string ReleaseDate { get; set; }
        public string Overview { get; set; }
        public string PosterPath { get; set; }
        public List<string> Actors { get; set; }
    }
}

