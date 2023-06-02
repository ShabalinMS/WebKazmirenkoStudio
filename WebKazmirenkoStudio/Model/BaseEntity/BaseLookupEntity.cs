using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebKazmirenkoStudio.Model.Base
{
    /// <summary>
    /// Базовая модель справочника
    /// </summary>
    public class BaseLookupEntity : BaseEntity
    {
        /// <summary>
        /// Заголоок
        /// </summary>
        [DisplayName(displayName:"Заголовок")]
        [Required]
        public string? Caption { get; set; }
    }
}
