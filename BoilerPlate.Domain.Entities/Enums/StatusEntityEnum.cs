using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerPlate.Domain.Entities.Enums
{
    public enum StatusEntityEnum
    {
        Delete,
        Active,
        DeleteUser,
        Hide
    }

    public static class StatusEntityEnumExtension
    {
        public static string ToFriendlyString(this StatusEntityEnum me)
        {
            return me switch
            {
                StatusEntityEnum.Delete => "Delete",
                StatusEntityEnum.Active => "Active",
                StatusEntityEnum.DeleteUser => "Delete User",
                StatusEntityEnum.Hide => "Hide",
                _ => "Unknown",
            };
        }
    }
}
