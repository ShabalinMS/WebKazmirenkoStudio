using System.ComponentModel.DataAnnotations;
using WebKazmirenkoStudio.Model.Base;
using WebKazmirenkoStudio.Model.MaterialConsumptionPlan;

namespace WebKazmirenkoStudio.Model
{
    /// <summary>
    /// Модель данных планового расхода исходников 
    /// </summary>
    public class MaterialConsumptionPlanModel :BaseEntity
    {
        /// <summary>
        /// Скдад
        /// </summary>
        [Display(Name = "WarehouseId", ResourceType = typeof(MaterialConsumptionPlanResource))]
        public WarehouseModel Warehouse { get; set; }

        /// <summary>
        /// Продукт
        /// </summary>
        [Display(Name = "ProductId", ResourceType = typeof(MaterialConsumptionPlanResource))]
        public ProductModel Product { get; set; }

        /// <summary>
        /// Количество израсходованного материала
        /// </summary>
        [Display(Name = "Quantity", ResourceType = typeof(MaterialConsumptionPlanResource))]
        public int Quantity { get; set; }

    }
}
