using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergence.Common.Model
{
    public class DictItem
    {
        private string _name;
        private string _value;

        public string Name { get => _name; set => _name = value; }
        public string Value { get => _value; set => _value = value; }
    }
}
