using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergence.Common.Model
{
    public class ChildEvent
    {
        private bool _isChecked = false;
        private long _id;
        private string _childGrade;
        private string _childGradeName;
        private string _childLatitude;
        private string _childLocale;
        private string _childLongitude;
        private string _childRemarks;
        private string _childTitle;
        private string _personLiable;
        private string _state;
        private string _mainEventId;

        public bool IsChecked { get => _isChecked; set => _isChecked = value; }
        public long Id { get => _id; set => _id = value; }
        public string ChildGrade { get => _childGrade; set => _childGrade = value; }
        public string ChildGradeName { get => _childGradeName; set => _childGradeName = value; }
        public string ChildLatitude { get => _childLatitude; set => _childLatitude = value; }
        public string ChildLocale { get => _childLocale; set => _childLocale = value; }
        public string ChildLongitude { get => _childLongitude; set => _childLongitude = value; }
        public string ChildRemarks { get => _childRemarks; set => _childRemarks = value; }
        public string ChildTitle { get => _childTitle; set => _childTitle = value; }
        public string PersonLiable { get => _personLiable; set => _personLiable = value; }
        public string State { get => _state; set => _state = value; }
        public string MainEventId { get => _mainEventId; set => _mainEventId = value; }
    }
}
