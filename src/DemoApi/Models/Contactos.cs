namespace DemoApi.Models;
public class Contacto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;
    public DateTime Birthday { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; }
}
