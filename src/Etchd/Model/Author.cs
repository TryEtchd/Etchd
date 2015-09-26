using System.ComponentModel.DataAnnotations;

namespace Etchd.Model
{
    public class Author
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Key]
        public int id { get; set; }
    }
}