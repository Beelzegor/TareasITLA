<?php
require 'includes/conexion.php';
require 'includes/auth.php';
requireLogin();

$id = (int) ($_POST['id'] ?? 0);

if ($id > 0) {
    $stmt = $pdo->prepare('DELETE FROM solicitudes WHERE id = ?');
    $stmt->execute([$id]);
    $_SESSION['mensaje'] = ['tipo' => 'success', 'texto' => 'Solicitud eliminada correctamente.'];
}

header('Location: solicitudes.php');
exit;
