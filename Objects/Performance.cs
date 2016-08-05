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

    public int GetId()
    {
      return _id;
    }

    public int GetVenueId()
    {
      return _venueId;
    }
    public void SetVenueId(int newVenueId)
    {
      _venueId = newVenueId;
    }

    public int GetBandId()
    {
      return _bandId;
    }
    public void SetBandId(int newBandId)
    {
      _bandId = newBandId;
    }

    public string GetPerformanceDate()
    {
      return _performanceDate.ToString("MM/dd/yyyy");
    }
    public void SetVenueId(DateTime newPerformanceDate)
    {
      _performanceDate = newPerformanceDate;
    }

    public override bool Equals(System.Object otherPerformance)
    {
      if (!(otherPerformance is Performance))
      {
        return false;
      }
      else
      {
        Performance newPerformance = (Performance) otherPerformance;
        bool idEquality = this.GetId() == newPerformance.GetId();
        bool venueIdEquality = this.GetVenueId() == newPerformance.GetVenueId();
        bool bandIdEquality = this.GetBandId() == newPerformance.GetBandId();
        bool performanceDateEquality = this.GetPerformanceDate() == newPerformance.GetPerformanceDate();

        return (idEquality && venueIdEquality && bandIdEquality && performanceDateEquality);
      }
    }

    public static List<Performance> GetAll()
    {
      List<Performance> allPerformances = new List<Performance>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM performances ORDER BY performance_date;", conn);
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

    public string GetBandName()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT name FROM bands WHERE id = @BandId;", conn);
      SqlParameter bandIdParameter = new SqlParameter();
      bandIdParameter.ParameterName = "@BandId";
      bandIdParameter.Value = this.GetBandId();

      cmd.Parameters.Add(bandIdParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      string foundBandName = "";

      while (rdr.Read())
      {
        foundBandName = rdr.GetString(0);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundBandName;
    }

    public string GetVenueName()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT name FROM venues WHERE id = @VenueId;", conn);
      SqlParameter venueIdParameter = new SqlParameter();
      venueIdParameter.ParameterName = "@VenueId";
      venueIdParameter.Value = this.GetVenueId();

      cmd.Parameters.Add(venueIdParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      string foundVenueName = "";

      while (rdr.Read())
      {
        foundVenueName = rdr.GetString(0);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundVenueName;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO performances (venue_id, band_id, performance_date) OUTPUT INSERTED.id VALUES (@VenueId, @BandId, @PerformanceDate);", conn);

      SqlParameter venueIdParameter = new SqlParameter();
      venueIdParameter.ParameterName = "@VenueId";
      venueIdParameter.Value = this.GetVenueId();
      cmd.Parameters.Add(venueIdParameter);

      SqlParameter bandIdParameter = new SqlParameter();
      bandIdParameter.ParameterName = "@BandId";
      bandIdParameter.Value = this.GetBandId();
      cmd.Parameters.Add(bandIdParameter);

      SqlParameter performanceDateParameter = new SqlParameter();
      performanceDateParameter.ParameterName = "@PerformanceDate";
      performanceDateParameter.Value = this.GetPerformanceDate();
      cmd.Parameters.Add(performanceDateParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      while (rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }

    public static Performance Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM performances WHERE id = @PerformanceId;", conn);
      SqlParameter performanceIdParameter = new SqlParameter();
      performanceIdParameter.ParameterName = "@PerformanceId";
      performanceIdParameter.Value = id.ToString();

      cmd.Parameters.Add(performanceIdParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      int foundPerformanceId = 0;
      int foundVenueId = 0;
      int foundBandId = 0;
      DateTime foundPerformanceDate = DateTime.MinValue;

      while (rdr.Read())
      {
        foundPerformanceId = rdr.GetInt32(0);
        foundVenueId = rdr.GetInt32(1);
        foundBandId = rdr.GetInt32(2);
        foundPerformanceDate = rdr.GetDateTime(3);
      }

      Performance foundPerformance = new Performance(foundVenueId, foundBandId, foundPerformanceDate, foundPerformanceId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundPerformance;
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
