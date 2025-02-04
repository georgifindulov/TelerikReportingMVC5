using System;
using System.ComponentModel.DataAnnotations;

namespace TelerikReportingMVC5.ViewModels
{
    public class IndexViewModel
    {
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime JoinedSince { get; set; }
    }
}