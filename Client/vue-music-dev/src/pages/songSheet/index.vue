<template>
  <div>
    <my-loading v-if="hotSongList.length === 0"></my-loading>
    <!-- 推荐分类 -->
    <cube-scroll
      :data="hotSongList"
      class="scroll-wrapper"
      ref="scroll"
      :options="options"
      @pulling-up="onPullingUp"
    >
      <category :category="recommend"></category>
      <!-- 精品歌单 -->
      <hot-song-list :hotSongList="hotSongList"></hot-song-list>
    </cube-scroll>
  </div>
</template>
<script type="text/javascript">
import HotSongList from "./hotSongList.vue";
import Category from "./category.vue";
export default {
  name: "songSheet",
  data() {
    return {
      recommend: [],
      hotSongList: [],
      options: this.options,
      sin: 0,
      ein: 9,
    };
  },
  created() {
    this.getCategoryData();
    this.getHotSongList();
  },
  activated() {
    this.$refs.scroll && this.$refs.scroll.refresh();
  },
  methods: {
    onPullingUp() {
      this.getHotSongList();
    },
    async getHotSongList() {
      var option = {
        sin: this.sin,
        ein: this.ein,
      };
      if (this.lasttime) {
        option.lasttime = this.lasttime;
      }

      var { code, data } = await this.__getJson(this.__SONG_SHEET_LIST, option);
      var al = [
        {
          dissid: "6960007429",
          createtime: "2020-03-08",
          commit_time: "2020-03-08",
          dissname: " 被禁忌的游戏(2004)",
          imgurl: "https://2019334.xyz/share/cover/1.jpg",
          introduction: "",
          listennum: 4086148,
          score: 0,
          version: 0,
          creator: {
            type: 0,
            qq: 2060774762,
            encrypt_uin: "ownsoeSl7eSsoc**",
            name: "李志",
            isVip: 0,
            avatarUrl: "",
            followflag: 0,
          },
        },
        {
          dissid: "7684071290",
          createtime: "2020-08-19",
          commit_time: "2020-08-19",
          dissname: "梵高先生(2005)",
          imgurl: "https://2019334.xyz/share/cover/2.jpg",
          introduction: "",
          listennum: 1212172,
          score: 0,
          version: 0,
          creator: {
            type: 0,
            qq: 1131072037,
            encrypt_uin: "oK6ioKnlowni7z**",
            name: "李志",
            isVip: 0,
            avatarUrl: "",
            followflag: 0,
          },
        },
        {
          dissid: "7677627835",
          createtime: "2020-08-29",
          commit_time: "2020-08-29",
          dissname: "这个世界会好吗(2006)",
          imgurl: "https://2019334.xyz/share/cover/3.jpg",
          introduction: "",
          listennum: 752555,
          score: 0,
          version: 0,
          creator: {
            type: 0,
            qq: 2280371540,
            encrypt_uin: "ow-FoeoloK4Pon**",
            name: "李志",
            isVip: 0,
            avatarUrl: "",
            followflag: 0,
          },
        },
        {
          dissid: "7677627835",
          createtime: "2020-08-29",
          commit_time: "2020-08-29",
          dissname: "工体东路没有人(2009)",
          imgurl: "https://2019334.xyz/share/cover/4.jpg",
          introduction: "",
          listennum: 752555,
          score: 0,
          version: 0,
          creator: {
            type: 0,
            qq: 2280371540,
            encrypt_uin: "ow-FoeoloK4Pon**",
            name: "李志",
            isVip: 0,
            avatarUrl: "",
            followflag: 0,
          },
        },
        {
          dissid: "7677627835",
          createtime: "2020-08-29",
          commit_time: "2020-08-29",
          dissname: "二零零九年十月十六日事件(2009",
          imgurl: "https://2019334.xyz/share/cover/5.jpg",
          introduction: "",
          listennum: 752555,
          score: 0,
          version: 0,
          creator: {
            type: 0,
            qq: 2280371540,
            encrypt_uin: "ow-FoeoloK4Pon**",
            name: "李志",
            isVip: 0,
            avatarUrl: "",
            followflag: 0,
          },
        },
      ];
      // let list = data.list;
      let list = al;
      list.forEach((item) => {
        item.id = item.dissid;
      });
      if (code == this.__QERR_OK) {
        this.hotSongList.push(...list);
        this.sin += 10;
        this.ein += 10;
      }
    },

    async getCategoryData() {
      // var { code, data } = await this.__getJson(
      //   `/getCategoryTags`
      // )
      // if (code == 0) {
      //   this.category = data.categories
      //   this._formatCategory()
      // }
    },
    async _formatCategory() {
      let num = 3;
      this.category.forEach((item, index) => {
        if (index == 0) return;

        const arr = item.items.slice(0, num);
        this.recommend.push(...arr);
        this.recommend.sort((a, b) => {
          return a.categoryId + b.categoryId;
        });
      });
    },
  },
  components: {
    HotSongList,
    Category,
  },
};
</script>
<style scoped lang="less">
.my-loading {
  top: 50%;
}

.scroll-wrapper {
  margin-top: 14px;
  padding: 0 14px;
}
</style>
