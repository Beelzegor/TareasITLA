<?php
require 'includes/conexion.php';
require 'includes/auth.php';
requireLogin();

$id = (int) ($_GET['id'] ?? $_POST['id'] ?? 0);
if ($id <= 0) {
    header('Location: solicitudes.php');
    exit;
}

$stmt = $pdo->prepare('SELECT * FROM solicitudes WHERE id = ?');
$stmt->execute([$id]);
$solicitud = $stmt->fetch();

if (!$solicitud) {
    $_SESSION['mensaje'] = ['tipo' => 'danger', 'texto' => 'La solicitud solicitada no existe.'];
    header('Location: solicitudes.php');
    exit;
}

$errores = [];

if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    $tipo = trim($_POST['tipo'] ?? '');
    $origen = trim($_POST['origen'] ?? '');
    $destino = trim($_POST['destino'] ?? '');
    $fecha = trim($_POST['fecha'] ?? '');
    $descripcion = trim($_POST['descripcion'] ?? '');

    if ($tipo === '' || $origen === '' || $destino === '' || $fecha === '') {
        $errores[] = 'Los campos tipo, origen, destino y fecha son obligatorios.';
    }

    if (empty($errores)) {
        $stmt = $pdo->prepare(
            'UPDATE solicitudes SET tipo = ?, origen = ?, destino = ?, fecha = ?, descripcion = ? WHERE id = ?'
        );
        $stmt->execute([$tipo, $origen, $destino, $fecha, $descripcion, $id]);

        $_SESSION['mensaje'] = ['tipo' => 'success', 'texto' => 'Solicitud actualizada correctamente.'];
        header('Location: solicitudes.php');
        exit;
    }

    $solicitud = ['id' => $id, 'tipo' => $tipo, 'origen' => $origen, 'destino' => $destino, 'fecha' => $fecha, 'descripcion' => $descripcion];
}

include 'includes/header.php';
?>

<div class="row justify-content-center">
    <div class="col-md-8 col-lg-6">
        <div class="card shadow-sm">
            <div class="card-body p-4">
                <h3 class="card-title mb-4">Editar solicitud #<?= (int) $solicitud['id'] ?></h3>

                <?php if (!empty($errores)): ?>
                    <div class="alert alert-danger">
                        <ul class="mb-0">
                            <?php foreach ($errores as $error): ?>
                                <li><?= htmlspecialchars($error) ?></li>
                            <?php endforeach; ?>
                        </ul>
                    </div>
                <?php endif; ?>

                <form method="POST" action="editar_solicitud.php?id=<?= (int) $solicitud['id'] ?>" novalidate class="needs-validation">
                    <input type="hidden" name="id" value="<?= (int) $solicitud['id'] ?>">
                    <div class="mb-3">
                        <label class="form-label">Tipo de servicio</label>
                        <select name="tipo" class="form-select" required>
                            <?php foreach (['Transporte de pasajeros', 'Transporte de carga', 'Entrega/domicilio', 'Mudanza'] as $opcion): ?>
                                <option value="<?= htmlspecialchars($opcion) ?>" <?= $solicitud['tipo'] === $opcion ? 'selected' : '' ?>><?= htmlspecialchars($opcion) ?></option>
                            <?php endforeach; ?>
                        </select>
                    </div>
                    <div class="row">
                        <div class="col-md-6 mb-3">
                            <label class="form-label">Origen</label>
                            <input type="text" name="origen" class="form-control" value="<?= htmlspecialchars($solicitud['origen']) ?>" required>
                        </div>
                        <div class="col-md-6 mb-3">
                            <label class="form-label">Destino</label>
                            <input type="text" name="destino" class="form-control" value="<?= htmlspecialchars($solicitud['destino']) ?>" required>
                        </div>
                    </div>
                    <div class="mb-3">
                        <label class="form-label">Fecha</label>
                        <input type="date" name="fecha" class="form-control" value="<?= htmlspecialchars($solicitud['fecha']) ?>" required>
                    </div>
                    <div class="mb-3">
                        <label class="form-label">Descripcion</label>
                        <textarea name="descripcion" class="form-control" rows="3"><?= htmlspecialchars($solicitud['descripcion']) ?></textarea>
                    </div>
                    <div class="d-flex gap-2">
                        <button type="submit" class="btn btn-primary">Actualizar solicitud</button>
                        <a href="solicitudes.php" class="btn btn-outline-secondary">Cancelar</a>
                    </div>
                </form>
            </div>
        </div>
    </div>
</div>

<?php include 'includes/footer.php'; ?>
