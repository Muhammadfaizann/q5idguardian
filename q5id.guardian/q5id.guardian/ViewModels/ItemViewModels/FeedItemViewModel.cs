using System;
using q5id.guardian.Models;

namespace q5id.guardian.ViewModels.ItemViewModels
{
    public class FeedItemViewModel : BaseItemViewModel<Feed>
    {
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public string Action { get; set; }
        public string UpdatedTime { get; set; }
        public string Detail { get; set; }
        public bool IsParent { get; set; }

        public FeedItemViewModel(Feed feed) : base(feed)
        {

        }
    }
}
