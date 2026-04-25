const API = '/api';

async function apiFetch(url, method = 'GET', body = null) {
  const opts = { method, headers: { 'Content-Type': 'application/json' } };
  if (body) opts.body = JSON.stringify(body);
  const res = await fetch(API + url, opts);
  if (!res.ok) {
    const msg = await res.text();
    throw new Error(msg || res.statusText);
  }
  if (res.status === 204 || res.headers.get('content-length') === '0') return null;
  return res.json();
}

function showAlert(msg, type = 'success') {
  const area = document.getElementById('content-area');
  const div = document.createElement('div');
  div.className = `alert alert-${type} alert-dismissible fade show mt-2`;
  div.innerHTML = `${msg}<button type="button" class="btn-close" data-bs-dismiss="alert"></button>`;
  area.prepend(div);
  setTimeout(() => div.remove(), 4000);
}

function estadoBadge(estado) {
  const map = {
    'Recibido':       'background:#E2E8F0;color:#475569',
    'En Diagnóstico': 'background:#DBEAFE;color:#1D4ED8',
    'En Reparación':  'background:#FEF3C7;color:#B45309',
    'Listo':          'background:#D1FAE5;color:#065F46',
    'Entregado':      'background:#DCFCE7;color:#166534',
  };
  const style = map[estado] || 'background:#F1F5F9;color:#334155';
  return `<span class="badge-estado" style="${style}">${estado}</span>`;
}
