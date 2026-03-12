let birthday: String = "Birthday"

let valentine: String = "Valentine's Day"

let anniversary: String = "Anniversary"

let space: Character = " "

let exclamation: Character = "!"

func buildSign(for occasion: String, name: String) -> String {
  return "Happy \(occasion) \(name)!"
}

func graduationFor(name: String, year: Int) -> String {
  return "Congratulations \(name)!\nClass of \(year)"
}

func costOf(sign: String) -> Int {
  return 20 + sign.count * 2 
}
