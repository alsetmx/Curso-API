namespace DemoApi.Models;
/// <summary>
/// Representa un contacto en la agenda.
/// </summary>
public class Contacto
{
    /// <summary>
    /// Identificador único del contacto.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nombre del contacto.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Apellido del contacto.
    /// </summary>
    public string Lastname { get; set; } = string.Empty;

    /// <summary>
    /// Fecha de nacimiento del contacto.
    /// </summary>
    public DateTime Birthday { get; set; }

    /// <summary>
    /// Número de teléfono del contacto.
    /// </summary>
    public string PhoneNumber { get; set; } = string.Empty;

    /// <summary>
    /// Correo electrónico del contacto.
    /// </summary>
    public string Email { get; set; }
}
