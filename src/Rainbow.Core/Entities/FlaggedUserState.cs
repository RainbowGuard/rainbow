namespace Rainbow.Core.Entities
{
    public class FlaggedUserState : IEntity
    {
        public string Id { get; set; }

        public int FlagCount { get; set; }
    }
}