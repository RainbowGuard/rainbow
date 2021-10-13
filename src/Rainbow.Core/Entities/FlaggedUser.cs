namespace Rainbow.Core.Entities
{
    public class FlaggedUser : IEntity
    {
        public string Id { get; set; }

        public string FlagReason { get; set; }
    }
}