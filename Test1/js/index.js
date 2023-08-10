import { convertTime, newIdVideo } from "./support.js";

// External function
function handleNewVideo(video, increase) {
    const nextId = newIdVideo(video, increase);
    const nextVideo = document.getElementById(nextId);
    
    banner.setup(nextId, nextVideo.dataset.title, nextVideo.dataset.desc,
        nextVideo.dataset.src, nextVideo.dataset.img);
    banner.render();

    // Signal push state
    if (increase !== null) {
        pushState(nextId);
    }

    watchRoot.style.display = "block";
    player.control.style.display = "none";
}

// Push state
function pushState(id) {
    const state = {
        videoId: id,
    };
    if (id !== 'video-1') {
        history.pushState(state, "", `?id=${id}`);
    } else {
        history.pushState(state, "", "/");
    }
}

function replaceState(id) {
    const state = {
        videoId: id,
    };
    if (id !== 'video-1') {
        history.replaceState(state, "", `?id=${id}`);
    } else {
        history.replaceState(state, "", "/");
    }
}

// Create video template
function Video(video, id, src, img) {
    this.video = video;
    this.id = id;
    this.src = src;
    this.img = img;

    this.render = function() {
        this.video.load();
        this.video.poster = this.img;

        const source = this.video.firstElementChild;
        source.src = this.src;
    }

    this.play = function() {
        player.playIcon.classList.remove("fa-play");
        player.playIcon.classList.add("fa-pause");

        this.video.play();
    }

    this.pause = function() {
        player.playIcon.classList.remove("fa-pause");
        player.playIcon.classList.add("fa-play");

        this.video.pause();
    }

    this.addCallBack = function() {
        const that = this;

        // Handle play button
        player.play.onclick = function() {
            if (that.video.paused || that.video.ended) {
                that.play();
            } else {
                that.pause();
            }
        };

        player.back.onclick = function() {
            handleNewVideo(that.id, false);
        };

        player.next.onclick = function() {
            handleNewVideo(that.id, true);
        };

        // End video
        this.video.addEventListener('ended', () => {
            player.playIcon.classList.remove("fa-pause");
            player.playIcon.classList.add("fa-play");
        });

        // Handle duration
        this.video.addEventListener('loadedmetadata', () => {
            progress.bar.setAttribute('max', that.video.duration);
            progress.duration.textContent = convertTime(that.video.duration);
        });

        // Handle progress update real-time
        this.video.addEventListener('timeupdate', () => {
            if (!progress.bar.getAttribute('max')) {
                progress.bar.max = that.video.duration;
                progress.duration.textContent = convertTime(that.video.duration);
            }

            progress.bar.value = that.video.currentTime;
            progress.currentTime.textContent = convertTime(that.video.currentTime);
        });

        // Click on progress to move on
        progress.bar.addEventListener('click', function(e) {
            const rect = this.getBoundingClientRect();
            const pos = (e.pageX - rect.left) / this.offsetWidth;
            that.video.currentTime = pos * that.video.duration;
        });
    }
}

// Banner template
function Banner(banner, watchButton) {
    this.banner = banner;
    this.watchButton = watchButton;

    this.setup = function(id, title, desc, src, img) {
        this.banner.dataset.id = id;
        this.banner.dataset.title = title;
        this.banner.dataset.desc = desc;
        this.banner.dataset.src = src;
        this.banner.dataset.img = img;
    }

    this.render = function() {
        // Set banner root id
        const id = this.banner.dataset.id ? this.banner.dataset.id : this.banner.id;
        bannerRoot.dataset.id = id;

        // Set Poster
        info.title.textContent = this.banner.dataset.title;
        info.desc.textContent = this.banner.dataset.desc;

        // Set New Video
        const video = new Video(videoRoot, id, this.banner.dataset.src, this.banner.dataset.img);
        video.render();

        return video;
    }

    this.addCallBack = function() {
        const that = this;

        this.watchButton.addEventListener('click', () => {
            watchRoot.style.display = "none";

            player.control.style.display = "block";
            player.playIcon.classList.remove("fa-play");
            player.playIcon.classList.add("fa-pause");

            // Play video
            const video = that.render();
            if (!watchRoot.isEqualNode(that.watchButton)) {
                pushState(video.id);
            }

            video.play();

            video.addCallBack();
        });
    }
}

// Global
// Buttons to control video
const info = {
    title: document.querySelector('.banner h2'),
    desc: document.querySelector('.banner p')
};

const player = {
    playIcon: document.querySelector('#playButton i'),
    control: document.querySelector('.banner div.container__button'),
    back: document.getElementById('backButton'),
    next: document.getElementById('nextButton'),
    play: document.getElementById('playButton'),
};

const progress = {
    bar: document.querySelector('.video__progressbar'),
    currentTime: document.querySelector('.video__currentTime'),
    duration: document.querySelector('.video__duration')
};

// Video root
const defaultVid = document.getElementById('video-1');
const videoRoot = document.querySelector('.banner video');

// Banner root
const bannerRoot = document.querySelector('.banner');
const watchRoot = document.querySelector('.banner button');
const banner = new Banner(bannerRoot, watchRoot);

// Get data banner from localStorage
let lastId = localStorage.getItem('lastVideo');

if (lastId === null) {
    banner.setup(defaultVid.id, defaultVid.dataset.title, defaultVid.dataset.desc,
        defaultVid.dataset.src, defaultVid.dataset.img);

    lastId = defaultVid.id;
} else {
    const lastVid = document.getElementById(lastId);

    banner.setup(lastId, lastVid.dataset.title, lastVid.dataset.desc,
        lastVid.dataset.src, lastVid.dataset.img);
}
banner.render();
replaceState(lastId);
banner.addCallBack();

// Trailer banner
let listVideo = [];
listVideo = listVideo.concat(document.querySelectorAll('.trailer__video .video'))
    .concat(document.querySelectorAll('.trailer__video .intro-video'))
    .concat(document.querySelectorAll('.suggest__products .video'));

listVideo.forEach((x) => {
    x.forEach(y => {
        const newBanner = new Banner(y, document.querySelector(`#${y.id} .button`));

        newBanner.addCallBack();
    })
});

// Handle reload page
window.addEventListener('beforeunload', () => {
    const currentVid = bannerRoot.dataset.id;

    const lastVid = localStorage.getItem('lastVideo');
    if ((lastVid === null && currentVid !== 'video-1') || (lastVid !== null && lastVid !== currentVid)) {
        localStorage.setItem('lastVideo', currentVid);
    }
});

// Back and forward page
window.addEventListener('popstate', (event) => {
    const newId = event.state.videoId;
    console.log('Pop', newId);
    handleNewVideo(newId, null);
});

