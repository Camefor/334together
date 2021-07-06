<template>
  <div>
    <my-loading v-if="loading"></my-loading>
    <div class="no-data-wrap" v-if="!loading && musicList.length === 0">
      <span class="text">暂无数据</span>
    </div>
    <div>
      <!-- 顶部导航 -->
      <mt-header
        class="mt-header"
        fixed
        :title="$route.query.dissname || $route.query.name"
      >
        <mt-button @click="$router.back()" slot="left" icon="back"
          >返回</mt-button
        >
        <mt-button icon="more" slot="right"></mt-button>
      </mt-header>
      <!-- 背景图片 -->
      <div class="themePic" v-lazy:background-image="$route.query.imgurl">
        <div v-show="Math.abs(scrollY) < picHeight - reserved" class="playBtn">
          <!-- <mt-button class="mt-button-play" :light="true" :primary="true" :outline="true">播放全部</mt-button> -->
        </div>
        <div class="bg-layer"></div>
      </div>
      <!-- 滚动遮罩 -->
      <div class="layer"></div>
      <!-- 歌曲列表 -->
      <cube-scroll
        :data="musicList"
        :scroll-events="['scroll']"
        :probeType="2"
        @scroll="onScroll"
        class="scrollWrap"
        ref="scroll"
      >
        <music-list
          :marginTop="20"
          v-if="musicList.length"
          :list="musicList"
          @selectPlayForIndex="selectPlayForIndex"
        ></music-list>
      </cube-scroll>
    </div>
  </div>
</template>
<script type="text/javascript">
import { mapActions } from "vuex";

//添加signalr js库
import * as signalR from "@microsoft/signalr";

//初始化signalR
let hubUrl = "http://localhost:44370/hubs/listenTogether";

