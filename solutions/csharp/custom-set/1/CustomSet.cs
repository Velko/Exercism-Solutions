using System;
using System.Collections.Generic;

public class CustomSet
{
    /* Values in CustomSet are stored in an array, in ascending order */
    int[] items;
    int count;

    public CustomSet(params int[] values)
        : this(values, values.Length)
    {
    }

    private CustomSet(int[] values, int length)
    {
        items = new int[length];
        count = 0;

        var heap = new Heap(values, length);

        // first item
        if (heap.Extract() is int first)
        {
            items[count++] = first;
        }

        // take the rest of the items, skipping duplicates
        foreach (var item in heap.ExtractAll())
        {
            if (items[count - 1] != item)
                items[count++] = item;
        }
    }

    public CustomSet Add(int value)
    {
        var new_items = new int[count + 1];
        items.CopyTo(new_items, 0);
        new_items[^1] = value;

        return new CustomSet(new_items);
    }

    public bool Empty() => count == 0;

    public bool Contains(int value)
    {
        // binary search
        var lo = 0;
        var hi = count;

        while (lo < hi)
        {
            var i = (lo + hi) / 2;
            var cmp = value - items[i];

            if (cmp > 0)
                lo = i + 1;
            else if (cmp < 0)
                hi = i;
            else
                return true;
        }

        return false;
    }

    public bool Subset(CustomSet right)
    {
        var r = 0;
        for (int i = 0; i < count; ++i)
        {
            while (r < right.count && right.items[r] < items[i]) ++r;

            if (r == right.count || right.items[r] > items[i])
                return false;
        }

        return true;
    }

    public bool Disjoint(CustomSet right)
    {
        int i = 0, r = 0;

        while (i < count && r < right.count)
        {
            if (items[i] == right.items[r])
               return false;
            else if (items[i] < right.items[r])
                ++i;
            else
                ++r;
        }

        return true;
    }

    public CustomSet Intersection(CustomSet right)
    {
        var min_len = count;
        if (right.count < min_len)
            min_len = right.count;

        var common = new int[min_len];
        var common_count = 0;

        int i = 0, r = 0;

        while (i < count && r < right.count)
        {
            if (items[i] == right.items[r])
            {
                common[common_count++] = items[i];
                ++i; ++r;
            }
            else if (items[i] < right.items[r])
                ++i;
            else
                ++r;
        }

        return new CustomSet(common, common_count);
    }

    public CustomSet Difference(CustomSet right)
    {
        var different = new int[count];
        var diff_count = 0;

        int i = 0, r = 0;

        while (i < count && r < right.count)
        {
            if (items[i] == right.items[r])
            {
                ++i; ++r;
            }
            else if (items[i] < right.items[r])
                different[diff_count++] = items[i++];
            else
                ++r;
        }

        while (i < count)
            different[diff_count++] = items[i++];

        return new CustomSet(different, diff_count);
    }

    public CustomSet Union(CustomSet right)
    {
        var new_items = new int[count + right.count];
        Array.Copy(items, 0, new_items, 0, count);
        Array.Copy(right.items, 0, new_items, count, right.count);

        return new CustomSet(new_items);
    }

    #region Required for comparison
    public override bool Equals(object obj)
    {
        if (obj is CustomSet other && count == other.count)
        {
            for (int i = 0; i < count; ++i)
                if (items[i] != other.items[i])
                    return false;

            return true;
        }

        return false;
    }

    public override int GetHashCode() => items.GetHashCode();
    #endregion


    class Heap
    {
        int[] m_arr;
        int m_count;

        public Heap(int[] arr, int count)
        {
            m_arr = arr;
            m_count = count;

            for (int start = IParent(m_count-1); start >= 0; --start)
                SiftDown(start, m_count-1);
        }

        public int? Extract()
        {
            if (m_count > 0)
            {
                var result = m_arr[0];
                m_arr[0] = m_arr[m_count - 1];

                --m_count;
                SiftDown(0, m_count - 1);

                return result;
            }

            return null;
        }

        public IEnumerable<int> ExtractAll()
        {
            while (Extract() is int first)
            {
                yield return first;
            }
        }

        private static int IParent(int i) => (i - 1) / 2;
        private static int ILeftChild(int i) => i * 2 + 1;

        private void SiftDown(int start, int end)
        {
            var root = start;

            var child = ILeftChild(root);
            while (child <= end)
            {
                var swap = root;

                if (m_arr[swap] > m_arr[child])
                    swap = child;

                if (child + 1 <= end && m_arr[swap] > m_arr[child + 1])
                    swap = child + 1;

                if (swap != root) {
                    (m_arr[root], m_arr[swap]) = (m_arr[swap], m_arr[root]);
                    root = swap;
                    child = ILeftChild(root);
                }
                else
                    return;
            }
        }
    }
}