using System.ComponentModel.DataAnnotations;

namespace LetsChatFinal.Models
{
    public class Post
    {
        
        public int Id { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        [StringLength (300)]
        public string Content { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Created { get; set; }

        public string UserName { get; set; }

        public Post()
        {
            Created = DateTime.Now;
        }


    }
}
