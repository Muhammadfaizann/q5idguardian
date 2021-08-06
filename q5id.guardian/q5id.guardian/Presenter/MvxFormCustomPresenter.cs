using MvvmCross.Forms.Presenters;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Presenters.Attributes;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace q5id.guardian.Presenter
{
    public class PresentationConstantValue
    {
        public const string CLEAR_STACK_AND_SHOW_PAGE = "ClearStackAndShowPage";
    }

    public class MvxFormCustomPresenter : MvxFormsPagePresenter
    {
        public MvxFormCustomPresenter(IMvxFormsViewPresenter platformPresenter) : base(platformPresenter)
        {
        }

        public override MvxBasePresentationAttribute GetPresentationAttribute(MvxViewModelRequest request)
        {
            var att = base.GetPresentationAttribute(request);
            if (request.PresentationValues != null)
            {
                if (request.PresentationValues.ContainsKey(PresentationConstantValue.CLEAR_STACK_AND_SHOW_PAGE))
                {
                    ((MvxPagePresentationAttribute)att).NoHistory = true;
                }
            }
            return att;
        }

    }
}
