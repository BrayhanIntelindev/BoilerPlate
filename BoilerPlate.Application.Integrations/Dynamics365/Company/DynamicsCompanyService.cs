using BoilerPlate.Application.Integrations.Dynamics365.Authorization;
using BoilerPlate.Application.Integrations.Dynamics365.Base;
using BoilerPlate.Application.Integrations.Dynamics365.Dto.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerPlate.Application.Integrations.Dynamics365.Company
{
    public class DynamicsCompanyService(HttpClient apiClient, IDynamicsAuthorizationService crmAuthorizationService) :
        DynamicsServiceBase<DynamicsCompanyResponse>(apiClient, crmAuthorizationService, "companies"), IDynamicsCompanyService
    {
    }
}
