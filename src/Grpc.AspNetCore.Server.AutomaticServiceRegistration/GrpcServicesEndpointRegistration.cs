using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// Extension to scan for Grpc services
    /// </summary>
    public static class GrpcServicesEndpointRegistration
    {
        /// <summary>
        /// Maps Grpc services to <see cref="IApplicationBuilder"/> from provided assemblies
        /// </summary>
        /// <param name="applicationBuilder">Application builder</param>
        /// <param name="assemblies">Assemblies to scan</param>
        /// <returns>Application builder</returns>
        public static IApplicationBuilder MapGrpcServicesFromAssemblies(this IApplicationBuilder applicationBuilder, params Assembly[] assemblies)
        {
            if (!assemblies.Any())
            {
                throw new ArgumentException("No assemblies found to scan. Provide at least one assembly to scan for Grpc services.");
            }

            var method = typeof(GrpcEndpointRouteBuilderExtensions).GetMethod("MapGrpcService");

            List<Type> exportedTypes;
            List<Type> grpcBaseServices;
            Type? grpcServiceType;

            foreach (var item in assemblies)
            {
                exportedTypes = item.GetExportedTypes().Where(type => type.IsClass).ToList();

                grpcBaseServices = exportedTypes.Where(type => type.CustomAttributes.Any(x => x.AttributeType == typeof(BindServiceMethodAttribute))).ToList();

                foreach (var service in grpcBaseServices)
                {
                    grpcServiceType = exportedTypes.FirstOrDefault(x => !x.IsAbstract && x.BaseType == service);

                    if (grpcServiceType != null)
                    {
                        method!.MakeGenericMethod(grpcServiceType).Invoke(null, new object[] { applicationBuilder });
                    }
                }
            }

            return applicationBuilder;
        }
    }
}