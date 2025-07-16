document.addEventListener("DOMContentLoaded", function () {
  const toggleBtn = document.querySelector(".menu-toggle");
  const navbar = document.querySelector(".navbar");

  if (toggleBtn && navbar) {
    toggleBtn.addEventListener("click", () => {
      navbar.classList.toggle("show");
    });
  }
});

document.getElementById('loginForm').addEventListener('submit', async function(e) {
  e.preventDefault();

  const username = document.getElementById('username').value;
  const password = document.getElementById('password').value;

  try {
    const response = await fetch('/api/login', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ username, password })
    });

    const result = await response.json();

    if (result.success && result.role === 'admin') {
      window.location.href = '/admin/admin.html';  // ✅ Redirige al panel admin
    } else {
      document.getElementById('error').innerText = 'Credenciales inválidas';
    }

  } catch (error) {
    console.error('Error al iniciar sesión:', error);
  }
});
