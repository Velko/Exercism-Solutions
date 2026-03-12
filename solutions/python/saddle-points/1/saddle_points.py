"""Saddle Points exercise on Exercism's Python Track."""

def saddle_points(matrix):
    """Detect saddle points in a matrix."""

    # Are all rows same length?
    if len(set(map(len, matrix))) > 1:
        raise ValueError("irregular matrix")

    # build a set of (row, col) tuples with max elements in each row
    max_per_row=map(max, matrix)
    max_cells = set(_select_from_rows(max_per_row, matrix))

    # to find min elements in each column use same logic as for rows,
    # but transpose the matrix first and swap back the coordinates at the end
    [*transposed_matrix] = zip(*matrix)
    min_per_col=map(min, transposed_matrix)
    transposed_min_cells = _select_from_rows(min_per_col, transposed_matrix)
    min_cells = set(_swap_coordinates(transposed_min_cells))

    # if coordinate is in both sets, it's a saddle point
    saddle_points_found = max_cells.intersection(min_cells)

    # re-format the result from set of tuples into expected output format: list of dicts
    return list(map(_format_coordinates, saddle_points_found))

def _select_from_rows(values_to_select, matrix):
    for (row_idx, value_to_select), row in zip(enumerate(values_to_select), matrix):
        for col_idx, value in enumerate(row):
            if value_to_select == value:
                yield (row_idx + 1, col_idx + 1)

def _swap_coordinates(cells):
    return map(lambda xy: tuple(reversed(xy)), cells)

def _format_coordinates(cell_tuple):
    return { "row": cell_tuple[0], "column": cell_tuple[1]}
