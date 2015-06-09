using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tetromino
{
    /// <summary>Tetronimo shapes are dropped and imprinted onto the block grid.
    /// Once imprinted, a Tetronimo shape's blocks can fall due to
    /// gravity or be cleared their corresponding rows are filled</summary>
    class BlockGrid
    {
        /// <summary>
        /// A pair of row/column indicies for specifying the position of elements in a
        /// 2D array
        /// </summary>
        public struct Position
        {
            public int Row;
            public int Column;

            public Position(int row, int col)
                : this()
            {
                Row = row;
                Column = col;
            }
        }

        public Color[,] Blocks { get; private set; }
        public int Rows { get; private set; }
        public int Columns { get; private set; }
        /// <summary>
        /// These are the number of rows that are hidden from view
        /// at the top of the block grid.
        /// </summary>
        public int HiddenRows { get; private set; }

        public const int StandardColumns = 10;
        public const int StandardRows = 20;

        /// <summary>These are the extra rows above the top of the grid.
        /// They allow shapes to overflow the top.</summary>
        public const int StandardHiddenRows = 2;
        public static readonly Color EmptySpace = default(Color);

        public BlockGrid(int cols = StandardColumns, int rows = StandardRows,
            int hiddenRows = StandardHiddenRows)
        {
            if (cols < Shape.MaxWidth || rows < Shape.MaxHeight || hiddenRows < 0)
                throw new ArgumentOutOfRangeException();

            Columns = cols;
            Rows = rows;
            HiddenRows = hiddenRows;
            Blocks = new Color[rows, cols];
        }

        /// <summary>
        /// Place a shape onto the block grid at the specified
        /// position.
        /// </summary>
        /// <param name="position">The shape's position.</param>
        /// <param name="shape">The shape to add to the block grid.</param>
        public void Imprint(Position position, Shape shape)
        {
            for (int row = 0; row < shape.BlockArray.GetLength(0); row++)
            {
                for (int col = 0; col < shape.BlockArray.GetLength(1); col++)
                {
                    if (row + position.Row < 0 || row + position.Row >= Rows
                        || col + position.Column < 0 || col + position.Column >= Columns)
                    {
                        continue;
                    }

                    if (shape.BlockArray[row, col])
                        Blocks[row + position.Row, col + position.Column] = shape.Color;
                }
            }
        }

        /// <summary>
        /// Is the specified row entirely clear of blocks?
        /// </summary>
        /// <param name="row"></param>
        /// <returns>True if empty. False, otherwise.</returns>
        public bool RowIsEmpty(int row)
        {
            if (row < 0 || row >= Rows)
                throw new ArgumentOutOfRangeException();

            for (int col = 0; col < Columns; col++)
            {
                // Is this cell empty?
                if (Blocks[row, col] != EmptySpace)
                    return false;
            }

            return true;
        }

        public void CopyRow(int from, int to)
        {
            if (from < -HiddenRows || from >= Rows || to < -HiddenRows || to >= Rows)
                throw new ArgumentOutOfRangeException();

            for (int col = 0; col < Columns; col++)
                Blocks[to, col] = Blocks[from, col];
        }

        public void ClearRow(int row)
        {
            if (row < -HiddenRows || row >= Rows)
                throw new ArgumentOutOfRangeException();

            for (int col = 0; col < Columns; col++)
                Blocks[row, col] = EmptySpace;
        }

        /// <summary>
        /// Is the specified row completely filled with blocks?
        /// </summary>
        /// <param name="row"></param>
        /// <returns>True if filled. False, otherwise.</returns>
        public bool RowIsFilled(int row)
        {
            if (row < 0 || row >= Rows)
                throw new ArgumentOutOfRangeException();

            for (int col = 0; col < Columns; col++)
            {
                // Is this cell filled?
                if (Blocks[row, col] == EmptySpace)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Do any of the blocks in the Tetronimo shape overlap with any of the blocks
        /// in the block grid at the specified position?
        /// </summary>
        /// <param name="position">The shape's position.</param>
        /// <param name="shape">The shape to add to the block grid.</param>
        /// <returns>True if the shape overlaps with at least one
        /// block in the grid. False, otherwise.</returns>
        public bool Overlaps(Position position, Shape shape)
        {
            for (int row = 0; row < shape.BlockArray.GetLength(0); row++)
            {
                for (int col = 0; col < shape.BlockArray.GetLength(1); col++)
                {
                    if (row + position.Row < 0 || row + position.Row >= Rows
                        || col + position.Column < 0 || col + position.Column >= Columns)
                    {
                        continue;
                    }

                    if (shape.BlockArray[row, col]
                        && Blocks[row + position.Row, col + position.Column] != EmptySpace)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Will the shape "stick out" over the top of the grid if 
        /// it were to be placed at the specified position?
        /// </summary>
        /// <param name="position">The shape's position.</param>
        /// <param name="shape">The shape to add to the block grid.</param>
        /// <returns>True if the shape overflows over the top of the grid.
        /// False, otherwise.</returns>
        public bool OverflowsTop(Position position, Shape shape)
        {
            if (position.Row < 0)
            {
                for (int row = 0; row < shape.BlockArray.GetLength(0); row++)
                {
                    for (int col = 0; col < shape.BlockArray.GetLength(1); col++)
                    {
                        if (shape.BlockArray[row, col] && row + position.Row < 0)
                            return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Will the shape "stick out" below the bottom of the grid if 
        /// it were to be placed at the specified position?
        /// </summary>
        /// <param name="position">The shape's position.</param>
        /// <param name="shape">The shape to add to the block grid.</param>
        /// <returns>True if the shape overflows over the top of the grid.
        /// False, otherwise.</returns>
        public bool OverflowsBottom(Position position, Shape shape)
        {
            if (position.Row >= Rows - Shape.MaxHeight)
            {
                for (int row = 0; row < shape.BlockArray.GetLength(0); row++)
                {
                    for (int col = 0; col < shape.BlockArray.GetLength(1); col++)
                    {
                        if (shape.BlockArray[row, col] && row + position.Row >= Rows)
                            return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Is the shape completely inside of the grid (including the hidden rows?)?
        /// </summary>
        /// <param name="position">The shape's position.</param>
        /// <param name="shape">The shape to add to the block grid.</param>
        /// <returns>True if the shape is within the grid.
        /// False, otherwise.</returns>
        public bool IsInsideGrid(Position position, Shape shape)
        {
            if (position.Column >= 0 && position.Column < Columns - Shape.MaxWidth
                && position.Row >= -HiddenRows && position.Row < Rows - Shape.MaxHeight)
            {
                return true;
            }

            for (int row = 0; row < shape.BlockArray.GetLength(0); row++)
            {
                for (int col = 0; col < shape.BlockArray.GetLength(1); col++)
                {
                    if (shape.BlockArray[row, col])
                    {
                        if (row + position.Row < -HiddenRows
                            || row + position.Row >= Rows
                            || col + position.Column < 0
                            || col + position.Column >= Columns)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }
    }
}
