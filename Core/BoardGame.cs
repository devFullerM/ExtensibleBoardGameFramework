using System;
using System.Linq;
using BoardGames.Players;
using BoardGames.Interfaces;
using BoardGames.Core.Commands;

namespace BoardGames.Core
{
    public abstract class BoardGame
    {
        private Player _playerOne = null!; // Players set in SetPlayers() before game starts. 
        private Player _playerTwo = null!;
        private Player _currentPlayer = null!;
        protected IGameRules Rules;
        protected IGameHelp GameHelp;
        protected AbstractCommandFactory CommandFactory;

        private IBoard _board;
        protected GameStorage GameStorage;
        public HistoryManager History;

        public Player CurrentPlayer
        {
            get { return _currentPlayer; }
            set { _currentPlayer = value; }
        }
        public Player PlayerOne
        {
            get { return _playerOne; }
            set { _playerOne = value; }
        }
        public Player PlayerTwo
        {
            get { return _playerTwo; }
            set { _playerTwo = value; }
        }
        public IBoard Board
        {
            get { return _board; }
            set { _board = value; }
        }

        // Constructor for initialising rules, help, command factory, board, history, and storage
        public BoardGame(IGameRules rules, IGameHelp gameHelp, AbstractCommandFactory commandFactory, IBoard board, HistoryManager history, GameStorage gameStorage)
        {
            Rules = rules;
            GameHelp = gameHelp;
            CommandFactory = commandFactory;
            _board = board;
            History = history;
            GameStorage = gameStorage;
        }

        // Game specific hooks
        public abstract void ShowWelcome();
        public abstract IPiece CreatePiece(int value);
        public virtual string[] GetStartingOrderOptions()
        {
            return new[] { "You play first", "You play second" };
        }
        protected virtual Player CreateComputerPlayer(int id)
        {
            return new ComputerPlayer(id);
        }

        // Player setup - create human/computer player based on menu selection.
        public void SetPlayers(bool p1Human, bool p2Human)
        {
            _playerOne = p1Human ? new HumanPlayer(1) : CreateComputerPlayer(1);
            _playerTwo = p2Human ? new HumanPlayer(2) : CreateComputerPlayer(2);
            _currentPlayer = _playerOne; // P1 start
            InitialisePieces();
        }

        // Game specific starting resources
        protected virtual void InitialisePieces()
        {
            // Override in subclasses to set up AvailablePieces
        }

        // Turn management
        public void SwitchPlayer()
        {
            if (_currentPlayer == _playerOne)
            {
                _currentPlayer = _playerTwo;
            }
            else
            {
                _currentPlayer = _playerOne;
            }
        }

        // Execute move after rules validation
        protected bool MakeMove(int row, int column, IPiece piece)
        {
            if (!Rules.IsValidMove(row, column, piece, _board, _currentPlayer))
            {
                Console.WriteLine("Invalid move.");
                return false;
            }

            IGameCommand command = CommandFactory.CreatePlaceCommand(row, column, piece, _board, this, Rules);
            History.ExecuteCommand(command);
            return true;
        }

        public bool Undo()
        {
            return History.Undo();
        }
        public bool Redo()
        {
            return History.Redo();
        }

        public void Save(string fileName)
        {
            GameStorage.Save(this, fileName);
        }
        public void Load(string fileName)
        {
            GameStorage.Load(this, fileName);
        }

        // Game loop
        public void Play()
        {
            if (_currentPlayer is HumanPlayer) // prevent extra empty board being displayed in HvC when CP goes first
            {
                _board.DisplayBoard();
            }

            while (true)
            {
                if (Rules.CheckWin(_board))
                {
                    SwitchPlayer();
                    Console.WriteLine($"Player {_currentPlayer.Id} wins!");
                    return;
                }

                if (Rules.IsDraw(_board))
                {
                    Console.WriteLine("It's a draw!");
                    return;
                }

                if (_currentPlayer is HumanPlayer)
                {
                    Console.WriteLine(
                        $"Player {_currentPlayer.Id}'s turn. Available numbers: " +
                        string.Join(", ", _currentPlayer.AvailablePieces.OrderBy(p => p.Value).Select(p => p.Value))
                    );
                    bool moved = false;
                    while (!moved)
                    {
                        string input = Console.ReadLine()!.Trim().ToLower();
                        moved = ProcessHumanInput(input);
                    }
                }
                else
                {
                    ComputerPlayerTurn();
                }

                _board.DisplayBoard();
            }
        }

