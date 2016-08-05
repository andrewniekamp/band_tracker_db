using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BandTracker
{
  public class VenueTest : IDisposable
  {
    public VenueTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
    }
    public void Dispose()
    {
      Venue.DeleteAll();
      Band.DeleteAll();
      Performance.DeleteAll();
    }

    [Fact]
    public void T1_DBEmptyAtFirst()
    {
      int result = Venue.GetAll().Count;
      Assert.Equal(0, result);
    }

    [Fact]
    public void T2_Equal_ReturnsTrueIfVenueIsSame()
    {
      Venue firstVenue = new Venue("Paramount Theatre");
      Venue secondVenue = new Venue("Paramount Theatre");

      Assert.Equal(firstVenue, secondVenue);
    }

    [Fact]
    public void T3_Save_SavesToDB()
    {
      Venue testVenue = new Venue("Paramount Theatre");
      testVenue.Save();

      List<Venue> result = Venue.GetAll();
      List<Venue> testList = new List<Venue>{testVenue};

      Assert.Equal(testList, result);
    }

    [Fact]
    public void T4_Save_AssignsIdToVenue()
    {
      Venue testVenue = new Venue("Paramount Theatre");
      testVenue.Save();

      Venue savedVenue = Venue.GetAll()[0];
      int result = savedVenue.GetId();
      int testId = testVenue.GetId();

      Assert.Equal(testId, result);
    }

    [Fact]
    public void T5_Find_FindsVenueInDB()
    {
      Venue testVenue = new Venue("Paramount Theatre");
      testVenue.Save();

      Venue foundVenue = Venue.Find(testVenue.GetId());

      Assert.Equal(testVenue, foundVenue);
    }

    [Fact]
    public void T6_Update_UpdatesVenueInDB()
    {
      Venue testVenue = new Venue("Epicodus Classroom");
      testVenue.Save();
      string newName = "Paramount Theatre";

      testVenue.Update(newName);
      string result = testVenue.GetName();

      Assert.Equal(newName, result);
    }

    [Fact]
    public void T7_Delete_DeleteVenueFromDB()
    {
      Venue testVenue1 = new Venue("Epicodus Classroom");
      testVenue1.Save();
      Venue testVenue2 = new Venue("Paramount Theatre");
      testVenue2.Save();

      testVenue1.Delete();
      List<Venue> result = Venue.GetAll();
      List<Venue> testVenues = new List<Venue>{testVenue2};

      Assert.Equal(testVenues, result);
    }

    [Fact]
    public void T8_GetBands_RetrievesAllVenueBands()
    {
      Venue testVenue = new Venue("Paramount Theatre");
      testVenue.Save();

      Band testBand1 = new Band("Brand New");
      testBand1.Save();
      Band testBand2 = new Band("A Little Old");
      testBand2.Save();

      DateTime performanceDate1 = new DateTime(2016,08,04);
      DateTime performanceDate2 = new DateTime(2016,09,20);

      Performance newPerformance1 = new Performance(testVenue.GetId(), testBand1.GetId(), performanceDate1);
      newPerformance1.Save();
      Performance newPerformance2 = new Performance(testVenue.GetId(), testBand2.GetId(), performanceDate2);
      newPerformance2.Save();

      List<Band> testBands = new List<Band> {testBand1, testBand2};
      List<Band> result = testVenue.GetBands();

      Assert.Equal(testBands, result);
    }
  }
}
