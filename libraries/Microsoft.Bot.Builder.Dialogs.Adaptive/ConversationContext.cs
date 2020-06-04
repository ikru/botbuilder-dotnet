using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Bot.Builder.Dialogs.Adaptive
{
    public class ConversationContext
    {
        private readonly int _defaultExpiryTurns = 5;
        private readonly int _defaultExpirySeconds = 120;

        public ConversationContext()
        {
            Started = DateTime.Now.ToUniversalTime();
            Entities = new Dictionary<string, ContextEntity>();
        }

        public int TurnCount { get; set; }

        public DateTime Started { get; set; }

        public int Duration => (DateTime.Now.ToUniversalTime() - Started).Seconds;

        public Dictionary<string, ContextEntity> Entities { get; set; }

        public void OnTurn(ITurnContext turnContext, RecognizerResult recognized)
        {
            TurnCount++;

            var entitiesToRemove = Entities.Where(e =>
                    e.Value?.ExpiresAtTurn < TurnCount || e.Value?.ExpiresAt?.ToUniversalTime() < DateTime.Now.ToUniversalTime()).ToList();

            if (entitiesToRemove.Any())
            {
                foreach (var entityToRemove in entitiesToRemove)
                {
                    Entities.Remove(entityToRemove.Key);
                }
            }

            foreach (var entity in recognized.Entities)
            {
                AddOrUpdateEntity(entity.Key, entity.Value);
            }
        }

        public void AddOrUpdateEntity(string key, object entity)
        {
            if (key != "$instance")
            {
                var contextEntity = new ContextEntity()
                {
                    ExpiresAt = DateTime.Now.ToUniversalTime().AddSeconds(_defaultExpirySeconds),
                    ExpiresAtTurn = TurnCount + _defaultExpiryTurns,
                    Value = entity
                };

                if (Entities.ContainsKey(key))
                {
                    Entities[key] = contextEntity;
                }
                else
                {
                    Entities.Add(key, contextEntity);
                }
            }
        }
    }
}
