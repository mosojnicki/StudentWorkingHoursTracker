using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudEvidencija.Models
{
	public class Statistika
	{
		public string Ime { get; set; }
		public string Prezime { get; set; }


		[DataType(DataType.Date)]
		[Display(Name = "Od datuma")]
		[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
		public DateTime? DatumOd { get; set; }

		[DataType(DataType.Date)]
		[Display(Name = "Do datuma")]
		[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
		public DateTime? DatumDo { get; set; }

		[Display(Name = "Radni sati")]
		[DisplayFormat(ApplyFormatInEditMode = true)]
		public decimal TrajanjeRada { get; set; }




	}
}