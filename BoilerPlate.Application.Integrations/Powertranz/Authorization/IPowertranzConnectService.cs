using BoilerPlate.Domain.Entities.Settings.Powertranz;

namespace BoilerPlate.Application.Integrations.Powertranz.Authorization
{
    public interface IPowertranzConnectService
    {
        PowertranzSettings PowertranzSettings { get; set; }
        string PowerTranzUrl { get; } 
    }
}