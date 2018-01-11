using Framework;

namespace Emergence.Common.Model
{
	public class DictItem : NotificationObject
	{
		private string _name;
		private string _value;

		public virtual string Name { get => _name; set => _name = value; }
		public virtual string Value { get => _value; set => _value = value; }
	}
}
