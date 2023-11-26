namespace Nop.Core.Enumeration
{
    public class DescriptionAttribute : System.Attribute
    {
        private string _value;
        public DescriptionAttribute(string value)
        {
            _value = value;
        }

        public string Value
        {
            get { return _value; }
        }
    }
}
