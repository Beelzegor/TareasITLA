document.addEventListener('DOMContentLoaded', function () {
    // Confirmacion antes de eliminar una solicitud (RF-07)
    document.querySelectorAll('.formulario-eliminar').forEach(function (formulario) {
        formulario.addEventListener('submit', function (evento) {
            if (!window.confirm('¿Seguro que deseas eliminar esta solicitud? Esta accion no se puede deshacer.')) {
                evento.preventDefault();
            }
        });
    });

    // Validacion Bootstrap para formularios marcados con needs-validation
    document.querySelectorAll('form.needs-validation').forEach(function (formulario) {
        formulario.addEventListener('submit', function (evento) {
            if (!formulario.checkValidity()) {
                evento.preventDefault();
                evento.stopPropagation();
            }
            formulario.classList.add('was-validated');
        });
    });

    // Confirmar que la contrasena y su confirmacion coincidan en tiempo real (RF-01)
    var password = document.querySelector('input[name="password"]');
    var confirmar = document.querySelector('input[name="confirmar_password"]');
    if (password && confirmar) {
        var validarCoincidencia = function () {
            if (confirmar.value !== '' && confirmar.value !== password.value) {
                confirmar.setCustomValidity('Las contrasenas no coinciden');
            } else {
                confirmar.setCustomValidity('');
            }
        };
        password.addEventListener('input', validarCoincidencia);
        confirmar.addEventListener('input', validarCoincidencia);
    }

    // Auto-cerrar alertas de confirmacion despues de unos segundos
    document.querySelectorAll('.alert').forEach(function (alerta) {
        setTimeout(function () {
            var instancia = bootstrap.Alert.getOrCreateInstance(alerta);
            instancia.close();
        }, 5000);
    });
});
