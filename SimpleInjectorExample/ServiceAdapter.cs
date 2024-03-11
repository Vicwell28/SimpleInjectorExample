using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace SimpleInjectorExample
{
    /// <summary>
    /// Envoltorio para servicios que permite gestionar su ciclo de vida.
    /// Implementa IDisposable para permitir una limpieza explícita.
    /// </summary>
    /// <typeparam name="TService">Tipo de servicio envuelto.</typeparam>
    public class ServiceAdapter<TService> : IDisposable where TService : class
    {
        public TService Service { get; private set; }

        public ServiceAdapter(TService service)
        {
            Service = service ?? throw new ArgumentNullException(nameof(service));
        }

        /// <summary>
        /// Limpia los recursos, en este caso, simplemente liberando la referencia al servicio.
        /// </summary>
        public void Dispose()
        {
            // Nota: Aquí podrías implementar lógica adicional de limpieza si fuera necesario.
            Service = null;
        }
    }
}