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
        
        for data in self.into_iter() {
            reversed_list.push(data);
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

impl<T> From<SimpleLinkedList<T>> for Vec<T> {
    fn from(linked_list: SimpleLinkedList<T>) -> Vec<T> {
        linked_list.rev().into_iter().collect()
    }
}


// Implementing the IntoIterator, just because we can!
pub struct SimpleLinkedListIter<T>(Option<Box<Node<T>>>);

impl<T> Iterator for SimpleLinkedListIter<T> {
    type Item = T;
    
    fn next(&mut self) -> Option<Self::Item> {
        match self.0.take() {
            None => None,
            Some(node) => {
                self.0 = node.next;
                Some(node.data)
            }
        }
    }
}

impl<T> IntoIterator for SimpleLinkedList<T> {
    type Item = T;
    type IntoIter = SimpleLinkedListIter<T>;
    
    fn into_iter(self) -> Self::IntoIter {
        SimpleLinkedListIter {
            0: self.head
        }
    }
}
