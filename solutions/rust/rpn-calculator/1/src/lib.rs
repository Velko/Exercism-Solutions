#[derive(Debug, Clone)]
pub enum CalculatorInput {
    Add,
    Subtract,
    Multiply,
    Divide,
    Value(i32),
}

pub fn evaluate(inputs: &[CalculatorInput]) -> Option<i32> {
    let mut stack = Vec::from(inputs);

    match eval_arg(&mut stack) {
        Some(result) if stack.is_empty() => Some(result),
        _ => None,
    }
}

fn eval_arg(stack: &mut Vec<CalculatorInput>) -> Option<i32> {
    match stack.pop() {
        Some(CalculatorInput::Value(val)) => Some(val),
        Some(operation) => {
            let arg_r = eval_arg(stack)?;
            let arg_l = eval_arg(stack)?;
            match operation {
                CalculatorInput::Add => Some(arg_l + arg_r),
                CalculatorInput::Subtract => Some(arg_l - arg_r),
                CalculatorInput::Multiply => Some(arg_l * arg_r),
                CalculatorInput::Divide => Some(arg_l / arg_r),
                _ => None,
            }
        }
       _ => None,
    }
}
