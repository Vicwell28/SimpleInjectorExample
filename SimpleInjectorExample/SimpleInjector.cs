using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleInjectorExample
{
    public class SimpleInjector
    {
        private readonly Dictionary<Type, Func<object>> _registrations = new();

        // Registra una instancia concreta para una interfaz
        public void Register<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService, new()
        {
            _registrations[typeof(TService)] = () => new TImplementation();
        }

        // Registra una fábrica para una interfaz
        public void Register<TService>(Func<TService> instanceCreator) where TService : class
        {
            _registrations[typeof(TService)] = () => instanceCreator();
        }

        // Resuelve una instancia para una interfaz registrada
        public TService Resolve<TService>() where TService : class
        {
            return (TService)_registrations[typeof(TService)]();
        }
    }
}
