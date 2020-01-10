using Lider.DPVAT.APIFonetica.Application;
using Lider.DPVAT.APIFonetica.Application.Interfaces;
using Lider.DPVAT.APIFonetica.Domain.Interfaces.Services;
using Lider.DPVAT.APIFonetica.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Lider.DPVAT.APIFonetica.Infra.CrossCutting.IOC
{
    public class IOC
    {
        //LogFonetica
        public void InjecaoAppService(IServiceCollection services)
        {
            services.AddScoped<IFoneticaAppService, FoneticaAppService>();
        }

        public void InjecaoDomain(IServiceCollection services)
        {
            services.AddScoped<IFoneticaService, FoneticaService>();
        }
    }
}
