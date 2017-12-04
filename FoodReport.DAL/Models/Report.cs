using FoodReport.DAL.Abstractions;
using System;
using System.Collections.Generic;

namespace FoodReport.DAL.Models
{
    public class Report : Entity
    {
        public List<Field> List { get; set; }
        public string Owner { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public bool isEdited { get; set; }
        public DateTime EditingTime { get; set; }
    }
}
