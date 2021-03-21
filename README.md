<p align="center">
    <img src="https://i.imgur.com/BNkDfMV.png" alt="E621Client" width="256"/>
</p>

[![Build Status](https://img.shields.io/endpoint.svg?url=https%3A%2F%2Factions-badge.atrox.dev%2Fnoppesthefolf%2FE621Client%2Fbadge%3Fref%3Dmaster&style=flat)](https://actions-badge.atrox.dev/noppesthefolf/E621Client/goto?ref=master) ![Nuget](https://img.shields.io/nuget/v/Noppes.E621Client) [![Codacy Badge](https://api.codacy.com/project/badge/Grade/a70a205126ab4a7496d48ce63a2e66d0)](https://www.codacy.com/manual/NoppesTheFolf/E621Client?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=NoppesTheFolf/E621Client&amp;utm_campaign=Badge_Grade)


# E621Client

E621Client is an unofficial .NET Standard 2.1 library for interacting with the [e621 and e926 API](https://e621.net/wiki_pages/2425) maintained by me, [Noppes](https://twitter.com/NoppesTheFolf). It uses the newest language version, which at the moment of writing this is C# 8.0, and has support for [nullable and non-nullable reference types](https://docs.microsoft.com/en-us/dotnet/csharp/nullable-references).

## Table of Contents

1. [Completeness](#completeness)
2. [Installation](#installation)
3. [Getting started](#getting-started)
4. [Authentication](#authentication)
    1. [Logging in](#logging-in)
    2. [Logging out](#logging-out)
5. [Functionality per API area](#functionality-per-api-area)
    1. [Posts](#posts)
        1. [Retrieving a post](#retrieving-a-post)
        2. [Retrieving posts](#retrieving-posts)
    2. [Tags](#tags)
        1. [Retrieving a tag](#retrieving-a-tag)
        2. [Retrieving tags](#retrieving-tags)
    3. [Users](#users)
        1. [Retrieving a user](#retrieving-a-user)
        2. [Retrieving the logged-in user](#retrieving-the-logged-in-user)
    4. [Favorites](#favorites)
        1. [Adding a post](#adding-a-post)
        2. [Removing a post](#removing-a-post)
        3. [Retrieving favorites](#retrieving-favorites)
    5. [IQDB (Reverse image searching)](#iqdb-reverse-image-searching)
    6. [Additional](#additional)
        1. [Get response body as stream](#get-response-body-as-a-stream)
6. [Testing](#testing)    
7. [Report a bug](#report-a-bug)
8. [Contributing](#contributing)

## Completeness

This project is far from a complete wrapper around the e621 API. A table can be seen below to give you a quick idea of how complete this library is. I might never implement most of the API because I'd never use those endpoints myself. However, feel free to implement those areas yourself if you need them! Just make sure the check out [how to contribute](#contributing) first.

_Legend_

| Symbol             | Meaning               |
|--------------------|-----------------------|
| :x:                | Not implemented       |
| :heavy_minus_sign: | Partially implemented |
| :heavy_check_mark: | Fully implemented     |

_Cover per API area_

| Area           | Complete           | Comment                                             |
|----------------|--------------------|-----------------------------------------------------|
| Authentication | :heavy_check_mark: |                                                     |
| Posts          | :heavy_minus_sign: | Only the retrieval of posts                         |
| Tags           | :heavy_minus_sign: | Only the retrieval of tags                          |
| Tag Aliases    | :x:                |                                                     |
| Notes          | :x:                |                                                     |
| Pools          | :x:                |                                                     |
| Users          | :heavy_minus_sign: | Only the retrieval of a user by name                |
| Favorites      | :heavy_check_mark: | Not yet documented by e621 at the moment of writing |
| IQDB           | :heavy_check_mark: | Not yet documented by e621 at the moment of writing |

## Installation

E621Client is available as a NuGet package listed as `Noppes.E621Client`. You can easily install it using either the Package Manager Console or the .NET CLI.

_Package Manager Console_

```
Install-Package Noppes.E621Client -Version 0.4.1
```

_.NET CLI_

```
dotnet add package Noppes.E621Client --version 0.4.1
```

## Getting started

You will need a `E621Client` instance in order to interact with the API. These instances can only be created using the `E621ClientBuilder` class. The builder will allow you to create your very own personalized `E621Client` instance in a fluent manner based on the specific needs of your application. Just make sure you at least specify User-Agent information, as e621 requires it. Not specifying it will cause an exception to be thrown.

_Bare minimum example_

```csharp
var e621Client = new E621ClientBuilder()
    .WithUserAgent("MyApplicationName", "MyApplicationVersion", "MyTwitterUsername", "Twitter")
    .Build();
```

However, you might need something a little more suited for your application. The default `E621Client` instance built above uses settings tuned to make sure the load on e621's side is kept to a minimum. This is not desirable if you're, for example, developing an interactive tool and therefore want it to be as snappy as possible.

_Example for an interactive application_

```csharp
var e621Client = new E621ClientBuilder()
    .WithUserAgent("MyApplicationName", "MyApplicationVersion", "MyTwitterUsername", "Twitter")
    .WithMaximumConnections(E621Constants.MaximumConnectionsLimit)
    .WithRequestInterval(E621Constants.MinimumRequestInterval)
    .Build();
```

`E621Client` instances can be disposed of, but you generally shouldn't do this. It uses a `HttpClient` behind the screens which gets disposed when you dispose the associated `E621Client`. You can read more about why that's bad at _[You're using HttpClient wrong and it is destabilizing your software](https://aspnetmonsters.com/2016/08/2016-08-27-httpclientwrong/)_ if you're interested.

## Authentication

Authentication will have to take place in order to be gain access to certain protected resources. E621Client makes this convenient by mimicking a login- and logout-based system.

### Logging in

Logging in can only be done based on a combination of username and API key.

_Log in using a username and API key_

```csharp
bool success = await e621Client.LogInAsync("MyUsername", "MyApiKey");
```

The method returns a boolean that indicates whether not the login attempt was a success. It does this by [requesting the user's favorites](#favorites). This method executing successfully, implies that the user entered valid credentials. In case the method return false, it may also mean that the user doesn't have API access enabled in their account. There is no way of determining if that's the case though.

### Logging out

You need to log out in order for another log in to be allowed to happen. You'll most likely don't need this this for most applications, but here is a method that does so anyway.

_Log the currently logged-in user out_

```csharp
e621Client.Logout();
```

## Functionality per API area

The main point of this section is to show how the e621 API endpoints map to `E621Client` methods. The methods themselves are more elaborately documented than they are here. So don't worry if things may still seem kind off vague after reading this, check the method documentation out too!

### Posts

#### Retrieving a post

A post can be retrieved using either its ID or its image's MD5 hash by calling the `GetPostAsync` method.

_Retrieving a post by its ID_

```csharp
var postById = await e621Client.GetPostAsync(546281);
```

_Retrieving a post by its image's MD5 hash_

```csharp
var postByMd5 = await e621Client.GetPostAsync("900e98af5b512ba1a5f8a1a9885c1ef1");
```

#### Retrieving posts

A collection of posts can be retrieved using the `GetPostsAsync` method. There is a limit on how much posts can be retrieved in a single call to this method. This limited is defined at `E621Constants.PostsMaximumLimit`. Therefore, you need to be able to navigate through e621's post collection. There are two ways to do this: pagination and relative positioning.

##### Without navigation

You can request a bunch of posts without navigation, but the use cases are very limited of course.

_Retrieve a collection of posts tagged "canine" with the maximum number of posts retrievable in a single call without using any navigation_

```csharp
var posts = await e621Client.GetPostsAsync("canine", limit: E621Constants.PostsMaximumLimit);
```

##### Navigation using pagination

You're probably already familiar with the concept of pagination: a collection of something, in this case posts, is split into parts of equal size. Each of those parts is assigned a number and these parts can then be requested using that number. That number is which we call a "page".

Be aware that there is a limit on the maximum allowed page number. This limit is defined at `E621Constants.PostsMaximumPage`. Exceeding this number will cause an exception to be thrown.

_Retrieve the fifteenth page of a collection of posts tagged "canine"_

```csharp
var posts = await e621Client.GetPostsAsync("canine", 15);
```

_Retrieve the fifteenth page of a collection of posts tagged "canine" with the maximum number of posts retrievable in a single call_

```csharp
var posts = await e621Client.GetPostsAsync("canine", 15, E621Constants.PostsMaximumLimit);
```

##### Navigation using relative positioning

This may sound a bit scary at first, but it really isn't. All you need to do is specify both an post ID and a position. The position parameter defines the position of the returned posts relative to the given post ID.

Let's take post with ID 1000 as an example. Passing this ID in combination with `Position.Before` will cause the posts 999, 998, 997, etc. to be retrieved. Using `Position.After` will retrieve the posts 1001, 1002, 1003, etc. You should use this method if you don't need pagination or need to avoid the limit pagination comes with. Moreover, this is the most efficient way to navigate through posts.

_Retrieve a collection of posts with an ID less than 200_

```csharp
var posts = await e621Client.GetPostsAsync(200, Position.Before);
```

_Retrieve a collection of posts with an ID greater than 200_

```csharp
var posts = await e621Client.GetPostsAsync(200, Position.After);
```

_Retrieve a collection of posts tagged "canine" with an ID greater than 200 using the maximum limit of posts retrieved in a single call_

```csharp
var posts = await e621Client.GetPostsAsync(200, Position.After, "canine", E621Constants.PostsMaximumLimit);
```

### Tags

#### Retrieving a tag

A tag can either be retrieved by either its name or ID by using the `GetTagAsync` method.

_Retrieve a tag by ID_

```csharp
var tag = await e621Client.GetTagAsync(813847);
```

_Retrieve a tag by name_

```csharp
var tag = await e621Client.GetTagAsync("noppes");
```

#### Retrieving tags

There are a couple of different ways a collection of tags can be retrieved: without using any filter, using the names of the tags and using a query.

Searching for tags without using filter, opens up the usage of pagination with relative positioning. [This works in exactly the same way as it does for posts.](#navigation-using-relative-positioning). [All of the other available overloads make use of pagination as you're used to.](#navigation-using-pagination). The maximum allowed page number is defined at `E621Constants.TagsMaximumPage`.

Note that there is a limit on the number of tags that can be retrieved in a single call to any of the overloads. This limit is defined at `E621Constants.TagsMaximumLimit`.

##### Without filter

The first way of retrieving tags is without using any specific name filter with the `GetTagsAsync` method.

_Get the maximum possible number of tags after ID 1000_

```csharp
var tags = await e621Client.GetTagsAsync(1000, Position.After, E621Constants.TagsMaximumLimit);
```

_Get the 10th page using the maximum possible number of tags ordered by the number of posts making use of the tag_

```csharp
var tags = await e621Client.GetTagsAsync(10, E621Constants.TagsMaximumLimit, order: TagOrder.Count);
```

##### Using tags and their names

A collection of tags can also be retrieved using their names using the `GetTagsUsingNamesAsync` method.

_Get tags using their names_

```csharp
var tagNames = new []
{
    "noppes",
    "blep",
    "fur"
};
var tags = await e621Client.GetTagsByNames(tagNames);
```

##### Using a search query

Tags can also be retrieved using a search query on the tag their names using a wildcards, for example, with an overload of the `GetTagsUsingNamesAsync` method.

_Get the first page of tags that start with 'wolf' in the species category_

```csharp
var tags = await e621Client.GetTagsByNames("wolf*", category: TagCategory.Species);
```

### Users

Currently E621Client doesn't support much of the users area of the API mainly due to there being no documentation on it whatsoever at the moment this was written.

#### Retrieving a user

You can retrieve a part of the info available about a user by searching for them using their username.

_Get information about a user by searching by their username_

```csharp
var user = await e621Client.GetUserAsync("noppes");
```

#### Retrieving the logged-in user

You can also retrieve the same info for the user that is currently logged-in.

_Get information about the currently logged-in user_

```csharp
var user = await e621Client.GetLoggedInUserAsync();
```

### Favorites

#### Adding a post

Adding a post to the logged-in user their favorites, can be done with the `AddFavoriteAsync` method.

_Adding a post using its ID to the logged-in user's favorites_

```csharp
await e621Client.AddFavoriteAsync(546281);
```

#### Removing a post

You can remove a post from the logged-in user their favorites using the `RemoveFavoriteAsync` method.

_Removing a post by its ID from the logged-in user's favorites_

```csharp
await e621Client.RemoveFavoriteAsync(546281);
```

#### Retrieving favorites

We can retrieve the posts favorited by either the logged-in user or some other user by using their user ID.

_Retrieving the first page of posts favorited by the logged-in user_

```csharp
var favorites = await e621Client.GetOwnFavoritesAsync();
```

_Retrieving the fifth page of posts favorited by the logged-in user_

```csharp
var favorites = await e621Client.GetOwnFavoritesAsync(5);
```

_Retrieving the first page of posts favorited by the user with ID 11271 (SnowWolf)_

```csharp
var favorites = await e621Client.GetFavoritesAsync(11271);
```

_Retrieving the seventh page of posts favorited by the user with ID 11271 (SnowWolf)_

```csharp
var favorites = await e621Client.GetFavoritesAsync(11271, 7);
```

### IQDB (Reverse image searching)

Since the overhaul of e621 at the 5th of March, it has become possible to reverse search images using IQDB. You can reverse search an image (and therefore a post) by either a locally stored image, a stream or a URL. This will return a collection of posts of which the images are similar to the submitted image. The returned posts have an additional property named `IqdbScore` which can be used to assess how similar the image is to the submitted one. E621Client will by default not return posts that have been deleted. However, if you'd like to include them, you can simply pass a boolean to any of the methods associated with querying IQDB.

In case you're using the URL method, note that e621 will download images only from domains whitelisted by them. Which domains are on the whitelist is unknown. You should test if the domains of the URLs you are planning to use are whitelisted or not.

_Reverse image searching using a file, also returning deleted posts_

```csharp
var results = await e621Client.QueryIqdbByFileAsync("/my/path", false);
```

_Reverse image searching using a URL_

```csharp
var results = await e621Client.QueryIqdbByUrlAsync("https://url.net");
```

_Reverse image searching using a stream_

```csharp
// You can use any stream, a FileStream is simply used as an example here
await using var exampleStream = File.OpenRead("/my/path");
var results = await e621Client.QueryIqdbByStreamAsync(exampleStream);
```

### Additional

#### Get response body as a stream

In case you need to get data from e621 that requires authorization, especially images, you can request said data as a `Stream` by using the `GetStreamAsync` method.

_Get the response body as a stream from a given URL_

```csharp
await using var stream = await e621Client.GetStreamAsync("my/url");
```

## Testing

E621Client now supports testing and Mocking. `E621ClientBuilder.Build()` will return an interface `IE621Client` that can be mocked using a mocking framework. You can use this to test your own logic with different responses of the `E621Client`.

## Report a bug

You can [open an issue](https://github.com/NoppesTheFolf/E621Client/issues). I still have to set up a template for issues so be creative. Just make sure to at least include a piece of code that reproduces the bug.

## Contributing

Contributions to this project are very welcome! Just make sure to contact me on [Telegram](https://t.me/NoppesTheFolf) or on [Twitter](https://twitter.com/NoppesTheFolf) before doing anything. I might very well already be working on the feature you need or a bug that has to be fixed. It would be a shame if our hard work would be for nothing.
