document.addEventListener("DOMContentLoaded", () => {
    // Load user name
    async function loadUser() {
        try {
            const res = await fetch('/api/auth/me', { credentials: 'include' });

            const registerLink = document.getElementById('registerLink');
            const loginLink = document.getElementById('loginLink');
            const userMenu = document.getElementById('userMenu');
            const userNameDisplay = document.getElementById('userNameDisplay');

            if (!res.ok) {
                // Chưa đăng nhập
                registerLink.style.display = 'inline-block';
                loginLink.style.display = 'inline-block';
                userMenu.style.display = 'none';
                return;
            }

            const data = await res.json();

            if (data.roles && data.roles.includes("Admin")) {
                window.location.href = "/admin/dashboard";
                return;
            }

            // Đã đăng nhập
            registerLink.style.display = 'none';
            loginLink.style.display = 'none';
            userMenu.style.display = 'inline-block';
            userNameDisplay.innerText = data.userName;

            const avatarImg = document.querySelector('#userDropdown img');
            avatarImg.src = data.avatarUrl || '/uploads/images/useravatar.jpg';

            document.getElementById('profileLink').href = `/user/${data.userId}`;

            document.getElementById('logoutBtn').onclick = async (e) => {
                e.preventDefault();
                await fetch('/api/auth/logout', { method: 'POST', credentials: 'include' });
                window.location.reload();
            };

        } catch (err) {
            console.error(err);
        }
    }

    loadUser();
});
