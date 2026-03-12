"""Functions to help Azara and Rui locate pirate treasure."""


def get_coordinate(record):
    """Return coordinate value from a tuple containing the treasure name, and treasure coordinate.

    :param record: tuple - with a (treasure, coordinate) pair.
    :return: str - the extracted map coordinate.
    """

    _, coordinate = record

    return coordinate


def convert_coordinate(coordinate):
    """Split the given coordinate into tuple containing its individual components.

    :param coordinate: str - a string map coordinate
    :return: tuple - the string coordinate split into its individual components.
    """

    return coordinate[0], coordinate[1]


def compare_records(azara_record, rui_record):
    """Compare two record types and determine if their coordinates match.

    :param azara_record: tuple - a (treasure, coordinate) pair.
    :param rui_record: tuple - a (location, tuple(coordinate_1, coordinate_2), quadrant) trio.
    :return: bool - do the coordinates match?
    """

    _, azara_coordinate = azara_record
    _, rui_coordinate, _ = rui_record

    return convert_coordinate(azara_coordinate) == rui_coordinate


def create_record(azara_record, rui_record):
    """Combine the two record types (if possible) and create a combined record group.

    :param azara_record: tuple - a (treasure, coordinate) pair.
    :param rui_record: tuple - a (location, coordinate, quadrant) trio.
    :return: tuple or str - the combined record (if compatible), or the string
        "not a match" (if incompatible).
    """

    if not compare_records(azara_record, rui_record):
        return "not a match"

    treasure, azara_coordinate = azara_record
    location, rui_coordinate, quadrant = rui_record

    return treasure, azara_coordinate, location, rui_coordinate, quadrant


def clean_up(combined_record_group):
    """Clean up a combined record group into a multi-line string of single records.

    :param combined_record_group: tuple - everything from both participants.
    :return: str - everything "cleaned", excess coordinates and information are removed.

    The return statement should be a multi-lined string with items separated by newlines.

    (see HINTS.md for an example).
    """

    report = ""
    for treasure, _, location, rui_coordinate, quadrant in combined_record_group:
        report += f"{(treasure, location, rui_coordinate, quadrant)}\n"

    return report
