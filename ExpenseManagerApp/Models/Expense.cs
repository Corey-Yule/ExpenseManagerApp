﻿using SQLite;

namespace ExpenseManagerApp.Models
{
    public class Expense
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Title { get; set; }
        public decimal Amount { get; set; }


        public DateTime Date { get; set; }
        public string Category { get; set; }
    }
}
