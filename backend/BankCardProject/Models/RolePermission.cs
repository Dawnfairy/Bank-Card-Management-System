using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BankCardProject.Models
{
    public class RolePermission
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public int RoleId { get; set; }
    }
}
