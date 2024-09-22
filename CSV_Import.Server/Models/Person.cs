using System.ComponentModel.DataAnnotations;

namespace CSV_Import.Server.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool Married {  get; set; }
        public string Phone {  get; set; }
        public decimal Salary { get; set; }

    }
}
