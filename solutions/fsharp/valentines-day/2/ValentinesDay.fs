module ValentinesDay

// TODO: please define the 'Approval' discriminated union type
type Approval = 
    | Yes
    | No
    | Maybe

// TODO: please define the 'Cuisine' discriminated union type
type Cuisine =
    | Korean
    | Turkish

// TODO: please define the 'Genre' discriminated union type
type Genre = 
    | Crime
    | Horror
    | Romance
    | Thriller


// TODO: please define the 'Activity' discriminated union type
type Activity = 
    | BoardGame
    | Chill
    | Movie of Genre
    | Restaurant of Cuisine
    | Walk of int

let rateActivity (activity: Activity): Approval = 
    match activity with
    | BoardGame -> Approval.No
    | Chill -> Approval.No
    | Movie Romance -> Approval.Yes
    | Movie _ -> Approval.No
    | Restaurant Korean -> Approval.Yes
    | Restaurant _ -> Approval.Maybe
    | Walk distance when distance < 3 -> Approval.Yes
    | Walk distance when distance < 5 -> Approval.Maybe
    | Walk _ -> Approval.No
