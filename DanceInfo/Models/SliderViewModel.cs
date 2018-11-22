using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DanceInfo.Models
{
    public class SliderViewModel
    {
        public IList<string> Images { get; set; }
        public int Speed { get; set; }
        public string ItemId { get; set; }
    }
}