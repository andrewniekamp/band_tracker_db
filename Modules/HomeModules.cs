using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace BandTracker
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        List<Venue> allVenues = Venue.GetAll();
        List<Band> allBands = Band.GetAll();
        model.Add("venues", allVenues);
        model.Add("bands", allBands);
        return View["index.cshtml", model];
      };

      // Post["/success"] = _ => {
      //
      // };
    }
  }
}
