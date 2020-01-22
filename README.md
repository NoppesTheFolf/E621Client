<p align="center">
    <img src="https://i.imgur.com/BNkDfMV.png" alt="E621Client" width="256"/>
</p>

# E621Client

E621Client is an unofficial .NET Standard 2.0 library for working asynchronously with the [e621 and e926 API](https://e621.net/help/show/api). There wasn't a single API wrapper written in C# available for e621, which is the reason this project came to be. It uses the newest language version, which at the moment of writing this is C# 8.0, and has support for [nullable and non-nullable reference types](https://docs.microsoft.com/en-us/dotnet/csharp/nullable-references).

## Table of Contents

1. [Completeness](#completeness)
2. [Installation](#installation)
3. [Getting started](#getting-started)
4. [Authentication](#authentication)
   1. [Logging in](#logging-in)
   2. [Logging out](#logging-out)
5. [Report a bug](#report-a-bug)
6. [Contributing](#contributing)

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

## Getting started

You will need a `E621Client` instance in order to talk with the API. These instances can only be created using the `E621ClientBuilder` class. The builder will allow you to create your very own personalized `E621Client` instance in a fluent manner based on the specific needs of your application. Just make sure you at least specify User-Agent information, as e621 requires it. Not specifying it will cause an exception to be thrown.

_Bare minimum example_

```csharp
var e621Client = new E621ClientBuilder()
    .WithUserAgent("MyApplicationName", "MyApplicationVersion", "MyTwitterUsername", "Twitter")
    .Build();
```

However, you might need something a little more suited for your application. The default `E621Client` instance built above uses settings tuned to make sure the load on e621's side is kept to a minimum. Maybe your developing an interactive tool and therefore want it to be as responsive as possible.

_Example for an interactive application_

```csharp
var e621Client = new E621ClientBuilder()
    .WithUserAgent("MyApplicationName", "MyApplicationVersion", "MyTwitterUsername", "Twitter")
    .WithMaximumConnections(E621Client.MaximumConnectionsLimit)
    .WithRequestInterval(E621Client.MinimumRequestInterval)
    .Build();
```

`E621Client` instances can be disposed of, but you generally shouldn't do this. It uses a `HttpClient` behind the screens which gets disposed when you dispose the associated `E621Client`. You can read more about why that's bad at _[You're using HttpClient wrong and it is destabilizing your software](https://aspnetmonsters.com/2016/08/2016-08-27-httpclientwrong/)_ if you're interested.

## Authentication

Authentication will have to take place in order to be gain access to certain protected resources. E621Client makes this really easy and convenient by mimicking a login- and logout-based system.

### Logging in

Logging in can be easily done based on a username and password or a username and API key combination. An example of both can be seen below.

_Log in using a password_

```csharp
bool success = await e621Client.LogInUsingPasswordAsync("MyUsername", "MyPassword");
```

_Log in using an API key_

```csharp
bool success = await e621Client.LogInUsingApiKeyAsync("MyUsername", "MyApiKey");
```

Both methods return a boolean that indicate whether not the login attempt was a success. In case of the `LogInUsingPasswordAsync` method, it may also mean that the user doesn't have API access enabled in their account. There is no way of determining if that's the case though.

### Logging out

You need to log out in order for another log in to be allowed to happen. You'll most likely don't need this this for most applications, but here is a method that does so anyway.

_Log the currently logged-in user out_

```csharp
e621Client.Logout();
```

## Report a bug

You can open an [issue](https://github.com/NoppesTheFolf/E621Client/issues) here. I still have to set up a template for issues so be creative. Just make sure to at least include a piece of code that reproduces the bug.

## Contributing

Contributions to this project are very welcome! Just make sure to contact me on [Telegram](https://t.me/NoppesTheFolf) or on [Twitter](https://twitter.com/NoppesTheFolf) before doing anything. I might very well already be working on the feature you need or a bug that has to be fixed.
