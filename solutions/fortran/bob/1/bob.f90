module bob
  implicit none
contains

  function hey(statement)
    character(100) :: hey
    character(len=*), intent(in) :: statement

    if (len_trim(statement) == 0) then
        hey = "Fine. Be that way!"
    else if (is_shouting(statement) /= 0) then
        if (is_question(statement) /= 0) then
            hey = "Calm down, I know what I'm doing!"
        else
            hey = "Whoa, chill out!"
        end if
    else
        if (is_question(statement) /= 0) then
            hey = "Sure."
        else
            hey = "Whatever."
            !hey = statement
        end if
    end if

  end function hey

  function is_question(statement)
      integer :: is_question
      character(len=*), intent(in) :: statement

      integer :: l

      l = len_trim(statement)

      if (statement(l:l) == '?') then
          is_question = 1
      else
          is_question = 0
      end if

  end function is_question

  function is_shouting(statement)
      integer :: is_shouting
      character(len=*), intent(in) :: statement

      integer :: j
      integer :: num_upper
      integer :: num_lower

      num_upper = 0
      num_lower = 0

      do j = 1, len(statement)
          select case(statement(j:j))
          case('a':'z')
              num_lower = num_lower + 1
          case('A':'Z')
              num_upper = num_upper + 1
         end select
      end do

      if (num_upper > 0 .and. num_lower == 0) then
          is_shouting = 1
      else
          is_shouting = 0
      end if

  end function is_shouting



end module bob
