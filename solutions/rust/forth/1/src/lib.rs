use std::{collections::{HashMap}, str::SplitWhitespace};

pub type Value = i32;
pub type Result = std::result::Result<(), Error>;

pub struct Forth {
    stack: Vec<Value>,
    word_names: HashMap<String, usize>,
    operations: Vec<Operation>,
}

#[derive(Debug, PartialEq, Eq)]
pub enum Error {
    DivisionByZero,
    StackUnderflow,
    UnknownWord,
    InvalidWord,
}

#[derive(Clone)]
enum Operation {
    Number(Value),
    Add,
    Sub,
    Mul,
    Div,
    Dup,
    Drop,
    Swap,
    Over,
    Custom(Vec<usize>),
}

impl Forth {
    pub fn new() -> Forth {

        let operation_defs = vec![
            /* Initialize with built-ins, this is split into
               self.operations and self.word_names.

               Both of them are later amended with custom definitions.
             */
            ("+", Operation::Add),
            ("-", Operation::Sub),
            ("*", Operation::Mul),
            ("/", Operation::Div),
            ("DUP", Operation::Dup),
            ("DROP", Operation::Drop),
            ("SWAP", Operation::Swap),
            ("OVER", Operation::Over),
        ];

        Self {
            stack: Vec::new(),
            word_names: HashMap::from_iter(
                operation_defs
                    .iter()
                    .enumerate()
                    .map(|(n, (name, _))| (name.to_string(), n))
                ),
            operations: operation_defs
                            .into_iter()
                            .map(|(_, op)| op)
                            .collect(),
        }
    }

    pub fn stack(&self) -> &[Value] {
        &self.stack
    }

    /* ------ Main "engine" -------- */

    pub fn eval(&mut self, input: &str) -> Result {
        let mut tokens = input.split_whitespace();

        while let Some(token) = tokens.next() {
            if token == ":" {
                self.make_custom_word(&mut tokens)?;
            } else {
                let operation = self.token_to_operation(&token.to_uppercase())?;
                self.exec_single(&operation)?;
            }
        }

        Ok(())
    }

    fn token_to_operation(&self, token: &str) -> std::result::Result<Operation, Error> {
        if let Some(val) = token.parse::<Value>().ok() {
            Ok(Operation::Number(val))
        } else {
            let opcode = self.word_names.get(token).ok_or(Error::UnknownWord)?;
            Ok(self.operations.get(*opcode).cloned().unwrap()) // unwrap, because `word_names` and `operations` should be consistent
        }
    }

    fn exec_single(&mut self, operation: &Operation) -> Result {
        match operation {
            Operation::Number(val) => Ok(self.stack.push(*val)),
            Operation::Add => self.op_add(),
            Operation::Sub => self.op_sub(),
            Operation::Mul => self.op_mul(),
            Operation::Div => self.op_div(),
            Operation::Dup => self.op_dup(),
            Operation::Drop => self.op_drop(),
            Operation::Swap => self.op_swap(),
            Operation::Over => self.op_over(),
            Operation::Custom(steps) => {
                for step in steps {
                    let operation = self.operations.get(*step).cloned().unwrap(); // unwrap, because steps should contain valid codes only
                    self.exec_single(&operation)?;
                }
                Ok(())
            }
        }
    }

    /* ------ Built in operations -----  */

    fn op_add(&mut self) -> Result {
        let (arg1, arg2) = self.pop_2_args()?;
        Ok(self.stack.push(arg1 + arg2))
    }

    fn op_sub(&mut self) -> Result {
        let (arg1, arg2) = self.pop_2_args()?;
        Ok(self.stack.push(arg1 - arg2))
    }

    fn op_mul(&mut self) -> Result {
        let (arg1, arg2) = self.pop_2_args()?;
        Ok(self.stack.push(arg1 * arg2))
    }

    fn op_div(&mut self) -> Result {
        let (arg1, arg2) = self.pop_2_args()?;
        if arg2 != 0 {
            Ok(self.stack.push(arg1 / arg2))
        } else {
            Err(Error::DivisionByZero)
        }
    }

    fn op_dup(&mut self) -> Result {
        let arg = self.stack.last().cloned().ok_or(Error::StackUnderflow)?;
        Ok(self.stack.push(arg))
    }

    fn op_drop(&mut self) -> Result {
        _ = self.stack.pop().ok_or(Error::StackUnderflow)?;
        Ok(())
    }

    fn op_swap(&mut self) -> Result {
        let (arg1, arg2) = self.pop_2_args()?;
        self.stack.push(arg2);
        Ok(self.stack.push(arg1))
    }

    fn op_over(&mut self) -> Result {
        let idx = self.stack.len().checked_sub(2).ok_or(Error::StackUnderflow)?;
        Ok(self.stack.push(self.stack[idx]))
    }

    fn pop_2_args(&mut self) -> std::result::Result<(Value, Value), Error> {
        let arg2 = self.stack.pop().ok_or(Error::StackUnderflow)?;
        let arg1 = self.stack.pop().ok_or(Error::StackUnderflow)?;

        Ok((arg1, arg2))
    }

    /* --------- Custom definitions ---------- */

    fn make_custom_word(&mut self, tokens: &mut SplitWhitespace) -> Result {
        let name = tokens.next().ok_or(Error::InvalidWord)?;

        // check if name is not a number:
        _ = name
            .parse::<Value>()       // try to parse as a number
            .err()                  // extract parse error
            .ok_or(Error::InvalidWord)?; // and fail if there was no error


        // accumulate "opcodes"
        let mut steps = Vec::new();

        while let Some(step) = tokens.next() {
            if step == ";" {
                // save the sequence of steps as custom "opcode" and store a symbolic
                // alias for it
                self.operations.push(Operation::Custom(steps));
                *self.word_names.entry(name.to_uppercase()).or_default() = self.operations.len() - 1;
                return Ok(());
            }

            steps.push(self.token_to_opcode(step)?);
        }

        // stream ended but there was no ;
        Err(Error::InvalidWord)
    }

    fn token_to_opcode(&mut self, step: &str) -> std::result::Result<usize, Error> {
        if let Some(val) = step.parse::<Value>().ok() {
            // create a new "opcode" for constant
            self.operations.push(Operation::Number(val));
            Ok(self.operations.len() - 1)
        } else {
            // or look up existing symbolic one
            self.word_names.get(&step.to_uppercase()).cloned().ok_or(Error::InvalidWord)
        }
    }
}
