using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebKazmirenkoStudio.Model.Base;
using WebKazmirenkoStudio.Model.Product;
using WebKazmirenkoStudio.Model.RawMaterial;

namespace WebKazmirenkoStudio.Model
{
    /// <summary>
    /// Модель данных сырья
    /// </summary>
    public class RawMaterialModel: BaseEntity
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

        /// <summary>
        /// Стоимость доставки за штуку
        /// </summary>
        [Display(Name = "ShippingCostPerPiece", ResourceType = typeof(RawMaterialResource))]
        [Column(TypeName = "decimal(10, 2)")]
        public float ShippingCostPerPiece { get; set; }
    }
}
