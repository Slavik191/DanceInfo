using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DanceInfo.Models.Items
{
    public class Error
    {
        public virtual string Title { get; set; }
        public virtual string Content { get; set; }
        public virtual string Example { get; set; }
    }
}