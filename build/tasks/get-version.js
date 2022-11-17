"use strict";

const fs = require("fs");
const defaultVersionPath = "./build/props/version.props";

/**
 * @param {string} versionPrefixPath 
 */
function getVersion(versionPrefixPath) {
    let prefix, suffixMatch, suffix, data;

    // get version suffix
    data = fs.readFileSync(defaultVersionPath, "utf-8");
    prefix = data.match("<VersionPrefix>(.+?)<\/VersionPrefix>")[1];
    suffixMatch = data.match("<VersionSuffix>(.+?)<\/VersionSuffix>");
    suffix = suffixMatch ? `-${suffixMatch[1]}` : "";

    // get specific version prefix
    if (versionPrefixPath) {
        data = fs.readFileSync(versionPrefixPath, "utf-8");
        prefix = data.match("<VersionPrefix>(.+?)<\/VersionPrefix>")[1];
    }

    return {
        version: prefix,
        packageVersion: `${prefix}${suffix}`
    };
}

module.exports = getVersion;