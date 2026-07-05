<?php
require 'includes/conexion.php';
if (!empty($_SESSION['usuario_id'])) {
    header('Location: dashboard.php');
    exit;
}
include 'includes/header.php';
?>

<div class="row justify-content-center align-items-center" style="min-height:60vh;">
    <div class="col-md-8 text-center">
        <i class="bi bi-lightning-charge-fill display-1" style="color:#0D6EFD;"></i>
        <h1 class="fw-bold mt-3">FastWay</h1>
        <p class="lead text-muted">
            Plataforma web para gestionar solicitudes de transporte y movilidad de forma rapida y organizada.
        </p>
        <div class="d-flex justify-content-center gap-3 mt-4">
            <a href="login.php" class="btn btn-primary btn-lg px-4">Iniciar sesion</a>
            <a href="registro.php" class="btn btn-outline-primary btn-lg px-4">Registrarse</a>
        </div>
    </div>
</div>

<?php include 'includes/footer.php'; ?>
