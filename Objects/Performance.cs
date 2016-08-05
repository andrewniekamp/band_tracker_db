using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace BandTracker
{
  public class Performance
  {
    private int _id;
    private int _venueId;
    private int _bandId;
    private DateTime _performanceDate;

    public Performance (int venueId, int bandId, DateTime performanceDate, int id = 0)
    {
      _id = id;
      _venueId = venueId;
      _bandId = bandId;
      _performanceDate = performanceDate;
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM performances;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
  }
}
