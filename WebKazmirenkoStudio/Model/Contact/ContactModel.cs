using System.ComponentModel.DataAnnotations;
using WebKazmirenkoStudio.Model.Base;

namespace WebKazmirenkoStudio.Model.Contact
{
    /// <summary>
    /// Контакт
    /// </summary>
    public class ContactModel :BaseEntity
    {
        /// <summary>
        /// ФИО
        /// </summary>
        [Display(Name = "FIO", ResourceType = typeof(ContactResource))]
        public string FIO { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        [Display(Name = "Name", ResourceType = typeof(ContactResource))]
        public string Name { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        [Display(Name = "Surname", ResourceType = typeof(ContactResource))]
        public string? Surname { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        [Display(Name = "MiddleName", ResourceType = typeof(ContactResource))]
        public string? MiddleName { get; set; }
    }
}
