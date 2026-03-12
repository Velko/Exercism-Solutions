module leap
  implicit none

contains

  logical function is_leap_year(year)
    integer :: year

    is_leap_year = (is_divisible(year, 4) .and. (.not. is_divisible(year, 100))) &
        .or. is_divisible(year, 400)

  end function

  logical function is_divisible(year, num)
    integer :: year, num

    is_divisible = mod(year, num) == 0
  end function

end module

