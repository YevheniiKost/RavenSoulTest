using System;
using System.Collections.Generic;

namespace RavenSoul.Utilities.DependencyInjection
{
    public class DiServiceCollection
    {
        private Dictionary<Type, ServiceDescriptor> _serviceDescriptors = new Dictionary<Type, ServiceDescriptor>();

        public void RegisterSingleton<TService>()
        {
            var descriptor = new ServiceDescriptor(typeof(TService), ServiceLifetime.Singleton);
            _serviceDescriptors[descriptor.ServiceType] = descriptor;
        }

        public void RegisterSingleton<TService>(TService implementation)
        {
            var descriptor = new ServiceDescriptor(implementation, ServiceLifetime.Singleton);
            _serviceDescriptors[descriptor.ServiceType] = descriptor;
        }

        public void RegisterSingleton<TService, TImplementation>() where TImplementation : TService
        {
            var descriptor = new ServiceDescriptor(typeof(TService), typeof(TImplementation), ServiceLifetime.Singleton);
            _serviceDescriptors[descriptor.ServiceType] = descriptor;
        }

        public void RegisterTransient<TService, TImplementation>() where TImplementation : TService
        {
            var descriptor = new ServiceDescriptor(typeof(TService), typeof(TImplementation), ServiceLifetime.Transient);
            _serviceDescriptors[descriptor.ServiceType] = descriptor;
        }

        public void RegisterTransient<TService>()
        {
            var descriptor = new ServiceDescriptor(typeof(TService), ServiceLifetime.Transient);
            _serviceDescriptors[descriptor.ServiceType] = descriptor;
        }

        public DiContainer GenerateContainer()
        {
            DiContainer.Instance.Initialize(_serviceDescriptors);

            return DiContainer.Instance;
        }
    }

    public enum ServiceLifetime
    {
        Singleton,
        Transient
    }

    public class ServiceDescriptor
    {
        public ServiceLifetime Lifetime { get; set; }
        public Type ServiceType { get; set; }
        public Type ImplementationType { get; }
        public object Implementation { get; private set; }

        public ServiceDescriptor(object implementation, ServiceLifetime lifetime)
        {
            ServiceType = implementation.GetType();
            Implementation = implementation;
            Lifetime = lifetime;
        }

        public ServiceDescriptor(Type serviceType, ServiceLifetime lifetime)
        {
            ServiceType = serviceType;
            Lifetime = lifetime;
        }

        public ServiceDescriptor(Type serviceType, Type implementationType, ServiceLifetime singleton)
        {
            ServiceType = serviceType;
            ImplementationType = implementationType;
            Lifetime = singleton;
        }

        public void SetImplementation(object implementation)
        {
            Implementation = implementation;
        }
    }
}