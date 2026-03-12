use std::{ptr::null_mut};
use std::marker::PhantomData;


// this module adds some functionality based on the required implementations
// here like: `LinkedList::pop_back` or `Clone for LinkedList<T>`
// You are free to use anything in it, but it's mainly for the test framework.
mod pre_implemented;

pub struct LinkedList<T>
{
    pre_head: *mut NodeLinks<T>,
    post_tail: *mut NodeLinks<T>,
}

pub struct Cursor<'a, T> {
    current: *mut NodeLinks<T>,
    phantom: PhantomData<&'a T>
}

pub struct Iter<'a, T> {
    current: *mut NodeLinks<T>,
    phantom: PhantomData<&'a T>
}

struct NodeLinks<T> {
    next: *mut NodeLinks<T>,
    prev: *mut NodeLinks<T>,
}

impl<T> NodeLinks<T> {
    pub fn allocate() -> *mut Self {
        Box::into_raw(Box::new(NodeLinks::<T> { next: null_mut(), prev: null_mut() }))
    }

    pub fn free(ptr: *mut Self) {
        unsafe {
            _ = Box::from_raw(ptr);
        }
    }

    pub fn is_data_node(&self) -> bool {
        self.prev != null_mut() && self.next != null_mut()
    }
}


#[repr(C)]
struct Node<T> {
    links: NodeLinks<T>,
    data: T,
}

impl<T> Node<T> {
    pub fn new(next: *mut NodeLinks<T>, prev: *mut NodeLinks<T>, data: T) -> *mut NodeLinks<T> {
        Box::into_raw(Box::new(Node::<T> {
            links: NodeLinks::<T>{
                next,
                prev,
            },
            data,
        })) as *mut NodeLinks<T>
    }
}

impl<T> LinkedList<T> {
    pub fn new() -> Self {
        let list = Self {
            pre_head: NodeLinks::<T>::allocate(),
            post_tail: NodeLinks::<T>::allocate(),
        };

        // We're using dedicated pre-head and post-tail "nodes" without any data.
        // This eliminates a bunch of checks for NULL, as there are always a "node"
        // to point to. These elements are pinned in place, so it should be safe to
        // store/use pointers to them
        list.pre_head_mut().next = list.post_tail;
        list.post_tail_mut().prev = list.pre_head;

        list
    }

    fn pre_head_mut<'a>(&self) -> &'a mut NodeLinks<T> {
        unsafe {
            &mut *self.pre_head
        }
    }

    fn post_tail_mut<'a>(&self) -> &'a mut NodeLinks<T> {
        unsafe {
            &mut *self.post_tail
        }
    }

    // You may be wondering why it's necessary to have is_empty()
    // when it can easily be determined from len().
    // It's good custom to have both because len() can be expensive for some types,
    // whereas is_empty() is almost always cheap.
    // (Also ask yourself whether len() is expensive for LinkedList)
    pub fn is_empty(&self) -> bool {
        self.pre_head_mut().next == self.post_tail
    }

    pub fn len(&self) -> usize {
        self.iter().count()
    }

    /// Return a cursor positioned on the front element
    pub fn cursor_front(&mut self) -> Cursor<'_, T> {
        // pre_head.next should always contain a valid pointer, whether it is
        // pointing to post_tail or actual item in the list, so it should be safe
        // to dereference it 
        Cursor {
            current: self.pre_head_mut().next,
            phantom: PhantomData,
        }
        
    }

    /// Return a cursor positioned on the back element
    pub fn cursor_back(&mut self) -> Cursor<'_, T> {
        // post_tail.prev should always contain a valid pointer, whether it is
        // pointing to pre_head or actual item in the list, so it should be safe
        // to dereference it 
        Cursor {
            current: self.post_tail_mut().prev,
            phantom: PhantomData,
        }
    }

    /// Return an iterator that moves from front to back
    pub fn iter(&self) -> Iter<'_, T> {
        Iter {
            current: self.pre_head_mut().next,
            phantom: PhantomData,
        }
    }
}


impl<T> Drop for LinkedList<T> {

    fn drop(&mut self) {
        let mut cursor = self.cursor_front();
        while let Some(_) = cursor.take() {}

        NodeLinks::<T>::free(self.pre_head);
        NodeLinks::<T>::free(self.post_tail);
    }
}

// the cursor is expected to act as if it is at the position of an element
// and it also has to work with and be able to insert into an empty list.
impl<T> Cursor<'_, T> {
    /// Take a mutable reference to the current element
    pub fn peek_mut(&mut self) -> Option<&mut T> {
        if self.current_mut().is_data_node() {
            let node = unsafe { &mut *(self.current as *mut Node<T>) };
            Some(&mut node.data)
        } else {
            None
        }
    }

    fn current_mut<'a>(&self) -> &'a mut NodeLinks<T> {
        assert!(self.current != null_mut());
        unsafe {
            &mut *self.current
        }
    }

    fn prev_mut<'a>(&self) -> &'a mut NodeLinks<T> {
        assert!(self.current_mut().prev != null_mut());
        unsafe {
            &mut *self.current_mut().prev
        }
    }

    fn next_mut<'a>(&self) -> &'a mut NodeLinks<T> {
        assert!(self.current_mut().next != null_mut());
        unsafe {
            &mut *self.current_mut().next
        }
    }


    /// Move one position forward (towards the back) and
    /// return a reference to the new position
    #[allow(clippy::should_implement_trait)]
    pub fn next(&mut self) -> Option<&mut T> {
        self.current = self.current_mut().next;

        self.peek_mut()
    }

    /// Move one position backward (towards the front) and
    /// return a reference to the new position
    pub fn prev(&mut self) -> Option<&mut T> {
        self.current = self.current_mut().prev;

        self.peek_mut()
    }

    /// Remove and return the element at the current position and move the cursor
    /// to the neighboring element that's closest to the back. This can be
    /// either the next or previous position.
    pub fn take(&mut self) -> Option<T> {

        if !self.current_mut().is_data_node() {
            None
        } else {
            let node = unsafe {
                Box::from_raw(self.current as *mut Node<T>)
            };

            self.prev_mut().next = self.current_mut().next;
            self.next_mut().prev = self.current_mut().prev;
            self.current = self.current_mut().next;

            // special case if the removed item was last one
            // in that case we should back-up to previous
            if self.current_mut().next == null_mut() {
                self.current = self.current_mut().prev;                
            }

            Some(node.data)
        }
    }

    pub fn insert_after(&mut self, element: T) {
        // should not be called on post_tail
        assert!(self.current_mut().next != null_mut());

        let node = Node::new(self.current_mut().next, self.current, element);
        
        self.next_mut().prev = node;
        self.current_mut().next = node;
    }

    pub fn insert_before(&mut self, element: T) {

        // should not be called on pre_head
        assert!(self.current_mut().prev != null_mut());

        let node = Node::new(self.current, self.current_mut().prev, element);
        
        self.prev_mut().next = node;
        self.current_mut().prev = node;
    }
}

impl<'a, T> Iterator for Iter<'a, T> {
    type Item = &'a T;

    fn next(&mut self) -> Option<&'a T> {
        assert!(self.current != null_mut());

        let links = unsafe {
            &mut *self.current
        };

        if links.is_data_node() {
            let node = unsafe { &mut *(self.current as *mut Node<T>) };
            self.current = links.next;

            Some(&mut node.data)
        } else {
            None
        }
    }
}
