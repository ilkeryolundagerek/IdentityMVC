using System.Xml.Serialization;

namespace IdentityMVC.Models
{
    [XmlRoot("employee")]
    public class Employee
    {
        [XmlElement("id")]
        public int Id { get; set; }

        [XmlElement("first_name")]
        public string Firstname { get; set; }

        [XmlElement("last_name")]
        public string Lastname { get; set; }

        [XmlElement("email")]
        public string Email { get; set; }

        [XmlElement("material")]
        public string Material { get; set; }
    }

    [XmlRoot("employees")]
    public class EmployeesViewModel
    {
        [XmlArray(elementName:"employee")]
        public Employee[] Employees { get; set; }
    }
}
