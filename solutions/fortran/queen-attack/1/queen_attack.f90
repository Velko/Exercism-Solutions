
module queen_attack
  implicit none
contains

  logical function isValid(pos)
    integer, dimension(2) :: pos

    isValid = pos(1) >= 1 .and. pos(1) <= 8 &
        .and. pos(2) >= 1 .and. pos(2) <= 8
  end function

  logical function canAttack(white_pos, black_pos)
    integer, dimension(2) :: white_pos, black_pos

    integer, dimension(2) :: distance

    distance(1) = iabs(white_pos(1) - black_pos(1))
    distance(2) = iabs(white_pos(2) - black_pos(2))

    canAttack = distance(1) == 0 .or. distance(2) == 0 &
        .or. distance(1) == distance(2)

  end function

end module
