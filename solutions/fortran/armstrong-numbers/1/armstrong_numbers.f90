
module armstrong_numbers
  implicit none
contains

  logical function isArmstrongNumber(i)
    integer, intent(in) :: i

    integer :: p, n, s

    ! count digits
    p = 0
    n = i
    do while (n > 0)
        n = n / 10
        p = p + 1
    end do

    ! sum of exponentiated digits 
    s = 0
    n = i
    do while (n > 0)
        s = s + mod(n, 10) ** p
        n = n / 10
    end do

    isArmstrongNumber = s == i

  end function

end module
