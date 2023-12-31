﻿using System;
using System.Collections.Generic;

namespace gamer_store_api.Data.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public int IdRol { get; set; }

    public int IdEstado { get; set; }

    public string Nombres { get; set; } = null!;

    public string ApellidoPaterno { get; set; } = null!;

    public string ApellidoMaterno { get; set; } = null!;

    public string NumeroTelefonico { get; set; } = null!;

    public string Calle { get; set; } = null!;

    public string NumeroExterior { get; set; } = null!;

    public string? NumeroInterior { get; set; }

    public string Colonia { get; set; } = null!;

    public string CodigoPostal { get; set; } = null!;

    public bool Activo { get; set; }

    public string UsuarioCreacion { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public string UsuarioModificacion { get; set; } = null!;

    public DateTime FechaModificacion { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Envio> Envios { get; set; } = new List<Envio>();

    public virtual Estado IdEstadoNavigation { get; set; } = null!;

    public virtual Rol IdRolNavigation { get; set; } = null!;
}
