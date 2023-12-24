using gamer_store_api.Data;
using gamer_store_api.Data.DTOs;
using gamer_store_api.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace gamer_store_api.Services;

public class ProductosService
{
    private readonly GamerStoreContext _context;

    public ProductosService(GamerStoreContext context)
    {
        _context = context;
    }

    #region get
    public async Task<IEnumerable<ProductoDtoGet>> GetProductos()
{
    // Se hace un query para incluir "lefts joins"
    var query = _context.Productos
        .Join(_context.Categorias,
            producto => producto.IdCategoria,
            categoria => categoria.IdCategoria,
            (producto, categoria) => new { Producto = producto, Categoria = categoria })
        .Join(_context.UnidadesMedida,
            joinedTables => joinedTables.Producto.IdUnidadMedida,
            unidadMedida => unidadMedida.IdUnidadMedida,
            (joinedTables, unidadMedida) => new { joinedTables.Producto, joinedTables.Categoria, UnidadMedida = unidadMedida })
        .GroupJoin(_context.Plataformas,
            joinedTables => joinedTables.Producto.IdPlataforma,
            plataforma => plataforma.IdPlataforma,
            (joinedTables, plataformasGroup) => new { joinedTables.Producto, joinedTables.Categoria, joinedTables.UnidadMedida, PlataformasGroup = plataformasGroup })
        .SelectMany(joinedTables => joinedTables.PlataformasGroup.DefaultIfEmpty(),
            (joinedTables, plataforma) => new ProductoDtoGet
            {
                IdProducto = joinedTables.Producto.IdProducto,
                IdCategoria = joinedTables.Producto.IdCategoria,
                Categoria = joinedTables.Categoria.Nombre,
                IdPlataforma = joinedTables.Producto.IdPlataforma,
                Plataforma = plataforma!.Nombre, //pude que haya que quitar el signo de exclamacion, debido a que plataforma.nombre puede ser null
                IdUnidadMedida = joinedTables.Producto.IdUnidadMedida,
                UnidadMedida = joinedTables.UnidadMedida.Nombre,
                Nombre = joinedTables.Producto.Nombre,
                Descripcion = joinedTables.Producto.Descripcion,
                Peso = joinedTables.Producto.Peso,
                Precio = joinedTables.Producto.Precio,
                Stock = joinedTables.Producto.Stock,
                Activo = joinedTables.Producto.Activo,
                UsuarioModificacion = joinedTables.Producto.UsuarioModificacion
            });

    var result = await query.ToListAsync();

    return result;
}

public async Task<ProductoDtoGet> GetProductoById(int idProducto)
{
    // Se hace un query para incluir "lefts joins" y se filtra por IdProducto
    var query = _context.Productos
        .Join(_context.Categorias,
            producto => producto.IdCategoria,
            categoria => categoria.IdCategoria,
            (producto, categoria) => new { Producto = producto, Categoria = categoria })
        .Join(_context.UnidadesMedida,
            joinedTables => joinedTables.Producto.IdUnidadMedida,
            unidadMedida => unidadMedida.IdUnidadMedida,
            (joinedTables, unidadMedida) => new { joinedTables.Producto, joinedTables.Categoria, UnidadMedida = unidadMedida })
        .GroupJoin(_context.Plataformas,
            joinedTables => joinedTables.Producto.IdPlataforma,
            plataforma => plataforma.IdPlataforma,
            (joinedTables, plataformasGroup) => new { joinedTables.Producto, joinedTables.Categoria, joinedTables.UnidadMedida, PlataformasGroup = plataformasGroup })
        .SelectMany(joinedTables => joinedTables.PlataformasGroup.DefaultIfEmpty(),
            (joinedTables, plataforma) => new ProductoDtoGet
            {
                IdProducto = joinedTables.Producto.IdProducto,
                IdCategoria = joinedTables.Producto.IdCategoria,
                Categoria = joinedTables.Categoria.Nombre,
                IdPlataforma = joinedTables.Producto.IdPlataforma,
                Plataforma = plataforma!.Nombre, //pude que haya que quitar el signo de exclamacion, debido a que plataforma.nombre puede ser null
                IdUnidadMedida = joinedTables.Producto.IdUnidadMedida,
                UnidadMedida = joinedTables.UnidadMedida.Nombre,
                Nombre = joinedTables.Producto.Nombre,
                Descripcion = joinedTables.Producto.Descripcion,
                Peso = joinedTables.Producto.Peso,
                Precio = joinedTables.Producto.Precio,
                Stock = joinedTables.Producto.Stock,
                Activo = joinedTables.Producto.Activo,
                UsuarioModificacion = joinedTables.Producto.UsuarioModificacion
            })
        .Where(producto => producto.IdProducto == idProducto);

    var result = await query.SingleOrDefaultAsync();

    return result!;
}
    #endregion get
}