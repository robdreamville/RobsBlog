using System.ComponentModel.DataAnnotations;

namespace LetsChatFinal.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(300)]
        public string Content { get; set; } = string.Empty;

        [DataType(DataType.DateTime)]
        public DateTime Created { get; set; }

        public string UserName { get; set; } = string.Empty;

        public Post()
        {
            Created = DateTime.Now;
        }
    }
}
