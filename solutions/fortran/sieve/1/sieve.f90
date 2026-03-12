module sieve
  implicit none

contains

  function primes(limit) result(array)
    integer, intent(in) :: limit
    integer, allocatable :: array(:)

    logical, allocatable :: sieve(:)
    integer :: p
    integer :: m

    ! allocate and fill Sieve array
    allocate(sieve(limit))
    do p = 2, limit
      sieve(p) = .true.
    end do

    ! mark all multiples
    do p = 2, limit
      if (sieve(p)) then
        do m = p * p, limit, p
          sieve(m) = .false.
        end do
      end if
    end do

    ! count how many primes are there
    m = 0
    do p = 2, limit
      if (sieve(p)) then
        m = m + 1
      end if
    end do

    ! allocate and fill results array
    allocate(array(m))
    m = 1
    do p = 2, limit
      if (sieve(p)) then
        array(m) = p
        m = m + 1
      end if
    end do

  end function primes

end module sieve
