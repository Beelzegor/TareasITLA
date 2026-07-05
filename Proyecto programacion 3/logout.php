<?php
require 'includes/conexion.php';
$_SESSION = [];
session_destroy();
session_start();
$_SESSION['mensaje'] = ['tipo' => 'success', 'texto' => 'Sesion cerrada correctamente.'];
header('Location: login.php');
exit;
