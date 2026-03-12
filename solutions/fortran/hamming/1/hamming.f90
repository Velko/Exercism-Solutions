module hamming
  implicit none
contains

  function compute(strand1, strand2, distance)
      character(*) :: strand1, strand2
      integer :: distance
      logical :: compute
      integer :: i

      distance = 0
      compute = len(strand1) == len(strand2)

      if (compute) then
          do i = 1, len(strand1)
              if (strand1(i:i) /= strand2(i:i)) then
                  distance = distance + 1
              end if
          end do
      end if

  end function compute

end module hamming
