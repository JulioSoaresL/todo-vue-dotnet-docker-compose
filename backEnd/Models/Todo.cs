using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace todo_list.Models
{
    [Table("todo", Schema = "dbo")]
    public class Todo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("task_id")]
        public int Id { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("completed")]
        public bool Completed { get; set; }
    }
}
