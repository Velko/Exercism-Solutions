use core::panic;

use proc_macro::TokenStream;
use quote::quote;
use syn::{parse_macro_input, DeriveInput, Meta};

#[proc_macro_derive(Planet, attributes(OrbitalRatio))]
pub fn planet_macro_derive(input: TokenStream) -> TokenStream {
    // Construct a representation of Rust code as a syntax tree
    // that we can manipulate
    let ast = parse_macro_input!(input as DeriveInput);

    // Build the trait implementation
    impl_planet_macro(&ast)
}

fn impl_planet_macro(ast: &syn::DeriveInput) -> TokenStream {
    let name = &ast.ident;

    // VERY, VERY ERROR PRONE way to extract the attribute. Could not find good helper
    // functions and/or simple examples
    let orbitalratio = if let Ok(Meta::NameValue(namevalue)) = ast.attrs[0].parse_meta() {
        namevalue.lit
    } else {
        panic!();
    };

    let gen = quote! {
        impl Planet for #name {
            const ORBITAL_RATIO: f64 = #orbitalratio;
        }
    };

    gen.into()
}