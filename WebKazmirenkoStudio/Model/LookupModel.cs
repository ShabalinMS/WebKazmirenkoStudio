using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebKazmirenkoStudio.Model.Base;

namespace WebKazmirenkoStudio.Model
{
    /// <summary>
    /// Справочники
    /// </summary>
    public class LookupModel : BaseLookupEntity
    {
        /// <summary>
        /// Код справочника
        /// </summary>
        [Required]
        [DisplayName(displayName:"Код")]
        public string Code { get; set; }
    }
}
