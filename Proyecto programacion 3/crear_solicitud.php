<?php
require 'includes/conexion.php';
require 'includes/auth.php';
requireLogin();

$errores = [];
$tipo = $origen = $destino = $fecha = $descripcion = '';

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
            'INSERT INTO solicitudes (id_usuario, tipo, origen, destino, descripcion, estado, fecha)
             VALUES (?, ?, ?, ?, ?, ?, ?)'
        );
        $stmt->execute([$_SESSION['usuario_id'], $tipo, $origen, $destino, $descripcion, 'Pendiente', $fecha]);

        $_SESSION['mensaje'] = ['tipo' => 'success', 'texto' => 'Solicitud creada correctamente.'];
        header('Location: solicitudes.php');
        exit;
    }
}

include 'includes/header.php';
?>

<div class="row justify-content-center">
    <div class="col-md-8 col-lg-6">
        <div class="card shadow-sm">
            <div class="card-body p-4">
                <h3 class="card-title mb-4">Nueva solicitud</h3>

                <?php if (!empty($errores)): ?>
                    <div class="alert alert-danger">
                        <ul class="mb-0">
                            <?php foreach ($errores as $error): ?>
                                <li><?= htmlspecialchars($error) ?></li>
                            <?php endforeach; ?>
                        </ul>
                    </div>
                <?php endif; ?>

                <form method="POST" action="crear_solicitud.php" novalidate class="needs-validation">
                    <div class="mb-3">
                        <label class="form-label">Tipo de servicio</label>
                        <select name="tipo" class="form-select" required>
                            <option value="" disabled <?= $tipo === '' ? 'selected' : '' ?>>Selecciona un tipo</option>
                            <?php foreach (['Transporte de pasajeros', 'Transporte de carga', 'Entrega/domicilio', 'Mudanza'] as $opcion): ?>
                                <option value="<?= htmlspecialchars($opcion) ?>" <?= $tipo === $opcion ? 'selected' : '' ?>><?= htmlspecialchars($opcion) ?></option>
                            <?php endforeach; ?>
                        </select>
                    </div>
                    <div class="row">
                        <div class="col-md-6 mb-3">
                            <label class="form-label">Origen</label>
                            <input type="text" name="origen" class="form-control" value="<?= htmlspecialchars($origen) ?>" required>
                        </div>
                        <div class="col-md-6 mb-3">
                            <label class="form-label">Destino</label>
                            <input type="text" name="destino" class="form-control" value="<?= htmlspecialchars($destino) ?>" required>
                        </div>
                    </div>
                    <div class="mb-3">
                        <label class="form-label">Fecha</label>
                        <input type="date" name="fecha" class="form-control" value="<?= htmlspecialchars($fecha) ?>" required>
                    </div>
                    <div class="mb-3">
                        <label class="form-label">Descripcion</label>
                        <textarea name="descripcion" class="form-control" rows="3"><?= htmlspecialchars($descripcion) ?></textarea>
                    </div>
                    <div class="d-flex gap-2">
                        <button type="submit" class="btn btn-primary">Guardar solicitud</button>
                        <a href="solicitudes.php" class="btn btn-outline-secondary">Cancelar</a>
                    </div>
                </form>
            </div>
        </div>
    </div>
</div>

<?php include 'includes/footer.php'; ?>
