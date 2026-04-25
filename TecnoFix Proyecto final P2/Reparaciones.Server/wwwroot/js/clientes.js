async function clientes() {
  const area = document.getElementById('content-area');
  area.innerHTML = `<div class="text-center py-5"><div class="spinner-border text-primary"></div></div>`;
  try {
    const data = await apiFetch('/clientes');
    area.innerHTML = `
      <div class="d-flex justify-content-between align-items-center mb-3">
        <h4 class="fw-bold mb-0"><i class="bi bi-people me-2"></i>Clientes</h4>
        <button class="btn btn-tecnofix btn-sm" onclick="clienteFormModal()"><i class="bi bi-plus-lg me-1"></i>Nuevo Cliente</button>
      </div>
      <div class="card bg-white shadow-sm p-3">
        <table class="table table-hover table-sm">
          <thead><tr><th>#</th><th>Nombre</th><th>Teléfono</th><th>Email</th><th>Dirección</th><th>Acciones</th></tr></thead>
          <tbody id="clientes-tbody">
            ${data.map(c => clienteRow(c)).join('')}
          </tbody>
        </table>
      </div>
      <div id="cliente-modal-area"></div>`;
  } catch(e) {
    area.innerHTML = `<div class="alert alert-danger">${e.message}</div>`;
  }
}

function clienteRow(c) {
  return `<tr>
    <td>${c.id}</td>
    <td>${c.nombre} ${c.apellido}</td>
    <td>${c.telefono}</td>
    <td>${c.email || '—'}</td>
    <td>${c.direccion || '—'}</td>
    <td>
      <button class="btn btn-sm btn-outline-secondary me-1" onclick="clienteFormModal(${c.id})"><i class="bi bi-pencil"></i></button>
      <button class="btn btn-sm btn-outline-danger" onclick="eliminarCliente(${c.id})"><i class="bi bi-trash"></i></button>
    </td>
  </tr>`;
}

function clienteFormModal(id = null) {
  const area = document.getElementById('cliente-modal-area');
  area.innerHTML = `
    <div class="modal fade show d-block" tabindex="-1" style="background:rgba(0,0,0,.4)">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header"><h5 class="modal-title">${id ? 'Editar' : 'Nuevo'} Cliente</h5>
            <button class="btn-close" onclick="document.getElementById('cliente-modal-area').innerHTML=''"></button></div>
          <div class="modal-body" id="cliente-form-body">
            <div class="text-center"><div class="spinner-border text-primary"></div></div>
          </div>
        </div>
      </div>
    </div>`;
  cargarFormCliente(id);
}

async function cargarFormCliente(id) {
  let c = { id: 0, nombre: '', apellido: '', telefono: '', email: '', direccion: '' };
  if (id) c = await apiFetch(`/clientes/${id}`);
  document.getElementById('cliente-form-body').innerHTML = `
    <form onsubmit="guardarCliente(event, ${id || 0})">
      <div class="row g-2">
        <div class="col-6"><label class="form-label">Nombre</label><input class="form-control" name="nombre" value="${c.nombre}" required /></div>
        <div class="col-6"><label class="form-label">Apellido</label><input class="form-control" name="apellido" value="${c.apellido}" required /></div>
        <div class="col-6"><label class="form-label">Teléfono</label><input class="form-control" name="telefono" value="${c.telefono}" required /></div>
        <div class="col-6"><label class="form-label">Email</label><input class="form-control" name="email" value="${c.email || ''}" /></div>
        <div class="col-12"><label class="form-label">Dirección</label><input class="form-control" name="direccion" value="${c.direccion || ''}" /></div>
      </div>
      <div class="mt-3 text-end">
        <button type="button" class="btn btn-secondary me-2" onclick="document.getElementById('cliente-modal-area').innerHTML=''">Cancelar</button>
        <button type="submit" class="btn btn-tecnofix">Guardar</button>
      </div>
    </form>`;
}

async function guardarCliente(e, id) {
  e.preventDefault();
  const f = e.target;
  const body = { id, nombre: f.nombre.value, apellido: f.apellido.value, telefono: f.telefono.value, email: f.email.value, direccion: f.direccion.value };
  try {
    if (id) await apiFetch(`/clientes/${id}`, 'PUT', body);
    else await apiFetch('/clientes', 'POST', body);
    showAlert(`Cliente ${id ? 'actualizado' : 'creado'} correctamente.`);
    clientes();
  } catch(e) { showAlert(e.message, 'danger'); }
}

async function eliminarCliente(id) {
  if (!confirm('¿Eliminar este cliente?')) return;
  try {
    await apiFetch(`/clientes/${id}`, 'DELETE');
    showAlert('Cliente eliminado.');
    clientes();
  } catch(e) { showAlert(e.message, 'danger'); }
}
