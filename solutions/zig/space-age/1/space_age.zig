pub const Planet = enum {
    mercury,
    venus,
    earth,
    mars,
    jupiter,
    saturn,
    uranus,
    neptune,

    pub fn age(self: Planet, seconds: usize) f64 {
        const age_on_earth = @as(f64, @floatFromInt(seconds)) / 31557600;
        
        return switch(self) {
            Planet.mercury => age_on_earth / 0.2408467,
            Planet.venus => age_on_earth / 0.61519726,
            Planet.earth => age_on_earth / 1.0,
            Planet.mars => age_on_earth / 1.8808158,
            Planet.jupiter => age_on_earth / 11.86261,
            Planet.saturn => age_on_earth / 29.447498,
            Planet.uranus => age_on_earth / 84.016846,
            Planet.neptune => age_on_earth / 164.79132,
        };
    }
};
