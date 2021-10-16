using System;
using System.Collections.Generic;

namespace Rainbow.Core.Entities
{
    public class FlaggedUser : IEntity
    {
        public string Id { get; set; }

        public string FlagReason { get; set; }

        public ulong FlagCount { get; set; }

        private IList<Uri> Attachments { get; set; }
    }
}