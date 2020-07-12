using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiUsers.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int DepId { get; set; }
        [ForeignKey("DepId")]
        public Department Department { get; set; }
    }
}
