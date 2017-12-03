using Emergence.Common.Model;
using Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergence_WPF.ViewModel
{
    public class VM_MasterEventManagement: NotificationObject
    {
        //Grid related variables
        private int _pageSize = 10;
        private int _pageIndex = 0;
        private int _totalCount = 0;
        private int _totalPage = 0;

        //Search related variables
        private string _txtSerialNumber = string.Empty;
        private string _txtTitle = string.Empty;
        private string _txtTime = string.Empty;
        private string _txtSubmitParty = string.Empty;
        private string _txtLocate = string.Empty;

        public int PageSize { get => _pageSize; set => _pageSize = value; }
        public int PageIndex { get => _pageIndex; set => _pageIndex = value; }
        public int TotalCount { get => _totalCount; set => _totalCount = value; }
        public int TotalPage { get => _totalPage; set => _totalPage = value; }
        public string TxtSerialNumber { get => _txtSerialNumber; set => _txtSerialNumber = value; }
        public string TxtTitle { get => _txtTitle; set => _txtTitle = value; }
        public string TxtTime { get => _txtTime; set => _txtTime = value; }
        public string TxtSubmitParty { get => _txtSubmitParty; set => _txtSubmitParty = value; }
        public string TxtLocate { get => _txtLocate; set => _txtLocate = value; }
        public bool IsClick { get; set; }

        public List<MasterEvent> masterEventList = new List<MasterEvent>();
    }
}
