async function repuestos() {
  const area = document.getElementById('content-area');
  area.innerHTML = `<div class="text-center py-5"><div class="spinner-border text-primary"></div></div>`;
  try {
    const data = await apiFetch('/repuestos');
    area.innerHTML = `
      <div class="d-flex justify-content-between align-items-center mb-3">
        <h4 class="fw-bold mb-0"><i class="bi bi-box-seam me-2"></i>Repuestos</h4>
        <button class="btn btn-tecnofix btn-sm" onclick="repuestoFormModal()"><i class="bi bi-plus-lg me-1"></i>Nuevo Repuesto</button>
      </div>
      <div class="card bg-white shadow-sm p-3">
        <table class="table table-hover table-sm">
          <thead><tr><th>#</th><th>Nombre</th><th>Descripción</th><th>Precio</th><th>Stock</th><th>Acciones</th></tr></thead>
          <tbody>
            ${data.map(r => `<tr>
              <td>${r.id}</td>
              <td>${r.nombre}</td>
              <td>${r.descripcion || '—'}</td>
              <td>$${Number(r.precio).toFixed(2)}</td>
              <td><span class="badge bg-${r.stock > 0 ? 'success' : 'danger'}">${r.stock}</span></td>
              <td>
                <button class="btn btn-sm btn-outline-secondary me-1" onclick="repuestoFormModal(${r.id})"><i class="bi bi-pencil"></i></button>
                <button class="btn btn-sm btn-outline-danger" onclick="eliminarRepuesto(${r.id})"><i class="bi bi-trash"></i></button>
              </td>
            </tr>`).join('')}
          </tbody>
        </table>
      </div>
      <div id="repuesto-modal-area"></div>`;
  } catch(e) {
    area.innerHTML = `<div class="alert alert-danger">${e.message}</div>`;
  }
}

function repuestoFormModal(id = null) {
  const area = document.getElementById('repuesto-modal-area');
  area.innerHTML = `
    <div class="modal fade show d-block" tabindex="-1" style="background:rgba(0,0,0,.4)">
      <div class="modal-dialog"><div class="modal-content">
        <div class="modal-header"><h5 class="modal-title">${id ? 'Editar' : 'Nuevo'} Repuesto</h5>
          <button class="btn-close" onclick="document.getElementById('repuesto-modal-area').innerHTML=''"></button></div>
        <div class="modal-body" id="repuesto-form-body">
          <div class="text-center"><div class="spinner-border text-primary"></div></div>
        </div>
      </div></div>
    </div>`;
  cargarFormRepuesto(id);
}

async function cargarFormRepuesto(id) {
  let r = { id: 0, nombre: '', descripcion: '', precio: 0, stock: 0 };
  if (id) r = await apiFetch(`/repuestos/${id}`);
  document.getElementById('repuesto-form-body').innerHTML = `
    <form onsubmit="guardarRepuesto(event, ${id || 0})">
      <div class="row g-2">
        <div class="col-12"><label class="form-label">Nombre</label><input class="form-control" name="nombre" value="${r.nombre}" required /></div>
        <div class="col-12"><label class="form-label">Descripción</label><textarea class="form-control" name="descripcion" rows="2">${r.descripcion || ''}</textarea></div>
        <div class="col-6"><label class="form-label">Precio ($)</label><input class="form-control" name="precio" type="number" step="0.01" value="${r.precio}" required /></div>
        <div class="col-6"><label class="form-label">Stock</label><input class="form-control" name="stock" type="number" value="${r.stock}" required /></div>
      </div>
      <div class="mt-3 text-end">
        <button type="button" class="btn btn-secondary me-2" onclick="document.getElementById('repuesto-modal-area').innerHTML=''">Cancelar</button>
        <button type="submit" class="btn btn-tecnofix">Guardar</button>
      </div>
    </form>`;
}

async function guardarRepuesto(e, id) {
  e.preventDefault();
  const f = e.target;
  const body = { id, nombre: f.nombre.value, descripcion: f.descripcion.value, precio: parseFloat(f.precio.value), stock: parseInt(f.stock.value) };
  try {
    if (id) await apiFetch(`/repuestos/${id}`, 'PUT', body);
    else await apiFetch('/repuestos', 'POST', body);
    showAlert(`Repuesto ${id ? 'actualizado' : 'creado'} correctamente.`);
    repuestos();
  } catch(e) { showAlert(e.message, 'danger'); }
}

async function eliminarRepuesto(id) {
  if (!confirm('¿Eliminar este repuesto?')) return;
  try {
    await apiFetch(`/repuestos/${id}`, 'DELETE');
    showAlert('Repuesto eliminado.');
    repuestos();
  } catch(e) { showAlert(e.message, 'danger'); }
}
