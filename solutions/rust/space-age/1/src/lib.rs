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

#[derive(Planet)]
#[OrbitalRatio = 0.2408467]
pub struct Mercury;

#[derive(Planet)]
#[OrbitalRatio = 0.61519726]
pub struct Venus;

#[derive(Planet)]
#[OrbitalRatio = 1.0]
pub struct Earth;

#[derive(Planet)]
#[OrbitalRatio = 1.8808158]
pub struct Mars;

#[derive(Planet)]
#[OrbitalRatio = 11.862615]
pub struct Jupiter;

#[derive(Planet)]
#[OrbitalRatio = 29.447498]
pub struct Saturn;

#[derive(Planet)]
#[OrbitalRatio = 84.016846]
pub struct Uranus;

#[derive(Planet)]
#[OrbitalRatio = 164.79132]
pub struct Neptune;
