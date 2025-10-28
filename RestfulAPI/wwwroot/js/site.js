document.addEventListener("DOMContentLoaded", () => {
    async function loadTracks() {
        const res = await fetch('/api/tracks');
        const data = await res.json();
        const container = document.querySelector('.tracks-container');
        container.innerHTML = '';

        data.forEach(track => {
            const artistNames = track.artists.map(a => a.artistName).join(', ');

            const card = document.createElement('div');
            card.classList.add('track-card');
            card.innerHTML = `
                <img src="${track.trackImageUrl}" alt="${track.title}">
                <div class="bio-container">
                    <h3>${track.title}</h3>
                    <h3>${artistNames}</h3>
                    <button class="play-btn" 
                            data-url="${track.fileUrl}" 
                            data-title="${track.title}" 
                            data-image="${track.trackImageUrl}"
                            data-artists="${artistNames}">
                        <i class="fa-solid fa-play"></i>
                    </button>
                </div>
            `;
            card.addEventListener('click', function (e) {
                if (e.target.closest('.play-btn')) return;
                window.location.href = `/track/detail/${track.trackId}`;
            });

            container.appendChild(card);
        });

        // Gán sự kiện Play cho mỗi nút
        document.querySelectorAll('.play-btn').forEach(btn => {
            btn.addEventListener('click', function () {
                const url = this.dataset.url;
                const title = this.dataset.title;
                const image = this.dataset.image;
                const artists = this.dataset.artists;
                playSong(url, title, image, artists);
            });
        });
    }
    loadTracks();

    // Search form
    document.getElementById("search-form")?.addEventListener("submit", function (e) {
        e.preventDefault();
        const keyword = document.getElementById("search-input").value.trim();
        if (!keyword) return;
        window.location.href = `/search?keyword=${encodeURIComponent(keyword)}`;
    });

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

    // Function playSong
    window.playSong = function (url, title, image, artist) {
        const player = document.querySelector('.song-player');
        player.innerHTML = `
            <div class="d-flex align-items-center justify-content-between">
                <div class="d-flex align-items-center">
                    <img src="${image}" alt="${title}" style="width:70px; height:70px; border-radius:10px; object-fit:cover;">
                    <div>
                        <h5 class="text-white mb-1">${title}</h5>
                        <h5 class="text-white mb-1">${artist}</h5>
                        <p class="text-muted mb-0">Đang phát</p>
                    </div>
                </div>
                <audio controls autoplay style="width:500px;">
                    <source src="${url}" type="audio/mpeg">
                </audio>
            </div>
        `;
    };
});
