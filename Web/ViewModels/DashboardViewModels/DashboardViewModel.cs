﻿using System.Collections.Generic;

namespace Web.ViewModels.DashboardViewModels
{
    public class DashboardViewModel
    {
        public ICollection<DashboardWidgetViewModel> UserWidgets { get; set; }

        public ICollection<DashboardWidgetBookmarkViewModel>  UserWidgetBookmarks { get; set; }

        public DashboardViewModel()
        {
            UserWidgets = new List<DashboardWidgetViewModel>();
            UserWidgetBookmarks = new List<DashboardWidgetBookmarkViewModel>();
        }
    }
}
