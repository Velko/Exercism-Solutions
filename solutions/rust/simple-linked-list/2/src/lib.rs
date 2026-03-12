use std::{iter::FromIterator};

struct Node<T> {
    data: T,
    next: Option<Box<Node<T>>>,
}

pub struct SimpleLinkedList<T> {
    head: Option<Box<Node<T>>>,
}

impl<T> SimpleLinkedList<T> {
    pub fn new() -> Self {
        Self {
            head: None
        }
    }

    pub fn is_empty(&self) -> bool {
        self.head.is_none()
    }

    pub fn len(&self) -> usize {
        let mut count = 0;

        let mut node_p = &self.head;

        while let Some(node) = node_p {
            count += 1;
            node_p = &node.next;
        }

        count
    }

    pub fn push(&mut self, element: T) {
        let node = Some(Box::new(Node {
            data: element,
            next: self.head.take(),
        }));

        self.head = node
    }

    pub fn pop(&mut self) -> Option<T> {
        match self.head.take() {
            None => None,
            Some(node) => {
                self.head = node.next;
                Some(node.data)
            }
        }
    }

    pub fn peek(&self) -> Option<&T> {
        match &self.head {
            None => None,
            Some(node) => Some(&node.data)
        }
    }

    #[must_use]
    pub fn rev(self) -> SimpleLinkedList<T> {
        let mut reversed_list = SimpleLinkedList::new();
        
        let mut node_p = self.head;

        while let Some(node) = node_p {
            reversed_list.push(node.data);
            node_p = node.next;
        }

        reversed_list
    }
}

impl<T> FromIterator<T> for SimpleLinkedList<T> {
    fn from_iter<I: IntoIterator<Item = T>>(iter: I) -> Self {
        let mut list = Self::new();
        for element in iter {
            list.push(element);
        }

        list
    }
}


// In general, it would be preferable to implement IntoIterator for SimpleLinkedList<T>
// instead of implementing an explicit conversion to a vector. This is because, together,
// FromIterator and IntoIterator enable conversion between arbitrary collections.
// Given that implementation, converting to a vector is trivial:
//
// let vec: Vec<_> = simple_linked_list.into_iter().collect();
//
// The reason this exercise's API includes an explicit conversion to Vec<T> instead
// of IntoIterator is that implementing that interface is fairly complicated, and
// demands more of the student than we expect at this point in the track.

impl<T> From<SimpleLinkedList<T>> for Vec<T> {
    fn from(linked_list: SimpleLinkedList<T>) -> Vec<T> {
        let mut node_p = linked_list.rev().head.take();

        let mut result = Vec::new();

        while let Some(node) = node_p {
            result.push(node.data);
            node_p = node.next;
        }

        result
    }
}
