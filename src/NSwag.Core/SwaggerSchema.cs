//-----------------------------------------------------------------------
// <copyright file="SwaggerSchema.cs" company="NSwag">
//     Copyright (c) Rico Suter. All rights reserved.
// </copyright>
// <license>https://github.com/NSwag/NSwag/blob/master/LICENSE.md</license>
// <author>Rico Suter, mail@rsuter.com</author>
//-----------------------------------------------------------------------

using Newtonsoft.Json;

namespace Stucco.NSwag.Core
{
    /// <summary>The enumeration of Swagger protocol schemes.</summary>
    public enum SwaggerSchema
    {
        /// <summary>The HTTP schema.</summary>
        [JsonProperty("http")] http,

        /// <summary>The HTTPS schema.</summary>
        [JsonProperty("https")] https,

        /// <summary>The WS schema.</summary>
        [JsonProperty("ws")] ws,

        /// <summary>The WSS schema.</summary>
        [JsonProperty("wss")] wss
    }
}