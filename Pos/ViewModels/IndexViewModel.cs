using Pos.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.ViewModels
{
    public class IndexViewModel : BaseViewModel
    {
        public IndexViewModel()
        {
            MenuItems = new ObservableRangeCollection<Content>(new List<Content>() {
                new Content{ Code="home",Name="主页面"},
                new Content { Code = "sale", Name = "销售" },
                new Content{ Code="return",Name="退货"},
            });
        }
        public ObservableRangeCollection<Content> MenuItems { get; set; }
    }
}
