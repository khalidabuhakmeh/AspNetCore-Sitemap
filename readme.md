# ASP.NET Core Sitemap Sample

## Introduction

While ASP.NET Core is a robust web framework, it lacks some of the core features that make executing a creative-focused site more straightforward. One of those features is the ability to generate a sitemap. If folks can't find your content, then for all intents and purposes, it doesn't exist. 

In this sample, I'm using DotNetSitemapGenerator, a NuGet package, to provide sitemap intrinsics for generating the XML sitemap, but also creating a custom infrastructure for ASP.NET Core applications to include endpoints from ASP.NET Core MVC, Razor Pages, and Minimal API endpoints in the final sitemap artifact. It's essential to support all programming approaches, as each can produce a search engine-crawlable endpoint.

The combination of the package and my infrastructure gives developers the flexibility to add almost anything to a sitemap, including the ability to read endpoints from a database to generate dynamic entries. In this sample, you'll find an example of generating product urls from rows in a database table.

Another important aspect of this approach is the ability to inherit from the `SitemapAttribute` to define values specific to each endpoint. Given the limitation of .NET attributes, inheriting the attribute is the most approachable way of adding values to each attribute. Using an attribute, you can specify default values for dynamic endpoints in the form of a `RouteValueDictionary` instance, the change frequency, and the priority of an endpoint. This could also be expanded to more values, but at that point it becomes more straightforward to build your own `ISitemapUrlProvider`. All in all, you have a lot of options. 

While already powerful, there's more room to grow this solution, as it doesn't address the following edge cases of chunking sitemaps or video sitemaps. Sitemaps are limited to 50,000 entries each. In the case of a large number of pages, developers will need to create a sitemap index that points to several other sitemaps. That situation is not addressed here, but could be added. Additionally, video sitemaps are not addressed either, but given the current infrastructure, I could see adding it being trivial.

## Places to look

Some points of interest that you might find worth looking at when trying to understand the work in this repository.

- [Shopping/Sitemap](./Shopping/Sitemap): The infrastructure code
- [Shopping/Pages/Index.cshtml.cs](./Shopping/Pages/Index.cshtml.cs): Razor Pages with `SitemapAttribute`
- [Shopping/Controllers/TestController.cs](./Shopping/Controllers/TestController.cs): ASP.NET Core MVC approach
- [Shopping/Sitemap/Providers/ProductSitemapUrlProvider.cs](./Shopping/Sitemap/Providers/ProductSitemapUrlProvider.cs): Custom endpoints provider from database using EF Core
- [Shopping/Program.cs](./Shopping/Program.cs): Registration of sitemap endpoint along with a "cool" endpoint that has Sitemap metadata
- [Shopping/Sitemap/SitemapExtensions.cs](./Shopping/Sitemap/SitemapExtensions.cs) Extensions for Sitemap that add providers to ASP.NET Core services collection

## License

Copyright (c) 2023 Khalid Abuhakmeh

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.