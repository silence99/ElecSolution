using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergence.Common.Model
{
    public class ChildEvent
    {
        private long _id;
        private string _childEventState;
        private string _childEventStateName;
        private string _childEventType;
        private string _childEventTypeName;
        private string _childGrade;
        private string _childGradeName;
        private string _childLocale;
        private string _childRemarks;
        private string _childSubmitDept;
        private string _childSubmitParty;
        private string _childSubmitTelephoneNumber;
        private string _childName;
        private string _childTitle;
        private string _mainEventId;

        public long Id { get => _id; set => _id = value; }
        public string ChildEventState { get => _childEventState; set => _childEventState = value; }
        public string ChildEventStateName { get => _childEventStateName; set => _childEventStateName = value; }
        public string ChildEventType { get => _childEventType; set => _childEventType = value; }
        public string ChildEventTypeName { get => _childEventTypeName; set => _childEventTypeName = value; }
        public string ChildGrade { get => _childGrade; set => _childGrade = value; }
        public string ChildGradeName { get => _childGradeName; set => _childGradeName = value; }
        public string ChildLocate { get => _childLocale; set => _childLocale = value; }
        public string ChildRemarks { get => _childRemarks; set => _childRemarks = value; }
        public string ChildSubmitDept { get => _childSubmitDept; set => _childSubmitDept = value; }
        public string ChildSubmitParty { get => _childSubmitParty; set => _childSubmitParty = value; }
        public string ChildSubmitTelephoneNumber { get => _childSubmitTelephoneNumber; set => _childSubmitTelephoneNumber = value; }
        public string ChildName { get => _childName; set => _childName = value; }
        public string ChildTitle { get => _childTitle; set => _childTitle = value; }
        public string MainEventId { get => _mainEventId; set => _mainEventId = value; }
    }
}
