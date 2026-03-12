
module high_scores
  implicit none
contains

 function scores(score_list)
  integer, dimension(:) :: score_list
  integer, dimension(size(score_list)) :: scores

  scores = score_list

end function

function latest(score_list)
  integer, dimension(:) :: score_list
  integer :: latest

  latest = score_list(size(score_list))

end function

function personalBest(score_list)
  integer, dimension(:) :: score_list
  integer :: personalBest

  personalBest = maxval(score_list)

end function

function personalTopThree(score_list)
  integer, dimension(:) :: score_list
  integer, dimension(3) :: personalTopThree

  integer, dimension(size(score_list) + 3) :: work_buffer
  integer :: i, t
  logical :: swapped

  ! place scores in a "work buffer", pad with 3 zeros
  work_buffer(1:size(score_list)) = score_list
  work_buffer(size(score_list)+1:size(score_list)+3) = (/ 0, 0, 0 /)

  ! bubble sort descending
  swapped = .true.
  do while(swapped)
    swapped = .false.
    do i = 2, size(work_buffer)
      if (work_buffer(i-1) < work_buffer(i)) then
        t = work_buffer(i-1)
        work_buffer(i-1:i-1) = work_buffer(i:i)
        work_buffer(i:i) = t
        swapped = .true.
      end if
    end do
  end do

  personalTopThree = work_buffer(1:3)

end function

end module
