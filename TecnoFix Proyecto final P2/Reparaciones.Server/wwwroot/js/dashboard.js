async function dashboard() {
  const area = document.getElementById('content-area');
  area.innerHTML = `<div class="text-center py-5"><div class="spinner-border" style="color:#F97316"></div></div>`;
  try {
    const [reps, clientes, tecnicos, repuestos] = await Promise.all([
      apiFetch('/reparaciones'),
      apiFetch('/clientes'),
      apiFetch('/tecnicos'),
      apiFetch('/repuestos'),
    ]);
    const pendientes = reps.filter(r => r.estado !== 'Entregado').length;
    const entregados = reps.filter(r => r.estado === 'Entregado').length;

    area.innerHTML = `
      <div class="page-title">Dashboard</div>
      <div class="page-subtitle">Resumen general del sistema TecnoFix</div>

      <div class="row g-3 mb-4">
        <div class="col-md-3">
          <div class="stat-card">
            <div class="stat-icon orange"><i class="bi bi-wrench-adjustable"></i></div>
            <div class="stat-value">${reps.length}</div>
            <div class="stat-label">Total Reparaciones</div>
          </div>
        </div>
        <div class="col-md-3">
          <div class="stat-card">
            <div class="stat-icon blue"><i class="bi bi-hourglass-split"></i></div>
            <div class="stat-value">${pendientes}</div>
            <div class="stat-label">Pendientes</div>
          </div>
        </div>
        <div class="col-md-3">
          <div class="stat-card">
            <div class="stat-icon green"><i class="bi bi-people"></i></div>
            <div class="stat-value">${clientes.length}</div>
            <div class="stat-label">Clientes</div>
          </div>
        </div>
        <div class="col-md-3">
          <div class="stat-card">
            <div class="stat-icon purple"><i class="bi bi-person-badge"></i></div>
            <div class="stat-value">${tecnicos.length}</div>
            <div class="stat-label">Técnicos</div>
          </div>
        </div>
      </div>

      <div class="content-card">
        <div class="content-card-title"><i class="bi bi-clock-history me-2" style="color:#F97316"></i>Últimas Reparaciones</div>
        <table class="table">
          <thead>
            <tr><th>#</th><th>Cliente</th><th>Equipo</th><th>Técnico</th><th>Estado</th><th>Recepción</th></tr>
          </thead>
          <tbody>
            ${reps.length === 0
              ? `<tr><td colspan="6" class="text-center text-muted py-4">No hay reparaciones registradas aún.</td></tr>`
              : reps.slice(-8).reverse().map(r => `<tr>
                <td><span class="fw-semibold">#${r.id}</span></td>
                <td>${r.cliente ? r.cliente.nombre + ' ' + r.cliente.apellido : '—'}</td>
                <td>${r.electrodomestico ? r.electrodomestico.marca + ' ' + r.electrodomestico.modelo : '—'}</td>
                <td>${r.tecnico ? r.tecnico.nombre + ' ' + r.tecnico.apellido : '—'}</td>
                <td>${estadoBadge(r.estado)}</td>
                <td>${new Date(r.fechaRecepcion).toLocaleDateString('es-DO')}</td>
              </tr>`).join('')}
          </tbody>
        </table>
      </div>`;
  } catch(e) {
    area.innerHTML = `<div class="alert alert-danger">Error cargando el dashboard: ${e.message}</div>`;
  }
}
