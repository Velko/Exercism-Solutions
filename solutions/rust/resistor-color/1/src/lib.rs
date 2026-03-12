use int_enum::IntEnum;
use enum_iterator::{all, Sequence};
use strum_macros::Display;

#[repr(u32)]
#[derive(Debug, Clone, Copy, PartialEq, Eq, IntEnum, Sequence, Display)]
pub enum ResistorColor {
    Black = 0,
    Brown = 1,
    Red = 2,
    Orange = 3,
    Yellow = 4,
    Green = 5,
    Blue = 6,
    Violet = 7,
    Grey = 8,
    White= 9,
}

pub fn color_to_value(color: ResistorColor) -> u32 {
    color.int_value() as u32
}

pub fn value_to_color_string(value: u32) -> String {
    if let Ok(color) = ResistorColor::from_int(value)
    {
       color.to_string()
    } else {
        "value out of range".to_string()
    }
}

pub fn colors() -> Vec<ResistorColor> {
    all::<ResistorColor>().collect()
}
