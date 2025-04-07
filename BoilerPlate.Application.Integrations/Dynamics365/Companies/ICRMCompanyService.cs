using BoilerPlate.Application.Integrations.Dynamics365.Authorization;
using BoilerPlate.Application.Integrations.Dynamics365.Base;
using BoilerPlate.Application.Integrations.QuickBooks.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerPlate.Application.Integrations.Dynamics365.Companies
{
    public class CRMCompanyService(HttpClient apiClient, ICRMAuthorizationService cRMAuthorizationService) : 
        CRMServiceBase<string>(apiClient,cRMAuthorizationService,"companies"), ICRMCompanyService
    {
         
    }
}
