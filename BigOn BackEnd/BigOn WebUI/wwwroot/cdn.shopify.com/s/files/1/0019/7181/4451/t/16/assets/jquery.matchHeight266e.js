(function(t){"use strict";typeof define=="function"&&define.amd?define(["jquery"],t):typeof module!="undefined"&&module.exports?module.exports=t(require("jquery")):t(jQuery)})(function(t){var b=-1,_=-1,u=function(a){return parseFloat(a)||0},w=function(a){var i=1,r=t(a),l=null,o=[];return r.each(function(){var h=t(this),g=h.offset().top-u(h.css("margin-top")),f=o.length>0?o[o.length-1]:null;f===null?o.push(h):Math.floor(Math.abs(l-g))<=i?o[o.length-1]=f.add(h):o.push(h),l=g}),o},v=function(a){var i={byRow:!0,property:"height",target:null,remove:!1};return typeof a=="object"?t.extend(i,a):(typeof a=="boolean"?i.byRow=a:a==="remove"&&(i.remove=!0),i)},e=t.fn.matchHeight=function(a){var i=v(a);if(i.remove){var r=this;return this.css(i.property,""),t.each(e._groups,function(l,o){o.elements=o.elements.not(r)}),this}return this.length<=1&&!i.target?this:(e._groups.push({elements:this,options:i}),e._apply(this,i),this)};e.version="master",e._groups=[],e._throttle=80,e._maintainScroll=!1,e._beforeUpdate=null,e._afterUpdate=null,e._rows=w,e._parse=u,e._parseOptions=v,e._apply=function(a,i){var r=v(i),l=t(a),o=[l],h=t(window).scrollTop(),g=t("html").outerHeight(!0),f=l.parents().filter(":hidden");return f.each(function(){var n=t(this);n.data("style-cache",n.attr("style"))}),f.css("display","block"),r.byRow&&!r.target&&(l.each(function(){var n=t(this),p=n.css("display");p!=="inline-block"&&p!=="flex"&&p!=="inline-flex"&&(p="block"),n.data("style-cache",n.attr("style")),n.css({display:p,"padding-top":"0","padding-bottom":"0","margin-top":"0","margin-bottom":"0","border-top-width":"0","border-bottom-width":"0",height:"100px",overflow:"hidden"})}),o=w(l),l.each(function(){var n=t(this);n.attr("style",n.data("style-cache")||"")})),t.each(o,function(n,p){var y=t(p),m=0;if(r.target)m=r.target.outerHeight(!1);else{if(r.byRow&&y.length<=1){y.css(r.property,"");return}y.each(function(){var s=t(this),c=s.attr("style"),d=s.css("display");d!=="inline-block"&&d!=="flex"&&d!=="inline-flex"&&(d="block");var k={display:d};k[r.property]="",s.css(k),s.outerHeight(!1)>m&&(m=s.outerHeight(!1)),c?s.attr("style",c):s.css("display","")})}y.each(function(){var s=t(this),c=0;r.target&&s.is(r.target)||(s.css("box-sizing")!=="border-box"&&(c+=u(s.css("border-top-width"))+u(s.css("border-bottom-width")),c+=u(s.css("padding-top"))+u(s.css("padding-bottom"))),s.css(r.property,m-c+"px"))})}),f.each(function(){var n=t(this);n.attr("style",n.data("style-cache")||null)}),e._maintainScroll&&t(window).scrollTop(h/g*t("html").outerHeight(!0)),this},e._applyDataApi=function(){var a={};t("[data-match-height], [data-mh]").each(function(){var i=t(this),r=i.attr("data-mh")||i.attr("data-match-height");r in a?a[r]=a[r].add(i):a[r]=i}),t.each(a,function(){this.matchHeight(!0)})};var x=function(a){e._beforeUpdate&&e._beforeUpdate(a,e._groups),t.each(e._groups,function(){e._apply(this.elements,this.options)}),e._afterUpdate&&e._afterUpdate(a,e._groups)};e._update=function(a,i){if(i&&i.type==="resize"){var r=t(window).width();if(r===b)return;b=r}a?_===-1&&(_=setTimeout(function(){x(i),_=-1},e._throttle)):x(i)},t(e._applyDataApi);var H=t.fn.on?"on":"bind";t(window)[H]("load",function(a){e._update(!1,a)}),t(window)[H]("resize orientationchange",function(a){e._update(!0,a)})});
//# sourceMappingURL=/s/files/1/0019/7181/4451/t/16/assets/jquery.matchHeight.js.map?v=2422420511068287131632044623
