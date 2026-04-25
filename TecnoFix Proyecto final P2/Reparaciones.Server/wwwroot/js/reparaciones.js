async function reparaciones() {
  const area = document.getElementById('content-area');
  area.innerHTML = `<div class="text-center py-5"><div class="spinner-border text-primary"></div></div>`;
  try {
    const data = await apiFetch('/reparaciones');
    area.innerHTML = `
      <div class="d-flex justify-content-between align-items-center mb-3">
        <h4 class="fw-bold mb-0"><i class="bi bi-wrench-adjustable me-2"></i>Reparaciones</h4>
        <button class="btn btn-tecnofix btn-sm" onclick="reparacionFormModal()"><i class="bi bi-plus-lg me-1"></i>Nueva Reparación</button>
      </div>
      <div class="card bg-white shadow-sm p-3">
        <table class="table table-hover table-sm">
          <thead><tr><th>#</th><th>Cliente</th><th>Equipo</th><th>Técnico</th><th>Estado</th><th>Recepción</th><th>Acciones</th></tr></thead>
          <tbody>
            ${data.map(r => `<tr>
              <td>${r.id}</td>
              <td>${r.cliente ? r.cliente.nombre + ' ' + r.cliente.apellido : r.clienteId}</td>
              <td>${r.electrodomestico ? r.electrodomestico.marca + ' ' + r.electrodomestico.modelo : r.electrodomesticoId}</td>
              <td>${r.tecnico ? r.tecnico.nombre + ' ' + r.tecnico.apellido : r.tecnicoId}</td>
              <td>${estadoBadge(r.estado)}</td>
              <td>${new Date(r.fechaRecepcion).toLocaleDateString('es-DO')}</td>
              <td>
                <button class="btn btn-sm btn-outline-info me-1" onclick="verReparacion(${r.id})" title="Ver detalle"><i class="bi bi-eye"></i></button>
                <button class="btn btn-sm btn-outline-secondary me-1" onclick="reparacionFormModal(${r.id})" title="Editar"><i class="bi bi-pencil"></i></button>
                <button class="btn btn-sm btn-outline-danger" onclick="eliminarReparacion(${r.id})" title="Eliminar"><i class="bi bi-trash"></i></button>
              </td>
            </tr>`).join('')}
          </tbody>
        </table>
      </div>
      <div id="rep-modal-area"></div>`;
  } catch(e) {
    area.innerHTML = `<div class="alert alert-danger">${e.message}</div>`;
  }
}

async function reparacionFormModal(id = null) {
  const area = document.getElementById('rep-modal-area');
  area.innerHTML = `
    <div class="modal fade show d-block" tabindex="-1" style="background:rgba(0,0,0,.4)">
      <div class="modal-dialog modal-lg"><div class="modal-content">
        <div class="modal-header"><h5 class="modal-title">${id ? 'Editar' : 'Nueva'} Reparación</h5>
          <button class="btn-close" onclick="document.getElementById('rep-modal-area').innerHTML=''"></button></div>
        <div class="modal-body" id="rep-form-body">
          <div class="text-center"><div class="spinner-border text-primary"></div></div>
        </div>
      </div></div>
    </div>`;
  const [clientes, tecnicos, electros] = await Promise.all([
    apiFetch('/clientes'), apiFetch('/tecnicos'), apiFetch('/electrodomesticos')
  ]);
  let r = { id: 0, clienteId: '', tecnicoId: '', electrodomesticoId: '', diagnostico: '', estado: 'Recibido', costoManoObra: 0, observaciones: '' };
  if (id) r = await apiFetch(`/reparaciones/${id}`);

  document.getElementById('rep-form-body').innerHTML = `
    <form onsubmit="guardarReparacion(event, ${id || 0})">
      <div class="row g-2">
        <div class="col-6">
          <label class="form-label">Cliente</label>
          <select class="form-select" name="clienteId" required>
            <option value="">Seleccionar...</option>
            ${clientes.map(c => `<option value="${c.id}" ${r.clienteId==c.id?'selected':''}>${c.nombre} ${c.apellido}</option>`).join('')}
          </select>
        </div>
        <div class="col-6">
          <label class="form-label">Técnico</label>
          <select class="form-select" name="tecnicoId" required>
            <option value="">Seleccionar...</option>
            ${tecnicos.map(t => `<option value="${t.id}" ${r.tecnicoId==t.id?'selected':''}>${t.nombre} ${t.apellido}</option>`).join('')}
          </select>
        </div>
        <div class="col-6">
          <label class="form-label">Electrodoméstico</label>
          <select class="form-select" name="electrodomesticoId" required>
            <option value="">Seleccionar...</option>
            ${electros.map(e => `<option value="${e.id}" ${r.electrodomesticoId==e.id?'selected':''}>${e.marca} ${e.modelo} (${e.tipo})</option>`).join('')}
          </select>
        </div>
        <div class="col-6">
          <label class="form-label">Estado</label>
          <select class="form-select" name="estado">
            ${['Recibido','En Diagnóstico','En Reparación','Listo','Entregado'].map(s => `<option ${r.estado===s?'selected':''}>${s}</option>`).join('')}
          </select>
        </div>
        <div class="col-12"><label class="form-label">Diagnóstico</label><textarea class="form-control" name="diagnostico" rows="2">${r.diagnostico || ''}</textarea></div>
        <div class="col-6"><label class="form-label">Costo Mano de Obra ($)</label><input class="form-control" name="costoManoObra" type="number" step="0.01" value="${r.costoManoObra}" /></div>
        <div class="col-6"><label class="form-label">Observaciones</label><input class="form-control" name="observaciones" value="${r.observaciones || ''}" /></div>
      </div>
      <div class="mt-3 text-end">
        <button type="button" class="btn btn-secondary me-2" onclick="document.getElementById('rep-modal-area').innerHTML=''">Cancelar</button>
        <button type="submit" class="btn btn-tecnofix">Guardar</button>
      </div>
    </form>`;
}

