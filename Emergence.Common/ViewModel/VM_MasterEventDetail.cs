﻿using Emergence.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergence.Common.ViewModel
{
    public class VM_MasterEventDetail
    {
        //Grid related variables
        private int _pageSize = 5;
        private int _pageIndex = 1;
        private int _totalCount = 0;
        private int _totalPage = 0;

        //master event related variables
        private MasterEvent _masterEventInfo;

        public MasterEvent MasterEventInfo { get => _masterEventInfo; set => _masterEventInfo = value; }
        public int PageSize { get => _pageSize; set => _pageSize = value; }
        public int PageIndex { get => _pageIndex; set => _pageIndex = value; }
        public int TotalCount { get => _totalCount; set => _totalCount = value; }
        public int TotalPage { get => _totalPage; set => _totalPage = value; }

        public List<SubEvent> subEventList = new List<SubEvent>();
    }
}
