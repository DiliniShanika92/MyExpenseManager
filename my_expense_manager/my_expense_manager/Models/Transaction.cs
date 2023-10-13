using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace my_expense_manager.Models
{
    public class transaction
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public double Amount { get; set; }
        public string Category { get; set; }
        public string Discription { get; set; }
        public DateTime DateAndTime { get; set; }
        public bool TransactionType { get; set; }



    }
}
