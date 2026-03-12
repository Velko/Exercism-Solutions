using System;
using System.Collections;
using System.Collections.Generic;

public class SimpleLinkedList<T> : IEnumerable<T>
{
    Node _head;
    public int Count { get; private set; }

    public SimpleLinkedList(T value)
    {
        Push(value);
    }

    public SimpleLinkedList(params T[] values)
    {
        foreach (var item in values)
        {
            Push(item);
        }
    }

    public void Push(T value)
    {
        var node = new Node
        {
            next = _head,
            data = value,
        };
        _head = node;
        ++Count;
    }

    public T Pop()
    {
        var node = _head;
        _head = node.next;
        --Count;
        return node.data;
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (var node = _head; node != null; node = node.next)
            yield return node.data;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    class Node
    {
        internal Node next;
        internal T data;
    }
}