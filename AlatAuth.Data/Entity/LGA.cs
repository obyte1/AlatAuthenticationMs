namespace AlatAuth.Data.Entity
{
    public class LGA : BaseEntity
    {
        public string Name { get; set; }
        public int StateId { get; set; }
        public State State { get; set; }
    }
}
