namespace DotNetCoreSqlDb.Models;

public class Rating
{
    public int ID { get; set; }

    public short Score { get; set; }

    public string Feedback { get; set; } = string.Empty;
}
