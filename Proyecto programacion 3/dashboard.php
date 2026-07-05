<?php
require 'includes/conexion.php';
require 'includes/auth.php';
requireLogin();

$total = $pdo->query('SELECT COUNT(*) FROM solicitudes')->fetchColumn();
$pendientes = $pdo->query("SELECT COUNT(*) FROM solicitudes WHERE estado = 'Pendiente'")->fetchColumn();
$enProceso = $pdo->query("SELECT COUNT(*) FROM solicitudes WHERE estado = 'En proceso'")->fetchColumn();
$completadas = $pdo->query("SELECT COUNT(*) FROM solicitudes WHERE estado = 'Completado'")->fetchColumn();

$recientes = $pdo->query('SELECT id, tipo, origen, destino, estado, fecha FROM solicitudes ORDER BY id DESC LIMIT 5')->fetchAll();

function badgeEstado($estado) {
    $clases = [
        'Pendiente'  => 'warning',
        'En proceso' => 'primary',
        'Completado' => 'success',
        'Cancelado'  => 'danger',
    ];
    $clase = $clases[$estado] ?? 'secondary';
    return '<span class="badge bg-' . $clase . '">' . htmlspecialchars($estado) . '</span>';
}

include 'includes/header.php';
?>

<h2 class="mb-4">Dashboard</h2>

<div class="row g-3 mb-4">
    <div class="col-sm-6 col-lg-3">
        <div class="card text-center shadow-sm h-100">
            <div class="card-body">
                <i class="bi bi-list-check fs-2 text-primary"></i>
                <h3 class="mt-2 mb-0"><?= (int) $total ?></h3>
                <small class="text-muted">Total solicitudes</small>
            </div>
        </div>
    </div>
    <div class="col-sm-6 col-lg-3">
        <div class="card text-center shadow-sm h-100">
            <div class="card-body">
                <i class="bi bi-hourglass-split fs-2 text-warning"></i>
                <h3 class="mt-2 mb-0"><?= (int) $pendientes ?></h3>
                <small class="text-muted">Pendientes</small>
            </div>
        </div>
    </div>
    <div class="col-sm-6 col-lg-3">
        <div class="card text-center shadow-sm h-100">
            <div class="card-body">
                <i class="bi bi-arrow-repeat fs-2 text-primary"></i>
                <h3 class="mt-2 mb-0"><?= (int) $enProceso ?></h3>
                <small class="text-muted">En proceso</small>
            </div>
        </div>
    </div>
    <div class="col-sm-6 col-lg-3">
        <div class="card text-center shadow-sm h-100">
            <div class="card-body">
                <i class="bi bi-check-circle fs-2 text-success"></i>
                <h3 class="mt-2 mb-0"><?= (int) $completadas ?></h3>
                <small class="text-muted">Completadas</small>
            </div>
        </div>
    </div>
</div>

<div class="card shadow-sm">
    <div class="card-header d-flex justify-content-between align-items-center">
        <span>Solicitudes recientes</span>
        <a href="solicitudes.php" class="btn btn-sm btn-outline-primary">Ver todas</a>
    </div>
    <div class="card-body p-0">
        <div class="table-responsive">
            <table class="table table-hover align-middle mb-0">
                <thead class="table-light">
                    <tr>
                        <th>ID</th>
                        <th>Tipo</th>
                        <th>Origen</th>
                        <th>Destino</th>
                        <th>Estado</th>
                        <th>Fecha</th>
                    </tr>
                </thead>
                <tbody>
                    <?php if (empty($recientes)): ?>
                        <tr><td colspan="6" class="text-center text-muted py-4">Aun no hay solicitudes registradas.</td></tr>
                    <?php else: ?>
                        <?php foreach ($recientes as $s): ?>
                            <tr>
                                <td>#<?= (int) $s['id'] ?></td>
                                <td><?= htmlspecialchars($s['tipo']) ?></td>
                                <td><?= htmlspecialchars($s['origen']) ?></td>
                                <td><?= htmlspecialchars($s['destino']) ?></td>
                                <td><?= badgeEstado($s['estado']) ?></td>
                                <td><?= htmlspecialchars($s['fecha']) ?></td>
                            </tr>
                        <?php endforeach; ?>
                    <?php endif; ?>
                </tbody>
            </table>
        </div>
    </div>
</div>

<?php include 'includes/footer.php'; ?>
