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
}

func NewList(args ...interface{}) *List {
	list := new (List)

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
