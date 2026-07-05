<?php $paginaActual = basename($_SERVER['PHP_SELF']); ?>
<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>FastWay</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.css" rel="stylesheet">
    <link href="css/style.css" rel="stylesheet">
</head>
<body>

<nav class="navbar navbar-expand-lg navbar-dark" style="background-color:#0D6EFD;">
    <div class="container">
        <a class="navbar-brand fw-bold" href="<?= !empty($_SESSION['usuario_id']) ? 'dashboard.php' : 'index.php' ?>">
            <i class="bi bi-lightning-charge-fill"></i> FastWay
        </a>
        <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarFastWay">
            <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse" id="navbarFastWay">
            <ul class="navbar-nav ms-auto mb-2 mb-lg-0">
                <?php if (!empty($_SESSION['usuario_id'])): ?>
                    <li class="nav-item">
                        <a class="nav-link <?= $paginaActual === 'dashboard.php' ? 'active fw-bold' : '' ?>" href="dashboard.php">Dashboard</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link <?= $paginaActual === 'solicitudes.php' ? 'active fw-bold' : '' ?>" href="solicitudes.php">Solicitudes</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link <?= $paginaActual === 'crear_solicitud.php' ? 'active fw-bold' : '' ?>" href="crear_solicitud.php">Nueva solicitud</a>
                    </li>
                    <li class="nav-item">
                        <span class="nav-link text-white-50">
                            <i class="bi bi-person-circle"></i> <?= htmlspecialchars($_SESSION['usuario_nombre']) ?>
                        </span>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" href="logout.php">Cerrar sesion</a>
                    </li>
                <?php else: ?>
                    <li class="nav-item">
                        <a class="nav-link <?= $paginaActual === 'login.php' ? 'active fw-bold' : '' ?>" href="login.php">Iniciar sesion</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link <?= $paginaActual === 'registro.php' ? 'active fw-bold' : '' ?>" href="registro.php">Registrarse</a>
                    </li>
                <?php endif; ?>
            </ul>
        </div>
    </div>
</nav>

<main class="container py-4">
    <?php if (!empty($_SESSION['mensaje'])): ?>
        <div class="alert alert-<?= htmlspecialchars($_SESSION['mensaje']['tipo']) ?> alert-dismissible fade show" role="alert">
            <?= htmlspecialchars($_SESSION['mensaje']['texto']) ?>
            <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
        </div>
        <?php unset($_SESSION['mensaje']); ?>
    <?php endif; ?>
