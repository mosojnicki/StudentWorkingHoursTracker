using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudEvidencija.Models
{
	[Table("StudentData")]
	public class Student
	{
		public int StudID { get; set; }
		[Required]
		public string Ime { get; set; }
		[Required]
		public string Prezime { get; set; }
		[Display(Name ="Ime i prezime")]
		public List<SelectListItem> ImePrezime { get; set; }
		[Required]
		public string Fakultet { get; set; }
		public bool Aktivan { get; set; }
		public Rad rad { get; set; }
	}
}