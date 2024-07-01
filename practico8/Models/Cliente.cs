using System;
using System.Collections.Generic;

namespace practico8.Models;

public partial class Cliente
{
    public Cliente() { }
    public Cliente(int id, string nombre, string apellido, string direccion, string documentoIdentidad, string correo, string telefono, ICollection<Alquilere> Alquileress) 
    {
        Id = id;
        Nombre = nombre;
        Apellido = apellido;
        Direccion = direccion;
        DocumentoIdentidad = documentoIdentidad;
        Correo = correo;
        Telefono = telefono;
        Alquileres = Alquileress;
    }
    public long Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string DocumentoIdentidad { get; set; } = null!;

    public string? Correo { get; set; }

    public string Telefono { get; set; } = null!;

    public virtual ICollection<Alquilere> Alquileres { get; set; } = new List<Alquilere>();
}
