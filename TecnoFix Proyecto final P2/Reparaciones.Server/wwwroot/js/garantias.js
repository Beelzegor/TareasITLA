async function garantias() {
  const area = document.getElementById('content-area');
  area.innerHTML = `<div class="text-center py-5"><div class="spinner-border text-primary"></div></div>`;
  try {
    const data = await apiFetch('/garantias');
    area.innerHTML = `
      <div class="d-flex justify-content-between align-items-center mb-3">
        <h4 class="fw-bold mb-0"><i class="bi bi-shield-check me-2"></i>Garantías</h4>
        <button class="btn btn-tecnofix btn-sm" onclick="garantiaFormModal()"><i class="bi bi-plus-lg me-1"></i>Nueva Garantía</button>
      </div>
      <div class="card bg-white shadow-sm p-3">
        <table class="table table-hover table-sm">
          <thead><tr><th>#</th><th>Reparación</th><th>Inicio</th><th>Fin</th><th>Estado</th><th>Descripción</th><th>Acciones</th></tr></thead>
          <tbody>
            ${data.map(g => {
              const vigente = new Date(g.fechaFin) >= new Date();
              return `<tr>
                <td>${g.id}</td>
                <td>Reparación #${g.reparacionId}</td>
                <td>${new Date(g.fechaInicio).toLocaleDateString('es-DO')}</td>
                <td>${new Date(g.fechaFin).toLocaleDateString('es-DO')}</td>
                <td><span class="badge bg-${vigente ? 'success' : 'secondary'}">${vigente ? 'Vigente' : 'Vencida'}</span></td>
                <td>${g.descripcion || '—'}</td>
                <td>
                  <button class="btn btn-sm btn-outline-secondary me-1" onclick="garantiaFormModal(${g.id})"><i class="bi bi-pencil"></i></button>
                  <button class="btn btn-sm btn-outline-danger" onclick="eliminarGarantia(${g.id})"><i class="bi bi-trash"></i></button>
                </td>
              </tr>`;
            }).join('')}
          </tbody>
        </table>
      </div>
      <div id="garantia-modal-area"></div>`;
  } catch(e) {
    area.innerHTML = `<div class="alert alert-danger">${e.message}</div>`;
  }
}

function garantiaFormModal(id = null) {
  const area = document.getElementById('garantia-modal-area');
  area.innerHTML = `
    <div class="modal fade show d-block" tabindex="-1" style="background:rgba(0,0,0,.4)">
      <div class="modal-dialog"><div class="modal-content">
        <div class="modal-header"><h5 class="modal-title">${id ? 'Editar' : 'Nueva'} Garantía</h5>
          <button class="btn-close" onclick="document.getElementById('garantia-modal-area').innerHTML=''"></button></div>
        <div class="modal-body" id="garantia-form-body">
          <div class="text-center"><div class="spinner-border text-primary"></div></div>
        </div>
      </div></div>
    </div>`;
  cargarFormGarantia(id);
}

async function cargarFormGarantia(id) {
  let g = { id: 0, reparacionId: '', fechaInicio: '', fechaFin: '', descripcion: '' };
  const reps = await apiFetch('/reparaciones');
  if (id) g = await apiFetch(`/garantias/${id}`);
  const fi = g.fechaInicio ? g.fechaInicio.substring(0, 10) : '';
  const ff = g.fechaFin ? g.fechaFin.substring(0, 10) : '';
  document.getElementById('garantia-form-body').innerHTML = `
    <form onsubmit="guardarGarantia(event, ${id || 0})">
      <div class="row g-2">
        <div class="col-12">
          <label class="form-label">Reparación</label>
          <select class="form-select" name="reparacionId" required>
            <option value="">Seleccionar...</option>
            ${reps.map(r => `<option value="${r.id}" ${g.reparacionId==r.id?'selected':''}>Reparación #${r.id} — ${r.cliente ? r.cliente.nombre+' '+r.cliente.apellido : ''}</option>`).join('')}
          </select>
        </div>
        <div class="col-6"><label class="form-label">Fecha Inicio</label><input class="form-control" name="fechaInicio" type="date" value="${fi}" required /></div>
        <div class="col-6"><label class="form-label">Fecha Fin</label><input class="form-control" name="fechaFin" type="date" value="${ff}" required /></div>
        <div class="col-12"><label class="form-label">Descripción</label><textarea class="form-control" name="descripcion" rows="2">${g.descripcion || ''}</textarea></div>
      </div>
      <div class="mt-3 text-end">
        <button type="button" class="btn btn-secondary me-2" onclick="document.getElementById('garantia-modal-area').innerHTML=''">Cancelar</button>
        <button type="submit" class="btn btn-tecnofix">Guardar</button>
      </div>
    </form>`;
}

async function guardarGarantia(e, id) {
  e.preventDefault();
  const f = e.target;
  const body = { id, reparacionId: parseInt(f.reparacionId.value), fechaInicio: f.fechaInicio.value, fechaFin: f.fechaFin.value, descripcion: f.descripcion.value };
  try {
    if (id) await apiFetch(`/garantias/${id}`, 'PUT', body);
    else await apiFetch('/garantias', 'POST', body);
    showAlert(`Garantía ${id ? 'actualizada' : 'creada'} correctamente.`);
    garantias();
  } catch(e) { showAlert(e.message, 'danger'); }
}

async function eliminarGarantia(id) {
  if (!confirm('¿Eliminar esta garantía?')) return;
  try {
    await apiFetch(`/garantias/${id}`, 'DELETE');
    showAlert('Garantía eliminada.');
    garantias();
  } catch(e) { showAlert(e.message, 'danger'); }
}