export default {
  name: "songList",

  data() {
    return {
      loading: false,
      type: this.$route.query.type,
      isLoading: true,
      loadEnd: false,
      total_song_num: 0,
      song_begin: 0,
      song_num: 20,
      cd: {},
      musicList: [],
      scrollY: 0,
      picHeight: 0,
      reserved: 0,
      connection: "", //signalR的连接对象
    };
  },
  props: ["mid"],
  created() {
    this.cd = {};
    this.getSongList();
    this.initSignalR();
  },
  mounted() {
    this.reserved = $(".mt-header").height();
    this.$nextTick(() => {
      this.picHeight = $(".themePic").height();
      $(".scrollWrap").css("top", this.picHeight);
      this.minTranslateY = this.reserved - this.picHeight;
    });
  },
  methods: {
    ...mapActions(["selectPlay"]),

    selectPlayForIndex(list, index) {
      //todo:首次点击 加载 歌单列表后 传递歌曲列表和点击索引给serve
      var _obj = {
        funcName: "selectPlay",
        actionType: "selectPlay",
        list: list,
        index: index,
      };
      //web socket 长度限制
      // this.invokeSignalRServe(_obj);
    },
    onScroll({ x, y }) {
      this.scrollY = y;
      var zIndex = 0;
      var percent = Math.abs(y / this.picHeight);
      var translateY = Math.max(this.minTranslateY, y);
      var scale = 0;
      var blur = Math.min(30, 30 * percent);
      if (y > 0) {
        scale = 1 + percent;
        $(".themePic").css({
          transform: `scale(${scale})`,
          zIndex: 10,
        });
      } else {
        if ($(".scrollWrap").css("overflow") != "visible") {
          $(".scrollWrap").css("overflow", "visible");
        }
        $(".bg-layer").css({
          "-webkit-backdrop-filter": `blur(${blur}px)`,
        });
      }
      $(".layer").css("transform", `translate3d(0,${translateY}px,0)`);
      if (y < this.minTranslateY) {
        $(".themePic").css({ "z-index": 10, height: this.reserved });
      } else if (y < 0 && y > this.minTranslateY) {
        // (33)
        $(".themePic").css({ "z-index": 0, height: this.picHeight });
      }
    },
    onPullingUp() {
      // 加载更多数据
      this.song_begin += this.song_num;
      if (this.musicList.length >= this.total_song_num) {
      }

      this.getSongList();
    },
    initCd(cd) {
      this.$set(this.cd, "dissname", cd.dissname);
      this.$set(this.cd, "logo", cd.logo);
    },
    getMusicList(list) {
      $.each(list, (key, item) => {
        this.musicList.push(new this.__Song(item));
      });
    },

    async getSongList() {
      const commonParams = { begin: this.song_begin, num: this.song_num };
      this.loading = true;
      const action =
        this.type === "album"
          ? this.__getJson("/getAlbumSongList", {
              ...commonParams,
              albumMid: this.$route.query.mid,
              albumID: this.$route.query.id,
            })
          : this.__getJson(this.__SONG_LIST, {
              disstid: this.$route.query.dissid,
              ...commonParams,
            });
      let songlist = [];
      await action
        .then((response) => {
          songlist = response;
        })
        .catch(console.error);

      this.loading = false;

      this.musicList = songlist.map((item) => {
        return new this.__Song(item);
      });
    },
    initSignalR() {
      var _this = this;
      _this.connection = new signalR.HubConnectionBuilder()
        .withUrl(hubUrl)
        .configureLogging(signalR.LogLevel.Information)
        .build();

      //signalR接收Serve端的数据
      _this.connection.on("SignalR_ReceiveData", async function (data) {
        var res = JSON.parse(data);
        switch (res.actionType) {
          case "selectPlay": //
            console.log(res);
            var _list = _this.__cloneDeep__(res.list);
            var _index = res.index;

            _list.push({
              id: _index,
              mid: "6666",
              name: "666 (Live)",
              singer: "666",
              url: "http://dl.stream.qqmusic.qq.com/C400003WeSm61lh8L0.m4a?guid=5165714425&vkey=C82FABE3A56B197DF74E87257E6AEF2B9676DCFD91A14824DAF035BA6C79FA8FAD8DC40E913CA694CE5C0060539146D914F2F2B7F117A572&uin=&fromtag=38",
              picurl: {
                src: "https://y.gtimg.cn/music/photo_new/T002R300x300M000.jpg?max_age=2592000",
                error:
                  "https://y.gtimg.cn/music/photo_new/T001R300x300M000004aejZR04LPCH.jpg",
              },
              albummid: "",
              albumid: "0",
            });
            //把index 通过 list传递过去
            // {
            //     "id": 127595843,
            //     "mid": "000sQTtp2qSl2Y",
            //     "name": "什么是快乐星球 (Live)",
            //     "singer": "马嘉祺",
            //     "url": "http://dl.stream.qqmusic.qq.com/C400003WeSm61lh8L0.m4a?guid=5165714425&vkey=C82FABE3A56B197DF74E87257E6AEF2B9676DCFD91A14824DAF035BA6C79FA8FAD8DC40E913CA694CE5C0060539146D914F2F2B7F117A572&uin=&fromtag=38",
            //     "picurl": {
            //         "src": "https://y.gtimg.cn/music/photo_new/T002R300x300M000.jpg?max_age=2592000",
            //         "error": "https://y.gtimg.cn/music/photo_new/T001R300x300M000004aejZR04LPCH.jpg"
            //     },
            //     "albummid": "",
            //     "albumid": "0"
            // }

            //初始化播放列表
            // _this.selectPlay({ list: _list, _index });

            try {
            } catch (error) {
              console.log(error);
            }

            break;
        }
      });

      _this.connection.start().catch((err) => {
        console.log(err);
      });
    },

    /*调用后端方法 SignalR Serve 传入参数*/
    invokeSignalRServe(object) {
      var _this = this;
      object.connectionId = _this.connection.connectionId;
      object.val = true;
      //object为对象类型,如果可用。再序列化为json 字符串。
      //object对象其中要包括actionType属性，代表指令标识。 比如开始、暂停当前播放，切歌 等等
      var jsonPar = JSON.stringify(object);
      console.log(object);
      console.log(jsonPar);
      _this.connection.invoke("SendMessage", jsonPar);
      //当发起方已经完成相应指令后，serve无需再给发起方自己发送指令。或者发送指令，发起方这边不要再重复调用，否则会形成死循环
      //解决方案：那就把Client的 “状态” 都给serve,让serve决定要不要下发指令
    },
  },
};
</script>
<style scoped lang="less">
.my-loading {
  top: 70%;
}

.songList-page {
  z-index: 50;
}

.mt-header {
  z-index: 20;
  background: transparent;
}

.themePic {
  .posCenterBot(playBtn, 20px);

  .playBtn {
    // position: fixed;
    // top: 100px;
    width: 60%;
    // z-index: 0;

    .cube-button-play {
      border-radius: 20px;
      border: solid orange;
      outline: none;
    }
  }

  .bg-layer {
    width: 100%;
    height: 100%;
    background: rgba(0, 0, 0, 0.3);
    position: absolute;
  }

  text-align: center;
  height: 39vh;
  // padding-top: 70%;
  transform-origin: top;
  background-repeat: no-repeat;
  background-size: cover;
  background-position: top center;
  position: relative;
}

.layer {
  height: 1000px;
  background: #eee;
  position: absolute;
  width: 100%;
  z-index: 1;
}

.scrollWrap {
  height: 60vh;
  position: absolute;
  background: #eee;
  width: 100%;
  // padding: 10px;
  // padding-top: 18px;

  .loadTip {
    text-align: center;
  }
}

.no-data-wrap {
  position: fixed;
  top: 70%;
  text-align: center;
  width: 100%;
  z-index: 100;
  .font-dpr(12px);
}
</style>
