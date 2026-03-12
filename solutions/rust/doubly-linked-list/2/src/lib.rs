use std::ops::{Deref, DerefMut};
use std::ptr::NonNull;
use std::marker::PhantomData;



// this module adds some functionality based on the required implementations
// here like: `LinkedList::pop_back` or `Clone for LinkedList<T>`
// You are free to use anything in it, but it's mainly for the test framework.
mod pre_implemented;

pub struct LinkedList<T>
{
    /* Instead of typical head and tail pointers to nodes, we're using pre_head
       and post_tail pseudo-nodes. They are allocated with list's initialization and
       freed when list is dropped. The pre_head.prev points back to the pre_head, 
       similarly post_tail.next points to post_tail. */
    pre_head: NodePtr<T>,
    post_tail: NodePtr<T>,
}

pub struct Cursor<'a, T> {
    current: NodePtr<T>,
    phantom: PhantomData<&'a T>
}

pub struct Iter<'a, T> {
    current: NodePtr<T>,
    phantom: PhantomData<&'a T>
}

/* Node has pointers to next and previous nodes. The pre_head and post_tail pseudo-nodes
   does not contain any data. */
struct Node<T> {
    next: NodePtr<T>,
    prev: NodePtr<T>,
    data: Option<T>,
}

struct NodePtr<T>(NonNull<Node<T>>);

impl<T> NodePtr<T> {
    // Leaves NodePtr in unusable state, has to be fixed up by caller
    unsafe fn dangling() -> Self {
        Self {
            0: NonNull::dangling()
        }
    }

    // can not use "regular" deref_mut() from trait, as different lifetime
    // is required
    fn node_ref<'a>(&mut self) -> &'a mut Node<T> {
        unsafe {
            // except for brief initialization and cleanup moments, pointers to 
            // nodes are always valid. It should be safe to dereference them
            self.0.as_mut()
        }
    }

    // Unsafe, because Node is now packed back into the Box, which is free to move it around.
    // Consequently this instance of NodePtr (and all copies of it) can become invalid
    unsafe fn into_node_box(self) -> Box<Node<T>> {
        Box::from_raw(self.0.as_ptr())
    }

    fn is_post_tail(&self) -> bool {
        self.next.0 == self.0
    }

    fn is_pre_head(&self) -> bool {
        self.prev.0 == self.0
    }
}

impl<T> Deref for NodePtr<T> {
    type Target = Node<T>;

    fn deref(&self) -> &Self::Target {
        unsafe {
            // except for brief initialization and cleanup moments, pointers to 
            // nodes are always valid. It should be safe to dereference them
            self.0.as_ref()
        }
    }
}

impl<T> DerefMut for NodePtr<T> {
    fn deref_mut(&mut self) -> &mut Self::Target {
        unsafe {
            // except for brief initialization and cleanup moments, pointers to 
            // nodes are always valid. It should be safe to dereference them
            self.0.as_mut()
        }
    }
}

/* Unfortunately we can not #[derive(PartialEq, Clone, Copy)], as it tries to pull in these bounds for T,
   but we just want to compare or copy a pointer. */
impl<T> PartialEq for NodePtr<T> {
    fn eq(&self, other: &Self) -> bool {
        self.0 == other.0
    }
}

impl<T> Clone for NodePtr<T> {
    fn clone(&self) -> Self {
        Self {
            0 : self.0
        }
    }
}

impl<T> Copy for NodePtr<T> {}



impl<T> Node<T> {
    fn new_terminating() -> NodePtr<T> {
        unsafe {
            let links = Box::new(Self { next: NodePtr::dangling(), prev: NodePtr::dangling(), data: None });

            // we just created a Box, it should be safe to convert it into a NonNull pointer
            let mut ptr = NonNull::new_unchecked(Box::into_raw(links));

            // Links was created with dangling prev/next references, fix those up as pointing
            // to itself. Could not do it on creation because address was not known.
            // Caller may (and will) choose to re-assign them as it see fits. Still, having node to
            // point back to itself serves as an indicator that it is a pre_head or post_tail pseudo-node
            ptr.as_mut().next.0 = ptr;
            ptr.as_mut().prev.0 = ptr;

            NodePtr { 0: ptr }
        }
    }

    fn new(next: NodePtr<T>, prev: NodePtr<T>, data: T) -> NodePtr<T> {
        let node = Box::new(Node {
            next: next,
            prev: prev,
            data: Some(data),
        });

        unsafe {
            // We just created a Box, it should be safe to convert it into a NonNull pointer.
            NodePtr {
                0: NonNull::new_unchecked(Box::into_raw(node))
            }
        }
    }
}


