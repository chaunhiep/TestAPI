// Support function
const numOfVideo = {
    video: 4,
    introvideo: 6,
    suggest: 4,
};

function convertTime(time) {
    let minutes = Math.floor(time / 60);
    let seconds = Math.floor(time - 60 * minutes);
    return `${minutes < 10 ? `0${minutes}` : minutes}:${seconds < 10 ? `0${seconds}` : seconds}`;
}

function newIdVideo(video, increase) {
    if (increase === null) {
        return video;
    }

    let keys = video.split('-');
    let num = Number(keys[1]);

    if (increase) {
        num += 1;
    } else {
        num -= 1;
    }

    if (num > numOfVideo[keys[0]]) {
        num = 1;
    } else if (num < 1) {
        num = numOfVideo[keys[0]];
    }

    console.log(keys[0], num);
    return `${keys[0]}-${num}`;
}

function isObject(object) {
    return object !== null && typeof object === 'object';
}

function deepEqual(object1, object2) {
    const keys1 = Object.keys(object1);
    const keys2 = Object.keys(object2);

    if (keys1.length !== keys2.length) {
        return false;
    }

    keys1.forEach(key => {
        const val1 = object1[key];
        const val2 = object2[key];

        const areObjects = isObject(val1) && isObject(val2);
        if ((areObjects && !deepEqual(val1, val2)) || !areObjects || val1 !== val2) {
            return false;
        }

        return true;
    })
}

export { convertTime, newIdVideo };