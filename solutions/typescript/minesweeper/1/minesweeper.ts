export function annotate(field: string[]): string[] {

    let mutField = field.map(m => [...m]);

    for (let r = 0; r < mutField.length; ++r) {
        for (let c = 0; c < mutField[r].length; ++ c) {
            if (mutField[r][c] === '*') {
                update_cell(mutField, r - 1, c - 1);
                update_cell(mutField, r - 1, c + 0);
                update_cell(mutField, r - 1, c + 1);
                update_cell(mutField, r + 0, c - 1);

                update_cell(mutField, r + 0, c + 1);
                update_cell(mutField, r + 1, c - 1);
                update_cell(mutField, r + 1, c + 0);
                update_cell(mutField, r + 1, c + 1);
            }
        }
    }

    return mutField.map(m => m.join(""));
}

function update_cell(field: string[][], row: number, col: number) {
    if (row < 0 || row >= field.length) return;
    if (col < 0 || col >= field[row].length) return;
    switch (field[row][col]) {
        case ' ':
            field[row][col] = '1';
            break;
        case '*':
            break;
        default:
            field[row][col] = (parseInt(field[row][col]) + 1).toString();
            break;
    }
}