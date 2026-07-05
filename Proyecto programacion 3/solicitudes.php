<?php
require 'includes/conexion.php';
require 'includes/auth.php';
requireLogin();

$estadosDisponibles = ['Pendiente', 'En proceso', 'Completado', 'Cancelado'];

if ($_SERVER['REQUEST_METHOD'] === 'POST' && ($_POST['accion'] ?? '') === 'cambiar_estado') {
    $id = (int) ($_POST['id'] ?? 0);
    $nuevoEstado = $_POST['estado'] ?? '';

    if ($id > 0 && in_array($nuevoEstado, $estadosDisponibles, true)) {
        $stmt = $pdo->prepare('UPDATE solicitudes SET estado = ? WHERE id = ?');
        $stmt->execute([$nuevoEstado, $id]);
        $_SESSION['mensaje'] = ['tipo' => 'success', 'texto' => 'Estado actualizado correctamente.'];
    }
    header('Location: solicitudes.php');
    exit;
}

$solicitudes = $pdo->query('SELECT * FROM solicitudes ORDER BY id DESC')->fetchAll();

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

<div class="d-flex justify-content-between align-items-center mb-4">
    <h2 class="mb-0">Solicitudes</h2>
    <a href="crear_solicitud.php" class="btn btn-primary"><i class="bi bi-plus-lg"></i> Nueva solicitud</a>
</div>

<div class="card shadow-sm">
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
                        <th class="text-end">Acciones</th>
                    </tr>
                </thead>
                <tbody>
                    <?php if (empty($solicitudes)): ?>
                        <tr><td colspan="7" class="text-center text-muted py-4">Aun no hay solicitudes registradas.</td></tr>
                    <?php else: ?>
                        <?php foreach ($solicitudes as $s): ?>
                            <tr>
                                <td>#<?= (int) $s['id'] ?></td>
                                <td><?= htmlspecialchars($s['tipo']) ?></td>
                                <td><?= htmlspecialchars($s['origen']) ?></td>
                                <td><?= htmlspecialchars($s['destino']) ?></td>
                                <td>
                                    <?= badgeEstado($s['estado']) ?>
                                    <form method="POST" action="solicitudes.php" class="d-inline-block mt-1">
                                        <input type="hidden" name="accion" value="cambiar_estado">
                                        <input type="hidden" name="id" value="<?= (int) $s['id'] ?>">
                                        <select name="estado" class="form-select form-select-sm" onchange="this.form.submit()" style="min-width:130px;">
                                            <?php foreach ($estadosDisponibles as $opcion): ?>
                                                <option value="<?= htmlspecialchars($opcion) ?>" <?= $s['estado'] === $opcion ? 'selected' : '' ?>><?= htmlspecialchars($opcion) ?></option>
                                            <?php endforeach; ?>
                                        </select>
                                    </form>
                                </td>
                                <td><?= htmlspecialchars($s['fecha']) ?></td>
                                <td class="text-end">
                                    <a href="editar_solicitud.php?id=<?= (int) $s['id'] ?>" class="btn btn-sm btn-outline-primary">
                                        <i class="bi bi-pencil-square"></i> Editar
                                    </a>
                                    <form method="POST" action="eliminar_solicitud.php" class="d-inline formulario-eliminar">
                                        <input type="hidden" name="id" value="<?= (int) $s['id'] ?>">
                                        <button type="submit" class="btn btn-sm btn-outline-danger">
                                            <i class="bi bi-trash"></i> Eliminar
                                        </button>
                                    </form>
                                </td>
                            </tr>
                        <?php endforeach; ?>
                    <?php endif; ?>
                </tbody>
            </table>
        </div>
    </div>
</div>

<?php include 'includes/footer.php'; ?>
