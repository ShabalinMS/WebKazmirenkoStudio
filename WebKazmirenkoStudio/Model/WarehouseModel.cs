using System.ComponentModel;
using WebKazmirenkoStudio.Model.Base;

namespace WebKazmirenkoStudio.Model
{
    /// <summary>
    /// Модель склад
    /// </summary>
    public class WarehouseModel : BaseEntity
    {
        /// <summary>
        /// Сырье
        /// </summary>
        [DisplayName(displayName:"Наименование сырья")]
        public RawMaterialCaption? RawMaterialCaption { get; set; }

        /// <summary>
        /// Мера измерений
        /// </summary>
        [DisplayName(displayName:"Мера измерения")]
        public MeasureOfMeasurement? MeasureOfMeasurement {get;set;}

        /// <summary>
        /// Объем
        /// </summary>
        [DisplayName(displayName:"Объем")]
        public int? Quantity { get; set; }
    }
}
