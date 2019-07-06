using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Light_s_Out_CSharp
{
    abstract class Entity
    {
        protected int xPosition;
        protected int yPosition;

        /// <summary>
        /// Position on the X axis of the lantern.
        /// </summary>
        public int XPosition { get => xPosition; set => xPosition = value; }

        /// <summary>
        /// Position on the Y axis of the lantern.
        /// </summary>
        public int YPosition { get => yPosition; set => yPosition = value; }

        public abstract void Draw();
    }
}
