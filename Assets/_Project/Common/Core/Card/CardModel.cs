namespace Project.Core.Card
{
    public class CardModel
    { 
        public readonly CardHealth Health;
        public readonly EnemyCardSelectionHandler Handler;

        public CardModel(CardHealth health, EnemyCardSelectionHandler handler)
        {
            Health = health;
            Handler = handler;
        }
    }
}