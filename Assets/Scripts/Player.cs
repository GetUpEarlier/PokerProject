using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerSelf: MonoBehaviour
    {
        public List<CardInfo> Cards;

        public void Awake()
        {
            Cards = new List<CardInfo>();
        }
        
        public void AddCard(CardInfo card)
        {
            Cards.Add(card);
        }

        public void RemoveCard(CardInfo card)
        {
            Cards.Remove(card);
        }
    }

    public class PlayerOther : MonoBehaviour
    {
        
    }
}