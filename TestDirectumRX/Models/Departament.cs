using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace TestDirectumRX.Models
{
    public class Departament
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public IEnumerable<Empoyee> Empoyees { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"Id: {Id} Name: {Name}");
            if(Empoyees.Any())
            {
                sb.AppendLine("Empoyees:");
                foreach(var empoyee in Empoyees)
                {
                    sb.AppendLine($"Id: {empoyee.Id} ChiefId: {empoyee.ChiefId} Name: {empoyee.Name}");
                }
            }

            return sb.ToString();
        }
    }
}
