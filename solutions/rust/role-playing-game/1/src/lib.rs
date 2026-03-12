pub struct Player {
    pub health: u32,
    pub mana: Option<u32>,
    pub level: u32,
}

impl Player {
    pub fn revive(&self) -> Option<Player> {
        if self.health == 0 {
            Some(Player {
                health: 100,
                mana: if self.level >= 10 { Some(100) } else { None },
                level: self.level,
            })
        } else {
            None
        }
    }

    pub fn cast_spell(&mut self, mana_cost: u32) -> u32 {
        match self.mana {
            None => {
                self.health = u32::saturating_sub(self.health, mana_cost);
                0
            },
            Some(m) if m < mana_cost => 0,
            Some(m) => {
                self.mana = Some(m - mana_cost);
                mana_cost * 2
            }
        }
    }
}
