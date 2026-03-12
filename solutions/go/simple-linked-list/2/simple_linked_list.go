package simplelinkedlist

import "errors"

type Element struct {
	next *Element
	Value int
}

type List struct {
	head *Element
}

func New(values []int) *List {
	list := new(List)

	for _, value := range values {
		list.Push(value)
	}

	return list
}

func (l *List) Size() int {
	size := 0

	for node := l.head; node != nil; node = node.next {
		size++
	}

	return size
}

func (l *List) Push(element int) {
	node := new(Element)
	node.Value = element

	if l.head == nil {
		l.head = node
	} else {
		node.next = l.head
		l.head = node
	}
}

func (l *List) Pop() (int, error) {
	node := l.head
        
	if node == nil {
		return -1, errors.New("Empty list")
	}
	
	l.head = node.next

	return node.Value, nil
}

func (l *List) Array() []int {
	
	// List contains items in "backwards" order. In order to get "original"
	// sequence back, have to populate it starting from the end.
	size := l.Size()
	result := make([]int, size)

	for node, index := l.head, size-1; node != nil; node, index = node.next, index-1 {
		result[index] = node.Value
	}

	return result
}

func (l *List) Reverse() *List {

	// Function signature suggests that new, reversed list should be created.
	result := new(List)

	for node := l.head; node != nil; node = node.next {
		result.Push(node.Value)
	}

	return result
}
