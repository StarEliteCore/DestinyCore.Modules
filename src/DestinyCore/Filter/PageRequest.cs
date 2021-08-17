namespace DestinyCore.Filter
{
    public class PageRequest : PageParameters
    {

        public PageRequest()
        {
            PageIndex = 1;
            PageSize = 10;
            OrderConditions = new OrderCondition[] { };
            Filter = new QueryFilter();
        }
    }
}
