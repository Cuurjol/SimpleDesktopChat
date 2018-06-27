using System;
using System.ServiceModel;

namespace ChatHost
{
    // Добавление ссылки на службу:
    // http://qaru.site/questions/1213451/cant-add-service-reference-to-client-class-for-wcf-project-within-same-solution

    class Program
    {
        static void Main()
        {
            using (ServiceHost host = new ServiceHost(typeof(WcfService.ServiceChat)))
            {
                host.Open();
                Console.WriteLine("Хост начал свою работу.");
                Console.ReadLine();
            }
        }
    }
}
