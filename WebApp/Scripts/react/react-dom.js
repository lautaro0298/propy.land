/**
 * ReactDOM v0.14.7
 *
 * Copyright 2013-2015, Facebook, Inc.
 * All rights reserved.
 *
 * This source code is licensed under the BSD-style license found in the
 * LICENSE file in the root directory of this source tree. An additional grant
 * of patent rights can be found in the PATENTS file in the same directory.
 *
 */

// Based off https://github.com/ForbesLindesay/umd/blob/master/template.js

type GlobalObject = {
  React?: any;
};

const globalObject: GlobalObject =
  typeof window !== "undefined"
    ? window
    : typeof global !== "undefined"
    ? global
    : typeof self !== "undefined"
    ? self
    : {};

const React = globalObject.React;

if (!React) {
  throw new Error("React not found");
}

const ReactDOM = React.__SECRET_DOM_DO_NOT_USE_OR_YOU_WILL_BE_FIRED;

export default ReactDOM;

// CommonJS
if (typeof exports === "object" && typeof module !== "undefined") {
  module.exports = ReactDOM;

// RequireJS
} else if (typeof define === "function" && define.amd) {
  define(["react"], () => ReactDOM);

// <script>
} else {
  globalObject.ReactDOM = ReactDOM;
}
