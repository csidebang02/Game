public class Board : IBoard
{
    private int _boardSize;

    public Board(int size)
    {
        _boardSize = size;
    }

    public int GetBoardSize()
    {
        return _boardSize;
    }
}
