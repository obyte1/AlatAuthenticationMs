using AlatAuth.Common.Enum;

namespace AlatAuth.Data.Entity
{
    public class Customer : BaseEntity
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int StateOfResidenceId { get; set; }
        public virtual State StateOfResidence { get; set; }
        public int LGAId { get; set; }
        public  virtual LGA LGA { get; set; }
        public bool IsVerified { get; set; } = false;
        public ProgressState ProgressState { get; set; }
    }
}
