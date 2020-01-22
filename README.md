<p align="center">
    <img src="https://i.imgur.com/BNkDfMV.png" alt="E621Client" width="256"/>
</p>

# E621Client

E621Client is an unofficial .NET Standard 2.0 library for working asynchronously with the [e621 and e926 API](https://e621.net/help/show/api). There wasn't a single API wrapper written in C# available for e621, which is the reason this project came to be. It uses the newest language version, which at the moment of writing this is C# 8.0, and has support for [nullable and non-nullable reference types](https://docs.microsoft.com/en-us/dotnet/csharp/nullable-references).

## Table of Contents

1. [Completeness](#completeness)
2. [Installation](#installation)
3. [Report a bug](#report-a-bug)
4. [Contributing](#contributing)

## Completeness

This project is far from a complete wrapper around the e621 API. It has just been launched, and only the foundation has been implemented. A table can be seen below to give you a quick idea of how complete this library is. It also gives you a vague impression in which order the API areas might be implemented by me. Areas with an extremely low priority might never be implemented at all; it really depends on my motivation to do so. However, feel free to implement those areas yourself if you need them! Just make sure the check out how to [contribute](#contributing) first.

_Legend_

| Symbol             | Meaning               |
| ------------------ | --------------------- |
| :x:                | Not implemented       |
| :heavy_minus_sign: | Partially implemented |
| :heavy_check_mark: | Fully implemented     |

_Cover per API area_

| Area           | Complete           | Priority | Comment |
| -------------- | ------------------ | -------- | ------- |
| Authentication | :heavy_check_mark: | :one:    |         |
| Posts          | :x:                | :one:    |         |
| Tags           | :x:                | :one:    |         |
| Aliases        | :x:                | :two:    |         |
| Implications   | :x:                | :three:  |         |
| Artists        | :x:                | :two:    |         |
| Comments       | :x:                | :three:  |         |
| Blips          | :x:                | :nine:   |         |
| Wiki           | :x:                | :nine:   |         |
| Notes          | :x:                | :nine:   |         |
| Users          | :heavy_check_mark: | :one:    |         |
| User Records   | :x:                | :nine:   |         |
| DMail          | :x:                | :nine:   |         |
| Forum          | :x:                | :four:   |         |
| Pools          | :x:                | :one:    |         |
| Sets           | :x:                | :two:    |         |
| Favorites      | :x:                | :one:    |         |
| Tag History    | :x:                | :nine:   |         |
| Flag History   | :x:                | :nine:   |         |
| Tickets        | :x:                | :nine:   |         |

## Installation

You'll have to use this repository as a [submodule](https://git-scm.com/book/en/v2/Git-Tools-Submodules) for now. It'll be published on NuGet soon.

## Report a bug

You can open an [issue](https://github.com/NoppesTheFolf/E621Client/issues) here. I still have to set up a template for issues so be creative. Just make sure to at least include a piece of code that reproduces the bug.

## Contributing

Contributions to this project are very welcome! Just make sure to contact me on [Telegram](https://t.me/NoppesTheFolf) or on [Twitter](https://twitter.com/NoppesTheFolf) before doing anything. I might very well already be working on the feature you need or a bug that has to be fixed.
