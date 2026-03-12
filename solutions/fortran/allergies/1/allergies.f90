
module allergies
  implicit none

  character(len=12), dimension(8) :: allergens=[character(len=12) :: &
    "eggs", "peanuts", "shellfish", "strawberries", "tomatoes", "chocolate", "pollen", "cats" ]

contains

  logical function allergicTo(allergy_str, allergy_key)
    character(len=*), intent(in) :: allergy_str
    integer, intent(in) :: allergy_key

    integer :: allergy_index
    allergy_index = findloc(allergens, allergy_str, 1)

    allergicTo = btest(allergy_key, allergy_index - 1)
  end function


  function allergicList(allergy_key)
    integer, intent(in) :: allergy_key
    character(len=100) :: allergicList

    integer :: i
    allergicList = ''

    do i = 0, 7
      if (btest(allergy_key, i)) then
        allergicList = trim(allergicList) // ' ' //allergens(i + 1)
      end if
    end do

    allergicList = adjustl(allergicList)
  end function
end module
