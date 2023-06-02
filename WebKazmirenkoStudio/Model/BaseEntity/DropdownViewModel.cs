using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebKazmirenkoStudio.Model
{
    public class DropdownViewModel
    {
        /// <summary>
        /// Коллекция записей
        /// </summary>
        public List<SelectListItem> Collection { get; set; }

        /// <summary>
        /// Выбранный элемент
        /// </summary>
        public string SelectedItem { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public DropdownViewModel()
        {
            Collection = new List<SelectListItem>();
        }
    }
}
