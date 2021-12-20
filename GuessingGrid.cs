using System;
namespace src
{
	public enum State { Unknown, Ship, Empty };
	public class GuessingGrid
	{
		State[,] firingBoard;
		public GuessingGrid(int h, int w)
		{
			firingBoard = new State[h, w];
			for (int i = 0; i < h; i++)
			{
				for (int j = 0; j < w; j++)
					firingBoard[i, j] = State.Unknown;
			}
		}

		public void Display()
		{

		}
	}
}

