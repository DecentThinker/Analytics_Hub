namespace Analytics_Hub.Models
{
    public class PythonScript
    {
        public int Id { get; set; }

        // Foreign key to Category
        public int CategoryId { get; set; }

        // Navigation property to Category
        public Category Category { get; set; }

        public string ScriptName { get; set; }
    }
}
