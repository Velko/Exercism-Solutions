use std::collections::HashMap;
use itertools::Itertools;

pub struct CodonsInfo {
    pairs: HashMap<&'static str, &'static str>,
}

impl CodonsInfo {

    pub fn new() -> Self {
        let grouped = vec![
            ("methionine", vec!["AUG"]),
            ("phenylalanine", vec!["UUU", "UUC"]),
            ("leucine", vec!["UUA", "UUG"]),
            ("serine", vec!["UCU", "UCC", "UCA", "UCG"]),
            ("tyrosine", vec!["UAU", "UAC"]),
            ("cysteine", vec!["UGU", "UGC"]),
            ("tryptophan", vec!["UGG"]),
            ("stop codon", vec!["UAA", "UAG", "UGA"]),
        ];

        Self {
            pairs: grouped
                    .into_iter()
                    .flat_map(|(name, codons)| 
                        codons
                            .into_iter()
                            .map(move |codon| (codon, name))
                    )
                    .collect(),
        }
    }
    
    pub fn of_rna(&self, rna: &str) -> Option<Vec<&'static str>> {
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

pub fn translate(rna: &str) -> Option<Vec<&'static str>> {
    let codons = CodonsInfo::new();

    codons.of_rna(rna)
}
