using System;
using System.Collections.Generic;
using FoodReport.DAL.Abstractions;

namespace FoodReport.DAL.Models
{
    public class Report : Entity
    {
        public List<Field> List { get; set; }
        public string Owner { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public bool isEdited { get; set; }
        public DateTime LastEdited { get; set; }
        public string EditedBy { get; set; }
        public string Message { get; set; }
    }
}