        // Process human input
        protected bool ProcessHumanInput(string userInput)
        {
            if (userInput == "undo")
            {
                bool isHvC = (PlayerOne is HumanPlayer && PlayerTwo is ComputerPlayer) ||
                             (PlayerOne is ComputerPlayer && PlayerTwo is HumanPlayer);

                bool undone = Undo();
                if (isHvC) { Undo(); }

                if (undone)
                {
                    Console.WriteLine("Undo successful.");
                    _board.DisplayBoard();
                }
                else
                {
                    Console.WriteLine("No moves to undo.");
                }
                return false;
            }

            if (userInput == "redo")
            {
                bool isHvC = (PlayerOne is HumanPlayer && PlayerTwo is ComputerPlayer) ||
                             (PlayerOne is ComputerPlayer && PlayerTwo is HumanPlayer);

                bool redone = Redo();
                if (isHvC) { Redo(); }

                if (redone)
                {
                    Console.WriteLine("Redo successful.");
                    _board.DisplayBoard();
                }
                else
                {
                    Console.WriteLine("No moves to redo.");
                }
                return false;
            }

            if (userInput == "reroll") // essentially an undo for ComputerPlayer
            {
                bool isHvC =
                    (PlayerOne is HumanPlayer && PlayerTwo is ComputerPlayer) ||
                    (PlayerOne is ComputerPlayer && PlayerTwo is HumanPlayer);

                if (!isHvC)
                {
                    Console.WriteLine("Reroll is only available in Human vs Computer.");
                    return false;
                }

                var undone = Undo();
                if (!undone)
                {
                    Console.WriteLine("No computer move to reroll.");
                    return false;
                }

                ComputerPlayerTurn();
                Console.WriteLine("Computer move rerolled.");
                _board.DisplayBoard();
                return false;
            }

            if (userInput.StartsWith("save "))
            {
                string fileName = userInput.Substring(5).Trim();
                Save(fileName);
                Console.WriteLine("Game saved.");
                return false;
            }

            if (userInput.StartsWith("load "))
            {
                string fileName = userInput.Substring(5).Trim();
                Load(fileName);
                Console.WriteLine("Game loaded.");
                _board.DisplayBoard();
                Console.WriteLine($"Player {_currentPlayer.Id}'s turn. Available numbers: " +
                        string.Join(", ", _currentPlayer.AvailablePieces.OrderBy(p => p.Value).Select(p => p.Value)));
                return false;
            }

            if (userInput == "rules")
            {
                ShowWelcome();
                return false;
            }

            if (userInput == "help")
            {
                GameHelp.ShowHelp();
                return false;
            }

            if (userInput == "quit")
            {
                Console.WriteLine("Quitting game.");
                Environment.Exit(0);
                return true;
            }

            string[] parts = userInput.Split(' ');
            if (parts.Length == 4 && parts[0] == "place" &&
                int.TryParse(parts[1], out int row) && int.TryParse(parts[2], out int col) && int.TryParse(parts[3], out int num))
            {
                row--;
                col--;
                IPiece piece = CreatePiece(num);
                return MakeMove(row, col, piece);
            }

            Console.WriteLine("Invalid command. Use 'help' for options.");
            return false;
        }

        // Delegates computer move choice to the current ComputerPlayer.
        private bool ComputerPlayerTurn()
        {
            var cp = _currentPlayer as ComputerPlayer;
            if (cp == null) return false;

            var (r, c, piece) = cp.SelectMove(_board, Rules);
            if (piece == null) return false;

            MakeMove(r, c, piece);
            Console.WriteLine($"Computer places { piece.Value } at { r + 1 } { c + 1 }");
            return true;
        }
    }
}