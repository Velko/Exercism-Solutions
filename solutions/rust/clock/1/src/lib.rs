use std::fmt::{Display, Formatter};

#[derive(Debug, PartialEq)]
pub struct Clock(i32);

const MINUTES_PER_HOUR: i32 = 60;
const MINUTES_PER_DAY: i32 = MINUTES_PER_HOUR * 24;

impl Clock {
    pub fn new(hours: i32, minutes: i32) -> Self {
        Self {
            0: Self::wrap_in_day(hours * MINUTES_PER_HOUR + minutes)
        }        
    }

    pub fn add_minutes(&self, minutes: i32) -> Self {
        Self {
            0: Self::wrap_in_day(self.0 + minutes)
        }
    }

    fn wrap_in_day(minutes: i32) -> i32 {
        let min_modulo = minutes % MINUTES_PER_DAY;
        if min_modulo < 0 {
            min_modulo + MINUTES_PER_DAY
        } else {
            min_modulo
        }
    }
}



impl Display for Clock {
    fn fmt(&self, f: &mut Formatter<'_>) -> Result<(), std::fmt::Error> {
        write!(f, "{:02}:{:02}", self.0 / 60, self.0 % 60)
    }
}
