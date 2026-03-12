use std::collections::HashMap;
use itertools::Itertools;

pub struct CodonsInfo<'a> {
    pairs: HashMap<&'a str, &'a str>,
}

impl<'a> CodonsInfo<'a> {
    pub fn name_for(&self, codon: &str) -> Option<&'a str> {
        self.pairs.get(codon).copied()
    }

    pub fn of_rna(&self, rna: &str) -> Option<Vec<&'a str>> {
        rna
            .chars()
            .chunks(3)
            .into_iter()
            .map(|a| self.pairs.get(
                a.collect::<String>().as_str()
                ).copied()
            )
            .take_while(|s| *s != Some("stop codon"))
            .collect()
    }
}

pub fn parse<'a>(pairs: Vec<(&'a str, &'a str)>) -> CodonsInfo<'a> {
    CodonsInfo::<'a> {
        pairs: pairs
            .into_iter()
            .collect(),
    }
}
