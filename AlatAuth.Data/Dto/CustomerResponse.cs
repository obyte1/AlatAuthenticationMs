using AlatAuth.Common.Enum;

namespace AlatAuth.Data.Dto
{
    public class CustomerResponse
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public StateDto StateOfResidence { get; set; }
        public LGADto LGA { get; set; }
        public bool IsVerified { get; set; } = false;
        public ProgressState ProgressState { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class StateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class LGADto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
