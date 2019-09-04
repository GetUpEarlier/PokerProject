using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

using CardList = System.Collections.Generic.List<Assets.Scripts.CardInfo>;

namespace Assets.Scripts
{
    public class Global
    {
        public List<string> Players;
        public List<CardList> History;
        public CardList Stage;
        public int Current;
    }

    public enum ClientMessageType
    {
        Pass, Push, Pop
    }

    public class ClientMessage
    {
        public ClientMessageType Type;
        public string Player;
        public List<CardInfo> Cards;
    }

    public enum ServerMessageType
    {
        
    }

    public class ServerMessage
    {
        public ServerMessageType Type;
    }
    
    public interface IPlayer
    {
        void AddCards(List<CardInfo> cards);
        void RemoveCards(List<CardInfo> cards);
    }
    
    public interface IController
    {
        bool IsValid(List<CardInfo> cards);
        bool CompareCards(CardList cards1, CardList cards2);
    }
    
    public static class CardUtil{
        
        public static IEnumerable<Point> Tuples([NotNull] this CardList cards, int count)
        {
            if (cards == null) throw new ArgumentNullException(nameof(cards));
            foreach (Point point in cards.Select(card => card.Point))
            {
                if (cards.Count(card => card.Point == point) == count)
                {
                    yield return point;
                }
            }
        }
        
        public static IEnumerable<Point> Singles(this CardList cards)
        {
            return cards.Tuples(2);
        }
        
        public static IEnumerable<Point> Pairs(this CardList cards)
        {
            return cards.Tuples(2);
        }

        public static IEnumerable<Point> Triples(this CardList cards)
        {
            return cards.Tuples(3);
        }

        public static IEnumerable<Point> Quads(this CardList cards)
        {
            return cards.Tuples(4);
        }

        public static int Mode(this CardList cards)
        {
            foreach (var i in Enumerable.Range(0, cards.Count).Reverse())
            {
                if (cards.Tuples(i).Any())
                {
                    return i;
                }
            }

            return 0;
        }
    }

    public abstract class CardPattern
    {
        public int MinCount;
        public int MaxCount;

        public abstract bool Match(List<CardInfo> cards);
    }

    public class Single: CardPattern
    {
        public override bool Match(CardList cards)
        {
            return cards.Count == 1;
        }
    }

    public class Pair : CardPattern
    {
        public override bool Match(CardList cards)
        {
            return cards.Count == 2 && cards.Pairs().Any();
        }
    }

    public class Triple : CardPattern
    {
        public override bool Match(CardList cards)
        {
            
        }
    }

    public class CardBundle
    {
        public List<CardInfo> Cards;
        public CardPattern Pattern;
    }

    public delegate void CardAction();
    

    public class Graph<TV>
    {
        public readonly List<(TV, TV)> Edges;

        public Graph()
        {
            Edges = new List<(TV, TV)>();
        }

        public bool HasEdge(TV begin, TV end)
        {
            foreach ((TV source, TV target) in Edges)
            {
                if (source.Equals(begin) && target.Equals(end))
                {
                    return true;
                }
            }

            return false;
        }

        public IEnumerable<TV> DirectSuccessors(TV vertex)
        {
            foreach ((TV source, TV target) in Edges)
            {
                if (source.Equals(vertex))
                {
                    yield return target;
                }
            }
        }

        public bool HasPath(TV begin, TV end)
        {
            if (begin.Equals(end))
            {
                return true;
            }
            bool flag;
            var oldSet = new HashSet<TV> {begin};
            var newSet = new HashSet<TV> {begin};
            do
            {
                foreach (TV old in oldSet)
                {
                    foreach (TV successor in DirectSuccessors(old))
                    {
                        if (successor.Equals(end))
                        {
                            return true;
                        }
                        if (newSet.Add(successor))
                        {
                            flag = true;
                        }
                    }
                }
                
                oldSet.UnionWith(newSet);
            } while (flag);

            return false;
        }

        public void Connect(TV begin, TV end)
        {
            Edges.Add((begin, end));
        }
    }

    public class DdzController : IController
    {
        public List<CardPattern> Patterns;
        public DdzController()
        {
            Patterns = new List<CardPattern>();
        }

        public bool IsValid(List<CardInfo> cards)
        {
            foreach (CardPattern pattern in Patterns)
            {
                if (pattern.Match(cards))
                {
                    return true;
                }
            }

            return false;
        }

        public bool CompareCards(CardList cards1, CardList cards2)
        {
            throw new NotImplementedException();
        }
    }
}