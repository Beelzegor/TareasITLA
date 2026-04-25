async function tecnicos() {
  const area = document.getElementById('content-area');
  area.innerHTML = `<div class="text-center py-5"><div class="spinner-border text-primary"></div></div>`;
  try {
    const data = await apiFetch('/tecnicos');
    area.innerHTML = `
      <div class="d-flex justify-content-between align-items-center mb-3">
        <h4 class="fw-bold mb-0"><i class="bi bi-person-badge me-2"></i>Técnicos</h4>
        <button class="btn btn-tecnofix btn-sm" onclick="tecnicoFormModal()"><i class="bi bi-plus-lg me-1"></i>Nuevo Técnico</button>
      </div>
      <div class="card bg-white shadow-sm p-3">
        <table class="table table-hover table-sm">
          <thead><tr><th>#</th><th>Nombre</th><th>Especialidad</th><th>Teléfono</th><th>Acciones</th></tr></thead>
          <tbody>
            ${data.map(t => `<tr>
              <td>${t.id}</td>
              <td>${t.nombre} ${t.apellido}</td>
              <td>${t.especialidad}</td>
              <td>${t.telefono}</td>
              <td>
                <button class="btn btn-sm btn-outline-secondary me-1" onclick="tecnicoFormModal(${t.id})"><i class="bi bi-pencil"></i></button>
                <button class="btn btn-sm btn-outline-danger" onclick="eliminarTecnico(${t.id})"><i class="bi bi-trash"></i></button>
              </td>
            </tr>`).join('')}
          </tbody>
        </table>
      </div>
      <div id="tecnico-modal-area"></div>`;
  } catch(e) {
    area.innerHTML = `<div class="alert alert-danger">${e.message}</div>`;
  }
}

function tecnicoFormModal(id = null) {
  const area = document.getElementById('tecnico-modal-area');
  area.innerHTML = `
    <div class="modal fade show d-block" tabindex="-1" style="background:rgba(0,0,0,.4)">
      <div class="modal-dialog"><div class="modal-content">
        <div class="modal-header"><h5 class="modal-title">${id ? 'Editar' : 'Nuevo'} Técnico</h5>
          <button class="btn-close" onclick="document.getElementById('tecnico-modal-area').innerHTML=''"></button></div>
        <div class="modal-body" id="tecnico-form-body">
          <div class="text-center"><div class="spinner-border text-primary"></div></div>
        </div>
      </div></div>
    </div>`;
  cargarFormTecnico(id);
}

async function cargarFormTecnico(id) {
  let t = { id: 0, nombre: '', apellido: '', especialidad: '', telefono: '' };
  if (id) t = await apiFetch(`/tecnicos/${id}`);
  document.getElementById('tecnico-form-body').innerHTML = `
    <form onsubmit="guardarTecnico(event, ${id || 0})">
      <div class="row g-2">
        <div class="col-6"><label class="form-label">Nombre</label><input class="form-control" name="nombre" value="${t.nombre}" required /></div>
        <div class="col-6"><label class="form-label">Apellido</label><input class="form-control" name="apellido" value="${t.apellido}" required /></div>
        <div class="col-6"><label class="form-label">Especialidad</label><input class="form-control" name="especialidad" value="${t.especialidad}" required /></div>
        <div class="col-6"><label class="form-label">Teléfono</label><input class="form-control" name="telefono" value="${t.telefono}" required /></div>
      </div>
      <div class="mt-3 text-end">
        <button type="button" class="btn btn-secondary me-2" onclick="document.getElementById('tecnico-modal-area').innerHTML=''">Cancelar</button>
        <button type="submit" class="btn btn-tecnofix">Guardar</button>
      </div>
    </form>`;
}

async function guardarTecnico(e, id) {
  e.preventDefault();
  const f = e.target;
  const body = { id, nombre: f.nombre.value, apellido: f.apellido.value, especialidad: f.especialidad.value, telefono: f.telefono.value };
  try {
    if (id) await apiFetch(`/tecnicos/${id}`, 'PUT', body);
    else await apiFetch('/tecnicos', 'POST', body);
    showAlert(`Técnico ${id ? 'actualizado' : 'creado'} correctamente.`);
    tecnicos();
  } catch(e) { showAlert(e.message, 'danger'); }
}

async function eliminarTecnico(id) {
  if (!confirm('¿Eliminar este técnico?')) return;
  try {
    await apiFetch(`/tecnicos/${id}`, 'DELETE');
    showAlert('Técnico eliminado.');
    tecnicos();
  } catch(e) { showAlert(e.message, 'danger'); }
}
