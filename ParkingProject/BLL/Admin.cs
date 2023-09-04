namespace ParkingProject.BLL;

public class Admin
{
    public string Username { get; set; }
    public string Password { get; set; }

    public Admin(string username, string password)
    {
        Username = username;
        Password = password;
    }

    public bool IsLoggedIn(string username, string password)
    {
        if (username == "admin" && password == "1234")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}