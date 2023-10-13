using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace my_expense_manager.Models
{
    public class Category
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }

        public bool CategoryType { get; set; }
    }
}
