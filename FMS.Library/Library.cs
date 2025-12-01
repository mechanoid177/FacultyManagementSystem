using FMS.Faculty;
using System;
using System.Collections.Generic;
using System.Text;

namespace FMS.Library
{
    // Upon writing the name in the search bar suggestions should appear
    public class Library
    {
        public List<Person> Members { get; set; }
        public List<Book> Books { get; set; }

        public delegate void OnTransactionEventHandler(object sender, EventArgs args);
        public event OnTransactionEventHandler BorrowingFailed;
        public event OnTransactionEventHandler BorrowingSucceeded;
        public event OnTransactionEventHandler ReturnOverdue;
        public event OnTransactionEventHandler NotInSystem;



        public bool Transaction(string bookBarcode, string memberBarcode) { return false; }

        public bool Transaction(string bookBarcode, string memberBarcode) { return false; }

        public void AddBook() { }

        public void RemoveBook() { }

        public void ReplaceBook() { }

        public void SearchBooks() { }

        public void SearchMembers() { }

        private void FailedToBorrow()
        {
            OnBorrowed.Invoke(this, EventArgs.Empty);
        }
    }
}
