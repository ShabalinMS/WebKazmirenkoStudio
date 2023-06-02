using System.ComponentModel.DataAnnotations;
using WebKazmirenkoStudio.Model.Base;

namespace WebKazmirenkoStudio.Model.MaterialConsumptionFact
{
    /// <summary>
    /// Фактическое значение использованного сырья
    /// </summary>
    public class MaterialConsumptionFactModel :BaseEntity
    {
        /// <summary>
        /// Плановый расход сырья
        /// </summary>
        [Display(Name = "MaterialConsumptionPlan", ResourceType = typeof(MaterialConsumptionFactResource))]
        public MaterialConsumptionPlanModel MaterialConsumptionPlan { get; set; }

        /// <summary>
        /// Расход
        /// </summary>
        [Display(Name = "Quantity", ResourceType = typeof(MaterialConsumptionFactResource))]
        public int Quantity { get; set; }


        /// <summary>
        /// Заказ
        /// </summary>
        [Display(Name = "Order", ResourceType = typeof(MaterialConsumptionFactResource))]
        public OrderModel Order { get; set; }
    }
}
