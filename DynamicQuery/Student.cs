using System;

namespace DynamicQuery
{
    public record Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Tel { get; set; }
        public string City { get; set; }
        public DateTime Birthday { get; set; }
    }
}
