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
  }
}
