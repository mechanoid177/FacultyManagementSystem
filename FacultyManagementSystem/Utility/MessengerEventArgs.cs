using System;
using System.Collections.Generic;
using System.Text;

namespace FacultyManagementSystem.Utility
{
    public class MessengerEventArgs : EventArgs
    {
        public string Message { get; set; }    

        public MessengerEventArgs(string message)
        {
            Message = message;
        }
    }
}
