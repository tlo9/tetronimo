using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tetromino
{
    /// <summary>A Tetronimo shape is composed of a group of 4 colored blocks
    /// within a 4x4 region</summary>
    class Shape
    {
        /// <summary>Represents a Tetronimo Shape as an array of true/false values
        /// True means there is a block in the space
        /// False means the space is empty</summary>
        public bool[,] BlockArray { get; private set; }
        public Color Color { get; private set; }
        public const int MaxWidth = 4;
        public const int MaxHeight = 4;
        public enum Standard { O, I, S, Z, L, J, T }

        private static readonly Shape I = new Shape(
        new bool[MaxHeight, MaxWidth]
            {
                {false, false, true, false},
                {false, false, true,  false},
                {false, false, true, false},
                {false, false, true, false}
            }, Color.Red);
        private static readonly Shape J = new Shape(
             new bool[MaxHeight, MaxWidth]
            {
                {false, false, false, false},
                {false, true,  true,  true},
                {false, false, false, true},
                {false, false, false, false}
            }, Color.Magenta);
        private static readonly Shape L = new Shape(
            new bool[MaxHeight, MaxWidth]
            {
                {false, false, false, false},
                {false, true,  true,  true},
                {false, true,  false, false},
                {false, false, false, false}
            }, Color.Yellow);
        private static readonly Shape O = new Shape(
            new bool[MaxHeight, MaxWidth]
            {
                {false, false, false, false},
                {false, true, true, false},
                {false, true, true, false},
                {false, false, false, false}
            }, Color.Cyan);
        private static readonly Shape Z = new Shape(
            new bool[MaxHeight, MaxWidth]
            {
                {false, false, false, false},
                {false, true,  true,  false},
                {false, false, true,  true},
                {false, false, false, false}
            }, Color.Lime);
        private static readonly Shape T = new Shape(
            new bool[MaxHeight, MaxWidth]
            {
                {false, false, false, false},
                {false, true,  true,  true},
                {false, false, true,  false},
                {false, false, false, false}
            }, Color.LightGray);
        private static readonly Shape S = new Shape(
            new bool[MaxHeight, MaxWidth]
            {
                {false, false, false, false},
                {false, false, true,  true},
                {false, true,  true,  false},
                {false, false, false, false}
            }, Color.Blue);

        private Shape(bool[,] blockArray, Color color)
        {
            if (color == BlockGrid.EmptySpace)
                throw new ArgumentException();

            BlockArray = blockArray;
            Color = color;
        }

        public Shape(Shape shape)
        {
            BlockArray = new bool[MaxHeight, MaxWidth];

            for (int row = 0; row < shape.BlockArray.GetLength(0); row++)
            {
                for (int col = 0; col < shape.BlockArray.GetLength(1); col++)
                {
                    BlockArray[row, col] = shape.BlockArray[row, col];
                }
            }
            Color = shape.Color;
        }

        public static Shape FromStandard(Standard s)
        {
            switch (s)
            {
                case Standard.I:
                    return new Shape(I);
                case Standard.O:
                    return new Shape(O);
                case Standard.J:
                    return new Shape(J);
                case Standard.S:
                    return new Shape(S);
                case Standard.L:
                    return new Shape(L);
                case Standard.Z:
                    return new Shape(Z);
                case Standard.T:
                    return new Shape(T);
                default:
                    return null;
            }
        }

        /// <summary>
        /// Rotate a group of blocks by 90 degrees counterclockwise
        /// </summary>
        public void Rotate()
        {
            int blocksRows = BlockArray.GetLength(0);
            int blocksCols = BlockArray.GetLength(1);
            var newBlocks = new bool[blocksCols, blocksRows];

            for (int row = 0; row < blocksRows; row++)
            {
                for (int col = 0; col < blocksCols; col++)
                    newBlocks[(blocksCols - 1) - col, row] = BlockArray[row, col];
            }

            BlockArray = newBlocks;
        }

        /// <summary>
        /// Is this shape an unrotated copy of a Standard Shape?
        /// </summary>
        /// <param name="s">The standard shape to compare with.</param>
        /// <returns>True, if similar. False, otherwise.</returns>

        public bool Equals(Standard s)
        {
            Shape shape;

            switch (s)
            {
                case Standard.I:
                    shape = I;
                    break;
                case Standard.O:
                    shape = O;
                    break;
                case Standard.J:
                    shape = J;
                    break;
                case Standard.S:
                    shape = S;
                    break;
                case Standard.L:
                    shape = L;
                    break;
                case Standard.Z:
                    shape = Z;
                    break;
                case Standard.T:
                    shape = T;
                    break;
                default:
                    return false;
            }

            for (int row = 0; row < MaxWidth; row++)
            {
                for (int col = 0; col < MaxHeight; col++)
                {
                    if (shape.BlockArray[col, row] != BlockArray[col, row])
                        return false;
                }
            }

            return true;
        }
    }
}
