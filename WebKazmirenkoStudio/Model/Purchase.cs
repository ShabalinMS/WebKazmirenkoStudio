using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebKazmirenkoStudio.Model.Base;

namespace WebKazmirenkoStudio.Model
{
    /// <summary>
    /// Модель закупка
    /// </summary>
    public class Purchase : BaseEntity
    {
        /// <summary>
        /// Цена доставки
        /// </summary>
        [DisplayName(displayName:"Цена доставки")]
        [Column(TypeName = "decimal(10, 2)")]
        public float PriceDelivery { get; set; }

        /// <summary>
        /// Дата доставки
        /// </summary>
        [DisplayName(displayName:"Дата закупки")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        /// <summary>
        /// Магазин
        /// </summary>
        [DisplayName(displayName: "Магазин")]
        public ShopModel? Shop { get; set; }

        /// <summary>
        /// Заголовок закупки
        /// </summary>
        [DisplayName(displayName: "Заголовок закупки")]
        public string? Caption { get; set; }
    }
}
