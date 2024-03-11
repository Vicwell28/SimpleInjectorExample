using SimpleInjectorExample;

// Antes de la creación del objeto
long memoriaAntes = GC.GetTotalMemory(true);

var injector = new AdvancedInjector();

// Medir después de crear el injector
long memoriaPostInjector = GC.GetTotalMemory(true);
Console.WriteLine($"Memoria utilizada tras crear injector: {memoriaPostInjector} bytes");

injector.Register<IDataService, DataService>();
injector.Register<ILogger, Logger>();
injector.Register<IRepository, Repository>();
injector.Register<IService, Service>();
injector.Register<IEmailService, EmailService>();
injector.Register<ILoggingService, LoggingService>();
injector.Register<IUserRepository, UserRepository>();
injector.Register<IUserService, UserService>();

// Medir después de registrar servicios
long memoriaPostRegistro = GC.GetTotalMemory(true);
Console.WriteLine($"Memoria utilizada tras registrar servicios: {memoriaPostRegistro} bytes");

using (var userServiceAdapter = injector.ResolveAdapter<IUserService>())
{
    var userService = userServiceAdapter.Service;
    // Medir después de registrar servicios
    long memoriauserServiceAdapter = GC.GetTotalMemory(true);
    Console.WriteLine($"Memoria utilizada tras registrar servicios: {memoriauserServiceAdapter} bytes");

    userService.RegisterUser("Victor", "123123");
}

// Medir después de resolver y usar IUserService
long memoriaPostUso = GC.GetTotalMemory(true);
Console.WriteLine($"Memoria utilizada tras resolver y usar IUserService: {memoriaPostUso} bytes");

// Forzar la recolección de basura
GC.Collect();
GC.WaitForPendingFinalizers();

// Verificar la memoria después de la recolección
long memoriaFinal = GC.GetTotalMemory(true);
Console.WriteLine($"Memoria después de GC: {memoriaFinal} bytes");


// Forzar la recolección de basura
GC.Collect();
GC.WaitForPendingFinalizers();

// Verificar la memoria después de la recolección
long memoriaFinal2 = GC.GetTotalMemory(true);
Console.WriteLine($"Memoria después de GC: {memoriaFinal2} bytes");
Console.ReadKey(); 