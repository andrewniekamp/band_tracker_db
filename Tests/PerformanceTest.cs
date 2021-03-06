using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BandTracker
{
  public class PerformanceTest : IDisposable
  {
    public PerformanceTest()
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
      int result = Performance.GetAll().Count;
      Assert.Equal(0, result);
    }

    [Fact]
    public void T2_Equal_ReturnsTrueIfPerformanceIsSame()
    {
      DateTime performanceDate = new DateTime(2016,08,04);

      Performance firstPerformance = new Performance(1, 2, performanceDate);
      Performance secondPerformance = new Performance(1, 2, performanceDate);

      Assert.Equal(firstPerformance, secondPerformance);
    }

    [Fact]
    public void T3_Save_SavesToDB()
    {
      DateTime performanceDate = new DateTime(2016,08,04);

      Performance testPerformance = new Performance(1, 2, performanceDate);
      testPerformance.Save();

      List<Performance> result = Performance.GetAll();
      List<Performance> testList = new List<Performance>{testPerformance};

      Assert.Equal(testList, result);
    }

    [Fact]
    public void T4_Save_AssignsIdToPerformance()
    {
      DateTime performanceDate = new DateTime(2016,08,04);

      Performance testPerformance = new Performance(1, 2, performanceDate);
      testPerformance.Save();

      Performance savedPerformance = Performance.GetAll()[0];
      int result = savedPerformance.GetId();
      int testId = testPerformance.GetId();

      Assert.Equal(testId, result);
    }

    [Fact]
    public void T5_Find_FindsPerformanceInDB()
    {
      DateTime performanceDate = new DateTime(2016,08,04);

      Performance testPerformance = new Performance(1, 2, performanceDate);
      testPerformance.Save();

      Performance foundPerformance = Performance.Find(testPerformance.GetId());

      Assert.Equal(testPerformance, foundPerformance);
    }

    [Fact]
    public void T6_GetBandName_GetsPerformanceBandName()
    {
      Band testBand = new Band("Brand New");
      testBand.Save();
      DateTime performanceDate = new DateTime(2016,08,04);

      Performance testPerformance = new Performance(1, testBand.GetId(), performanceDate);
      testPerformance.Save();

      string result = testPerformance.GetBandName();

      Assert.Equal("Brand New", result);
    }

    [Fact]
    public void T7_GetVenueName_GetsPerformanceVenueName()
    {
      Venue testVenue = new Venue("Paramount Theatre");
      testVenue.Save();
      DateTime performanceDate = new DateTime(2016,08,04);

      Performance testPerformance = new Performance(testVenue.GetId(), 1, performanceDate);
      testPerformance.Save();

      string result = testPerformance.GetVenueName();

      Assert.Equal("Paramount Theatre", result);
    }

  }
}