impl<T> LinkedList<T> {
    pub fn new() -> Self {
        let mut list = Self {
            pre_head: Node::new_terminating(),
            post_tail: Node::new_terminating(),
        };

        // We're using dedicated pre-head and post-tail "nodes" without any data.
        // This eliminates a bunch of checks, as there are always a "node" to point
        // to. Making them point to each other initially.
        list.pre_head.next = list.post_tail;
        list.post_tail.prev = list.pre_head;

        list
    }

    pub fn is_empty(&self) -> bool {
        self.pre_head.next == self.post_tail
    }

    pub fn len(&self) -> usize {
        self.iter().count()
    }

    /// Return a cursor positioned on the front element
    pub fn cursor_front(&mut self) -> Cursor<'_, T> {
        Cursor {
            current: self.pre_head.next,
            phantom: PhantomData,
        }
    }

    /// Return a cursor positioned on the back element
    pub fn cursor_back(&mut self) -> Cursor<'_, T> {
        Cursor {
            current: self.post_tail.prev,
            phantom: PhantomData,
        }
    }

    /// Return an iterator that moves from front to back
    pub fn iter(&self) -> Iter<T> {
        Iter {
            current: self.pre_head.next,
            phantom: PhantomData,
        }
    }
}


impl<T> Drop for LinkedList<T> {

    fn drop(&mut self) {
        let mut cursor = self.cursor_front();
        while let Some(_) = cursor.take() {}

        unsafe {
            // NodeLinks were allocated on initialization, now freeing them
            // After dropped, the LinkedList instance is considered unusable
            _ = self.pre_head.into_node_box();
            _ = self.post_tail.into_node_box();
        }
    }
}

// This should be safe as there's no way to get mutable aliases for
// LinkedList internals
unsafe impl<T: Sync> Sync for LinkedList<T> {}
unsafe impl<T: Send> Send for LinkedList<T> {}

// the cursor is expected to act as if it is at the position of an element
// and it also has to work with and be able to insert into an empty list.
impl<T> Cursor<'_, T> {
    /// Take a mutable reference to the current element
    pub fn peek_mut(&mut self) -> Option<&mut T> {
        self.current.data.as_mut()
    }

    /// Move one position forward (towards the back) and
    /// return a reference to the new position
    #[allow(clippy::should_implement_trait)]
    pub fn next(&mut self) -> Option<&mut T> {
        self.current = self.current.next;

        self.peek_mut()
    }

    /// Move one position backward (towards the front) and
    /// return a reference to the new position
    pub fn prev(&mut self) -> Option<&mut T> {
        self.current = self.current.prev;

        self.peek_mut()
    }

    /// Remove and return the element at the current position and move the cursor
    /// to the neighboring element that's closest to the back. This can be
    /// either the next or previous position.
    pub fn take(&mut self) -> Option<T> {

        // Do not remove pseudo-nodes!!!
        if self.current.data.is_none() {
            return None
        }

        let node = unsafe {
            // After call to the into_node_box, the self.current should not be
            // read as it could have become invalid. One can continue to use
            // the boxed 'node', it's, also, OK to overwrite it with pointer
            // to another Node
            self.current.into_node_box()
        };

        let mut prev = node.prev;
        let mut next = node.next;
        prev.next = next;
        next.prev = prev;
    
        // Normally current is advanced to the next one, except when removed item
        // was the last one. Then we should back-up to previous
        self.current = if !next.is_post_tail() { next } else { prev };

        node.data
    }

    pub fn insert_after(&mut self, element: T) {
        
        // Inserting after post-tail breaks the list by destroying the post-tail's condition
        // that it points back to itself. The cursor API does not disallow it, so we should
        // handle it gracefully.
        if self.current.is_post_tail() {
            return self.insert_before(element);
        }

        let node = Node::new(self.current.next, self.current, element);
        
        self.current.next.prev = node;
        self.current.next = node;
    }

    pub fn insert_before(&mut self, element: T) {

        // inserting before pre-head breaks the list, do an insert_after instead (see insert_after)
        if self.current.is_pre_head() {
            return self.insert_after(element);
        }

        let node = Node::new(self.current, self.current.prev, element);
        
        self.current.prev.next = node;
        self.current.prev = node;
    }
}

impl<'a, T> Iterator for Iter<'a, T> {
    type Item = &'a T;

    fn next(&mut self) -> Option<&'a T> {

        let node = self.current.node_ref();

        // all nodes always have valid "next"
        self.current = self.current.next;

        node.data.as_ref()
    }
}
