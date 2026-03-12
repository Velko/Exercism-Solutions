class Flatten {

  static flatten(list) {
    return list
      .reduce([]) { |flat, elem|
          if (elem is Sequence) {
            flat.addAll(flatten(elem))
          } else if (!(elem is Null)) {
            flat.add(elem)
          }
          return flat
      }
  }
}