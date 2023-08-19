public class Position
{
	public int X {get; }
	public int Y {get; }

	public Position(int x, int y)
	{
		X = x;
		Y = y;
	}
	public int GetX()
	{
		return X;
	}
	public int GetY()
	{
		return Y;
	}
}
