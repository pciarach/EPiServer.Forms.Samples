"use strict";

const gulp = require("gulp"),
    less = require("gulp-less"),
    config = {
        depends: false,
        compress: true,
        cleancss: false,
        max_line_len: -1,
        optimization: 1,
        silent: false,
        verbose: true,
        lint: true,
        paths: [],
        color: true,
        strictImports: false,
        insecure: false,
        rootpath: "",
        relativeUrls: true,
        ieCompat: true,
        strictMath: false,
        strictUnits: false,
        globalVariables: "",
        modifyVariables: ""
    };

const compileLess = (lessResources, done) => {
    return gulp.src(lessResources)
        .pipe(less(config))
        .pipe(gulp.dest((file) => {
            done();

            //Save the compiled file in the same folder as the .less file
            return file.base;
        }));
};

const tasks = () => {

    const formSamplesLessResources = ["src/EPiServer.Forms.Samples/ClientResources/**/*.less"];

    //  Compile less file of Forms.Samples project to css format
    gulp.task("less:compile", (done) => compileLess(formSamplesLessResources, done));
};

module.exports = tasks;