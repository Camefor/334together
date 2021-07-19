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
import { getUrlKey } from "@/config/util";
import $ from "zepto";

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
    };
  },
  props: ["mid"],
  created() {
    var _this = this;
    this.cd = {};
    this.getSongList();
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
      // const commonParams = { begin: this.song_begin, num: this.song_num };
      this.loading = true;
      // const action =
      //   this.type === "album"
      //     ? this.__getJson("/getAlbumSongList", {
      //         ...commonParams,
      //         albumMid: this.$route.query.mid,
      //         albumID: this.$route.query.id,
      //       })
      //     : this.__getJson(this.__SONG_LIST, {
      //         disstid: this.$route.query.dissid,
      //         ...commonParams,
      //       });
      // let songlist = [];
      // await action
      //   .then((response) => {
      //     songlist = response;
      //   })
      //   .catch(console.error);

      var testData = [
        {
          picurl: {
            src: "https://2019334.xyz/share/cover/1.jpg",
            error:
              "https://y.gtimg.cn/music/photo_new/T001R300x300M0000032raW44KlFoY.jpg",
          },
          purl: "https://2019334.xyz/share/cover/1.jpg",

          songid: 268293598,
          songmid: "002e587r1xLABu",
          albummid: "0026EB5y05nZq1",
          albumid: 11537813,
          singer: [
            {
              id: 3398615,
              mid: "0032raW44KlFoY",
              name: "李志",
            },
          ],
          url: "https://2019334.xyz/share/1.%20%E8%A2%AB%E7%A6%81%E5%BF%8C%E7%9A%84%E6%B8%B8%E6%88%8F%282004%29/01黑色信封.mp3",
          songname: "黑色信封",
        },
        {
          picurl: {
            src: "https://y.gtimg.cn/music/photo_new/T002R300x300M000001qgYSw0bxB9h.jpg?max_age=2592000",
            error:
              "https://y.gtimg.cn/music/photo_new/T001R300x300M000002Q5QT42dAf5f.jpg",
          },
          purl: "C400002H8Sxx4Pv2xk.m4a?guid=5165714425&vkey=9FC0E5A31EC331A55352796BB921E0871DAA4138C31D19809282560697C8147834DA10A2CAC0E3C62084702E9F6495C1CC1DBDF8CC8D7FF4&uin=&fromtag=38",
          songid: 269235051,
          songmid: "002H8Sxx4Pv2xk",
          albummid: "001qgYSw0bxB9h",
          albumid: 13074410,
          singer: [
            {
              id: 3087545,
              mid: "002Q5QT42dAf5f",
              name: "镜予歌",
            },
            {
              id: 2641977,
              mid: "002oe3992BnoEQ",
              name: "喧笑",
            },
            {
              id: 2628966,
              mid: "003mfi4h3svTUT",
              name: "陈亦洺",
            },
          ],
          url: "http://dl.stream.qqmusic.qq.com/C400002H8Sxx4Pv2xk.m4a?guid=5165714425&vkey=9FC0E5A31EC331A55352796BB921E0871DAA4138C31D19809282560697C8147834DA10A2CAC0E3C62084702E9F6495C1CC1DBDF8CC8D7FF4&uin=&fromtag=38",
          songname: "晚夜微雨问海棠 (剧情版)",
        },
      ];

      this.loading = false;

      this.musicList = testData.map((item) => {
        return new this.__Song(item);
      });
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
