using Microsoft.Build.Framework;
using System.ComponentModel;

namespace WebKazmirenkoStudio.Model.Base
{
    /// <summary>
    /// Базовая модель
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        [DisplayName(displayName:"ID")]
        [Required]
        public Guid Id { get; set; }
    }
}
