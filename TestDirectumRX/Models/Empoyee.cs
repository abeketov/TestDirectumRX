using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TestDirectumRX.Models
{
    public class Empoyee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int DepartamentId { get; set; }

        public int ChiefId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Salary { get; set; }

        public Departament Departament { get; set; }

        public Empoyee Chief { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"Id: {Id} / DepartamentName: {Departament.Name} / EmpoyeeName: {Name} / ChiefName: {Chief?.Name} / Salary: {Salary}");

            return sb.ToString();
        }
    }
}
