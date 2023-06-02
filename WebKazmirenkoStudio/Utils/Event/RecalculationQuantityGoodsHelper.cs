using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using WebKazmirenkoStudio.Data;
using WebKazmirenkoStudio.Model;

namespace WebKazmirenkoStudio.Utils.Event
{
    /// <summary>
    /// Перерасчет колличества товара
    /// </summary>
    public static class RecalculationQuantityGoodsHelper
    {
        /// <summary>
        /// Пересчитать
        /// </summary>
        /// <param name="context">Контекст</param>
        /// <param name="rawMaterialCaptionId">ID названия сущности</param>
        /// <param name="quantity">Увеличить на количество</param>
        public  async static void  Recalculation(WebKazmirenkoStudioContext context, Guid rawMaterialCaptionId, int? quantity)
        {
            await using var ctx = new WebKazmirenkoStudioContext();
            WarehouseModel? warehouse = ctx.Warehouse.Where(x=>x.RawMaterialCaption.Id.Equals(rawMaterialCaptionId)).FirstOrDefault();
            if(warehouse != null && quantity != null)
            {
                warehouse.Quantity = warehouse.Quantity + quantity;
                await ctx.SaveChangesAsync();
            }
        }
    }
}
