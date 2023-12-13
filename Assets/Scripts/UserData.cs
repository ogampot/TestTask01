[System.Serializable]
public class UserData
{
    public int level = 1;
    public int tickets = 100;

    public UserData()
    {
        level = 1;
        tickets = 100;
    }

    public void SetUserLevel(int level)
    {
        this.level = level;
    }

    public void SetUserTickets(int tickets)
    {
        this.tickets = tickets;
    }
}