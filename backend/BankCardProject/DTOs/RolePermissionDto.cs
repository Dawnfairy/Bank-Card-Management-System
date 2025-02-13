namespace BankCardProject.DTOs
{
    public class RolePermissionDto
    {
        public int Id { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public int RoleId { get; set; }
    }
}
