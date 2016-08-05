using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BandTracker
{
  public class BandTest : IDisposable
  {
    public BandTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
    }
    public void Dispose()
    {
      Band.DeleteAll();
    }

    [Fact]
    public void T1_DBEmptyAtFirst()
    {
      int result = Band.GetAll().Count;
      Assert.Equal(0, result);
    }

    [Fact]
    public void T2_Equal_ReturnsTrueIfBandIsSame()
    {
      Band firstBand = new Band("Brand New");
      Band secondBand = new Band("Brand New");

      Assert.Equal(firstBand, secondBand);
    }

    [Fact]
    public void T3_Save_SavesToDB()
    {
      Band testBand = new Band("Brand New");
      testBand.Save();

      List<Band> result = Band.GetAll();
      List<Band> testList = new List<Band>{testBand};

      Assert.Equal(testList, result);
    }
  }
}
