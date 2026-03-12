#[derive(Debug, Clone)]
pub enum CalculatorInput {
    Add,
    Subtract,
    Multiply,
    Divide,
    Value(i32),
}

pub fn evaluate(inputs: &[CalculatorInput]) -> Option<i32> {
    let mut stack = Vec::new();

    for input in inputs {
        match input {
            CalculatorInput::Value(val) => stack.push(*val),
            CalculatorInput::Add      => binary_operation(&mut stack, |l, r| l + r)?,
            CalculatorInput::Subtract => binary_operation(&mut stack, |l, r| l - r)?,
            CalculatorInput::Multiply => binary_operation(&mut stack, |l, r| l * r)?,
            CalculatorInput::Divide   => binary_operation(&mut stack, |l, r| l / r)?,
        }
    }

    match stack.pop() {
        Some(val) if stack.is_empty() => Some(val),
        _ => None
    }
}

fn binary_operation<F>(stack: &mut Vec<i32>, f: F) -> Option<()>
    where F: Fn(i32, i32) -> i32 {

    let r = stack.pop()?;
    let l = stack.pop()?;

    stack.push(f(l, r));

    Some(())
}