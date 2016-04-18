using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Models
{
    public class User
    {

    }

    public enum LogType
    {
        SignIn = 1,
        LogIn = 2,
        WaitForConfirm = 3,
        LogOut = 4,
        ProductDelete = 5,
        ProductInsert = 6,
        ProductUpdate = 7,
        Admin = 8,
        Log = 9,
        Home = 10
    }
    public enum LoginState
    {
        NoRecord = 1,
        Recorded = 2,
        WaitForApprove = 3,
        WaitForApproveWrongPassword = 4,
        WrongPassword = 5
    }
}
