public class Player : IPlayer
{
	private int _playerID;
	private string _playerName;

	public Player(int id, string name)
	{
		_playerID = id;
		_playerName = name;
	}

	public int GetId()
	{
		return _playerID;
	}

	public string GetName()
	{
		return _playerName;
	}

	public bool SetID(int id)
	{
		_playerID = id;
		return true;
	}

	public bool SetName(string name)
	{
		_playerName = name;
		return true;
	}
}
