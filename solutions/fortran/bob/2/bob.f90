module bob
  implicit none
contains

  function hey(statement)
    character(100) :: hey
    character(len=*), intent(in) :: statement

    if (len_trim(statement) == 0) then
        hey = "Fine. Be that way!"
    else if (is_shouting(statement)) then
        if (is_question(statement)) then
            hey = "Calm down, I know what I'm doing!"
        else
            hey = "Whoa, chill out!"
        end if
    else
        if (is_question(statement)) then
            hey = "Sure."
        else
            hey = "Whatever."
        end if
    end if

  end function hey

  function is_question(statement)
      logical :: is_question
      character(len=*), intent(in) :: statement

      integer :: l

      l = len_trim(statement)

      is_question = statement(l:l) == '?'

  end function is_question

  function is_shouting(statement)
      logical :: is_shouting
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

      is_shouting = num_upper > 0 .and. num_lower == 0

  end function is_shouting



end module bob
