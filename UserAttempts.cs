using System;

public class UserAttempts
{
    [Key]
    public int Id { get; set; }
    public string UserName { get; set; }
    public DateTime RequestMadeOn { get; set; }
}
