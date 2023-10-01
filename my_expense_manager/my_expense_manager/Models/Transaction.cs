using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace my_expense_manager.Models
{
    public class Transaction
    {
        [PrimaryKey, AutoIncrement]
        public int? Id { get; set; }
       
    }
}
