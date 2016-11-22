using Galaxies.Core.Identity;
using Galaxies.Model.LogicModel;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Galaxies.Core
{
    public static class GalaxiesExtionsion
    {
        public static IApplicationBuilder UseGalaxies(this IApplicationBuilder builder)
        {
            builder.UseMiddleware(typeof(GalaxiesMiddleware));
            return builder;
        }

        public static IApplicationBuilder UseGalaxies(this IApplicationBuilder builder, GalaxiesOptions options)
        {
            //TODO: set options
            return builder;
        }
    }
}
