namespace RMall_BE.Dto.UsersDto
{
    public class TenantDto : UserDto
    {
        public string FullName { get; set; }
        public DateTime BirthDay { get; set; }
        public string Phone_Number { get; set; }
        public string Address { get; set; }
    }
}
