using System;
using System.Collections.Generic;
using System.Linq;
using RavenSoul.Utilities.Singleton;

namespace RavenSoul.Utilities.DependencyInjection
{
    public class DiContainer : TrueSingleton<DiContainer>
    {
        private Dictionary<Type, ServiceDescriptor> _serviceDescriptors = new Dictionary<Type, ServiceDescriptor>();

        public DiContainer()
        {
            
        }
        public void Initialize(Dictionary<Type, ServiceDescriptor> serviceDescriptors)
        {
            _serviceDescriptors = serviceDescriptors;
        }

        public object GetService(Type serviceType)
        {
            if (!_serviceDescriptors.ContainsKey(serviceType))
            {
                throw new Exception($"Service of type {serviceType.Name} isn't registered");
            }

            var descriptor = _serviceDescriptors[serviceType];

            if (descriptor.Implementation != null)
            {
                return descriptor.Implementation;
            }

            var actualType = descriptor.ImplementationType ?? descriptor.ServiceType;

            if (actualType.IsAbstract || actualType.IsInterface)
            {
                throw new Exception($"Cannot instantiate abstract classes or interfaces");
            }

            var constructorInfo = actualType.GetConstructors().First();

            var parameters = constructorInfo.GetParameters()
                .Select(x=> GetService(x.ParameterType)).ToArray();

            var implementation = Activator.CreateInstance(actualType, parameters);

            if (descriptor.Lifetime == ServiceLifetime.Singleton)
            {
                descriptor.SetImplementation(implementation);
            }

            return implementation;
        }

        public T GetService<T>()
        {
            return (T)GetService(typeof(T));
        }
    }
}