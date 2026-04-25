async function electrodomesticos() {
  const area = document.getElementById('content-area');
  area.innerHTML = `<div class="text-center py-5"><div class="spinner-border text-primary"></div></div>`;
  try {
    const data = await apiFetch('/electrodomesticos');
    area.innerHTML = `
      <div class="d-flex justify-content-between align-items-center mb-3">
        <h4 class="fw-bold mb-0"><i class="bi bi-tv me-2"></i>Electrodomésticos</h4>
        <button class="btn btn-tecnofix btn-sm" onclick="electroFormModal()"><i class="bi bi-plus-lg me-1"></i>Nuevo</button>
      </div>
      <div class="card bg-white shadow-sm p-3">
        <table class="table table-hover table-sm">
          <thead><tr><th>#</th><th>Marca</th><th>Modelo</th><th>Tipo</th><th>N° Serie</th><th>Acciones</th></tr></thead>
          <tbody>
            ${data.map(e => `<tr>
              <td>${e.id}</td>
              <td>${e.marca}</td>
              <td>${e.modelo}</td>
              <td>${e.tipo}</td>
              <td>${e.numeroSerie || '—'}</td>
              <td>
                <button class="btn btn-sm btn-outline-secondary me-1" onclick="electroFormModal(${e.id})"><i class="bi bi-pencil"></i></button>
                <button class="btn btn-sm btn-outline-danger" onclick="eliminarElectro(${e.id})"><i class="bi bi-trash"></i></button>
              </td>
            </tr>`).join('')}
          </tbody>
        </table>
      </div>
      <div id="electro-modal-area"></div>`;
  } catch(e) {
    area.innerHTML = `<div class="alert alert-danger">${e.message}</div>`;
  }
}

function electroFormModal(id = null) {
  const area = document.getElementById('electro-modal-area');
  area.innerHTML = `
    <div class="modal fade show d-block" tabindex="-1" style="background:rgba(0,0,0,.4)">
      <div class="modal-dialog"><div class="modal-content">
        <div class="modal-header"><h5 class="modal-title">${id ? 'Editar' : 'Nuevo'} Electrodoméstico</h5>
          <button class="btn-close" onclick="document.getElementById('electro-modal-area').innerHTML=''"></button></div>
        <div class="modal-body" id="electro-form-body">
          <div class="text-center"><div class="spinner-border text-primary"></div></div>
        </div>
      </div></div>
    </div>`;
  cargarFormElectro(id);
}

async function cargarFormElectro(id) {
  let e = { id: 0, marca: '', modelo: '', tipo: '', numeroSerie: '' };
  if (id) e = await apiFetch(`/electrodomesticos/${id}`);
  document.getElementById('electro-form-body').innerHTML = `
    <form onsubmit="guardarElectro(event, ${id || 0})">
      <div class="row g-2">
        <div class="col-6"><label class="form-label">Marca</label><input class="form-control" name="marca" value="${e.marca}" required /></div>
        <div class="col-6"><label class="form-label">Modelo</label><input class="form-control" name="modelo" value="${e.modelo}" required /></div>
        <div class="col-6">
          <label class="form-label">Tipo</label>
          <select class="form-select" name="tipo" required>
            ${['Nevera','Lavadora','Secadora','Aire Acondicionado','Microondas','Televisor','Computadora','Otro'].map(t => `<option ${e.tipo===t?'selected':''}>${t}</option>`).join('')}
          </select>
        </div>
        <div class="col-6"><label class="form-label">N° Serie</label><input class="form-control" name="numeroSerie" value="${e.numeroSerie || ''}" /></div>
      </div>
      <div class="mt-3 text-end">
        <button type="button" class="btn btn-secondary me-2" onclick="document.getElementById('electro-modal-area').innerHTML=''">Cancelar</button>
        <button type="submit" class="btn btn-tecnofix">Guardar</button>
      </div>
    </form>`;
}

async function guardarElectro(ev, id) {
  ev.preventDefault();
  const f = ev.target;
  const body = { id, marca: f.marca.value, modelo: f.modelo.value, tipo: f.tipo.value, numeroSerie: f.numeroSerie.value };
  try {
    if (id) await apiFetch(`/electrodomesticos/${id}`, 'PUT', body);
    else await apiFetch('/electrodomesticos', 'POST', body);
    showAlert(`Electrodoméstico ${id ? 'actualizado' : 'creado'} correctamente.`);
    electrodomesticos();
  } catch(e) { showAlert(e.message, 'danger'); }
}

async function eliminarElectro(id) {
  if (!confirm('¿Eliminar este electrodoméstico?')) return;
  try {
    await apiFetch(`/electrodomesticos/${id}`, 'DELETE');
    showAlert('Electrodoméstico eliminado.');
    electrodomesticos();
  } catch(e) { showAlert(e.message, 'danger'); }
}
