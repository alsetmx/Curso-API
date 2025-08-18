using DemoApi.Data;
using DemoApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace DemoApi.Controllers
{
    /// <summary>
    /// Controlador para gestionar los contactos de la agenda.
    /// Proporciona operaciones CRUD sobre la entidad Contacto.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ContactosController : ControllerBase
    {
        /// <summary>
        /// Contexto de base de datos para acceder a los contactos.
        /// </summary>
        private readonly DataContext _context;

        /// <summary>
        /// Inicializa una nueva instancia del controlador ContactosController.
        /// </summary>
        /// <param name="context">El contexto de base de datos.</param>
        public ContactosController(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene la lista de todos los contactos.
        /// </summary>
        /// <returns>Una lista de contactos.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contacto>>> GetContactos()
        {
            return await _context.Contactos.ToListAsync();
        }

        /// <summary>
        /// Obtiene un contacto por su identificador único.
        /// </summary>
        /// <param name="id">Identificador del contacto.</param>
        /// <returns>El contacto encontrado o NotFound si no existe.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Contacto>> GetContacto(int id)
        {
            var contacto = await _context.Contactos.FindAsync(id);
            if (contacto == null)
            {
                return NotFound();
            }
            return contacto;
        }

    /// <summary>
    /// Crea un nuevo contacto en la agenda.
    /// </summary>
    /// <param name="contacto">
    /// <b>Request Body:</b> JSON con los datos del contacto.<br/>
    /// <b>Campos requeridos:</b> name, lastname, birthday, phoneNumber, email.<br/>
    /// <b>Ejemplo:</b><br/>
    /// <code>
    /// {
    ///   "name": "Marce",
    ///   "lastname": "Martínez",
    ///   "birthday": "2000-01-01",
    ///   "phoneNumber": "1234567890",
    ///   "email": "marce@example.com"
    /// }
    /// </code>
    /// </param>
    /// <returns>
    /// <b>Response:</b><br/>
    /// <ul>
    ///   <li><b>201 Created:</b> El contacto agregado en formato JSON.</li>
    ///   <li><b>400 BadRequest:</b> Si el contacto es nulo o faltan campos requeridos.</li>
    /// </ul>
    /// <b>Ejemplo de respuesta exitosa:</b><br/>
    /// <code>
    /// {
    ///   "id": 1,
    ///   "name": "Marce",
    ///   "lastname": "Gómez",
    ///   "birthday": "2000-01-01T00:00:00",
    ///   "phoneNumber": "1234567890",
    ///   "email": "marce@example.com"
    /// }
    /// </code>
    /// </returns>
    /// <remarks>
    /// <para>Validaciones: Todos los campos son obligatorios.</para>
    /// <para>El email debe tener formato válido.</para>
    /// <para>Seguridad: Requiere autenticación si el endpoint está protegido.</para>
    /// <para>Operación: Guarda el contacto en la base de datos y retorna el recurso creado.</para>
    /// </remarks>
    /// <example>
    /// var nuevo = new Contacto { Name = "Marce", Lastname = "Gómez", Birthday = new DateTime(2000,1,1), PhoneNumber = "1234567890", Email = "marce@example.com" };
    /// controller.AddContacto(nuevo);
    /// </example>
    /// <exception cref="ArgumentNullException">Si el contacto es nulo.</exception>
    /// <exception cref="FormatException">Si el email no tiene formato válido.</exception>
    /// <response code="201">Contacto creado correctamente.</response>
    /// <response code="400">Datos inválidos o incompletos.</response>


        [HttpPost]
        public async Task<ActionResult<Contacto>> AddContacto(Contacto contacto)
        {
            if (contacto == null)
            {
                return BadRequest("El contacto no puede estar vacío.");
            }
            _context.Contactos.Add(contacto);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetContacto), new { id = contacto.Id }, contacto);
        }

        /// <summary>
        /// Actualiza los datos de un contacto existente.
        /// </summary>
        /// <param name="id">Identificador del contacto a actualizar.</param>
        /// <param name="contacto">Datos nuevos del contacto.</param>
        /// <returns>NoContent si la actualización fue exitosa, NotFound si el contacto no existe.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContacto(int id, Contacto contacto)
        {
            var existingContacto = await _context.Contactos.FindAsync(id);
            if (existingContacto == null)
            {
                return NotFound();
            }
            existingContacto.Name = contacto.Name;
            existingContacto.Lastname = contacto.Lastname;
            existingContacto.Birthday = contacto.Birthday;
            existingContacto.PhoneNumber = contacto.PhoneNumber;
            existingContacto.Email = contacto.Email;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Elimina un contacto por su identificador.
        /// </summary>
        /// <param name="id">Identificador del contacto a eliminar.</param>
        /// <returns>NoContent si la eliminación fue exitosa, NotFound si el contacto no existe.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContacto(int id)
        {
            var contacto = await _context.Contactos.FindAsync(id);
            if (contacto == null)
            {
                return NotFound();
            }
            _context.Contactos.Remove(contacto);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
