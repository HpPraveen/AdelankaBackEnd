using System.ComponentModel.DataAnnotations;

namespace Adelanka.DAL.Models
{
    public class UserNote
    {
        [Key]
        public long Id { get; set; }

        public string Username { get; set; }
        public string Title { get; set; }
        public string Note { get; set; }
        public string Comment { get; set; }
        public string ModifiedDate { get; set; }
    }
}