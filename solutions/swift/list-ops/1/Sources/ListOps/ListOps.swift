class ListOps {
    class func append<T>(_ list1: [T], _ list2: [T]) -> [T] {
        return list1 + list2
    }

    class func concat<T>(_ lists: [T]...) -> [T] {
        return foldLeft(lists, accumulated: [], combine: append)
    }

    class func filter<T>(_ list: [T], predicate: (T) -> Bool) -> [T] {
        var result: [T] = []
        for item in list {
            if predicate(item) {
                result = append(result, [item])
            }
        }
        return result
    }

    class func map<TIn, TOut>(_ list: [TIn], transform: (TIn) -> TOut) -> [TOut] {
        var result: [TOut] = []
        for item in list {
            result = append(result, [transform(item)])
        }
        return result
    }

    class func length<T>(_ list: [T]) -> Int {
        return list.count
    }

    class func foldLeft<TIn, TOut>(_ list: [TIn], accumulated: TOut, combine: (TOut, TIn) -> TOut) -> TOut {
        var acc = accumulated
        for item in list {
            acc = combine(acc, item)
        }
        return acc
    }
    
    class func foldRight<TIn, TOut>(_ list: [TIn], accumulated: TOut, combine: (TIn, TOut) -> TOut) -> TOut {
        var acc = accumulated
        for item in reverse(list) {
            acc = combine(item, acc)
        }
        return acc
    }

    class func reverse<T>(_ list: [T]) -> [T] {
        return foldLeft(list, accumulated: [], combine: {
            (acc: [T], item: T) -> [T] in
                [item] + acc
        })
    }
}