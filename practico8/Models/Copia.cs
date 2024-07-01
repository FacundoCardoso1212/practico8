using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace practico8.Models;

public partial class Copia
{
    public Copia() { }
    public Copia(int id, long idPelicula, bool detenida, string formato, double precioAlquiler, ICollection<Alquilere> alquileres) 
    {
    Id = id;
    IdPelicula = idPelicula;
    Detenida = detenida;
    Formato = formato;
    PrecioAlquiler = precioAlquiler;
    Alquileres = alquileres;
    }
    public long Id { get; set; }

    public long IdPelicula { get; set; }

    public bool Detenida { get; set; }

    public string Formato { get; set; } = null!;

    public double PrecioAlquiler { get; set; }

    public virtual ICollection<Alquilere> Alquileres { get; set; } = new List<Alquilere>();

    public virtual Pelicula? IdPeliculaNavigation { get; set; }

    public bool EstaAlquilada => Alquileres.Any(a => a.FechaEntregada == null);

    public List<Copia> listaAlquiladas(List<Copia> listaCopias)
    {
        if (EstaAlquilada == false)
        {
            foreach (var item in listaCopias)
            {
                listaCopias.Add(item);
            }
        }
        return listaCopias;
    }

 }
