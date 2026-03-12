package linkedlist

import (
    "errors"
)

type Node struct {
	_next *Node
	_prev *Node
	Value interface{}
}

type List struct {
	head *Node
	tail *Node
    count int
}

func NewList(args ...interface{}) *List {
	list := new (List)
    list.count = 0

	for _, arg := range args {
		list.Push(arg)
	}

	return list
}

func (n *Node) Next() *Node {
	return n._next
}

func (n *Node) Prev() *Node {
	return n._prev
}

func (l *List) Unshift(v interface{}) {
	node := new(Node)
	node.Value = v

	if l.head == nil {
		l.head = node
		l.tail = node
	} else {
		node._next = l.head
		l.head._prev = node
		l.head = node
	}

    l.count += 1
}

func (l *List) Push(v interface{}) {

	node := new(Node)
	node.Value = v

	l.push_node(node)
}

func (l *List) push_node (node *Node) {
	
	if l.head == nil {
		// list was empty, new node becomes both head and tail
		l.head = node
		l.tail = node
	} else {
		// set links to/from original tail and replace it with new one
		node._prev = l.tail
		l.tail._next = node
		l.tail = node
	}

    l.count += 1
}

func (l *List) Shift() (interface{}, error) {
	node := l.head
	
	if node == nil {
		return nil, errors.New("Empty list")
	}
	
	l.head = node._next

	if l.head != nil {
		l.head._prev = nil
	} else {
		l.tail = nil
	}

    l.count -= 1
    
	return node.Value, nil
}

func (l *List) Pop() (interface{}, error) {
	node := l.tail
	
	if node == nil {
		return nil, errors.New("Empty list")
	}
	
	// reset tail
	l.tail = node._prev

	if l.tail != nil {
		// clean Next pointer if there are items left
		l.tail._next = nil
	} else {
		// clean Head if became empty
		l.head = nil
	}

    l.count -= 1

	return node.Value, nil
}

func (l *List) Reverse() {

	// grab last node
	node := l.tail;
	
	// reset list to be "empty"
	l.head = nil

	// go backwards and "push" nodes to the list again
	for node != nil {
		prev := node._prev
		l.push_node(node)
		node = prev
	}

	// clean up head/tail node pointers
	if l.head != nil {
		l.head._prev = nil
		l.tail._next = nil
	}
}

func (l *List) First() *Node {
	return l.head
}

func (l *List) Last() *Node {
	return l.tail
}

func (l *List) Count() int {
    return l.count
}

func (l *List) Delete(v interface{}) {
    node := l.head

    for node != nil && node.Value != v {
        node = node._next
    }

    if node != nil {

		if node._prev != nil {
            node._prev._next = node._next
        } else {
            l.head = node._next
        }

        if node._next != nil {
            node._next._prev = node._prev
        } else {
            l.tail = node._prev
        }
        
        l.count -= 1
    }
}
