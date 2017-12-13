﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoodReport.BLL.Interfaces
{
    public interface ISummaryReport<T>
    {
        Task ByDate(int date);
        Task<T> ByMonth(int month);
        Task ByYear(int year);
    }
}
