use std::collections::HashMap;
use itertools::Itertools;

pub struct CodonsInfo {
    pairs: HashMap<&'static str, &'static str>,
}

impl CodonsInfo {

    pub fn new() -> Self {
        let grouped = vec![
            ("Methionine", vec!["AUG"]),
            ("Phenylalanine", vec!["UUU", "UUC"]),
            ("Leucine", vec!["UUA", "UUG"]),
            ("Serine", vec!["UCU", "UCC", "UCA", "UCG"]),
            ("Tyrosine", vec!["UAU", "UAC"]),
            ("Cysteine", vec!["UGU", "UGC"]),
            ("Tryptophan", vec!["UGG"]),
            ("Stop Codon", vec!["UAA", "UAG", "UGA"]),
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
            .take_while(|s| *s != Some("Stop Codon"))
            .collect()
    }
}

pub fn translate(rna: &str) -> Option<Vec<&'static str>> {
    let codons = CodonsInfo::new();

    codons.of_rna(rna)
}
