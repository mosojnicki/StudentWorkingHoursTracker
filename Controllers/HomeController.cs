using StudEvidencija.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace StudEvidencija.Controllers
{
	public class HomeController : Controller
	{
		private SqlConnection con;
		private void connection()
		{
			string conString = ConfigurationManager.ConnectionStrings["mojConnString"].ToString();
			con = new SqlConnection(conString);
		}
		public ActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public ViewResult DodajStudenta()
		{
			return View(new Student());
		}
		[HttpPost]
		public ViewResult DodajStudenta(Student student)
		{

			if (ModelState.IsValid)
			{
				string statement = "Insert";
				connection();
				SqlCommand cmd = new SqlCommand("stp_Master", con);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@StudID", DBNull.Value);
				cmd.Parameters.AddWithValue("@Ime", student.Ime.Trim());
				cmd.Parameters.AddWithValue("@Prezime", student.Prezime.Trim());
				cmd.Parameters.AddWithValue("@Fakultet", student.Fakultet.Trim());
				cmd.Parameters.AddWithValue("@Aktivan", 1);
				cmd.Parameters.AddWithValue("@Statement", statement);
				con.Open();
				int i = cmd.ExecuteNonQuery();
				con.Close();
				if (i >= 1)
				{
					return View("Success");
				}
				else
				{
					return View("error");
				}

			}
			else
			{
				return View("error");
			}

		}
		[HttpGet]
		public ViewResult PregledStudenata()
		{
			List<Student> lstStudenti = new List<Student>();
			connection();
			SqlCommand cmd = new SqlCommand(@"SELECT * FROM [StudEvidencija].[dbo].[Student] ORDER BY StudID DESC", con);
			con.Open();

			SqlDataReader reader = cmd.ExecuteReader();
			if (reader.HasRows)
			{
				while (reader.Read())
				{
					Student student = new Student();
					student.StudID = int.Parse(reader["StudID"].ToString());
					student.Ime = reader["Ime"].ToString();
					student.Prezime = reader["Prezime"].ToString();
					student.Fakultet = reader["Fakultet"].ToString();
					student.Aktivan = (bool)reader["Aktivan"];

					lstStudenti.Add(student);

				}
			}
			reader.Close();
			return View(lstStudenti);
		}
		[HttpGet]
		public ViewResult Edit(int? id)
		{
			connection();
			SqlCommand cmd = new SqlCommand(@"SELECT * FROM [StudEvidencija].[dbo].[Student] WHERE StudID='" + id + "'", con);
			con.Open();

			Student student = new Student();
			SqlDataReader reader = cmd.ExecuteReader();
			if (reader.HasRows)
			{
				while (reader.Read())
				{
					student.StudID = int.Parse(reader["StudID"].ToString());
					student.Ime = reader["Ime"].ToString();
					student.Prezime = reader["Prezime"].ToString();
					student.Fakultet = reader["Fakultet"].ToString();
					student.Aktivan = (bool)reader["Aktivan"];

				}
			}
			con.Close();
			reader.Close();
			return View(student);

		}

		[HttpPost]
		public ActionResult Edit(Student student)
		{
			string statement = "Update";
			connection();
			SqlCommand cmd = new SqlCommand("stp_Master", con);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@StudID", student.StudID);
			cmd.Parameters.AddWithValue("@Ime", student.Ime.Trim());
			cmd.Parameters.AddWithValue("@Prezime", student.Prezime.Trim());
			cmd.Parameters.AddWithValue("@Fakultet", student.Fakultet.Trim());
			cmd.Parameters.AddWithValue("@Aktivan", student.Aktivan);
			cmd.Parameters.AddWithValue("@Statement", statement);

			try
			{
				con.Open();
				cmd.ExecuteNonQuery();
				con.Close();

			}
			catch (Exception)
			{

				return View("error");
			}
			return RedirectToAction("PregledStudenata");
		}

		[HttpGet]
		public ActionResult EvidentirajRad()
		{
			Student student = new Student();
			student.ImePrezime = DajImenaPrezimena();
			return View(student);

			#region
			//DataTable dt = new DataTable();
			//SqlDataAdapter da = new SqlDataAdapter();
			//connection();
			//SqlCommand cmd = new SqlCommand(@"SELECT StudID, Ime+' '+Prezime AS Ime FROM [StudEvidencija].[dbo].[Student] WHERE Aktivan=1", con);
			//con.Open();
			//da.SelectCommand = cmd;
			//da.Fill(dt);
			//da.Dispose();
			//cmd.Dispose();
			//con.Close();

			//List<SelectListItem> items = new List<SelectListItem>();

			//for (int i = 0; i < dt.Rows.Count; i++)
			//{
			//	items.Add(new SelectListItem { Text = dt.Rows[i].ItemArray[1].ToString(), Value = dt.Rows[i].ItemArray[0].ToString() });

			//}
			//ViewData["lista"] = items;

			//return View(ViewData["lista"]);
			//return View();
			#endregion
		}
		[HttpPost]
		public ActionResult EvidentirajRad(Student student)
		{
			try
			{
				connection();
				SqlCommand cmd = new SqlCommand("EvidencijaRada_Insert", con);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@StudID", student.StudID);
				cmd.Parameters.AddWithValue("@Datum", student.rad.Datum);
				cmd.Parameters.AddWithValue("@VrijemeOd", student.rad.VrijemeOd);
				cmd.Parameters.AddWithValue("@VrijemeDo", student.rad.VrijemeDo);
				con.Open();
				int i = cmd.ExecuteNonQuery();
				con.Close();
				if (i >= 1)
				{
					return View("Success");
				}
			}
			catch (Exception)
			{
				return View("Error");
			}
			return RedirectToAction("EvidentirajRad");
		}

		public ViewResult PregledajStatistiku(DateTime? DatumOd, DateTime? DatumDo)
		{
			string _DatumOd;
			string _DatumDo;
			if (DatumDo != null && DatumDo != null)
			{
				_DatumOd = DatumOd.Value.Year.ToString() + "-" + DatumOd.Value.Month.ToString().PadLeft(2, '0') + "-" + DatumOd.Value.Day.ToString().PadLeft(2, '0');
				_DatumDo = DatumDo.Value.Year.ToString() + "-" + DatumDo.Value.Month.ToString().PadLeft(2, '0') + "-" + DatumDo.Value.Day.ToString().PadLeft(2, '0');
			}
			else
			{
				_DatumOd = null;
				_DatumDo = null;
			}
			string conStr = ConfigurationManager.ConnectionStrings["mojConnString"].ConnectionString;
			SqlConnection sqlconn = new SqlConnection(conStr);
			string query = @" 
WITH cte_minute_rada(_StudID, _Datum, MinuteRada) as
  (
    Select s.StudID as _StudID, e.Datum as _Datum, DATEDIFF(MINUTE, VrijemeOd, VrijemeDo) as MinuteRada
	from 
	Student s
	right join EvidencijaRada e on s.StudID=e.StudID
  )
  select s.Ime, s.Prezime, _StudId, sum(MinuteRada) as TrajanjeRada
  from cte_minute_rada
  left join Student s on s.StudID=cte_minute_rada._StudID
  where _Datum between ' " + _DatumOd + "' and '" + _DatumDo + "' group by s.Ime, s.Prezime, _StudID ";
			SqlCommand cmd = new SqlCommand(query, sqlconn);
			sqlconn.Open();
			SqlDataAdapter sda = new SqlDataAdapter(cmd);
			DataSet ds = new DataSet();
			sda.Fill(ds);
			List<Statistika> lstStatisika = new List<Statistika>();
			foreach (DataRow dr in ds.Tables[0].Rows)
			{
				lstStatisika.Add(new Statistika
				{
					Ime = dr["Ime"].ToString(),
					Prezime = dr["Prezime"].ToString(),
					TrajanjeRada = Convert.ToDecimal(dr["TrajanjeRada"]) / 60


				});
			}
			if (DatumDo != null && DatumDo != null)
			{
				lstStatisika.Add(new Statistika
				{
					DatumOd = Convert.ToDateTime(_DatumOd),
					DatumDo = Convert.ToDateTime(_DatumDo)
				});

			}

			sqlconn.Close();
			ModelState.Clear();
			if (lstStatisika.Count < 1)
			{
				return View();
			}
			else
			{
				return View(lstStatisika);
			}
		}

		private static List<SelectListItem> DajImenaPrezimena()
		{
			List<SelectListItem> items = new List<SelectListItem>();
			string conStr = ConfigurationManager.ConnectionStrings["mojConnString"].ConnectionString;
			using (SqlConnection con = new SqlConnection(conStr))
			{
				string query = @"SELECT StudID, Ime+' '+Prezime AS Ime FROM [StudEvidencija].[dbo].[Student] WHERE Aktivan=1";
				using (SqlCommand cmd = new SqlCommand(query))
				{
					cmd.Connection = con;
					con.Open();
					using (SqlDataReader sdr = cmd.ExecuteReader())
					{
						while (sdr.Read())
						{
							items.Add(new SelectListItem
							{
								Text = sdr["Ime"].ToString(),
								Value = sdr["StudID"].ToString()
							});
						}
					}
					con.Close();
				}

			}
			return items;

		}
	}
}