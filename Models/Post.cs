using System.ComponentModel.DataAnnotations;

namespace LetsChatFinal.Models
{
    public class Post
    {
        public int Id { get; set; }

        [StringLength(150)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.MultilineText)]
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
