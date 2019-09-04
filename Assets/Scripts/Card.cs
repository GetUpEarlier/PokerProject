using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{

    public enum Suit
    {
        
    }

    public enum Point
    {
    }


    public struct CardInfo
    {
        public Suit Suit;
        public Point Point;

        public bool Equals(CardInfo other)
        {
            return Suit == other.Suit && Point == other.Point;
        }

        public override bool Equals(object obj)
        {
            return obj is CardInfo other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int) Suit * 397) ^ (int) Point;
            }
        }
        
        public static bool operator==(CardInfo info1, CardInfo info2)
        {
            return info1.Suit == info2.Suit && info1.Point == info2.Point;
        }

        public static bool operator !=(CardInfo info1, CardInfo info2)
        {
            return !(info1 == info2);
        }
    }

    public class Card
    {
        public CardInfo Info;
        
    }
    
}
