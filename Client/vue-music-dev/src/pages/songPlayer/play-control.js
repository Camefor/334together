export default {
  togglePrev() {
    if (!this.songReady) {
      return
    }

    let index = this.currentIndex - 1;
    if (index == -1) {
      index = this.playlist.length - 1
    }
    this.setCurrentIndex(index)
    this.songReady = false

      //todo:传递切换音乐信号给serve，歌曲相同。
      var _obj = {
        funcName: "togglePrev",
        actionType: "togglePrev",
      };
      this.invokeSignalRServe(_obj);
  },
  async togglePlaying() {

    this.playing && this.setPrevTransform()
    this.setPlayingState(!this.playing)

    this.currentLyric && this.currentLyric.togglePlay()



  },
  toggleNext() {
    if (!this.songReady) {
      return
    }

    let index = this.currentIndex + 1;
    if (index == this.playlist.length) {
      index = 0
    }
    this.setCurrentIndex(index)
    this.songReady = false

    //todo:传递切换音乐信号给serve，歌曲相同。
    var _obj = {
      funcName: "toggleNext",
      actionType: "toggleNext",
    };
    this.invokeSignalRServe(_obj);

  },
}
