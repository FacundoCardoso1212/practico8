using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace practico8.Models;

public partial class Alquilere
{

    public Alquilere() { }

    public Alquilere(long id, long idCopia, long idCliente, DateTime fechaAlquiler, DateTime fechaTope, DateTime? fechaEntregada, Cliente? idClienteNavigation, Copia? idCopiaNavigation)
    {
        Id = id;
        IdCopia = idCopia;
        IdCliente = idCliente;
        FechaAlquiler = fechaAlquiler;
        FechaTope = fechaTope;
        FechaEntregada = fechaEntregada;
        IdClienteNavigation = idClienteNavigation;
        IdCopiaNavigation = idCopiaNavigation;
    }

    public long Id { get; set; }

    public long IdCopia { get; set; }

    public long IdCliente { get; set; }

    public DateTime FechaAlquiler { get; set; }

    public DateTime FechaTope { get; set; }

    public DateTime? FechaEntregada { get; set; }

    public virtual Cliente? IdClienteNavigation { get; set; }
    public virtual Copia? IdCopiaNavigation { get; set; }

    [NotMapped]
    public string? PeliculaTitulo { get; private set; }

    public async Task SetPeliculaTituloAsync(Practico8Context context)
    {
        if (IdCopiaNavigation != null && IdCopiaNavigation.IdPelicula != 0)
        {
            var pelicula = await context.Peliculas.FindAsync(IdCopiaNavigation.IdPelicula);
            PeliculaTitulo = pelicula?.Titulo ?? "Sin título";
        }
        else
        {
            PeliculaTitulo = "Sin título";
        }
    }

}


