<template>
  <div @click.once="initPlay" id="app" style>
    <div class="page">
      <nav-bar
        class="home-navbar"
        :navList="navList"
        namePrefix="home"
        ref="navbar"
      ></nav-bar>

      <transition :name="transitionName">
        <keep-alive max="1">
          <router-view
            :style="zIndex"
            :key="$route.query.id"
            :class="[{ fullScreenFixed }, pageCls]"
          />
        </keep-alive>
      </transition>
    </div>
    <!-- 音乐播放器 -->
    <!-- <keep-alive> -->
    <song-player></song-player>
    <!-- </keep-alive> -->
  </div>
</template>
<script>
import SongPlayer from "@/pages/songPlayer";
export default {
  data() {
    return {
      transitionName: "",
      navbarHeight: 0,
      duration: 400,
      navList: [
        // {
        //   label: "歌手",
        //   name: "singer",
        //   // link: '/singer'
        // },
        {
          label: "歌单",
          name: "songSheet",
          // link: '/songSheet'
        },
        {
          label: "视频",
          name: "mv",
          link: "/mv",
        },
        {
          label: "搜索",
          name: "search",
          link: "/search",
        },
      ],
    };
  },
  mounted() {


var c = 'padding:0px;margin:0px;  font-family:Arial,Helvetica,sans-serif;font-size:13px;';
    // console.log('%c相信未来',c
    //  );

    // console.log('%c相信未来',
    //  ` width:auto; 
    //   float:right; font-family:"Trebuchet MS", Arial, Helvetica, sans-serif;
    //   font-size:20px; 
    //   color:#999999;
    //   line-height:25px;
    //   letter-spacing:1px`);

   
    console.log(
      "%c 如果天总也不亮，那就摸黑过生活；",c
    );
    console.log(
      "%c 如果发出声音是危险的，那就保持沉默；",
      c
    );
    console.log(
      "%c 如果自觉无力发光,那就去照亮别人。",
      c
    );
    console.log(
      "%c 但是---但是",
      c
    );
    console.log(
      "%c 不要习惯了黑暗，就为黑暗辩护；",
      c
    );
    console.log(
      "%c 不要为自己的苟且而得意洋洋；",
      c
    );
    console.log(
      "%c 不要嘲讽那些比自己更勇敢，更有热量的人们。",
      c
    );
    console.log(
      "%c 可以卑微如尘土，不可扭曲如蛆虫。",
      c
    );
  },
  computed: {
    zIndex() {
      return this.$route.name
        ? {
            zIndex: this.$route.matched[0].meta.index,
          }
        : {};
    },
    pageCls() {
      return this.$route.name ? this.$route.matched[0].name + "-page" : "";
    },
    fullScreenFixed() {
      var matchRoutes = this.$route.matched;
      return matchRoutes[0] && matchRoutes[0].meta.fullScreenFixed;
    },
  },
  watch: {
    $route: function (to, from) {
      if (!to.name || !from.name) {
        return;
      }
      this.oldRoute = from;
      this.setTransitionName(to, from);
    },
  },
  components: { SongPlayer },
  methods: {
    enter(el, done) {
      el.style.zIndex = this.$route.matched[0].meta.index;
      setTimeout(done, this.duration);
    },
    afterEnter(el) {
      el.style.removeProperty("z-index");
    },
    leave(el, done) {
      el.style.zIndex = this.oldRoute.matched[0].meta.index;

      setTimeout(done, this.duration);
    },
    afterLeave(el) {
      el.style.removeProperty("z-index");
    },
    setTransitionName(to, from) {
      to = to.matched[0];
      from = from.matched[0];

      const fromIndex = from.meta.index;
      const toIndex = to.meta.index;
      if (toIndex < fromIndex) {
        this.transitionName = "prev";
        if (fromIndex >= 4) {
          this.transitionName = "back";
        }
      } else {
        this.transitionName = "next";
        if (!to.meta.isHome) {
          this.transitionName = "forward";
        }
      }
    },
    initPlay() {
      $("audio")[0]
        .play()
        .catch((err) => {
          console.error(err);
        });
    },
  },
};
</script>
<style scoped lang="less">
#app {
  font-family: "Avenir", Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  color: #2c3e50;
  .posCenter(loadingIcon);

  .page {
    position: fixed;
    width: 100vw;
    top: 0;
  }

  .home-navbar {
    line-height: 2;
  }
}

.loadingIcon {
  font-size: 80px !important;
  z-index: 10;
}

.next-enter-active,
.next-leave-active,
.prev-leave-active,
.prev-enter-active,
.back-enter-active,
.back-leave-active,
.forward-enter-active,
.forward-leave-active {
  transition: all 0.3s;
  width: 100vw;

  position: fixed;
}

.next-enter,
.prev-leave-to,
.back-leave-to,
.forward-enter {
  transform: translate3d(100vw, 0, 0);
}

.next-leave-to,
.prev-enter {
  transform: translate3d(-100vw, 0, 0);
}

.back-enter,
.forward-leave-to {
  transform: translate3d(-20vw, 0, 0);
}
</style>
