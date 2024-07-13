namespace AlatAuth.Data.Dto.Banks
{

    public class BankDto
    {
        public string bankName { get; set; }
        public string bankCode { get; set; }
    }

    public class GetBankResponseDto
    {
        public List<BankDto> result { get; set; }
        public string errorMessage { get; set; }
        public List<string> errorMessages { get; set; }
        public bool hasError { get; set; }
        public DateTime timeGenerated { get; set; }
    }
}
