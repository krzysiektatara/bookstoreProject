﻿using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using System.ComponentModel;

namespace BookStoreApplicationAPI.Data.Models
{
    public class Link
    {
        public const string GetMethod = "GET";
        public static Link To(string routeName, object routeValues = null)
            => new Link
            {
                RouteName = routeName,
                RouteValues = routeValues,
                Method = GetMethod,
                Relations = null
            };

        public static Link ToCollection(string routeName, object routeValues = null)
            => new Link
            {
                RouteName = routeName,
                RouteValues = routeValues,
                Method = GetMethod,
                Relations = new[] { "collection" }
            };

        [JsonProperty(Order = -4)]
        public string Href { get; set; }

        [JsonProperty(Order = -3,
            PropertyName = "rel",
            NullValueHandling = NullValueHandling.Ignore)]
        public string[] Relations { get; set; }

        [JsonProperty(Order = -2,
            DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore)]

        [DefaultValue(GetMethod)]
        public string Method { get; set; }

        //Stores the route name before being rewriten by LinkRewritingFilter
        [System.Text.Json.Serialization.JsonIgnore]
        public string RouteName { get; set; }
        //Stores the route parameters before being rewriten by LinkRewritingFilter
        [System.Text.Json.Serialization.JsonIgnore]
        public object RouteValues { get; set; }
    }
}
