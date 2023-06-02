using System.ComponentModel;
using WebKazmirenkoStudio.Model.Base;

namespace WebKazmirenkoStudio.Model
{
    /// <summary>
    /// Модель данных сырья
    /// </summary>
    public class RawMaterial: BaseEntity
    {
        /// <summary>
        /// Закупка
        /// </summary>
        [DisplayName(displayName: "Закупка")]
        public virtual Purchase? Purchase { get; set; }

        /// <summary>
        /// Наименование сырья
        /// </summary>
        [DisplayName(displayName: "Наименование сырья")]
        public virtual RawMaterialCaption? RawMaterialCaption { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        [DisplayName(displayName: "Цена")]
        public float Price { get; set; }


        /// <summary>
        /// Купленное количество 
        /// </summary>
        [DisplayName(displayName: "Количество")]
        public int? Quantity { get; set; }

        /// <summary>
        /// Мера измерения
        /// </summary
        [DisplayName(displayName: "Мера измерения")]
        public MeasureOfMeasurement? MeasureOfMeasurement { get; set; }

        /// <summary>
        /// Ссылка на источник
        /// </summary>
        [DisplayName(displayName: "Ссылка на источник")]
        public string? LinkSource { get; set; }
    }
}
