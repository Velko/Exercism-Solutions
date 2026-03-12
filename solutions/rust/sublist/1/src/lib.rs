use std::cmp::Ordering;

#[derive(Debug, PartialEq, Eq)]
pub enum Comparison {
    Equal,
    Sublist,
    Superlist,
    Unequal,
}

pub fn sublist<T: PartialEq>(first_list: &[T], second_list: &[T]) -> Comparison {

    match first_list.len().cmp(&second_list.len())
    {
        Ordering::Equal if first_list == second_list => { Comparison::Equal },
        Ordering::Greater if second_list.is_empty() ||  is_sub_of(first_list, second_list) => Comparison::Superlist,
        Ordering::Less if first_list.is_empty() || is_sub_of(second_list, first_list) => Comparison::Sublist,
        _ => Comparison::Unequal
    }
}

fn is_sub_of<T: PartialEq>(superlist: &[T], sublist: &[T]) -> bool {
    superlist.windows(sublist.len()).any(|super_window| super_window == sublist)
}