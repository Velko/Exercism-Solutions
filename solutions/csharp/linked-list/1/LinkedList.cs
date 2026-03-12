using System;

public class Deque<T>
{
    NodeBase pre_head;
    NodeBase post_tail;

    public Deque()
    {
        pre_head = new NodeBase();
        post_tail = new NodeBase();

        pre_head.next = post_tail;
        post_tail.prev = pre_head;
    }

    public void Push(T value)
    {
        var node = new Node
        {
            prev = pre_head,
            next = pre_head.next,
            data = value,
        };

        pre_head.next.prev = node;
        pre_head.next = node;
    }

    public T Pop()
    {
        var node = (Node)pre_head.next;

        node.next.prev = node.prev;
        node.prev.next = node.next;

        return node.data;
    }

    public void Unshift(T value)
    {
        var node = new Node
        {
            next = post_tail,
            prev = post_tail.prev,
            data = value,
        };

        post_tail.prev.next = node;
        post_tail.prev = node;
    }

    public T Shift()
    {
        var node = (Node)post_tail.prev;

        node.next.prev = node.prev;
        node.prev.next = node.next;

        return node.data;
    }

    class NodeBase
    {
        internal NodeBase next;
        internal NodeBase prev;
    }

    class Node : NodeBase
    {
        internal T data;
    }
}