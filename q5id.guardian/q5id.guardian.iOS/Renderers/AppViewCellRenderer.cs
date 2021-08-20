using System;
using q5id.guardian.Controls;
using q5id.guardian.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(AppViewCell), typeof(AppViewCellRenderer))]
namespace q5id.guardian.iOS.Renderers
{
    public class AppViewCellRenderer : ViewCellRenderer
    {
        public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
        {
            UITableViewCell cell = base.GetCell(item, reusableCell, tv);
            cell.Accessory = UIKit.UITableViewCellAccessory.None;
            cell.SelectedBackgroundView = new UIView();
            return cell;
        }
    }
}
