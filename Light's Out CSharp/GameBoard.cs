using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

enum GameBoardState { GAME_OVER, PLAYING, DONE };


namespace Light_s_Out_CSharp
{
    class GameBoard : Entity
    {
        // PRIVATE =========================================================
        private int boardRows;
        private int boardColumns;
        private int currentRow;
        private int currentColumn;
        private int movements;
        private int currentMoves;
        private GameBoardState currentState;
        private Lantern[,] lanternBoard;

        // =================================================================


        /// <summary>
        /// Constructor of the game board.
        /// </summary>
        /// <param name="_xPosition">Position on the X axis.</param>
        /// <param name="_yPosition">Position on the Y axis.</param>
        /// <param name="_rows">Number of rows. (min: 3, max:9)</param>
        /// <param name="_columns">Number of columns (min:3, max:9.</param>
        // GameBoard Constructor =============================================================================
        public GameBoard(int _xPosition, int _yPosition, int _rows, int _columns)
        {
            Console.Clear();

            xPosition = _xPosition;
            yPosition = _yPosition;

            if (_rows < 3 || _rows > 9)
                boardRows = 3;
            else boardRows = _rows;

            if (_columns < 3 || _columns > 9)
                boardColumns = 3;
            else boardColumns = _columns;

            currentMoves = 0;
            movements = 0;

            lanternBoard = new Lantern[boardRows, boardColumns];
            InitializeBoard();

            currentState = GameBoardState.PLAYING;

            currentRow = 0;
            currentColumn = 0;
        }
        // End GameBoard Constructor =========================================================================


        /// <summary>
        /// Current number of movements made.
        /// </summary>
        // Property CurrentMoves =============================================================================
        public int CurrentMoves { get => currentMoves; }
        // End Property CurrentMoves =========================================================================


        /// <summary>
        /// Current state of game board.
        /// </summary>
        // Property CurrentState =============================================================================
        public GameBoardState CurrentState { get => currentState; }
        // End Property CurrentState =========================================================================


        /// <summary>
        /// Function that creates all the flashlights of the game board and orders them.
        /// </summary>
        // Function InitializeLanterns =======================================================================
        private void InitializeLanterns()
        {
            for (int column = 0; column < boardColumns; ++column)
            {
                for (int row = 0; row < boardRows; ++row)
                {
                    lanternBoard[row, column] = new Lantern(xPosition, yPosition);
                    lanternBoard[row, column].XPosition += (lanternBoard[row, column].Width + 1) * column;
                    lanternBoard[row, column].YPosition += (lanternBoard[row, column].Height + 1) * row;
                }
            }

            lanternBoard[0, 0].InvertSelectionState();
        }
        // End Function InitializeLanterns ====================================================================


        /// <summary>
        /// Function that allows to initialize the board, initialize the flashlights so that there is always a solution.
        /// </summary>
        // Function InitializeBoard ===========================================================================
        private void InitializeBoard()
        {
            InitializeLanterns();

            Random random = new Random();
            int repeat = random.Next(0, 10);
            int randomRow;
            int randomColumn;

            for (int i = 0; i < repeat; ++i)
            {
                randomRow = random.Next(0, boardRows - 1);
                randomColumn = random.Next(0, boardColumns - 1);

                UpdateLanterns(randomRow, randomColumn);

                ++movements;
            }
        }
        // End Function InitializeBoard =======================================================================


        /// <summary>
        /// Function that checks the game board to see if all the lanterns are off.
        /// </summary>
        // Function CheckBoard ================================================================================
        private void CheckBoard()
        {
            if (currentState == GameBoardState.PLAYING)
            {
                foreach (Lantern linterna in lanternBoard)
                    if (linterna.CurrentState != LanternState.OFF)
                        return;

                currentState = GameBoardState.DONE;
            }
        }
        // End Function CheckBoard ============================================================================


        /// <summary>
        /// Function that allows to change the state of the selected lantern and the adjacent lanterns.
        /// </summary>
        /// <param name="_row">Row of the selected lantern.</param>
        /// <param name="_column">Column of the selected lantern.</param>
        // Function UpdateLanterns ===========================================================================
        private void UpdateLanterns(int _row, int _column)
        {
            lanternBoard[_row, _column].InvertState();

            if (_column + 1 <= boardColumns - 1)
                lanternBoard[_row, _column + 1].InvertState();

            if (_column - 1 >= 0)
                lanternBoard[_row, _column - 1].InvertState();

            if (_row + 1 <= boardRows - 1)
                lanternBoard[_row + 1, _column].InvertState();

            if (_row - 1 >= 0)
                lanternBoard[_row - 1, _column].InvertState();
        }
        // End Function UpdateLanterns =======================================================================


        /// <summary>
        /// Function that manages all the logic of the game.
        /// </summary>
        // Function Gameplay =================================================================================
        private void Gameplay()
        {
            ConsoleKeyInfo input = Console.ReadKey(true);

            if (input.Key == ConsoleKey.Spacebar)
            {
                ++currentMoves;
                UpdateLanterns(currentRow, currentColumn);
            }


            if (input.Key == ConsoleKey.Escape)
                currentState = GameBoardState.GAME_OVER;


            if (input.Key == ConsoleKey.RightArrow && currentColumn < boardColumns - 1)
            {
                lanternBoard[currentRow, currentColumn].InvertSelectionState();
                lanternBoard[currentRow, ++currentColumn].InvertSelectionState();
            }

            if (input.Key == ConsoleKey.LeftArrow && currentColumn > 0)
            {
                lanternBoard[currentRow, currentColumn].InvertSelectionState();
                lanternBoard[currentRow, --currentColumn].InvertSelectionState();
            }

            if (input.Key == ConsoleKey.UpArrow && currentRow > 0)
            {
                lanternBoard[currentRow, currentColumn].InvertSelectionState();
                lanternBoard[--currentRow, currentColumn].InvertSelectionState();
            }

            if (input.Key == ConsoleKey.DownArrow && currentRow < boardRows - 1)
            {
                lanternBoard[currentRow, currentColumn].InvertSelectionState();
                lanternBoard[++currentRow, currentColumn].InvertSelectionState();
            }
        }
        // End Function Gameplay =============================================================================


        /// <summary>
        /// Function that allows to draw the Lantern in the console.
        /// </summary>
        // Function Draw =====================================================================================
        public override void Draw()
        {
            foreach (Lantern lantern in lanternBoard)
                lantern.Draw();

            Console.SetCursorPosition(1, 3 * boardColumns);
            Console.WriteLine("Current row: {0}, current column: {1}.", currentRow + 1, currentColumn + 1);
            Console.SetCursorPosition(1, 3 * boardColumns + 1);
            Console.Write("Movements: {0}, Current moves: {1}.", movements, currentMoves);
        }
        // End Function Draw =================================================================================


        /// <summary>
        /// Function that manages all the routines of the game board. Functions like draw, gameplay and check
        /// if the game board are done.
        /// </summary>
        // Function Routines =================================================================================
        public void Routines()
        {
            Draw();
            Gameplay();
            CheckBoard();
        }
        // End Function Routines =============================================================================




    }
}
