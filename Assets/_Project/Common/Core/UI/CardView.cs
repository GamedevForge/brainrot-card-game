using System;
using Project.Core.Services;
using Unity.VisualScripting;

namespace Project.Core.UI
{
    public class CardView : IInitializable, IDisposable
    {
        private readonly CardCreatedData _currentCard;

        public CardView(CardCreatedData currentCard)
        {
            _currentCard = currentCard;
        }

        public void Initialize()
        {
            _currentCard.Health.OnTakedGamage += DrawCurrentHealth;
            _currentCard.Health.OnRevive += DrawCurrentHealth;
        }
        
        public void Dispose()
        {
            _currentCard.Health.OnTakedGamage -= DrawCurrentHealth;
            _currentCard.Health.OnRevive -= DrawCurrentHealth;
        }

        private void DrawCurrentHealth(int health) =>
            _currentCard.CardComponents.CardForceIndex.text = health.ToString();

    }
}