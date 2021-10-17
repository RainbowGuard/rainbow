namespace Rainbow.Core.Entities
{
    public class GuildConfiguration : IEntity
    {
        public string Id { get; set; }

        public ulong SystemChannelId { get; set; }
    }
}