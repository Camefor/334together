// // 兼容低版本浏览器
// import 'babel-polyfill'

import 'lib-flexible'
// 注册全局组件
import './registerComp/custom.js'
import './registerComp/cube.js'
import './registerComp/mint.js'
import './registerComp/vux.js'

// 引入全局自定义样式
// import './style/global.less'
import './style'

// 引入布局框架
import './config/index.js'

import store from './store/index.js'
import Vue from 'vue'

// 引入调试工具
// import vconsole from 'vconsole'

import App from './App'
import router from './router'
import FastClick from 'fastclick'
import VueLazyLoad from 'vue-lazyload'
import layer from "layui-layer";


// import Cube from 'cube-ui'
import vueTap from 'v-tap'

// 全局设置mixin
import './common/mixins/global.js'
import signalr from './common/js/signalR.js'
import VueClipboard from 'vue-clipboard2'
Vue.use(VueClipboard)

Vue.use(layer)
Vue.use(vueTap)
Vue.use(signalr)

// new vconsole()

Vue.use(VueLazyLoad, {
  // preLoad: 1,
  error: require('./assets/logo.png'),
  loading: require('./assets/logo.png'),
})

if ('addEventListener' in document) {
  document.addEventListener(
    'DOMContentLoaded',
    function() {
      FastClick.attach(document.body)
    },
    false
  )
}

Vue.config.productionTip = false
Vue.config.devtools = true
Vue.config.errorHandler = function(err, vm, info) {
  console.log(err,info)
}

/* eslint-disable no-new */
// window.store = store

new Vue({
  el: '#app',
  router,
  store,
  template: '<App/>',
  components: { App },
})


/* 

部署：
cd /etc/nginx/

vim nginx.conf



server {
 listen 80;
 server_name ip; 140.82.13.152

 # 注意设定 root路径是有dist的
 location / {
   root /usr/local/webapp/dist;
   index /index.html;
 }

 #跨域 ip和port自行替换
 location /adminApi {
   proxy_pass http://ip:port;
 }

}



  server {
        listen       80;
        server_name  localhost;

        #charset koi8-r;

        #access_log  logs/host.access.log  main;

        location / {
            root  /var/334musicvue;
            index  index.html;
        }

    }


    重新加载配置： nginx -s reload

*/
