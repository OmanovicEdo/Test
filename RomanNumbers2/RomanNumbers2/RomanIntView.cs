using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;

namespace RomanNumbers2
{
    public interface RomanIntView
    {
        void DisplayResult(string result);
        void HandleErrorMessage(Exception ex);
        void ClearResult();
    }
}
