using System.ComponentModel;
using WebKazmirenkoStudio.Model.Base;

namespace WebKazmirenkoStudio.Model
{
    /// <summary>
    /// Модельный класс справочника магазины
    /// </summary>
    public class ShopModel : BaseLookupEntity
    {
        /// <summary>
        /// Адрес магазина
        /// </summary>
        [DisplayName(displayName: "Адрес магазина")]
        public string? Address { get; set; }

        /// <summary>
        /// Ссылка на ресурс
        /// </summary>
        [DisplayName(displayName: "Ссылка на источник")]
        public string? LinkSource { get; set; }
    }
}
