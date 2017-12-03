using System.Collections.Generic;

namespace Emergence.Common.ViewModel
{
    public class VM_MasterEventManagement
    {
        //Grid related variables
        private int _pageSize = 5;
        private int _pageIndex = 1;
        private int _totalCount = 0;
        private int _totalPage = 0;

        //Search related variables
        private string _txtSerialNumber = string.Empty;
        private string _txtTitle = string.Empty;
        private string _txtTime = string.Empty;
        private string _txtSubmitParty = string.Empty;
        private string _txtLocate = string.Empty;

        public virtual int PageSize { get => _pageSize; set => _pageSize = value; }
        public virtual int PageIndex { get => _pageIndex; set => _pageIndex = value; }
        public virtual int TotalCount { get => _totalCount; set => _totalCount = value; }
        public virtual int TotalPage { get => _totalPage; set => _totalPage = value; }
        public virtual string TxtSerialNumber { get => _txtSerialNumber; set => _txtSerialNumber = value; }
        public virtual string TxtTitle { get => _txtTitle; set => _txtTitle = value; }
        public virtual string TxtTime { get => _txtTime; set => _txtTime = value; }
        public virtual string TxtSubmitParty { get => _txtSubmitParty; set => _txtSubmitParty = value; }
        public virtual string TxtLocate { get => _txtLocate; set => _txtLocate = value; }

        public List<MasterEventUiModel> masterEventList = new List<MasterEventUiModel>();
    }
}
