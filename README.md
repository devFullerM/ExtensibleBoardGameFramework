# Extensible Board Game Framework (C# / .NET 9)

This project is an extensible board game engine written in C# as part of my Object-Oriented Programming and Design Patterns coursework.  
The framework provides a reusable structure for building turn-based board games such as Numerical Tic-Tac-Toe, Connect Four, and Sudoku sketches.

## Documentation

The full design documentation is available here:

--> [DiagramsAndDocumentation.pdf](docs/DiagramsAndDocumentation.pdf)

## Key Features

### Extensible Architecture
A core `BoardGame` engine defines the game lifecycle:
- Initialisation
- Turn-taking
- Command execution
- Validation and rules enforcement
- Undo/redo
- Win/loss/draw detection

### Design Patterns Implemented

| Pattern | Location | Purpose |
|--------|----------|---------|
| Command Pattern | `/Core/Commands` | Execute, undo, and redo moves |
| Factory Method / Abstract Factory | Game-specific command factories | Create commands per game without modifying engine |
| Strategy Pattern | `NumericalComputerPlayer` | AI behaviours |
| Template Method Pattern | `BoardGame` base class | Defines game loop steps |

### Supported Games

#### Numerical Tic-Tac-Toe (Full Implementation)
Located at: `Games/NumericalTicTacToe/`  
Includes: rules engine, command factory, pieces, undo/redo, computer player, help text.

## Running the Project

Requires **.NET 9** installed.


## Learning Outcomes

This project demonstrates:
- Object-oriented design
- Abstraction and interface-driven architecture
- Use of multiple design patterns
- Extensible game structure
- Undo/redo system
- Framework-style modularity

## Future Improvements
- Add GUI (WPF, MAUI, or web UI)
- Complete Connect Four & Sudoku
- Improve computer AI strategies
- Add game recording/export features
- Add logging & test coverage

## License
MIT License

## Author
Mitchell Fuller  
Graduate Diploma of IT (Computer Science), QUT