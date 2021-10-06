Deep Dive into Active Patterns
===

This repository provides a concise, but thorough, review of one of F#'s more-powerful features -- Active Patterns! This feature allows one to extend the pattern-matching capabilities of the language. Active patterns help improve a declarative style of coding. And are critical for tasks such as: integrating procedural APIs, constructing embedded DSLs, and much more. The session first tours the "breathe" of the feature. Then follows a discussion of the underlying mechanics. Finally, a few different "worked examples" are reviewed in-depth. Attendees are encouraged to pose questions throughout, but they will only be answered at the conclusion of the presentation. This talk is aimed at advanced beginners who are familiar with F#’s general syntax and usage.

---

#### Getting Started

After cloning this repository:
```sh
dotnet tool restore
dotnet paket restore
```

Then optionally (but strongly recommended):
```sh
dotnet paket generate-load-scripts
```

---

#### Contributors

Special thanks to the following folks who have helped to improve the quality of this presentation and its asscicated materials:

* [Romain Deneau][https://github.com/rdeneau]

---

#### License

All source code, documentation, presentations, and other materials are licensed
under version 2.0 of the [Mozilla Public License][1] (`MPL-2.0`). See [LICENSE.txt](./LICENSE.txt) for full details.


[1]: https://www.mozilla.org/en-US/MPL/2.0/
