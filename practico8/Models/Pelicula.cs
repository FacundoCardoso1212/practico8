using System;
using System.Collections.Generic;

namespace practico8.Models;

public partial class Pelicula
{
    public Pelicula() { }   
    public Pelicula(int id, string titulo, int anio, int calificacion, ICollection<Copia>copia)
    
    {

    Id = id;
    Titulo = titulo;
    Anio = anio;
    Calificacion = calificacion;
    Anio = anio;
    Copia = copia;
    
    }

    public long Id { get; set; }

    public string Titulo { get; set; } = null!;

    public int Anio { get; set; }

    public int Calificacion { get; set; }

    public virtual ICollection<Copia> Copia { get; set; } = new List<Copia>();
}
