document.addEventListener("DOMContentLoaded", () => {
    const root = document.body;
    const btn = document.getElementById("btn-modo-oscuro");

    // Eliminar cualquier clase antigua
    root.classList.remove("dark");

    // Aplicar el estado guardado
    const estado = localStorage.getItem("modoOscuro");
    if (estado === "on") {
        root.classList.add("dark-total");
    } else {
        root.classList.remove("dark-total");
    }

    // Cambiar el texto del botón al cargar la página
    if (btn) {
        btn.innerText = estado === "on" ? "☀️ Alternar Reino Claro" : "🌙 Alternar Reino Oscuro";

        // Alternar el modo al hacer clic
        btn.addEventListener("click", () => {
            const activo = root.classList.toggle("dark-total");
            localStorage.setItem("modoOscuro", activo ? "on" : "off");
            btn.innerText = activo ? "☀️ Alternar Reino Claro" : "🌙 Alternar Reino Oscuro";
        });
    }
});
