/// <binding Clean='clean' />
"use strict";

var gulp = require("gulp"),
  rimraf = require("rimraf"),
  concat = require("gulp-concat"),
  cssmin = require("gulp-cssmin"),
  uglify = require("gulp-uglify"),
  less = require("gulp-less"),
  path = require("path"),
  runSequence = require('gulp-run-sequence'),
  watch = require("gulp-watch");

var webroot = "./wwwroot/";

var paths = {
  js: webroot + "js/**/*.js",
  minJs: webroot + "js/**/*.min.js",
  less: webroot + "less/yotodo.less",
  lessIncludes: webroot + "less/includes",
  lessDest: webroot + "css",
  css: webroot + "css/**/*.css",
  minCss: webroot + "css/**/*.min.css",
  concatJsDest: webroot + "js/site.min.js",
  concatCssDest: webroot + "css/site.min.css"
};

gulp.task("clean:js", function (cb) {
  rimraf(paths.concatJsDest, cb);
});

gulp.task("clean:css", function (cb) {
  rimraf(paths.concatCssDest, cb);
});

gulp.task("clean", ["clean:js", "clean:css"]);

gulp.task("min:js", function () {
  return gulp.src([paths.js, "!" + paths.minJs], {
    base: "."
  })
    .pipe(concat(paths.concatJsDest))
    .pipe(uglify())
    .pipe(gulp.dest("."));
});

gulp.task("min:css", function () {
  return gulp.src([paths.css, "!" + paths.minCss])
    .pipe(concat(paths.concatCssDest))
    .pipe(cssmin())
    .pipe(gulp.dest("."));
});

gulp.task("less", function(){
  return gulp.src(paths.less)
    .pipe(less({
      paths: [],
      filename: "yotodo.less"
    }))
    .pipe(gulp.dest(paths.lessDest));
});



gulp.task('watch:less', function () {
    gulp.watch(webroot + "less/**/*.less", ["less"])
});


gulp.task("min", ["min:js", "min:css"]);

gulp.task("build", function(cb){
    runSequence("clean", "less", "min",cb);
});
