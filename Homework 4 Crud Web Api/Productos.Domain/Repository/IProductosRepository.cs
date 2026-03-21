using System;
using System.Collections.Generic;
using System.Text;
using Productos.Domain;
using Productos.Server.Models;


namespace Productos.Domain.Interface
{

        public interface IProductosRepository {
            public Task<IEnumerable<Producto>> GetAll();
            public Task<Producto> GetProductobyID(int Id);
            public Task AddProducto(Producto producto);
            public Task EditarProducto(Producto producto);
            public Task RemoveProducto(Producto producto);
        }
}

