using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Analytics_Hub.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Analysis Name")]
        public string Name { get; set; }
        public PythonScript PythonScript { get; set; }
    }
}
