use space_age_derive::Planet;

#[derive(Debug)]
pub struct Duration(f64);

const SECONDS_IN_EARTH_YEAR: f64 = 31557600.0;

impl From<u64> for Duration {
    fn from(s: u64) -> Self {
        Self {
            0: s as f64 / SECONDS_IN_EARTH_YEAR
        }
    }
}

pub trait Planet {
    const ORBITAL_RATIO: f64;
    fn years_during(d: &Duration) -> f64 {
        d.0 as f64 / Self::ORBITAL_RATIO
    }
}

macro_rules! planet {
    ($name:ident, $orbital_ratio:expr) => {
        #[derive(Planet)]
        #[OrbitalRatio = $orbital_ratio]
        pub struct $name;
    };
}

planet!(Mercury, 0.2408467);
planet!(Venus, 0.61519726);

// an example to what planet!() macro expands to
#[derive(Planet)]
#[OrbitalRatio = 1.0]
pub struct Earth;

planet!(Mars, 1.8808158);
planet!(Jupiter, 11.862615);
planet!(Saturn, 29.447498);
planet!(Uranus, 84.016846);
planet!(Neptune, 164.79132);
