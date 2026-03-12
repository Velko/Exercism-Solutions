module MatchingBrackets

let rec findMatching input expect =
    match input with
    | [] -> List.isEmpty expect
    | '[' :: tail -> findMatching tail (']'::expect)
    | '{' :: tail -> findMatching tail ('}'::expect)
    | '(' :: tail -> findMatching tail (')'::expect)
    | ']' :: tail -> (List.tryHead expect) = Some(']') && findMatching tail (List.tail expect)
    | '}' :: tail -> (List.tryHead expect) = Some('}') && findMatching tail (List.tail expect)
    | ')' :: tail -> (List.tryHead expect) = Some(')') && findMatching tail (List.tail expect)
    | _ :: tail -> findMatching tail expect

let isPaired (input: string) =
    findMatching (List.ofSeq input) []
