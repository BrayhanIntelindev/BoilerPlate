using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerPlate.Application.Integrations.Powertranz.Dto.Auth
{
    /// <summary>
    /// https://developer.powertranz.com/reference/post_auth
    /// </summary>
    public class AuthRequest
    {
        private double _totalAmount = 1;
        private double? _tipAmount = null;
        /// <summary>
        /// Identificador de transacción.
        /// Representación de cadena de un GUID.
        /// </summary>
        public Guid TransactionIdentifier { get; set; } = Guid.NewGuid();
        /// <summary>
        /// Monto total para autorizar
        /// required
        /// 0 to 1000000000000000
        /// Defaults to 1
        /// </summary>
        public double TotalAmount
        {
            get { return _totalAmount; }
            set
            {
                if (value >= 0 && value <= 1000000000000000)
                {
                    _totalAmount = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value, "El valor debe estar entre 0 y 1,000,000,000,000,000.");
                }
            }
        }
        /// <summary>
        /// 0 to 1000000000000000
        /// Defaults to null
        /// </summary>
        public double? TipAmount
        {
            get { return _tipAmount; }
            set
            {
                if (value == null || (value >= 0 && value <= 1000000000000000))
                {
                    _tipAmount = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value, "El valor debe estar entre 0 y 1,000,000,000,000,000.");
                }
            }
        }
        /// <summary>
        /// Código de moneda numérico (ISO 4217) required ≥ 1 
        /// Defaults to 840 (USD)
        /// </summary>
        public string CurrencyCode { get; set; } = "840";
        /// <summary>
        /// Si es verdadero, realiza el procesamiento 3DSecure con la transacción si está presente 
        /// required
        /// Defaults to true
        /// </summary>
        public bool ThreeDSecure { get; set; } = true;

    }

    public class ExtendedData
    {
        public string MerchantResponseUrl { get; set; } = string.Empty;
        public ThreeDSecure? ThreeDSecure { get; set; }
        public Recurring? Recurring { get; set; }
        public HostedPage? HostedPage { get; set; }

    }

    public class ThreeDSecure
    {
        private string _challengeIndicator = "01";
        private string? _authenticationIndicator = null;
        private string? _messageCategory = null;
        private readonly HashSet<string> _challengeIndicatorValids = ["01", "02", "03", "04"];
        private readonly HashSet<string> _authenticationIndicatorValids = ["01", "04", "05"];
        private readonly HashSet<string> _messageCategoryValids = ["01", "02"];
        /// <summary>
        /// Tamaño obligatorio de la ventana de desafío para 3DS2.
        /// Default FULL
        /// </summary>
        public ChallengeWindowSize ChallengeWindowSize { get; set; } = ChallengeWindowSize.FULL;
        /// <summary>
        /// '01' (no preference),
        /// '02' (No challenge requested),
        /// '03' (3DS Requestor Preference),
        /// '04' (Mandate).
        /// Default is '01' (no preference)
        /// </summary>
        public string ChallengeIndicator
        {
            get { return _challengeIndicator; }
            set
            {
                if (_challengeIndicatorValids.Contains(value))
                {
                    _challengeIndicator = value;
                }
                else
                {
                    throw new ArgumentException($"El valor '{value}' no es válido para ChallengeIndicator. Los valores permitidos son: {string.Join(", ", _challengeIndicatorValids)}");
                }
            }
        }
        /// <summary>
        /// 3DS2 Authentication Indicator.
        /// "01" = Payment transaction,
        /// "04" = Add card,
        /// "05" = Maintain card,
        /// Defaults to "01"
        /// </summary>
        public string? AuthenticationIndicator
        {
            get { return _authenticationIndicator; }
            set
            {
                if (value != null)
                    if (_authenticationIndicatorValids.Contains(value))
                    {
                        _authenticationIndicator = value;
                    }
                    else
                    {
                        throw new ArgumentException($"El valor '{value}' no es válido para AuthenticationIndicator. Los valores permitidos son: {string.Join(", ", _authenticationIndicatorValids)}");
                    }
                else _authenticationIndicator = value;
            }
        }
        /// <summary>
        /// 3DS2 Message Category. 
        /// "01" = PA, 
        /// "02" = NPA.
        /// Default null.
        /// </summary>
        public string? MessageCategory
        {
            get { return _messageCategory; }
            set
            {
                if (value != null)
                    if (_messageCategoryValids.Contains(value))
                    {
                        _messageCategory = value;
                    }
                    else
                    {
                        throw new ArgumentException($"El valor '{value}' no es válido para MessageCategory. Los valores permitidos son: {string.Join(", ", _messageCategoryValids)}");
                    }
                else _messageCategory = value;
            }
        }
    }

    public enum ChallengeWindowSize : int
    {
        /// <summary>
        /// 250x400 px
        /// </summary>
        XS,
        /// <summary>
        /// 390x400 px
        /// </summary>
        S,
        /// <summary>
        /// 500x600 px
        /// </summary>
        M,
        /// <summary>
        /// 600x400 px
        /// </summary>
        L,
        /// <summary>
        /// 100%
        /// </summary>
        FULL
    }

    public class Recurring
    {
        private string _frequency = "M";
        private readonly HashSet<string> _frequencyValid = ["D", "W", "F", "M", "B", "Q", "S", "Y"];
        /// <summary>
        /// Propiedad para guardar la fecha en Formato Datetime
        /// </summary>
        public DateTime ExpiryDateSet { get; set; }
        public DateTime StartDateSet { get; set; }
        /// <summary>
        /// The last date the recurring cycle can be run (Example: 20240720)
        /// </summary>
        public string ExpiryDate
        {
            get { return ExpiryDateSet.ToString("yyyyMMdd"); }
        }

        /// <summary>
        /// Date of the first requested occurrence (Example: 20240620)
        /// </summary>
        public string StartDate
        {
            get { return StartDateSet.ToString("yyyyMMdd"); }
        }

        public string Frequency
        {
            get { return _frequency; }
            set
            {
                if (_frequencyValid.Contains(value))
                {
                    _frequency = value;
                }
                else
                {
                    throw new ArgumentException($"El valor '{value}' no es válido para Frequency. Los valores permitidos son: {string.Join(", ", _frequencyValid)}");
                }
            }
        }
        /// <summary>
        /// Boolean ( true for Powertranz Managed reccurring transactions)
        /// </summary>
        public bool Managed { get; set; } = true;
    }

    public class HostedPage
    {
        /// <summary>
        /// Hosted page name
        /// </summary>
        public string? PageName { get; set; } = null;
        /// <summary>
        /// Hosted page set name.
        /// <para>
        /// Pages created in the Powertranz Merchant Portal must contain the 'PTZ/' prefix in the request
        /// </para>
        /// </summary>
        public string? PageSet { get; set; } = null;
    }

    public class FraudCheck
    {
        /// <summary>
        /// Phone number of caller id for call center
        /// </summary>
        public string CallerId { get; set; } = string.Empty;
        /// <summary>
        /// Optional if using NoDataColection: cardholder IP address
        /// </summary>
        public string IPAddress { get; set; } = string.Empty;

    }
}
