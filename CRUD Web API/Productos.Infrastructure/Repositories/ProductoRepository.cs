using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Productos.Domain.Interface;
using Productos.Server.Models;
using System;
using System.Collections.Generic;
using System.Text;
using AppProductoDb = Productos.Infrastructure.Context.ProductosContext;

namespace Productos.Infrastructure.Repositories
{
    public class ProductoRepository : IProductosRepository
    {
        private readonly AppProductoDb db;

        public ProductoRepository(AppProductoDb _db)
        {
            db = _db;
        }

        public async Task AddProducto(Producto producto)
        {
            await db.Productos.AddAsync(producto);
            await db.SaveChangesAsync();
        }

        public async Task EditarProducto(Producto producto)
        {
            var productoExistente = await db.Productos
                .Where(x => x.Id == producto.Id)
                .FirstOrDefaultAsync();

            if (productoExistente is null)
                throw new KeyNotFoundException($"Producto con ID {producto.Id} no encontrado.");

            db.Entry(productoExistente).CurrentValues.SetValues(producto);
            await db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Producto>> GetAll()
        {
            var products = await db.Productos.AsNoTracking().ToListAsync();
            return products;
        }

        public async Task<Producto?> GetProductobyID(int id)
        {
            var producto = await db.Productos
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            return producto;
        }

        public async Task RemoveProducto(Producto producto)
        {
            db.Remove(producto);
            await db.SaveChangesAsync();
        }
    }
}