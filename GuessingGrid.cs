using System;
namespace src
{
	public enum State { Unknown, Ship, Empty };
	public class GuessingGrid
	{
		State[,] firingBoard;
        public int length, width;
		public GuessingGrid(int h, int w)
		{
            length = h;
            width = w;
			firingBoard = new State[h, w];
			for (int i = 0; i < h; i++)
			{
				for (int j = 0; j < w; j++)
					firingBoard[i, j] = State.Unknown;
			}
		}

		public void ChangeState(int x, int y, bool isShip)
		{
			if (isShip)
				firingBoard[y, x] = State.Ship;
			else
				firingBoard[y, x] = State.Empty;
		}

		public void Display()
		{
            Console.WriteLine("Enemy field :");
            List<string> firstLine = new List<string> { " | A" };
            char[] letter = new char[1];
            letter[0] = 'B';

            for (int i = 1; i < width; i++)
            {
                firstLine.Add(new string(letter));
                letter[0]++;
            }

            Console.WriteLine();
            Console.WriteLine("   " + String.Join(" | ", firstLine) + " |");
            for (int i = 0; i < (4 * (width + 1)); i++)
                Console.Write("-");
            Console.WriteLine();

            for (int line = 1; line < (length + 1); line++)
            {
                List<string> ships = new List<string>();
                if (firingBoard[line - 1, 0] == State.Ship)
                    ships.Add(" | X");
                else if (firingBoard[line - 1, 0] == State.Empty)
                    ships.Add(" | ~");
                else
                    ships.Add(" |  ");
                for (int j = 1; j < width; j++)
                {
                    if (firingBoard[line - 1, j] == State.Ship)
                        ships.Add("X");
                    else if (firingBoard[line - 1, j] == State.Unknown)
                        ships.Add(" ");
                    else
                        ships.Add("~");
                }

                if (line >= 10)
                    Console.WriteLine(line + " " + String.Join(" | ", ships) + " |");
                else
                    Console.WriteLine(" " + line + " " + String.Join(" | ", ships) + " |");

                for (int i = 0; i < (4 * (width + 1)); i++)
                    Console.Write("-");
                Console.Write("\n");
            }
        }
	}
}

