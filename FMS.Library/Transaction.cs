using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace FMS.Library
{
    public class Transaction
    {
        
        public Guid Id { get; set;  }

        public string BookBarcode { get; set; }

        public string MemberBarcode { get; set; }

        public DateTime BorrowDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        public DateTime DueDate { get; set; }

        public Transaction(Guid id, string bookBarcode, string memberBarcode, DateTime borrowDate, DateTime dueDate)
        {
            Id = id;
            BookBarcode = bookBarcode;
            MemberBarcode = memberBarcode;
            BorrowDate = borrowDate;
            DueDate = dueDate;
            ReturnDate = null;
        }

        public Transaction()
        {
            
        }
    }
}
