// Activar modo oscuro
function toggleDarkMode() {
    const isDark = document.body.classList.toggle("dark");
    localStorage.setItem("modoOscuro", isDark ? "on" : "off");
}

document.addEventListener("DOMContentLoaded", () => {
    // Aplicar modo oscuro si estaba activado
    if (localStorage.getItem("modoOscuro") === "on") {
        document.body.classList.add("dark");
    }

    // SUBMENÚ DE RELIQUIAS RETRO
    const btn = document.getElementById("reliquiasBtn");
    const dropdown = document.querySelector(".dropdown");

    if (btn && dropdown) {
        btn.addEventListener("click", function (e) {
            e.preventDefault();
            dropdown.classList.toggle("show");
        });

        window.addEventListener("click", function (e) {
            if (!e.target.matches("#reliquiasBtn") && dropdown.classList.contains("show")) {
                dropdown.classList.remove("show");
            }
        });
    }

    // ACTUALIZAR CONTADOR DE MOCHILA DESDE LOCALSTORAGE
    const carrito = JSON.parse(localStorage.getItem("carrito")) || [];
    const contador = document.getElementById("cart-count");
    if (contador) contador.textContent = carrito.length;
});

// ✅ AGREGAR PRODUCTO AL CARRITO EN LOCALSTORAGE
function agregarAlCarrito(id_producto, nombre, precio, imagen_url, usa_talla) {
    const usuarioRaw = localStorage.getItem("usuarioLogueado");
    const usuario = typeof usuarioRaw === 'string' && usuarioRaw.includes('{')
        ? JSON.parse(usuarioRaw)
        : { id_usuario: usuarioRaw };

    if (!usuario || !usuario.id_usuario) {
        alert("Debes iniciar sesión para agregar a la mochila.");
        window.location.href = "/Usuarios/Login";
        return;
    }

    const talla = usa_talla ? prompt("Selecciona tu talla (XS, S, M, L, XL):", "M") : "Única";
    if (!talla) return;

    const input = document.querySelector(`input[data-id="${id_producto}"]`);
    let cantidad = 1;
    if (input) {
        cantidad = parseInt(input.value);
        if (isNaN(cantidad) || cantidad < 1) cantidad = 1;
        const max = parseInt(input.max);
        if (cantidad > max) cantidad = max;
    }

    const carrito = JSON.parse(localStorage.getItem("carrito")) || [];

    carrito.push({
        id_producto,
        nombre,
        precio_unitario: precio,
        imagen_url,
        talla,
        cantidad
    });

    localStorage.setItem("carrito", JSON.stringify(carrito));
    alert(`${nombre} agregado a la mochila 🛒`);

    const contador = document.getElementById("cart-count");
    if (contador) contador.textContent = carrito.length;
}
