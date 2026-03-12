module binary_search
  implicit none
contains

  function find(array, val) result(idx)
    integer, dimension(:), intent(in) :: array
    integer, intent(in) :: val
    integer :: idx

    integer :: lo
    integer :: hi

    lo = 0
    hi = size(array)

    do while (lo < hi)
      idx = (lo + hi) / 2
      if (array(idx + 1) < val) then
        lo = idx + 1
      else if (array(idx + 1) > val) then
        hi = idx
      else
        idx = idx + 1
        return
      end if
    end do

    idx = -1

  end function

end module
