namespace IdentityMVC.Toolbox.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Field | AttributeTargets.Property)]
    public class SimpleAttribute : Attribute
    {
        public string Name = "Simple";
        public string? Description;
        public SimpleAttribute() { }
        public SimpleAttribute(string name, string? description = null)
        {
            Name = name;
            Description = description;
        }

        public bool IsValid(string text)
        {
            return !string.IsNullOrEmpty(text);
        }
    }

    //[Simple("I'm simple...")]
    [Simple]
    public class MySimple
    {
        private string? _name;
        public string Name
        {
            get
            {
                var attr = (SimpleAttribute)typeof(MySimple).GetCustomAttributes(typeof(SimpleAttribute), false).First();
                if (attr.IsValid(_name))
                {
                    return _name;
                }
                else
                {
                    return "Empty";
                }
            }
        }
        public void SetName(string n)
        {
            _name = n;
        }
    }

    [Simple("I'm so complex...")]
    public class MyComplex
    {

    }
}
