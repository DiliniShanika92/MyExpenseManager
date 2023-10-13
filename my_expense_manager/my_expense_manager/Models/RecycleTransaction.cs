using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace my_expense_manager.Models
{
    public class RecycleTransaction
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int RId { get; set; }
        
    }
}
