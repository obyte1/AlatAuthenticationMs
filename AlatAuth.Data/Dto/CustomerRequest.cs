namespace AlatAuth.Data.Dto
{
    public class CustomerRequest
    {
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int StateOfResidenceId { get; set; }
        public int LGAId { get; set; }

    }
}
