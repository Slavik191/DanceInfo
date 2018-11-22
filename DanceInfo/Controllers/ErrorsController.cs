using DanceInfo.Models.Items;
using Glass.Mapper.Sc;
using Glass.Mapper.Sc.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DanceInfo.Controllers
{
    public class ErrorsController : GlassController
    {
        // GET: Errors
        public ActionResult InfoError() {       
        var model = GetContextItem<Error>();
        //var model = mvcContext.GetCurrentItem<Error>(); 
        return View("~/Views/Renderings/Error.cshtml", model);
        }
    }
}