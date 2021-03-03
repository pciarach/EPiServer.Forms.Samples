"use strict";

const gulp = require("gulp"),
    path = require("path"),
    { pack } = require("gulp-dotnet-cli");

/**
 * @param {{configuration: "debug"|"release", version: string}} options 
 */
function packNuget(options) {
    gulp.task("nuget:Forms.Samples", function () {
        return gulp.src([
            "**/*.Samples.csproj"
        ], { read: false })
            .pipe(pack({
                output: path.join(process.cwd(), "nupkgs"),
                version: options.version,
                configuration: options.configuration,
                noBuild: true,
                noRestore: true
            }));
    });
}

module.exports = packNuget;