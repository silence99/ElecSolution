using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergence.Common.Data
{
    public static class Enumerator
    {
        #region [MasterEventDetailPage]
        public enum SubEventStatus
        {
            Registed,
            Published,
            Accepted,
            Executed,
            Completed
        }
            #endregion
    }
}
