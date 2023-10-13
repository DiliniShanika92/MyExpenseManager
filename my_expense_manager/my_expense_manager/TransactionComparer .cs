using my_expense_manager.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace my_expense_manager
{
   public class TransactionComparer : IEqualityComparer<transaction>
    {
        //public bool Equals(transaction x, transaction y)
        //{
        //    if (ReferenceEquals(x, y)) return true;
        //    if (ReferenceEquals(x, null) || ReferenceEquals(y, null)) return false;
        //    return x.Id == y.Id && 
        //           x.Amount == y.Amount &&
        //           x.Category == y.Category &&
        //           x.Discription == y.Discription &&
        //           x.TransactionType == y.TransactionType &&
        //           x.DateAndTime == y.DateAndTime ;
        //}

        //public int GetHashCode(transaction obj)
        //{
        //    if (ReferenceEquals(obj, null)) return 0;
        //    int hashId = obj.Id.GetHashCode();
        //    int hashAmount = obj.Amount.GetHashCode();
        //    int hashCategory = obj.Category.GetHashCode();
        //    int hashDiscription = obj.Discription.GetHashCode();
        //    int hashTransactionType = obj.TransactionType.GetHashCode();
        //    int hashDateAndTime = obj.DateAndTime.GetHashCode();
        //    return hashId ^ hashAmount ^ hashCategory ^ hashDiscription ^ hashTransactionType ^ hashDateAndTime;
        //}

        public bool Equals(transaction x, transaction y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null) || ReferenceEquals(y, null)) return false;
            return x.Id == y.Id && x.Amount==y.Amount && x.Category == y.Category && x.Discription == y.Discription && x.DateAndTime == y.DateAndTime;
        }

        public int GetHashCode(transaction obj)
        {
            if (ReferenceEquals(obj, null)) return 0;
            return obj.Id.GetHashCode();
        }
    }
}
