module reverse_string
  implicit none
contains

  function reverse(input) result(reversed)
    character(*), intent(in) :: input
    character(len=len(input)) :: reversed

    integer :: i, l

    l = len_trim(input) + 1

    do i = 1, l - 1
        reversed(l-i:l-i) = input(i:i)
    end do
  end function
end module
