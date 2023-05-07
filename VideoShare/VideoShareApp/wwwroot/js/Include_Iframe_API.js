function initializeYouTubeIframeAPI() {
    var tag = document.createElement('script');

    tag.src = "https://www.youtube.com/iframe_api";
    var videoTag = document.getElementById('youtube-iframe-VideoShare')[0];
    videoTag.parentNode.insertBefore(tag, videoTag);
}

var playerMap = new Map();
var intervalIDMap = new Map();

function setupYouTubeIframe(dotnetInstance) {
    onYouTubeIframeAPIReady(dotnetInstance);
}

function onYouTubeIframeAPIReady() {
    //Do nothing. This is to prevent runtime error when initially loading the API script
}

function onYouTubeIframeAPIReady(dotnetInstance) {
    player = new YT.Player('youtube-iframe-VideoShare', {
        events: {
            'onStateChange': onPlayerStateChange
        }
    });
    //Add a new parameter for the dotnetInstance that this player is a part of
    player.dotnetInstanceParam = dotnetInstance;
    playerMap.set(dotnetInstance, player);
}

function onPlayerStateChange(event) {
    //event.data == 0 stopped, 1 playing, 2 paused, 3 buffering or other
    //event.target == YT Video Player
    var videoState = event.data;
    var dotnetInstance = event.target.dotnetInstanceParam;
    switch (videoState) {
        case 1:
            //Started Playing
            var intervalID = window.setInterval(updateTime, 100, dotnetInstance);
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

function updateTime(dotnetInstance) {
    //trigger time update in blazor with integer arg
    var player = playerMap.get(dotnetInstance);
    var intvalue = Math.floor(player.getCurrentTime());
    dotnetInstance.invokeMethodAsync('UpdatePlaybackTime', intvalue);
}

function cleanup(dotnetInstance) {
    var intervalID = intervalIDMap.get(dotnetInstance);
    window.clearInterval(intervalID);
    intervalIDMap.delete(dotnetInstance);
    playerMap.delete(dotnetInstance);
}
//time elapsed in seconds: player.getCurrentTime()

//Might need to use this: window.onYouTubeIframeAPIReady = function() {}
//Might also need player.destroy()