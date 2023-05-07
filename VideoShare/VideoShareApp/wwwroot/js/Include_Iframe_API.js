function createIFrame() {
    var newFrame = document.createElement('div');
    newFrame.setAttribute("id", 'youtube-VideoShare');

    var placementDiv = document.getElementById('dummy-div');
    placementDiv.parentNode.insertBefore(newFrame, placementDiv);
}

window.intervalIDMap = new Map();

function setupYouTubeIframe(dotnetInstance, videoID, startTime) {
    createIFrame();
    onYouTubeIframeAPIReady(dotnetInstance, videoID, startTime);
}

function loadVideo(dotnetInstance, VideoID, startTime) {
    if (window.player !== undefined) {
        var oldDotNetInstance = window.player.dotnetInstanceParam;
        var intervalID = intervalIDMap.get(oldDotNetInstance);
        window.clearInterval(intervalID);
        intervalIDMap.delete(oldDotNetInstance);

        window.player.dotnetInstanceParam = dotnetInstance;
        window.player.loadVideoById({
            'videoId': VideoID,
            'startSeconds': startTime
        });
    }
}

function onPlayerReady(event) {
    event.target.playVideo();
}

function onYouTubeIframeAPIReady(dotnetInstance, videoID, startTime) {
    if (dotnetInstance === undefined || videoID === undefined) { return; }
    window.player = new YT.Player('youtube-VideoShare', {
        width: 800,
        height: 450,
        videoId: videoID,
        playerVars: {
            'start': startTime
        },
        events: {
            'onStateChange': onPlayerStateChange,
            'onReady': onPlayerReady
        }
    });
    //Add a new parameter for the dotnetInstance that this player is a part of
    window.player.dotnetInstanceParam = dotnetInstance;
}

function onPlayerStateChange(event) {
    //event.data == 0 stopped, 1 playing, 2 paused, 3 buffering or other
    //event.target == YT Video Player
    var videoState = event.data;
    var dotnetInstance = event.target.dotnetInstanceParam;
    switch (videoState) {
        case 1:
            //Started Playing
            var intervalID = window.setInterval(updateTime, 100, event.target);
            intervalIDMap.set(dotnetInstance, intervalID);
            break;
        case 2:
        case 3:
            //Paused or Buffering
            var intervalID = intervalIDMap.get(dotnetInstance);
            window.clearInterval(intervalID);
            intervalIDMap.delete(dotnetInstance);
            break;
        case 0:
            //Stopped
            var intervalID = intervalIDMap.get(dotnetInstance);
            window.clearInterval(intervalID);
            intervalIDMap.delete(dotnetInstance);
            //Trigger video completed logic
            dotnetInstance.invokeMethodAsync('CompleteVideoAsync');
            break;
        default:
            //do nothing. This is for states -1 and 5
    }
}

function updateTime(player) {
    //trigger time update in blazor with integer arg
    var dotnetInstance = player.dotnetInstanceParam;
    var intvalue = Math.floor(player.getCurrentTime());
    dotnetInstance.invokeMethodAsync('UpdatePlaybackTime', intvalue);
}

function cleanup() {
    for (let value of intervalIDMap.values()) {
        window.clearInterval(value);
    }
    intervalIDMap.clear();
    window.player.destroy();
}
//time elapsed in seconds: player.getCurrentTime()

//Might need to use this: window.onYouTubeIframeAPIReady = function() {}
//Might also need player.destroy()