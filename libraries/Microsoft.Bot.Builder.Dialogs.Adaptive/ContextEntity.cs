using System;

namespace Microsoft.Bot.Builder.Dialogs.Adaptive
{
    public class ContextEntity
    {
        public object Value { get; set; }

        public int? ExpiresAtTurn { get; set; }

        public DateTime? ExpiresAt { get; set; }
    }
}
