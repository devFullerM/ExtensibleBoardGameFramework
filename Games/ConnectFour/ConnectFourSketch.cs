// Concept for extensibility purposes.
// Connect Four on GridBoard(6x7).
// Cells store ints: 0 = empty, 1 = P1 disc, 2 = P2 disc. Pieces unlimited.
// Move = drop in column (falls to lowest empty).
// Win = any 4 in a row (horizontal, vertical, diagonal)
// Input would be "drop c" instead of "place r c n". Would need a small game-specifc input hook.