async function guardarReparacion(e, id) {
  e.preventDefault();
  const f = e.target;
  const body = {
    id,
    clienteId: parseInt(f.clienteId.value),
    tecnicoId: parseInt(f.tecnicoId.value),
    electrodomesticoId: parseInt(f.electrodomesticoId.value),
    diagnostico: f.diagnostico.value,
    estado: f.estado.value,
    costoManoObra: parseFloat(f.costoManoObra.value),
    observaciones: f.observaciones.value
  };
  try {
    if (id) await apiFetch(`/reparaciones/${id}`, 'PUT', body);
    else await apiFetch('/reparaciones', 'POST', body);
    showAlert(`Reparación ${id ? 'actualizada' : 'creada'} correctamente.`);
    reparaciones();
  } catch(e) { showAlert(e.message, 'danger'); }
}

async function verReparacion(id) {
  const area = document.getElementById('rep-modal-area');
  area.innerHTML = `
    <div class="modal fade show d-block" tabindex="-1" style="background:rgba(0,0,0,.4)">
      <div class="modal-dialog modal-lg"><div class="modal-content">
        <div class="modal-header"><h5 class="modal-title">Detalle Reparación #${id}</h5>
          <button class="btn-close" onclick="document.getElementById('rep-modal-area').innerHTML=''"></button></div>
        <div class="modal-body" id="rep-detail-body">
          <div class="text-center"><div class="spinner-border text-primary"></div></div>
        </div>
      </div></div>
    </div>`;
  const r = await apiFetch(`/reparaciones/${id}`);
  document.getElementById('rep-detail-body').innerHTML = `
    <dl class="row">
      <dt class="col-4">Cliente</dt><dd class="col-8">${r.cliente ? r.cliente.nombre + ' ' + r.cliente.apellido : '—'}</dd>
      <dt class="col-4">Técnico</dt><dd class="col-8">${r.tecnico ? r.tecnico.nombre + ' ' + r.tecnico.apellido : '—'}</dd>
      <dt class="col-4">Equipo</dt><dd class="col-8">${r.electrodomestico ? r.electrodomestico.marca + ' ' + r.electrodomestico.modelo : '—'}</dd>
      <dt class="col-4">Estado</dt><dd class="col-8">${estadoBadge(r.estado)}</dd>
      <dt class="col-4">Diagnóstico</dt><dd class="col-8">${r.diagnostico || '—'}</dd>
      <dt class="col-4">Costo M.O.</dt><dd class="col-8">$${Number(r.costoManoObra).toFixed(2)}</dd>
      <dt class="col-4">Observaciones</dt><dd class="col-8">${r.observaciones || '—'}</dd>
      <dt class="col-4">Recepción</dt><dd class="col-8">${new Date(r.fechaRecepcion).toLocaleDateString('es-DO')}</dd>
      <dt class="col-4">Entrega</dt><dd class="col-8">${r.fechaEntrega ? new Date(r.fechaEntrega).toLocaleDateString('es-DO') : 'Pendiente'}</dd>
      <dt class="col-4">Garantía</dt><dd class="col-8">${r.garantia ? `Vigente hasta ${new Date(r.garantia.fechaFin).toLocaleDateString('es-DO')}` : 'Sin garantía'}</dd>
    </dl>`;
}

async function eliminarReparacion(id) {
  if (!confirm('¿Eliminar esta reparación?')) return;
  try {
    await apiFetch(`/reparaciones/${id}`, 'DELETE');
    showAlert('Reparación eliminada.');
    reparaciones();
  } catch(e) { showAlert(e.message, 'danger'); }
}
