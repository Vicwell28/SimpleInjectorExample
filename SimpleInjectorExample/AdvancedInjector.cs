using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleInjectorExample
{
    /// <summary>
    /// Clase para inyección de dependencias avanzada.
    /// Permite registrar servicios y sus implementaciones, y resolverlos de manera perezosa.
    /// </summary>
    public class AdvancedInjector
    {
        private readonly Dictionary<Type, Lazy<object>> _registrations = new();

        /// <summary>
        /// Registra un servicio con su implementación correspondiente.
        /// </summary>
        /// <typeparam name="TService">Tipo de servicio a registrar.</typeparam>
        /// <typeparam name="TImplementation">Implementación concreta del servicio.</typeparam>
        public void Register<TService, TImplementation>() where TImplementation : class, TService
        {
            // Utiliza Lazy<T> para crear instancias de manera perezosa.
            _registrations[typeof(TService)] = new Lazy<object>(() => CreateInstance(typeof(TImplementation)), true);
        }

        /// <summary>
        /// Resuelve un adaptador de servicio para el tipo de servicio especificado.
        /// </summary>
        /// <typeparam name="TService">Tipo de servicio a resolver.</typeparam>
        /// <returns>Adaptador de servicio para el tipo especificado.</returns>
        /// <exception cref="InvalidOperationException">Se lanza cuando no existe un registro para el tipo de servicio especificado.</exception>
        public ServiceAdapter<TService> ResolveAdapter<TService>() where TService : class
        {
            if (_registrations.TryGetValue(typeof(TService), out var lazyInstance))
            {
                var service = (TService)lazyInstance.Value;
                return new ServiceAdapter<TService>(service);
            }

            throw new InvalidOperationException($"No hay registro para {typeof(TService)}");
        }

        /// <summary>
        /// Crea una instancia del tipo especificado, resolviendo sus dependencias de forma recursiva.
        /// </summary>
        /// <param name="implementationType">Tipo de implementación a instanciar.</param>
        /// <returns>Instancia creada del tipo especificado.</returns>
        private object CreateInstance(Type implementationType)
        {
            var ctor = implementationType.GetConstructors().OrderByDescending(c => c.GetParameters().Length).FirstOrDefault();
            var parameters = ctor?.GetParameters().Select(param =>
            {
                // Resuelve cada dependencia necesaria para el constructor.
                var dependency = _registrations[param.ParameterType].Value;
                return dependency;
            }).ToArray();

            return Activator.CreateInstance(implementationType, parameters);
        }
    }
}
