using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BoilerPlate.Application.Integrations.Dynamics365.Dto.Base
{
    public abstract class FilterDynamics
    {
        public string LogicBetweenColumns { get; set; } = "or"; // Valor predeterminado a "or"
    }

    public static class Dynamics365QueryBuilder
    {
        public static string BuildFilterQuery<T>(T filter) where T : FilterDynamics
        {
            if (filter == null)
            {
                return string.Empty;
            }

            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var conditions = new List<string>();

            foreach (var property in properties)
            {
                // Ignorar la propiedad LogicBetweenColumns
                if (property.Name == nameof(FilterDynamics.LogicBetweenColumns))
                {
                    continue;
                }

                var value = property.GetValue(filter);

                if (value != null)
                {
                    string logicalName = property.Name.ToLower(); // Asumimos que el nombre de la propiedad es el logical name del campo

                    if (value is string stringValue)
                    {
                        if (!string.IsNullOrEmpty(stringValue))
                        {
                            conditions.Add($"{logicalName} eq '{stringValue}'");
                        }
                    }
                    else if (value is bool boolValue)
                    {
                        conditions.Add($"{logicalName} eq {boolValue.ToString().ToLower()}");
                    }
                    else if (value.GetType().IsValueType) // Para tipos numéricos, DateTime, etc.
                    {
                        conditions.Add($"{logicalName} eq {value}");
                    }
                    // Puedes agregar más tipos de datos según tus necesidades
                }
            }

            if (conditions.Count == 0)
            {
                return string.Empty;
            }

            var logic = filter.LogicBetweenColumns.ToLower();
            // Validar que la lógica sea "or" o "and"
            if (logic != "or")
            {
                logic = "and";
            }

            return string.Join($" {logic} ", conditions);
        }
    }
}
