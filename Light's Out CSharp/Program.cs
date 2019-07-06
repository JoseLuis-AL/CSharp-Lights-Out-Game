using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Light_s_Out_CSharp
{
    class Program
    {
        // Function main =====================================================================================
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            int rows, columns;

            rows = ReadNumber("Number of rows [min = 3, max = 9]: ");
            columns = ReadNumber("Number of columns [min 3, max 9]: ");

            // Create the game board.
            GameBoard gameBoard = new GameBoard(1, 1, rows, columns);

            // Game lógic.
            while (gameBoard.CurrentState == GameBoardState.PLAYING)
            {
                gameBoard.Routines();
            }

            // Clear the console.
            Console.Clear();

            // Show a message when the game ends.
            if (gameBoard.CurrentState == GameBoardState.DONE)
                Console.WriteLine("---> Congratulations, you won!\n\n-> Number of movements: {0}. \n\n\n", gameBoard.CurrentMoves);
            else if (gameBoard.CurrentState == GameBoardState.GAME_OVER)
                Console.WriteLine("Better luck next time. \n\n");
        }
        // End Function main =================================================================================


        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        // Function ReadNumber ===============================================================================
        public static int ReadNumber(string message)
        {
            int input;

            do
            {
                try
                {
                    Console.Write(message);
                    input = Convert.ToInt32(Console.ReadLine());
                    return input;

                } catch (Exception e)
                {
                    Console.WriteLine("---> ERROR: That is not a number. Please try again.");
                }
            } while (true);
        }
        // End Function ReadNumber ===========================================================================
    }
}
