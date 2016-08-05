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

    public static List<Performance> GetAll()
    {
      List<Performance> allPerformances = new List<Performance>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM performances;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while (rdr.Read())
      {
        int performanceId = rdr.GetInt32(0);
        int venueId = rdr.GetInt32(1);
        int bandId = rdr.GetInt32(2);
        DateTime performanceDate = rdr.GetDateTime(3);

        Performance newPerformance = new Performance(venueId, bandId, performanceDate, performanceId);
        allPerformances.Add(newPerformance);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allPerformances;
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
