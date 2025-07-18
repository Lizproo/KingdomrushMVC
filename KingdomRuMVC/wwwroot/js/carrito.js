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
    /* nick*/


    // ACTUALIZAR CONTADOR DE MOCHILA DESDE LOCALSTORAGE
    const carrito = JSON.parse(localStorage.getItem("carrito")) || [];
    const contador = document.getElementById("cart-count");
    if (contador) {
        const totalCantidad = carrito.reduce((acc, item) => acc + item.cantidad, 0);
        contador.textContent = totalCantidad;
    }
    ;
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

    // Verificar si ya existe el producto con misma talla
    const index = carrito.findIndex(
        item => item.id_producto === id_producto && item.talla === talla
    );
    //nick
    const stockMax = input ? parseInt(input.max) : 9999; // stock desde input

    if (index !== -1) {
        const cantidadActual = carrito[index].cantidad;
        const nuevaCantidad = cantidadActual + cantidad;

        if (nuevaCantidad > stockMax) {
            alert(`Solo puedes agregar hasta ${stockMax} unidades de este producto.`);


            return;
        }

        carrito[index].cantidad = nuevaCantidad;
    } else {
        if (cantidad > stockMax) {
            alert(`Solo puedes agregar hasta ${stockMax} unidades de este producto.`);
            return;
        }

        carrito.push({
            id_producto,
            nombre,
            precio_unitario: precio,
            imagen_url,
            talla,
            cantidad
        });
    }
    //nick
    //nick//
    localStorage.setItem("carrito", JSON.stringify(carrito));
    alert(`${nombre} agregado a la mochila 🛒`);


    const carritoActualizado = JSON.parse(localStorage.getItem("carrito")) || [];
    const contador = document.getElementById("cart-count");
    if (contador) {
        const totalCantidad = carritoActualizado.reduce((acc, item) => acc + item.cantidad, 0);
        contador.textContent = totalCantidad;
    }

}


function renderMochila() {
    const mochila = JSON.parse(localStorage.getItem('carrito') || '[]');
    const contenedor = document.getElementById('mochila-contenido');
    const totalDiv = document.getElementById('total');
    contenedor.innerHTML = '';
    totalDiv.innerHTML = '';

    if (mochila.length === 0) {
        contenedor.innerHTML = '<p style="text-align:center;">Tu mochila está vacía.</p>';
        return;
    }

    let subtotal = 0;
    mochila.forEach((item, idx) => {
        subtotal += item.precio_unitario * item.cantidad;
        contenedor.innerHTML += `
    <div class="mochila-item">
        <img src="${item.imagen_url}" class="mochila-img" alt="${item.nombre}" />
        <div class="mochila-info">
            <h3>${item.nombre}</h3>
            <p>Precio: $${item.precio_unitario.toFixed(2)}</p>
            <p>Cantidad: ${item.cantidad}</p>


            <!-- nick eliminó la visualización de la talla -->



        </div>
        <button class="btn eliminar-btn" onclick="eliminarDelCarrito(${idx})">❌ Quitar</button>
    </div>
    `;
    });

    const iva = subtotal * 0.15;
    const total = subtotal + iva;

    totalDiv.innerHTML = `
    <p><strong>Subtotal:</strong> $${subtotal.toFixed(2)}</p>
    <p><strong>IVA (15%):</strong> $${iva.toFixed(2)}</p>
    <p><strong>Total a pagar:</strong> $${total.toFixed(2)}</p>
    <button class="btn" onclick="comprar()">✅ Comprar</button>
            `;
}

function eliminarDelCarrito(idx) {
    let mochila = JSON.parse(localStorage.getItem('carrito') || '[]');
    mochila.splice(idx, 1);
    localStorage.setItem('carrito', JSON.stringify(mochila));
    renderMochila();
}

function comprar() {
    const carrito = JSON.parse(localStorage.getItem('carrito') || '[]');
    const idUsuario = localStorage.getItem('idUsuario');
    if (!idUsuario) {
        alert('Debes iniciar sesión para comprar.');
        return;
    }
    if (carrito.length === 0) {
        alert('Tu mochila está vacía.');
        return;
    }

    // Mapeo de propiedades para que coincidan con lo que espera el backend
    const productos = carrito.map(item => ({
        Id_Producto: item.id_producto,
        Nombre: item.nombre,
        Precio_Unitario: item.precio_unitario,
        Imagen_Url: item.imagen_url,
        Talla: item.talla,
        Cantidad: item.cantidad
    }));

    fetch('/Facturas/Comprar', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            idUsuario: idUsuario,
            productos: productos
        })
    })
        .then(response => {
            if (response.ok) {
                localStorage.removeItem('carrito');
                alert('¡Compra realizada con éxito!');
                renderMochila();
            } else {
                alert('Error al procesar la compra.');
            }
        })
        .catch(() => alert('Error de conexión al procesar la compra.'));
}

if (window.location.pathname.includes("/Carrito/Mochila")) {
    document.addEventListener("DOMContentLoaded", () => {
        renderMochila();
    });
}
document.addEventListener('DOMContentLoaded', renderMochila);
