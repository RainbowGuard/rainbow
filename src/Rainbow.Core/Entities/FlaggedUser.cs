using System;
using System.Collections.Generic;

namespace Rainbow.Core.Entities
{
    public class FlaggedUser : IEntity
    {
        public string Id { get; set; }

        public string FlagReason { get; set; }

        private IList<Uri> Attachments { get; set; }
    }
}