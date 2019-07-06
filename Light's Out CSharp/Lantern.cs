using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Enumeration that represents the states of the Lantern.
/// </summary>
enum LanternState { OFF, ON };

namespace Light_s_Out_CSharp
{
    class Lantern : Entity
    {
        // PRIVATE =========================================================
        private int width;
        private int height;
        private bool selectionState;
        private LanternState currentState;

        // =================================================================


        /// <summary>
        /// Constructor of the Lantern.
        /// </summary>
        /// <param name="_coordX">Position on the X axis of the lantern.</param>
        /// <param name="_coordY">Position on the Y axis of the lantern.</param>
        /// <param name="_width">Lantern width.</param>
        /// <param name="_height">Lantern height.</param>
        /// <param name="_selectState">State of lantern selection.</param>
        // Lantern Constructor ===============================================================================
        public Lantern(int _coordX = 1, int _coordY = 1, int _width = 3, int _height = 2, bool _selectState = false)
        {
            xPosition = _coordX;
            yPosition = _coordY;

            width = _width;
            height = _height;

            selectionState = _selectState;
            currentState = LanternState.OFF;
        }
        // End Lantern Constructor ===========================================================================



        /// <summary>
        /// Lantern width.
        /// </summary>
        // Property Width ====================================================================================
        public int Width { get => width; set => width = value; }
        // End Property Width ================================================================================


        /// <summary>
        /// Lantern height.
        /// </summary>
        // Property Height ===================================================================================
        public int Height { get => height; set => height = value; }
        // End Property Height ===============================================================================


        /// <summary>
        /// State of lantern selection.
        /// </summary>
        // Property SelectState ==============================================================================
        public bool SelectState { get => selectionState; set => selectionState = value; }
        // End Property SelectState ==========================================================================


        /// <summary>
        /// Current state of lantern.
        /// </summary>
        // Property CurrentState =============================================================================
        public LanternState CurrentState { get => currentState; set => currentState = value; }
        // End Property CurrentState =========================================================================


        /// <summary>
        /// Function that allows to draw the Lantern in the console.
        /// </summary>
        // Function Draw =====================================================================================
        public override void Draw()
        {
            Console.SetCursorPosition(XPosition, YPosition);

            for (int y = 0; y <= height; ++y)
            {
                for (int x = 0; x < width; x++)
                {
                    if (selectionState == false)
                        Console.Write((currentState == LanternState.ON ? "█" : "░"));
                    else
                        Console.Write((currentState == LanternState.ON ? "▓" : "▒"));
                }
                Console.SetCursorPosition(xPosition, yPosition + y);
            }
        }
        // End Function Draw =================================================================================


        /// <summary>
        /// Function that allows invert the current state of Lantern.
        /// </summary>
        // Function InvertState ==============================================================================
        public void InvertState() => currentState = (currentState == LanternState.ON ? LanternState.OFF : LanternState.ON);
        // End Function InvertState ==========================================================================


        /// <summary>
        /// Function that allows invert the selection state of Lantern.
        /// </summary>
        // Function InvertSelectionState =====================================================================
        public void InvertSelectionState() => selectionState = (selectionState == true ? false : true);
        // End Function InvertSelectionState =================================================================
    }
}
