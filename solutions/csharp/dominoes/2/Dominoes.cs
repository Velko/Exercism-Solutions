using System;
using System.Collections.Generic;
using System.Linq;

public static class Dominoes
{
    public static bool CanChain(IEnumerable<(int, int)> dominoes)
    {
        var stones = new List<Stone>();
        int num_dominoes = 0;

        // Build a list of normal and reversed dominoes, assign an Id
        // to each of them. Original and reversed pieces share same Id
        foreach (var piece in dominoes)
        {
            stones.Add(new Stone(num_dominoes, piece));
            stones.Add(new Stone(num_dominoes, (piece.Item2, piece.Item1)));
            ++num_dominoes;
        }

        if (num_dominoes == 0)
            return true;

        foreach (var start in stones)
        {
            foreach (var chain in BuildChain(stones, new[] { start }, start, 1 << start.Id))
            {
                if (chain.Length == num_dominoes && start.Start == chain.Last().End)
                    return true;
            }
        }

        return false;
    }

    static IEnumerable<Stone[]> BuildChain(List<Stone> stones, Stone[] head, Stone last, int usedStoneMask)
    {
        bool pieceAdded = false;

        var candidates = stones
            .Where(piece => piece.Start == last.End
               && (usedStoneMask & (1 << piece.Id)) == 0);

        foreach (var piece in candidates)
        {
            var newHead = head.Append(piece).ToArray();
            var newUsed = usedStoneMask | ( 1 << piece.Id);
            pieceAdded = true;

            // forward what deeper levels return
            foreach (var subChain in BuildChain(stones, newHead, piece, newUsed))
                yield return subChain;
        }

        // only yield a chain if no stones can be added to it
        if (!pieceAdded)
            yield return head;
    }

    class Stone
    {
        public Stone (int id, (int start, int end) dots)
        {
            Id = id;
            Start = dots.start;
            End = dots.end;
        }

        public int Id { get; }
        public int Start { get; }
        public int End { get; }
    }
}